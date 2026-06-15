namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class Consultorio : Form
    {
        private TabControl tabControl;
        private TabPage tab_consultas;
        private TabPage tab_medicos;
        private TabPage tab_pacientes;

        public Consultorio()
        {
            InitializeConsultorio();
            InitializeTabControl();
            InitializeTabComponents();
            AddDataGridViewToTab(tab_consultas);
            AddDataGridViewToTab(tab_medicos);
            AddDataGridViewToTab(tab_pacientes);
        }

        private void InitializeConsultorio()
        {
            Text = "Consultório";
            Size = new Size(600, 400);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.FromArgb(29, 49, 49);
        }

        private void InitializeTabControl()
        {
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            tab_consultas = NewTab("consultas", "Consultas");
            tab_medicos = NewTab("medicos", "Médicos");
            tab_pacientes = NewTab("pacientes", "Pacientes");

            tabControl.TabPages.Add(tab_consultas);
            tabControl.TabPages.Add(tab_medicos);
            tabControl.TabPages.Add(tab_pacientes);

            this.Controls.Add(tabControl);
        }

        private void InitializeTabComponents()
        {
            // ## Aba consultas ##
            GroupBox grp_consultas = NewGroupBox("grp_consultas", "Buscar Consulta");

            Label lbl_consulta = NewLabel("Nº Consulta", 40);
            Label lbl_medico = NewLabel("Nome Médico", 80);
            Label lbl_paciente = NewLabel("Nome Paciente", 120);
            Label lbl_data = NewLabel("Data", 160);
            Label lbl_horario = NewLabel("Horário:", 160, 200);
            lbl_data.Width = 50;
            lbl_horario.Width = 50;

            CheckBox chk_retorno = new CheckBox { Text = "Retorno", Top = 160 - 5, Left = 400 };

            TextBox txt_consulta = NewTextBox("box_consulta", 100, 40);
            TextBox txt_medico = NewTextBox("box_medico", 200, 80);
            TextBox txt_paciente = NewTextBox("box_paciente", 200, 120);

            DateTimePicker dtp_data = NewDateTimePicker(160, 60, "dd/MM/yyyy");
            DateTimePicker dtp_time = NewDateTimePicker(160, 260, "HH:mm");
            dtp_data.MinDate = new DateTime(2026, 1, 1);
            dtp_data.MaxDate = new DateTime(2026, 12, 31);
            dtp_time.ShowUpDown = true;

            // ## aba médicos ##
            GroupBox grp_medicos = NewGroupBox("grp_medicos", "Buscar Médico");

            Label lbl_medico_id = NewLabel("Nº Médico", 40);
            Label lbl_medico_nome = NewLabel("Nome Médico", 80);
            TextBox txt_medico_id = NewTextBox("box_medico_id", 100, 40);
            TextBox txt_medico_nome = NewTextBox("box_medico_nome", 200, 80);

            // ## Aba pacientes ##
            GroupBox grp_pacientes = NewGroupBox("grp_pacientes", "Buscar Paciente");

            Label lbl_paciente_id = NewLabel("Nº Paciente", 40);
            Label lbl_paciente_nome = NewLabel("Nome Paciente", 80);
            TextBox txt_paciente_id = NewTextBox("box_paciente_id", 100, 40);
            TextBox txt_paciente_nome = NewTextBox("box_paciente_nome", 200, 80);

            // botões de controle para cada aba:
            var crud_consultas = new CrudButtonsControl();
            var crud_medicos = new CrudButtonsControl();
            var crud_pacientes = new CrudButtonsControl();

            InitializeBtnEvents(crud_consultas);
            InitializeBtnEvents(crud_medicos);
            InitializeBtnEvents(crud_pacientes);

            grp_consultas.Controls.Add(lbl_consulta);
            grp_consultas.Controls.Add(lbl_medico);
            grp_consultas.Controls.Add(lbl_paciente);
            grp_consultas.Controls.Add(lbl_data);
            grp_consultas.Controls.Add(dtp_data);
            grp_consultas.Controls.Add(lbl_horario);
            grp_consultas.Controls.Add(dtp_time);
            grp_consultas.Controls.Add(chk_retorno);
            grp_consultas.Controls.Add(txt_consulta);
            grp_consultas.Controls.Add(txt_medico);
            grp_consultas.Controls.Add(txt_paciente);
            grp_consultas.Controls.Add(crud_consultas);

            grp_medicos.Controls.Add(lbl_medico_id);
            grp_medicos.Controls.Add(lbl_medico_nome);
            grp_medicos.Controls.Add(txt_medico_id);
            grp_medicos.Controls.Add(txt_medico_nome);
            grp_medicos.Controls.Add(crud_medicos);

            grp_pacientes.Controls.Add(lbl_paciente_id);
            grp_pacientes.Controls.Add(lbl_paciente_nome);
            grp_pacientes.Controls.Add(txt_paciente_id);
            grp_pacientes.Controls.Add(txt_paciente_nome);
            grp_pacientes.Controls.Add(crud_pacientes);

            tab_consultas.Controls.Add(grp_consultas);
            tab_medicos.Controls.Add(grp_medicos);
            tab_pacientes.Controls.Add(grp_pacientes);
        }

        private void InitializeBtnEvents(CrudButtonsControl btn)
        {
            btn.AddClicked += (s, e) => MessageBox.Show("Criar item");
            btn.EditClicked += (s, e) => MessageBox.Show("Editar item");
            btn.DeleteClicked += (s, e) => MessageBox.Show("Deletar item");
        }

        private DateTimePicker NewDateTimePicker(int top, int left, string format)
        {
            return new DateTimePicker
            {
                Width = 80,
                Top = top - 5, // 5 é apenas um pequeno offset pra alinhar verticalmente com o texto dos labels.
                Left = left,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = format,
            };
        }

        private TabPage NewTab(string name, string txt)
        {
            return new TabPage
            {
                Name = name,
                Text = txt,
                BackColor = Color.White
            };
        }

        private Label NewLabel(string text, int top, int left = 10)
        {
            return new Label
            {
                Text = text,
                Top = top,
                Left = left
            };
        }

        private TextBox NewTextBox(string name, int width, int top)
        {
            return new TextBox
            {
                Name = name,
                Width = width,
                Top = top - 5,
                Left = 120
            };
        }

        private GroupBox NewGroupBox(string name, string text)
        {
            return new GroupBox
            {
                Name = name,
                Text = text,
                Size = new Size(555, 190),
                Location = new Point(10, 10)
            };
        }

        private void AddDataGridViewToTab(TabPage tab)
        {
            DataGridView table = new DataGridView
            {
                Size = new Size(575, 200),
                Top = 210,
                Dock = DockStyle.None,
                RowHeadersVisible = false,
                ColumnHeadersVisible = true,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
            };
            table.AllowUserToAddRows = false;

            table.Columns.Add("id_consulta", "Nº Consulta");
            table.Columns.Add("id_medico", "Médico");
            table.Columns.Add("id_paciente", "Paciente");
            table.Columns.Add("data", "Data");
            table.Columns.Add("horario", "Horário");
            table.Columns.Add("retorno", "Retorno");

            tab.Controls.Add(table);
        }
    }
}

// componente customizado, que engloba três botões, de modo que só precisemos implementar estes botões todos uma só vez por tab.
public class CrudButtonsControl : UserControl
{
    // eventos customizados para os botões:
    public event EventHandler AddClicked;
    public event EventHandler EditClicked;
    public event EventHandler DeleteClicked;

    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;

    public CrudButtonsControl()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        this.Height = 40;
        this.Width = 130;
        this.Top = 10;
        this.Left = 420;

        btnAdd = new Button();
        btnEdit = new Button();
        btnDelete = new Button();

        btnAdd.Size = new Size(24, 24);
        btnEdit.Size = new Size(24, 24);
        btnDelete.Size = new Size(24, 24);

        // puxa o caminho base do app (..\bin\debug\net10.0-windows) e combina com appsForms\icons\*.png
        // para que isto funcione, é necessário incluir explicitamente "appsForms\icons\*.png" no .csproj do projeto, pois as pastas "appsForms" e subsequentes podem não ser adicionadas durante a build do projeto.
        string addIcon_Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsForms", "icons", "add_icon.png");
        string editIcon_Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsForms", "icons", "edit_icon.png");
        string deleteIcon_Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsForms", "icons", "delete_icon.png");

        // pra testar se qualquer arquivo existe no caminho especificado acima:
        // if (!File.Exists(addIconPath))
        // {
        //     MessageBox.Show("Image not found: " + addIcon_Path);
        // }

        btnAdd.Image = Image.FromFile(addIcon_Path);
        btnEdit.Image = Image.FromFile(editIcon_Path);
        btnDelete.Image = Image.FromFile(deleteIcon_Path);

        // define posição (x, y) para os botões dentro do controle:
        btnAdd.Location = new Point(0, 5);
        btnEdit.Location = new Point(50, 5);
        btnDelete.Location = new Point(100, 5);

        // evento click dos botões aciona os seguintes sub-eventos:
        btnAdd.Click += (s, e) =>
            AddClicked?.Invoke(this, EventArgs.Empty);

        btnEdit.Click += (s, e) =>
            EditClicked?.Invoke(this, EventArgs.Empty);

        btnDelete.Click += (s, e) =>
            DeleteClicked?.Invoke(this, EventArgs.Empty);


        this.Controls.Add(btnAdd);
        this.Controls.Add(btnEdit);
        this.Controls.Add(btnDelete);
    }
}