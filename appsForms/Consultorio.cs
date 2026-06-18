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

            // tc = tabela consultas; tm = tabela medicos; tp = tabela pacientes
            Builder.AddDataGridViewToTab(tab_consultas, [
                ("tc_id", "Código"),
                ("tc_medico_id", "Médico"),
                ("tc_paciente_id", "Paciente"),
                ("tc_data", "Data"),
                ("tc_horario", "Horário"),
                ("tc_retorno", "Retorno")
                ]);

            Builder.AddDataGridViewToTab(tab_medicos, [
                ("tm_id", "Código"),
                ("tm_nome", "Nome"),
                ("tm_telefone", "Telefone"),
                ("tm_valor_consulta", "Valor Consulta")
                ]);
            Builder.AddDataGridViewToTab(tab_pacientes, [
                ("tp_id", "Código"),
                ("tp_nome", "Nome"),
                ("tp_endereco", "Endereço"),
                ("tp_numero", "Número"),
                ("tp_bairro", "Bairro"),
                ("tp_cidade", "Cidade"),
                ("tp_cep", "CEP"),
                ("tp_sexo", "Sexo"),
                ("tp_telefone", "Telefone"),
                ("tp_celular", "Celulare"),
                ]);
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

            tab_consultas = Builder.NewTab("consultas", "Consultas");
            tab_medicos = Builder.NewTab("medicos", "Médicos");
            tab_pacientes = Builder.NewTab("pacientes", "Pacientes");

            tabControl.TabPages.Add(tab_consultas);
            tabControl.TabPages.Add(tab_medicos);
            tabControl.TabPages.Add(tab_pacientes);

            this.Controls.Add(tabControl);
        }

        private void InitializeTabComponents()
        {
            // ## Aba consultas ##
            GroupBox grp_consultas = Builder.NewGroupBox("grp_consultas", "Buscar Consulta");

            var lbl_consulta = Builder.NewLabel("Nº Consulta", 100, 10, 40);
            var lbl_medico = Builder.NewLabel("Nome Médico", 100, 10, 80);
            var lbl_paciente = Builder.NewLabel("Nome Paciente", 100, 10, 120);
            var lbl_data = Builder.NewLabel("Data", 40, 10, 160 - 5);
            var lbl_horario = Builder.NewLabel("Horário", 100, 200, 160 - 5);
            lbl_data.Width = 50;
            lbl_horario.Width = 50;

            CheckBox chk_retorno = new CheckBox { Text = "Retorno", Top = 160 - 5, Left = 400 };

            var txt_consulta = Builder.NewTextBox("box_consulta_id", 100, 120, 40);
            var txt_medico = Builder.NewTextBox("box_medico", 200, 120, 80);
            var txt_paciente = Builder.NewTextBox("box_paciente", 200, 120, 120);

            DateTimePicker dtp_data = Builder.NewDateTimePicker(160, 60, "dd/MM/yyyy");
            DateTimePicker dtp_time = Builder.NewDateTimePicker(160, 260, "HH:mm");
            dtp_data.MinDate = new DateTime(2026, 1, 1);
            dtp_data.MaxDate = new DateTime(2026, 12, 31);
            dtp_time.ShowUpDown = true;

            // ## aba médicos ##
            GroupBox grp_medicos = Builder.NewGroupBox("grp_medicos", "Buscar Médico");

            var lbl_medico_id = Builder.NewLabel("Nº Médico", 100, 10, 40);
            var lbl_medico_nome = Builder.NewLabel("Nome Médico", 100, 10, 80);
            var txt_medico_id = Builder.NewTextBox("box_medico_id", 100, 120, 40);
            var txt_medico_nome = Builder.NewTextBox("box_medico_nome", 200, 120, 80);

            // ## Aba pacientes ##
            GroupBox grp_pacientes = Builder.NewGroupBox("grp_pacientes", "Buscar Paciente");

            var lbl_paciente_id = Builder.NewLabel("Nº Paciente", 100, 10, 40);
            var lbl_paciente_nome = Builder.NewLabel("Nome Paciente", 100, 10, 80);
            var txt_paciente_id = Builder.NewTextBox("box_paciente_id", 100, 120, 40);
            var txt_paciente_nome = Builder.NewTextBox("box_paciente_nome", 200, 120, 80);

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
    // TODO: para todos os componentes que se repetem entre os forms, ver se dá pra reutilizar, ao invés de fazer redeclarações.
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
                EditConsulta(actionType, subject);
                break;
            case ("Editar", "médico"):
                EditMedico(actionType, subject);
                break;
            case ("Editar", "paciente"):
                EditPaciente(actionType, subject);
                break;
            case ("Deletar", "consulta"):
                DeleteConsulta(actionType, subject);
                break;
            case ("Deletar", "médico"):
                DeleteMedico(actionType, subject);
                break;
            case ("Deletar", "paciente"):
                DeletePaciente(actionType, subject);
                break;
        }
    }

    private static void AddConsulta(string actionType, string subject)
    {
        formDialog.Text = actionType + " " + subject;
        formDialog.Height = 250;

        var lblNomePaciente = Builder.NewLabel("Paciente", 55, 10, 10);
        var lblNomeMedico = Builder.NewLabel("Médico", 55, 10, 40);
        var lblData = Builder.NewLabel("Data", 55, 10, 70);
        var lblHorario = Builder.NewLabel("Horário", 55, 200, 70);

        var txtNomePaciente = Builder.NewTextBox("consulta_nome_paciente", 250, 80, 10);
        var txtNomeMedico = Builder.NewTextBox("consulta_nome_medico", 250, 80, 40);

        var dtpData = Builder.NewDateTimePicker(75, 80, "dd/MM/yyyy");
        dtpData.MinDate = new DateTime(2026, 1, 1);
        dtpData.MaxDate = new DateTime(2026, 12, 31);

        var dtpTime = Builder.NewDateTimePicker(75, 260, "HH:mm");
        dtpTime.Width = 60;
        dtpTime.ShowUpDown = true;

        var btnOk = Builder.AddButton("Salvar", 90, 150);
        var btnCancel = Builder.AddButton("Cancelar", 230, 150);

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

        var lblNomeMedico = Builder.NewLabel("Nome", 80, 10, 10);
        var lblTelefone = Builder.NewLabel("Telefone", 80, 10, 40);
        var lblValorConsulta = Builder.NewLabel("Consulta (R$)", 80, 10, 70);

        var txtNomeMedico = Builder.NewTextBox("medico_nome", 250, 100, 10);
        var txtTelefone = Builder.NewTextBox("medico_telefone", 150, 100, 40);
        var txtValorConsulta = Builder.NewTextBox("medico_valor_consulta", 150, 100, 70);

        var btnOk = Builder.AddButton("Salvar", 90, 150);
        var btnCancel = Builder.AddButton("Cancelar", 230, 150);

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

        var lblNomePaciente = Builder.NewLabel("Nome", 80, 10, 10);
        var lblEndereco = Builder.NewLabel("Endereço", 80, 10, 40);
        var lblNumero = Builder.NewLabel("Número", 80, 10, 70);
        var lblBairro = Builder.NewLabel("Bairro", 60, 150, 70);
        var lblCidade = Builder.NewLabel("Cidade", 80, 10, 100);
        var lblCep = Builder.NewLabel("CEP", 30, 220, 100);
        var lblSexo = Builder.NewLabel("Sexo", 80, 10, 130);
        var lblTelefone = Builder.NewLabel("Telefone", 80, 10, 160);
        var lblCelular = Builder.NewLabel("Celular", 80, 10, 190);

        var txtNomePaciente = Builder.NewTextBox("paciente_nome", 250, 100, 10);
        var txtEndereco = Builder.NewTextBox("paciente_endereco", 250, 100, 40);
        var txtNumero = Builder.NewTextBox("paciente_numero", 50, 100, 70);
        var txtBairro = Builder.NewTextBox("paciente_bairro", 130, 220, 70);
        var txtCidade = Builder.NewTextBox("paciente_cidade", 100, 100, 100);
        var txtCep = Builder.NewTextBox("paciente_cep", 90, 260, 100);

        var radioMasculino = Builder.AddRadio("Masculino", 90, 100, 130);
        var radioFeminino = Builder.AddRadio("Feminino", 90, 200, 130);

        var txtTelefone = Builder.NewTextBox("paciente_telefone", 150, 100, 160);
        var txtCelular = Builder.NewTextBox("paciente_celular", 150, 100, 190);

        var btnOk = Builder.AddButton("Salvar", 90, 250);
        var btnCancel = Builder.AddButton("Cancelar", 230, 250);

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
            // TODO...
        }
    }

    //OBS.: todos estes formulários de edição devem puxar a entrada de registro atualmente selecionada.
    private static void EditConsulta(string actionType, string subject)
    {
        formDialog.Text = actionType + " " + subject;
        formDialog.Height = 250;

        var lblNomePaciente = Builder.NewLabel("Paciente", 55, 10, 10);
        var lblNomeMedico = Builder.NewLabel("Médico", 55, 10, 40);
        var lblData = Builder.NewLabel("Data", 55, 10, 70);
        var lblHorario = Builder.NewLabel("Horário", 55, 200, 70);

        var txtNomePaciente = Builder.NewTextBox("consulta_nome_paciente", 250, 80, 10);
        var txtNomeMedico = Builder.NewTextBox("consulta_nome_medico", 250, 80, 40);

        var dtpData = Builder.NewDateTimePicker(75, 80, "dd/MM/yyyy");
        dtpData.MinDate = new DateTime(2026, 1, 1);
        dtpData.MaxDate = new DateTime(2026, 12, 31);

        var dtpTime = Builder.NewDateTimePicker(75, 260, "HH:mm");
        dtpTime.Width = 60;
        dtpTime.ShowUpDown = true;

        var btnOk = Builder.AddButton("Salvar", 90, 150);
        var btnCancel = Builder.AddButton("Cancelar", 230, 150);

        formDialog.Controls.Clear();

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
            // TODO...
        }
    }

    private static void EditMedico(string actionType, string subject)
    {
        formDialog.Text = actionType + " " + subject;
        formDialog.Height = 250;

        var lblNomeMedico = Builder.NewLabel("Nome", 80, 10, 10);
        var lblTelefone = Builder.NewLabel("Telefone", 80, 10, 40);
        var lblValorConsulta = Builder.NewLabel("Consulta (R$)", 80, 10, 70);

        var txtNomeMedico = Builder.NewTextBox("medico_nome", 250, 100, 10);
        var txtTelefone = Builder.NewTextBox("medico_telefone", 150, 100, 40);
        var txtValorConsulta = Builder.NewTextBox("medico_valor_consulta", 150, 100, 70);

        var btnOk = Builder.AddButton("Salvar", 90, 150);
        var btnCancel = Builder.AddButton("Cancelar", 230, 150);

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
            // TODO...
        }
    }

    private static void EditPaciente(string actionType, string subject)
    {
        formDialog.Text = actionType + " " + subject;
        formDialog.Height = 350;

        var lblNomePaciente = Builder.NewLabel("Nome", 80, 10, 10);
        var lblEndereco = Builder.NewLabel("Endereço", 80, 10, 40);
        var lblNumero = Builder.NewLabel("Número", 80, 10, 70);
        var lblBairro = Builder.NewLabel("Bairro", 60, 150, 70);
        var lblCidade = Builder.NewLabel("Cidade", 80, 10, 100);
        var lblCep = Builder.NewLabel("CEP", 30, 220, 100);
        var lblSexo = Builder.NewLabel("Sexo", 80, 10, 130);
        var lblTelefone = Builder.NewLabel("Telefone", 80, 10, 160);
        var lblCelular = Builder.NewLabel("Celular", 80, 10, 190);

        var txtNomePaciente = Builder.NewTextBox("paciente_nome", 250, 100, 10);
        var txtEndereco = Builder.NewTextBox("paciente_endereco", 250, 100, 40);
        var txtNumero = Builder.NewTextBox("paciente_numero", 50, 100, 70);
        var txtBairro = Builder.NewTextBox("paciente_bairro", 130, 220, 70);
        var txtCidade = Builder.NewTextBox("paciente_cidade", 100, 100, 100);
        var txtCep = Builder.NewTextBox("paciente_cep", 90, 260, 100);

        var radioMasculino = Builder.AddRadio("Masculino", 90, 100, 130);
        var radioFeminino = Builder.AddRadio("Feminino", 90, 200, 130);

        var txtTelefone = Builder.NewTextBox("paciente_telefone", 150, 100, 160);
        var txtCelular = Builder.NewTextBox("paciente_celular", 150, 100, 190);

        var btnOk = Builder.AddButton("Salvar", 90, 250);
        var btnCancel = Builder.AddButton("Cancelar", 230, 250);

        formDialog.Controls.Clear();

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
            // TODO...
        }
    }


    // OBS.: Qualquer exclusão de entradas no registro deve seguir esta lógica: na tentativa de deletar um médico do cadastro, primeiramente deve-se verificar se há consultas para ele/ela, se houver, a exclusão não pode ocorrer até que as consultas sejam editadas. No caso, o usuário terá de pesquisar consultas por médico e substituir o médico a ser excluido do registro por outro, só então poderá ser excluido. Já no caso de pacientes, a lógica é a mesma. Isto tudo é para que não haja tabelas na base de dados com informações desatualizadas.
    private static void DeleteConsulta(string actionType, string subject)
    {
        formDialog.Text = actionType + " " + subject;
        formDialog.Height = 150;

        var lblConfirmDelete = Builder.NewLabel("Confirmar exclusão de consulta do registro?", 300, 10, 20);

        var btnOk = Builder.AddButton("Excluir", 90, 70);
        var btnCancel = Builder.AddButton("Cancelar", 230, 70);

        formDialog.Controls.Clear();

        formDialog.Controls.Add(lblConfirmDelete);

        formDialog.Controls.Add(btnOk);
        formDialog.Controls.Add(btnCancel);
        formDialog.AcceptButton = btnOk;
        formDialog.CancelButton = btnCancel;

        if (formDialog.ShowDialog() == DialogResult.OK)
        {
            // TODO...
        }
    }

    private static void DeleteMedico(string actionType, string subject)
    {
        formDialog.Text = actionType + " " + subject;
        formDialog.Height = 150;

        var lblConfirmDelete = Builder.NewLabel("Confirmar exclusão de médico do cadastro?", 300, 10, 20);

        var btnOk = Builder.AddButton("Confirmar", 90, 70);
        var btnCancel = Builder.AddButton("Cancelar", 230, 70);

        formDialog.Controls.Clear();

        formDialog.Controls.Add(lblConfirmDelete);

        formDialog.Controls.Add(btnOk);
        formDialog.Controls.Add(btnCancel);
        formDialog.AcceptButton = btnOk;
        formDialog.CancelButton = btnCancel;

        if (formDialog.ShowDialog() == DialogResult.OK)
        {
            // TODO...
        }
    }

    private static void DeletePaciente(string actionType, string subject)
    {
        formDialog.Text = actionType + " " + subject;
        formDialog.Height = 150;

        var lblConfirmDelete = Builder.NewLabel("Confirmar exclusão de paciente do cadastro?", 300, 10, 20);

        var btnOk = Builder.AddButton("Confirmar", 90, 70);
        var btnCancel = Builder.AddButton("Cancelar", 230, 70);

        formDialog.Controls.Clear();

        formDialog.Controls.Add(lblConfirmDelete);

        formDialog.Controls.Add(btnOk);
        formDialog.Controls.Add(btnCancel);
        formDialog.AcceptButton = btnOk;
        formDialog.CancelButton = btnCancel;

        if (formDialog.ShowDialog() == DialogResult.OK)
        {
            // TODO...
        }
    }
}

public class Builder
{
    public static Label NewLabel(string text, int width, int left, int top)
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

    public static TextBox NewTextBox(string name, int width, int left, int top)
    {
        return new TextBox
        {
            Name = name,
            Width = width,
            Left = left,
            Top = top
        };
    }

    public static Button AddButton(string text, int left, int top)
    {
        return new Button
        {
            Text = text,
            Left = left,
            Top = top,
            DialogResult = DialogResult.OK
        };
    }

    public static RadioButton AddRadio(string text, int width, int left, int top)
    {
        return new RadioButton
        {
            Text = "Text",
            Width = width,
            Left = left,
            Top = top
        };
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

    public static TabPage NewTab(string name, string txt)
    {
        return new TabPage
        {
            Name = name,
            Text = txt,
            BackColor = Color.White
        };
    }

    public static GroupBox NewGroupBox(string name, string text)
    {
        return new GroupBox
        {
            Name = name,
            Text = text,
            Size = new Size(555, 190),
            Location = new Point(10, 10)
        };
    }

    // o primeiro item da tupla se refere ao nome de identificação da coluna, já o segundo se refere ao texto do header.
    public static void AddDataGridViewToTab(TabPage tab, List<(string, string)> colunas)
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

        // para cada name e header em colunas:
        foreach (var (name, header) in colunas)
        {
            table.Columns.Add(name, header);
        }

        tab.Controls.Add(table);
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