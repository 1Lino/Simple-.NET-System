namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class Consultorio : Form
    {
        // public static Consultorio Instance { get; private set; }
        private TabControl tabControl;

        public Consultorio()
        {
            // Instance = this;
            InitiateConsultorio();
            InitializeTabControl();
            AddDataGridViewToTab("consultas");
            AddDataGridViewToTab("medicos");
            AddDataGridViewToTab("pacientes");
        }

        private void InitiateConsultorio()
        {
            Text = "Consultório";
            Size = new Size(600, 400);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.FromArgb(29, 49, 49);
        }

        // TODO: Esse método pode ser subdividido em vários, pra melhor manuntenção.
        // Idealmente, este método é pra inicializar somente o TabControl e suas tabs.
        // O resto deve ser adicionado via outro método.
        private void InitializeTabControl()
        {
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            TabPage tab_consultas = new TabPage();
            tab_consultas.Name = "consultas";
            tab_consultas.Text = "Consultas";
            tab_consultas.BackColor = Color.White;

            GroupBox grp_consultas = NewGroupBox("grp_consultas", "Buscar Consulta");

            Label lbl_consulta = NewLabel("Nº Consulta", 40);
            Label lbl_medico = NewLabel("Nome Médico", 80);
            Label lbl_paciente = NewLabel("Nome Paciente", 120);
            Label lbl_data = NewLabel("Data", 160);
            lbl_data.Width = 50;
            Label lbl_horario = NewLabel("Horário:", 160, 200);
            lbl_horario.Width = 50;

            CheckBox chk_retorno = new CheckBox { Text = "Retorno", Top = 160 - 5, Left = 400 };

            TextBox txt_consulta = NewTextBox("box_consulta", 100, 40);
            TextBox txt_medico = NewTextBox("box_medico", 200, 80);
            TextBox txt_paciente = NewTextBox("box_paciente", 200, 120);

            DateTimePicker dtp_data = new DateTimePicker
            {
                Width = 80,
                Top = 160 - 5, // 5 é apenas um pequeno offset pra alinhar verticalmente com o texto dos labels.
                Left = 60,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                MinDate = new DateTime(2026, 1, 1),
                MaxDate = new DateTime(2026, 12, 31)
            };

            DateTimePicker dtp_time = new DateTimePicker
            {
                Width = 80,
                Top = 160 - 5,
                Left = 260,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "HH:mm",
                ShowUpDown = true
            };

            var crud_consultas = new CrudButtonsControl();

            crud_consultas.AddClicked += (s, e) => MessageBox.Show("Criar item");
            crud_consultas.EditClicked += (s, e) => MessageBox.Show("Editar item");
            crud_consultas.DeleteClicked += (s, e) => MessageBox.Show("Deletar item");

            tab_consultas.Controls.Add(crud_consultas);

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
            tab_consultas.Controls.Add(grp_consultas);

            TabPage tab_medicos = new TabPage();
            tab_medicos.Name = "medicos";
            tab_medicos.Text = "Médicos";
            tab_medicos.BackColor = Color.White;

            GroupBox grp_medicos = NewGroupBox("grp_medicos", "Buscar Médico");
            Label lbl_medico_id = NewLabel("Nº Médico", 40);
            Label lbl_medico_nome = NewLabel("Nome Médico", 80);
            TextBox txt_medico_id = NewTextBox("box_medico_id", 100, 40);
            TextBox txt_medico_nome = NewTextBox("box_medico_nome", 200, 80);

            var crud_medicos = new CrudButtonsControl();

            crud_medicos.AddClicked += (s, e) => MessageBox.Show("Criar item");
            crud_medicos.EditClicked += (s, e) => MessageBox.Show("Editar item");
            crud_medicos.DeleteClicked += (s, e) => MessageBox.Show("Deletar item");

            tab_medicos.Controls.Add(crud_medicos);

            grp_medicos.Controls.Add(lbl_medico_id);
            grp_medicos.Controls.Add(lbl_medico_nome);
            grp_medicos.Controls.Add(txt_medico_id);
            grp_medicos.Controls.Add(txt_medico_nome);
            tab_medicos.Controls.Add(grp_medicos);

            TabPage tab_pacientes = new TabPage();
            tab_pacientes.Name = "pacientes";
            tab_pacientes.Text = "Pacientes";
            tab_pacientes.BackColor = Color.White;

            GroupBox grp_pacientes = NewGroupBox("grp_pacientes", "Buscar Paciente");
            Label lbl_paciente_id = NewLabel("Nº Paciente", 40);
            Label lbl_paciente_nome = NewLabel("Nome Paciente", 80);
            TextBox txt_paciente_id = NewTextBox("box_paciente_id", 100, 40);
            TextBox txt_paciente_nome = NewTextBox("box_paciente_nome", 200, 80);

            var crud_pacientes = new CrudButtonsControl();

            crud_pacientes.AddClicked += (s, e) => MessageBox.Show("Criar item");
            crud_pacientes.EditClicked += (s, e) => MessageBox.Show("Editar item");
            crud_pacientes.DeleteClicked += (s, e) => MessageBox.Show("Deletar item");

            tab_pacientes.Controls.Add(crud_pacientes);

            grp_pacientes.Controls.Add(lbl_paciente_id);
            grp_pacientes.Controls.Add(lbl_paciente_nome);
            grp_pacientes.Controls.Add(txt_paciente_id);
            grp_pacientes.Controls.Add(txt_paciente_nome);
            tab_pacientes.Controls.Add(grp_pacientes);


            tabControl.TabPages.Add(tab_consultas);
            tabControl.TabPages.Add(tab_medicos);
            tabControl.TabPages.Add(tab_pacientes);

            // tabControl.TabPages["pacientes"].Controls.Add(grp_pacientes);

            this.Controls.Add(tabControl);
        }

        private void InitializeTabComponents()
        {

        }

        private Label NewLabel(string text, int top, int left = 10)
        {
            Label lbl = new Label
            {
                Text = text,
                Top = top,
                Left = left
            };

            return lbl;
        }

        private TextBox NewTextBox(string name, int width, int top)
        {
            TextBox txtBox = new TextBox
            {
                Name = name,
                Width = width,
                Top = top - 5,
                Left = 120
            };
            return txtBox;
        }

        private GroupBox NewGroupBox(string name, string text)
        {
            GroupBox groupBox = new GroupBox
            {
                Name = name,
                Text = text,
                Size = new Size(555, 190),
                Location = new Point(10, 10)
            };

            return groupBox;
        }

        private void AddDataGridViewToTab(string tabName)
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

            tabControl.Controls[tabName].Controls.Add(table);
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
        // this.Dock = DockStyle.Top;
        this.Top = 40;
        this.Left = 400;

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