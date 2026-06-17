using Sistema_De_Aplicativos_Simples__.NET.appsForms;

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

            InitializeBtnEvents(crud_consultas, "consulta");
            InitializeBtnEvents(crud_medicos, "médico");
            InitializeBtnEvents(crud_pacientes, "paciente");

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

        public static DateTimePicker NewDateTimePicker(int top, int left, string format)
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
                Left = left,
                TextAlign = ContentAlignment.TopRight
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

            //TODO: ver se existe uma forma de se implementar um spread operator para que o método 
            // aceite o tanto de colunas quanto forem passadas.
            table.Columns.Add("id_consulta", "Nº Consulta");
            table.Columns.Add("id_medico", "Médico");
            table.Columns.Add("id_paciente", "Paciente");
            table.Columns.Add("data", "Data");
            table.Columns.Add("horario", "Horário");
            table.Columns.Add("retorno", "Retorno");

            tab.Controls.Add(table);
        }

        // subject: consulta, médico ou paciente.
        private void InitializeBtnEvents(CrudButtonsControl btn, string subject)
        {
            btn.AddClicked += (s, e) => Dialog.InvokeDialog("Cadastrar", subject);
            btn.EditClicked += (s, e) => Dialog.InvokeDialog("Editar", subject);
            btn.DeleteClicked += (s, e) => Dialog.InvokeDialog("Deletar", subject);
        }

    }
}

public class Dialog
{

    private static Form formDialog = new Form
    {
        FormBorderStyle = FormBorderStyle.FixedSingle,
        StartPosition = FormStartPosition.CenterScreen,
        Width = 400,
        Height = 250,
        MaximizeBox = false
    };

    // TODO: na lógica de negócio deve haver uma validação que confere se o paciente e o médico existem no cadastro/base de dados, e se, caso existam, se a data e horários selecionados estão disponíveis, para evitar conflitos. 
    public static void InvokeDialog(string actionType, string subject)
    {
        switch (actionType, subject)
        {
            case ("Cadastrar", "consulta"):
                AddConsulta(actionType, subject);
                break;
            case ("Cadastrar", "médico"):
                AddMedico(actionType, subject);
                break;
            case ("Cadastrar", "paciente"):
                AddPaciente(actionType, subject);
                break;
            case ("Editar", "consulta"):
                break;
            case ("Editar", "médico"):
                break;
            case ("Editar", "paciente"):
                break;
            case ("Deletar", "consulta"):
                break;
            case ("Deletar", "médico"):
                break;
            case ("Deletar", "paciente"):
                break;
        }
    }

    private static void AddConsulta(string actionType, string subject)
    {
        formDialog.Text = actionType + " " + subject;
        formDialog.Height = 250;

        var lblNomePaciente = AddLabel("Paciente", 55, 10, 10);
        var lblNomeMedico = AddLabel("Médico", 55, 10, 40);
        var lblData = AddLabel("Data", 55, 10, 70);
        var lblHorario = AddLabel("Horário", 55, 200, 70);

        var txtNomePaciente = AddTextBox(250, 80, 10);
        var txtNomeMedico = AddTextBox(250, 80, 40);

        var dtpData = Consultorio.NewDateTimePicker(75, 80, "dd/MM/yyyy");
        dtpData.MinDate = new DateTime(2026, 1, 1);
        dtpData.MaxDate = new DateTime(2026, 12, 31);

        var dtpTime = Consultorio.NewDateTimePicker(75, 260, "HH:mm");
        dtpTime.Width = 60;
        dtpTime.ShowUpDown = true;

        var btnOk = AddButton("Salvar", 90, 150);
        var btnCancel = AddButton("Cancelar", 230, 150);

        formDialog.Controls.Clear(); // esta limpeza deve ser feita a cada chamada, pois o formDialog é apenas um único componente reutilizado em todas as situações.

        formDialog.Controls.Add(lblNomePaciente);
        formDialog.Controls.Add(lblNomeMedico);
        formDialog.Controls.Add(lblData);
        formDialog.Controls.Add(lblHorario);
        formDialog.Controls.Add(txtNomePaciente);
        formDialog.Controls.Add(txtNomeMedico);
        formDialog.Controls.Add(dtpData);
        formDialog.Controls.Add(dtpTime);
        formDialog.Controls.Add(btnOk);
        formDialog.Controls.Add(btnCancel);
        formDialog.AcceptButton = btnOk;
        formDialog.CancelButton = btnCancel;

        if (formDialog.ShowDialog() == DialogResult.OK)
        {
            // string nome = txtNome.Text;
        }
    }

    private static void AddMedico(string actionType, string subject)
    {
        formDialog.Text = actionType + " " + subject;
        formDialog.Height = 250;

        var lblNomeMedico = AddLabel("Nome", 80, 10, 10);
        var lblTelefone = AddLabel("Telefone", 80, 10, 40);
        var lblValorConsulta = AddLabel("Consulta (R$)", 80, 10, 70);

        var txtNomeMedico = AddTextBox(250, 100, 10);
        var txtTelefone = AddTextBox(150, 100, 40);
        var txtValorConsulta = AddTextBox(150, 100, 70);

        var btnOk = AddButton("Salvar", 90, 150);
        var btnCancel = AddButton("Cancelar", 230, 150);

        formDialog.Controls.Clear();

        formDialog.Controls.Add(lblNomeMedico);
        formDialog.Controls.Add(lblTelefone);
        formDialog.Controls.Add(lblValorConsulta);
        formDialog.Controls.Add(txtNomeMedico);
        formDialog.Controls.Add(txtTelefone);
        formDialog.Controls.Add(txtValorConsulta);
        formDialog.Controls.Add(btnOk);
        formDialog.Controls.Add(btnCancel);
        formDialog.AcceptButton = btnOk;
        formDialog.CancelButton = btnCancel;

        if (formDialog.ShowDialog() == DialogResult.OK)
        {
            // string nome = txtNome.Text;
        }
    }

    private static void AddPaciente(string actionType, string subject)
    {
        formDialog.Text = actionType + " " + subject;
        formDialog.Height = 350;

        var lblNomePaciente = AddLabel("Nome", 80, 10, 10);
        var lblEndereco = AddLabel("Endereço", 80, 10, 40);
        var lblNumero = AddLabel("Número", 80, 10, 70);
        var lblBairro = AddLabel("Bairro", 60, 150, 70);
        var lblCidade = AddLabel("Cidade", 80, 10, 100);
        var lblCep = AddLabel("CEP", 30, 220, 100);
        var lblSexo = AddLabel("Sexo", 80, 10, 130);
        var lblTelefone = AddLabel("Telefone", 80, 10, 160);
        var lblCelular = AddLabel("Celular", 80, 10, 190);

        var txtNomePaciente = AddTextBox(250, 100, 10);
        var txtEndereco = AddTextBox(250, 100, 40);
        var txtNumero = AddTextBox(50, 100, 70);
        var txtBairro = AddTextBox(130, 220, 70);
        var txtCidade = AddTextBox(100, 100, 100);
        var txtCep = AddTextBox(90, 260, 100);

        var radioMasculino = AddRadio("Masculino", 90, 100, 130);
        var radioFeminino = AddRadio("Feminino", 90, 200, 130);

        var txtTelefone = AddTextBox(150, 100, 160);
        var txtCelular = AddTextBox(150, 100, 190);

        var btnOk = AddButton("Salvar", 90, 250);
        var btnCancel = AddButton("Cancelar", 230, 250);

        formDialog.Controls.Clear(); // remove todos os componentes que estiverem no form.

        formDialog.Controls.Add(lblNomePaciente);
        formDialog.Controls.Add(lblEndereco);
        formDialog.Controls.Add(lblNumero);
        formDialog.Controls.Add(lblBairro);
        formDialog.Controls.Add(lblCidade);
        formDialog.Controls.Add(lblCep);
        formDialog.Controls.Add(lblSexo);
        formDialog.Controls.Add(lblTelefone);
        formDialog.Controls.Add(lblCelular);
        formDialog.Controls.Add(txtNomePaciente);
        formDialog.Controls.Add(txtEndereco);
        formDialog.Controls.Add(txtNumero);
        formDialog.Controls.Add(txtBairro);
        formDialog.Controls.Add(txtCidade);
        formDialog.Controls.Add(txtCep);
        formDialog.Controls.Add(txtTelefone);
        formDialog.Controls.Add(txtCelular);
        formDialog.Controls.Add(radioMasculino);
        formDialog.Controls.Add(radioFeminino);
        formDialog.Controls.Add(btnOk);
        formDialog.Controls.Add(btnCancel);
        formDialog.AcceptButton = btnOk;
        formDialog.CancelButton = btnCancel;

        if (formDialog.ShowDialog() == DialogResult.OK)
        {
            // string nome = txtNome.Text;
        }
    }

    private static Label AddLabel(string text, int width, int left, int top)
    {
        return new Label
        {
            Text = text,
            Width = width,
            Left = left,
            Top = top,
            TextAlign = ContentAlignment.MiddleRight
        };
    }

    private static TextBox AddTextBox(int width, int left, int top)
    {
        return new TextBox
        {
            Width = width,
            Left = left,
            Top = top
        };
    }

    private static Button AddButton(string text, int left, int top)
    {
        return new Button
        {
            Text = text,
            Left = left,
            Top = top,
            DialogResult = DialogResult.OK
        };
    }

    private static RadioButton AddRadio(string text, int width, int left, int top)
    {
        return new RadioButton
        {
            Text = "Text",
            Width = width,
            Left = left,
            Top = top
        };
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