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
        var form = new Form
        {
            Text = actionType + " " + subject, // ex.: adicionar + consulta = Adicionar consulta.
            FormBorderStyle = FormBorderStyle.FixedSingle,
            StartPosition = FormStartPosition.CenterScreen,
            Width = 400,
            Height = 250,
            MaximizeBox = false
        };

        var lblNomePaciente = new Label
        {
            Width = 55,
            Text = "Paciente",
            Left = 10,
            Top = 10,
            TextAlign = ContentAlignment.MiddleRight
        };

        var lblNomeMedico = new Label
        {
            Width = 55,
            Text = "Médico",
            Left = 10,
            Top = 40,
            TextAlign = ContentAlignment.MiddleRight
        };

        var lblData = new Label
        {
            Width = 55,
            Text = "Data",
            Left = 10,
            Top = 70,
            TextAlign = ContentAlignment.MiddleRight
        };

        var lblHorario = new Label
        {
            Width = 55,
            Text = "Horário",
            Left = 200,
            Top = 70,
            TextAlign = ContentAlignment.MiddleRight
        };

        var txtNomePaciente = new TextBox
        {
            Left = 80,
            Top = 10,
            Width = 250
        };

        var txtNomeMedico = new TextBox
        {
            Left = 80,
            Top = 40,
            Width = 250
        };

        var dtpData = Consultorio.NewDateTimePicker(75, 80, "dd/MM/yyyy");
        dtpData.MinDate = new DateTime(2026, 1, 1);
        dtpData.MaxDate = new DateTime(2026, 12, 31);

        var dtpTime = Consultorio.NewDateTimePicker(75, 260, "HH:mm");
        dtpTime.Width = 60;
        dtpTime.ShowUpDown = true;

        var btnOk = new Button
        {
            Text = "OK",
            Left = 90,
            Top = 150,
            DialogResult = DialogResult.OK
        };

        var btnCancel = new Button
        {
            Text = "Cancel",
            Left = 230,
            Top = 150,
            DialogResult = DialogResult.OK
        };

        form.Controls.Add(lblNomePaciente);
        form.Controls.Add(lblNomeMedico);
        form.Controls.Add(lblData);
        form.Controls.Add(lblHorario);
        form.Controls.Add(txtNomePaciente);
        form.Controls.Add(txtNomeMedico);
        form.Controls.Add(dtpData);
        form.Controls.Add(dtpTime);
        form.Controls.Add(btnOk);
        form.Controls.Add(btnCancel);
        form.AcceptButton = btnOk;
        form.CancelButton = btnCancel;

        if (form.ShowDialog() == DialogResult.OK)
        {
            // string nome = txtNome.Text;
        }
    }

    private static void AddMedico(string actionType, string subject)
    {
        var form = new Form
        {
            Text = actionType + " " + subject, // ex.: adicionar + consulta = Adicionar consulta.
            FormBorderStyle = FormBorderStyle.FixedSingle,
            StartPosition = FormStartPosition.CenterScreen,
            Width = 400,
            Height = 250,
            MaximizeBox = false
        };

        var lblNomeMedico = new Label
        {
            Width = 80,
            Text = "Nome",
            Left = 10,
            Top = 10,
            TextAlign = ContentAlignment.MiddleRight
        };

        var lblTelefone = new Label
        {
            Width = 80,
            Text = "Telefone",
            Left = 10,
            Top = 40,
            TextAlign = ContentAlignment.MiddleRight
        };

        var lblValorConsulta = new Label
        {
            Width = 80,
            Text = "Consulta (R$)",
            Left = 10,
            Top = 70,
            TextAlign = ContentAlignment.MiddleRight
        };

        var txtNomeMedico = new TextBox
        {
            Left = 100,
            Top = 10,
            Width = 250
        };

        var txtTelefone = new TextBox
        {
            Left = 100,
            Top = 40,
            Width = 150
        };

        var txtValorConsulta = new TextBox
        {
            Left = 100,
            Top = 70,
            Width = 150
        };

        var btnOk = new Button
        {
            Text = "OK",
            Left = 90,
            Top = 150,
            DialogResult = DialogResult.OK
        };

        var btnCancel = new Button
        {
            Text = "Cancel",
            Left = 230,
            Top = 150,
            DialogResult = DialogResult.OK
        };

        form.Controls.Add(lblNomeMedico);
        form.Controls.Add(lblTelefone);
        form.Controls.Add(lblValorConsulta);
        form.Controls.Add(txtNomeMedico);
        form.Controls.Add(txtTelefone);
        form.Controls.Add(txtValorConsulta);
        form.Controls.Add(btnOk);
        form.Controls.Add(btnCancel);
        form.AcceptButton = btnOk;
        form.CancelButton = btnCancel;

        if (form.ShowDialog() == DialogResult.OK)
        {
            // string nome = txtNome.Text;
        }
    }

    private static void AddPaciente(string actionType, string subject)
    {
        var form = new Form
        {
            Text = actionType + " " + subject, // ex.: adicionar + consulta = Adicionar consulta.
            FormBorderStyle = FormBorderStyle.FixedSingle,
            StartPosition = FormStartPosition.CenterScreen,
            Width = 400,
            Height = 350,
            MaximizeBox = false
        };

        var lblNomePaciente = new Label
        {
            Width = 80,
            Text = "Nome",
            Left = 10,
            Top = 10,
            TextAlign = ContentAlignment.MiddleRight
        };

        var lblEndereco = new Label
        {
            Width = 80,
            Text = "Endereço",
            Left = 10,
            Top = 40,
            TextAlign = ContentAlignment.MiddleRight
        };

        var lblNumero = new Label
        {
            Width = 80,
            Text = "Número",
            Left = 10,
            Top = 70,
            TextAlign = ContentAlignment.MiddleRight
        };

        var lblBairro = new Label
        {
            Width = 60,
            Text = "Bairro",
            Left = 150,
            Top = 70,
            TextAlign = ContentAlignment.MiddleRight
        };

        var lblCidade = new Label
        {
            Width = 80,
            Text = "Cidade",
            Left = 10,
            Top = 100,
            TextAlign = ContentAlignment.MiddleRight
        };

        var lblCep = new Label
        {
            Width = 30,
            Text = "CEP",
            Left = 220,
            Top = 100,
            TextAlign = ContentAlignment.MiddleRight
        };

        var lblSexo = new Label
        {
            Width = 80,
            Text = "Sexo",
            Left = 10,
            Top = 130,
            TextAlign = ContentAlignment.MiddleRight
        };

        var lblTelefone = new Label
        {
            Width = 80,
            Text = "Telefone",
            Left = 10,
            Top = 160,
            TextAlign = ContentAlignment.MiddleRight
        };

        var lblCelular = new Label
        {
            Width = 80,
            Text = "Celular",
            Left = 10,
            Top = 190,
            TextAlign = ContentAlignment.MiddleRight
        };

        var txtNomePaciente = new TextBox
        {
            Left = 100,
            Top = 10,
            Width = 250
        };

        var txtEndereco = new TextBox
        {
            Left = 100,
            Top = 40,
            Width = 250
        };

        var txtNumero = new TextBox
        {
            Left = 100,
            Top = 70,
            Width = 50
        };

        var txtBairro = new TextBox
        {
            Left = 220,
            Top = 70,
            Width = 130
        };

        var txtCidade = new TextBox
        {
            Left = 100,
            Top = 100,
            Width = 100
        };

        var txtCep = new TextBox
        {
            Left = 260,
            Top = 100,
            Width = 90
        };

        var radioMasculino = new RadioButton
        {
            Width = 90,
            Text = "Masculino",
            Left = 100,
            Top = 130
        };

        var radioFeminino = new RadioButton
        {
            Width = 90,
            Text = "Feminino",
            Left = 200,
            Top = 130
        };

        var txtTelefone = new TextBox
        {
            Left = 100,
            Top = 160,
            Width = 150
        };

        var txtCelular = new TextBox
        {
            Left = 100,
            Top = 190,
            Width = 150
        };

        var btnOk = new Button
        {
            Text = "OK",
            Left = 90,
            Top = 250,
            DialogResult = DialogResult.OK
        };

        var btnCancel = new Button
        {
            Text = "Cancel",
            Left = 230,
            Top = 250,
            DialogResult = DialogResult.OK
        };

        form.Controls.Add(lblNomePaciente);
        form.Controls.Add(lblEndereco);
        form.Controls.Add(lblNumero);
        form.Controls.Add(lblBairro);
        form.Controls.Add(lblCidade);
        form.Controls.Add(lblCep);
        form.Controls.Add(lblSexo);
        form.Controls.Add(lblTelefone);
        form.Controls.Add(lblCelular);
        form.Controls.Add(txtNomePaciente);
        form.Controls.Add(txtEndereco);
        form.Controls.Add(txtNumero);
        form.Controls.Add(txtBairro);
        form.Controls.Add(txtCidade);
        form.Controls.Add(txtCep);
        form.Controls.Add(txtTelefone);
        form.Controls.Add(txtCelular);
        form.Controls.Add(radioMasculino);
        form.Controls.Add(radioFeminino);
        form.Controls.Add(btnOk);
        form.Controls.Add(btnCancel);
        form.AcceptButton = btnOk;
        form.CancelButton = btnCancel;

        if (form.ShowDialog() == DialogResult.OK)
        {
            // string nome = txtNome.Text;
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