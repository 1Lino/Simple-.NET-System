using System.DirectoryServices;

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
                ("tc_id", "ID"),
                ("tc_medico_nome", "Médico"),
                ("tc_paciente_nome", "Paciente"),
                ("tc_data", "Data"),
                ("tc_horario", "Horário"),
                ("tc_retorno", "Retorno")
                ]);
            Builder.AddDataGridViewToTab(tab_medicos, [
                ("tm_id", "ID"),
                ("tm_nome", "Nome"),
                ("tm_telefone", "Telefone"),
                ("tm_valor_consulta", "Valor Consulta")
                ]);
            Builder.AddDataGridViewToTab(tab_pacientes, [
                ("tp_id", "ID"),
                ("tp_nome", "Nome"),
                ("tp_endereco", "Endereço"),
                ("tp_numero", "Número"),
                ("tp_bairro", "Bairro"),
                ("tp_cidade", "Cidade"),
                ("tp_cep", "CEP"),
                ("tp_sexo", "Sexo"),
                ("tp_telefone", "Telefone"),
                ("tp_celular", "Celular"),
                ]);

            // carrega informações da base nas tabelas:
            DBTest.LoadConsultasToTable((DataGridView)tab_consultas.Controls[$"table_{tab_consultas.Name}"], DBTest.dadosConsulta);
            DBTest.LoadMedicosToTable((DataGridView)tab_medicos.Controls[$"table_{tab_medicos.Name}"], DBTest.dadosMedico);
            DBTest.LoadPacientesToTable((DataGridView)tab_pacientes.Controls[$"table_{tab_pacientes.Name}"], DBTest.dadosPaciente);
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

            tabControl.SelectedIndexChanged += OnTabChange;

            this.Controls.Add(tabControl);
        }

        // TODO: a lógica presente neste callback de evento pode e deve ser generalizada, pois, por exemplo, suponha que o usuário
        // digite algo para filtrar resultados, a tabela irá para a linha que corresponde aquele filtro, ou seja, uma nova seleção que
        // deve ser detectada e atualizada para o SelectedRowId. Além do filtro, existe também o sort da tabela, que naturalmente muda a linha selecionada.
        private void OnTabChange(object sender, EventArgs e)
        {
            TabPage currentPage = tabControl.SelectedTab;
            DataGridView dgv = (DataGridView)currentPage.Controls[$"table_{currentPage.Name}"];
            int currentSelectedRowId = dgv.CurrentRow.Index;

            Eventos.ResetSelectedRowId(currentSelectedRowId);

            Console.WriteLine(dgv.Name);
            for (int i = 0; i < dgv.RowCount; i++)
            {
                for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
                {
                    Console.Write($" {dgv.Rows[i].Cells[j].Value} ");
                }
                Console.WriteLine("");
            }

            // testes:
            Console.WriteLine($"Página selecionada: {currentPage.Name}");
            Console.WriteLine("Id da linha atualmente selecionada no DGV da atual página:" + Eventos.GetCurrentSelectedRowId());
        }

        private void InitializeTabComponents()
        {
            // ## ABA CONSULTAS ##
            GroupBox grp_consultas = Builder.NewGroupBox("grp_consultas", "Buscar Consulta");

            var lbl_consulta = Builder.NewLabel("ID Consulta", 100, 10, 40);
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

            // ## ABA MÉDICOS ##
            GroupBox grp_medicos = Builder.NewGroupBox("grp_medicos", "Buscar Médico");

            var lbl_medico_id = Builder.NewLabel("ID Médico", 100, 10, 40);
            var lbl_medico_nome = Builder.NewLabel("Nome Médico", 100, 10, 80);
            var txt_medico_id = Builder.NewTextBox("box_medico_id", 100, 120, 40);
            var txt_medico_nome = Builder.NewTextBox("box_medico_nome", 200, 120, 80);

            // ## ABA PACIENTES ##
            GroupBox grp_pacientes = Builder.NewGroupBox("grp_pacientes", "Buscar Paciente");

            var lbl_paciente_id = Builder.NewLabel("ID Paciente", 100, 10, 40);
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


            txt_consulta.TextChanged += (_, _) => OnTextChange(txt_consulta.Text);
        }

        // TODO: este método, como será usado no evento de vários texBoxes diferentes, de diferentes páginas, o valor de FilteredData deverá ser passado como argumento, através de uma lista de tipo genérico, basicamente, "filtro" e "FilteredData" são dados que deverão receber seus valores de fora da função, totalizando dois argumentos vindo de fora.
        private void OnTextChange(string text)
        {
            TabPage currentPage = tabControl.SelectedTab;
            DataGridView dgv = (DataGridView)currentPage.Controls[$"table_{currentPage.Name}"];

            FiltroConsulta filtro = new FiltroConsulta(text, "", "", "", "", null, "");
            List<RegistroConsulta> FilteredData = DBTest.SearchConsultas(DBTest.dadosConsulta, filtro);

            DBTest.LoadConsultasToTable(dgv, FilteredData);
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

// classe que lida com formulários de Criação, Edição e Remoção de cadastros:
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
        var lblRetorno = Builder.NewLabel("Retorno", 55, 10, 100);

        var txtNomePaciente = Builder.NewTextBox("consulta_nome_paciente", 250, 80, 10);
        var txtNomeMedico = Builder.NewTextBox("consulta_nome_medico", 250, 80, 40);

        var dtpData = Builder.NewDateTimePicker(75, 80, "dd/MM/yyyy");
        dtpData.MinDate = new DateTime(2026, 1, 1);
        dtpData.MaxDate = new DateTime(2026, 12, 31);

        var dtpTime = Builder.NewDateTimePicker(75, 260, "HH:mm");
        dtpTime.Width = 60;
        dtpTime.ShowUpDown = true;

        // Regra de negócio: data mínima de retorno jamais pode ser anterior à data da primeira consulta.
        // por isso definimos a data atual da consulta como período mínimo para o picker, ou seja, o dia de retorno só pode ser a partir do mesmo dia da consulta. Outra coisa é que uma consulta pode ou não ter data de retorno, neste caso, só se carrega tal dado se de fato for verificado que há retorno para a consulta.
        var dtpRetorno = Builder.NewDateTimePicker(105, 80, "dd/MM/yyyy");
        dtpRetorno.MinDate = DateTime.Parse(dtpData.Value.ToString());
        dtpRetorno.MaxDate = new DateTime(2026, 12, 31);

        bool retorno = DBTest.dadosConsulta[Eventos.GetCurrentSelectedRowId()].retorno;
        var chkRetorno = new CheckBox
        {
            Top = 100,
            Left = 210,
            Text = "Retorno",
            Checked = retorno
        };


        // Carrega dos dados as informações, para a interface, de acordo com o id da linha selecionada no momento.
        txtNomePaciente.Text = DBTest.dadosConsulta[Eventos.GetCurrentSelectedRowId()].nomePaciente;
        txtNomeMedico.Text = DBTest.dadosConsulta[Eventos.GetCurrentSelectedRowId()].nomeMedico;

        string dataConsulta = DBTest.dadosConsulta[Eventos.GetCurrentSelectedRowId()].data.ToString();
        dtpData.Text = dataConsulta;

        dtpTime.Text = DBTest.dadosConsulta[Eventos.GetCurrentSelectedRowId()].horario.ToString();

        // TODO: deve-se configurar o chkRetorno para que, quando for alterado o estado entre "checked" true e false, o dtpRetorno também mude de Enabled true para false, etc.
        if (retorno)
        {
            dtpRetorno.Text = DBTest.dadosConsulta[Eventos.GetCurrentSelectedRowId()].dataRetorno.ToString();
        }
        else
        {
            dtpRetorno.Text = dataConsulta;
            dtpRetorno.Enabled = false;
        }

        var btnOk = Builder.AddButton("Salvar", 90, 150);
        var btnCancel = Builder.AddButton("Cancelar", 230, 150);

        formDialog.Controls.Clear();

        formDialog.Controls.Add(lblNomePaciente);
        formDialog.Controls.Add(lblNomeMedico);
        formDialog.Controls.Add(lblData);
        formDialog.Controls.Add(lblHorario);
        formDialog.Controls.Add(lblRetorno);
        formDialog.Controls.Add(txtNomePaciente);
        formDialog.Controls.Add(txtNomeMedico);
        formDialog.Controls.Add(dtpData);
        formDialog.Controls.Add(dtpTime);
        formDialog.Controls.Add(dtpRetorno);
        formDialog.Controls.Add(chkRetorno);
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

        txtNomeMedico.Text = DBTest.dadosMedico[Eventos.GetCurrentSelectedRowId()].nomeMedico;
        txtTelefone.Text = DBTest.dadosMedico[Eventos.GetCurrentSelectedRowId()].telefone;
        txtValorConsulta.Text = DBTest.dadosMedico[Eventos.GetCurrentSelectedRowId()].valorConsulta.ToString();

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

        // TODO... 
        txtNomePaciente.Text = DBTest.dadosPaciente[Eventos.GetCurrentSelectedRowId()].nomePaciente;
        txtEndereco.Text = DBTest.dadosPaciente[Eventos.GetCurrentSelectedRowId()].endereco;
        txtNumero.Text = DBTest.dadosPaciente[Eventos.GetCurrentSelectedRowId()].numero.ToString();
        txtBairro.Text = DBTest.dadosPaciente[Eventos.GetCurrentSelectedRowId()].bairro;
        txtCidade.Text = DBTest.dadosPaciente[Eventos.GetCurrentSelectedRowId()].cidade;
        txtCep.Text = DBTest.dadosPaciente[Eventos.GetCurrentSelectedRowId()].cep.ToString();
        txtTelefone.Text = DBTest.dadosPaciente[Eventos.GetCurrentSelectedRowId()].telefone.ToString();
        txtCelular.Text = DBTest.dadosPaciente[Eventos.GetCurrentSelectedRowId()].celular.ToString();

        if (DBTest.dadosPaciente[Eventos.GetCurrentSelectedRowId()].sexo == "Masculino")
        {
            radioMasculino.Checked = true;
        }
        else
        {
            radioFeminino.Checked = true;
        }

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
            Text = text,
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
            Name = "table_" + tab.Name,
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
        table.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // seleciona visualmente toda a linha.
        table.MultiSelect = false;
        table.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;

        // para cada name e header em colunas:
        foreach (var (name, header) in colunas)
        {
            table.Columns.Add(name, header);
        }

        table.CellClick += Eventos.dataGridView_CellClick;

        tab.Controls.Add(table);
    }
}


//TODO: seria interessante simular situações assícronas (async) como delays de carga, etc, pra ver como o sistema reage a isso.
//DBTest poderá se tornar uma interface de serviço: ou seja, é esta camada que deverá conectar-se ao banco e fazer a ponte entre front e dados (front não acessa dados diretamente).
public class DBTest
{
    // Objetos do tipo "RegistroConsulta", "RegistroMedico" e "RegistroPaciente". Estes valores são apenas teste (hardcoded), mas o que deve ocorrer de verdade é que um carregamento deve ser feito da base de dados para cá, para isso deve haver um método para cada tipo de objeto.

    // Regra de negócio: data de retorno jamais pode ser anterior à data da consulta.
    public static List<RegistroConsulta> dadosConsulta = new List<RegistroConsulta>
    {
        new(1029,  "Marcela Andrade", "João da Silva",  "2026-07-14", TimeSpan.Parse("14:30"), true, "2026-07-30"),
        new(1030,  "Pedro Alcantara", "Maria Cruz",  "2026-07-15", TimeSpan.Parse("15:30"), false, "-"),
        new(1031,  "Marcos Almeida", "Jacinta Ribeiro",  "2026-07-18", TimeSpan.Parse("16:30"), true, "2026-08-05")
    };

    public static List<RegistroMedico> dadosMedico = new List<RegistroMedico>
    {
        new(1,  "Marcela Andrade", "0800-9090", 170),
        new(2,  "Pedro Alcantara", "0500-9092", 250),
        new(3,  "Marcos Almeida", "0400-9095", 180)
    };

    public static List<RegistroPaciente> dadosPaciente = new List<RegistroPaciente>
    {
        new(40, "João da Silva", "R. Terra das Dores", 157, "Jabuti", "Vilalopolis", 12300000, "Masculino", "-", "(65) 99345-7657"),
        new(45, "Maria Cruz", "R. Francisco Polo", 161, "Mapuio", "Areias", 17800000, "Feminino", "0500-0880", "(85) 97385-1359"),
        new(49, "Jacinta Ribeiro", "R. Ivo de Almeida", 240, "Jangada", "Verdes Prados", 19500000, "Feminino", "-", "(50) 98445-2656")
    };

    // protótipos das funções de carregamento da base:
    private static void LoadConsultasFromDB() { }
    private static void LoadMedicosFromDB() { }
    private static void LoadPacientesFromDB() { }

    public static List<RegistroConsulta> SearchConsultas(List<RegistroConsulta> dados, FiltroConsulta filtro)
    {
        // string codigo, string nomeMedico, string nomePaciente, string data, string horario, bool retorno)
        IEnumerable<RegistroConsulta> SearchFiltering = dados;

        // passa cada um dos filtros ao SearchFiltering, verificando antes se os campos não estão vazios:
        if (!string.IsNullOrWhiteSpace(filtro.codigo))
            SearchFiltering = SearchFiltering.Where(entry => entry.codigo == int.Parse(filtro.codigo));

        if (!string.IsNullOrWhiteSpace(filtro.nomeMedico))
            SearchFiltering = SearchFiltering.Where(entry => entry.nomeMedico == filtro.nomeMedico);

        if (!string.IsNullOrWhiteSpace(filtro.nomePaciente))
            SearchFiltering = SearchFiltering.Where(entry => entry.nomePaciente == filtro.nomePaciente);

        if (!string.IsNullOrWhiteSpace(filtro.data))
            SearchFiltering = SearchFiltering.Where(entry => entry.data == filtro.data);

        if (!string.IsNullOrWhiteSpace(filtro.dataRetorno.ToString()))
            SearchFiltering = SearchFiltering.Where(entry => entry.dataRetorno == filtro.dataRetorno);

        List<RegistroConsulta> SearchResult = SearchFiltering.ToList();

        return SearchResult;
    }
    public static FiltroMedico SearchMedico(List<RegistroMedico> dados, FiltroMedico filtro)
    {
        IEnumerable<RegistroMedico> SearchResult = dados;
        SearchResult = SearchResult.Where(entry => entry.codigo == int.Parse(filtro.codigo));
        return new FiltroMedico("", "", "", "");
    }
    public static FiltroPaciente SearchPaciente(List<RegistroPaciente> dados, FiltroPaciente filtro)
    {
        IEnumerable<RegistroPaciente> SearchResult = dados;
        SearchResult = SearchResult.Where(entry => entry.codigo == int.Parse(filtro.codigo));
        return new FiltroPaciente("", "", "", "", "", "", "", "", "", "");
    }

    public static void LoadConsultasToTable(DataGridView table, List<RegistroConsulta> dados)
    {
        table.Rows.Clear();
        for (int i = 0; i < dados.Count; i++)
        {
            table.Rows.Add(dados[i].codigo, dados[i].nomeMedico, dados[i].nomePaciente, dados[i].data, dados[i].horario, dados[i].retorno);
        }
    }

    public static void LoadMedicosToTable(DataGridView table, List<RegistroMedico> dados)
    {
        for (int i = 0; i < dados.Count; i++)
        {
            table.Rows.Add(dados[i].codigo, dados[i].nomeMedico, dados[i].telefone, dados[i].valorConsulta);
        }
    }

    public static void LoadPacientesToTable(DataGridView table, List<RegistroPaciente> dados)
    {
        for (int i = 0; i < dados.Count; i++)
        {
            table.Rows.Add(dados[i].codigo, dados[i].nomePaciente, dados[i].endereco, dados[i].numero, dados[i].bairro, dados[i].cidade, dados[i].cep, dados[i].sexo, dados[i].telefone, dados[i].celular);
        }
    }
}

public class Eventos
{
    private static int CurrentRowId = 0; // por padrão, o id inicial é 0, ou seja, o primeiro item da lista.

    public static void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return; // evita erro de (índice -1) ao clicar no cabeçalho

        DataGridView dgv = (DataGridView)sender;
        CurrentRowId = e.RowIndex;
        Console.WriteLine("Selecionou linha de id " + CurrentRowId);
    }
    public static void ResetSelectedRowId(int id)
    {
        CurrentRowId = id;
    }
    public static int GetCurrentSelectedRowId()
    {
        return CurrentRowId;
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

// record é basicamente uma classe, só que simplificada para contexto de dados.
// Modelo dos dados que serão apresentados. No caso, nomeMedico e nomePaciente serão puxados de tabelas diferentes da base, já que a tabela consulta só possui ids, e tais ids serão utilizados para puxar exatamente o nome que queremos.
public record RegistroConsulta(int codigo, string nomeMedico, string nomePaciente, string data, TimeSpan horario, bool retorno, string dataRetorno);

public record RegistroMedico(int codigo, string nomeMedico, string telefone, double valorConsulta);

public record RegistroPaciente(int codigo, string nomePaciente, string endereco, int numero, string bairro, string cidade, double cep, string sexo, string telefone, string celular);


// filtros (basicamente, o conteúdo Text das textBoxes, que deverão ser usados pra comparar com os dados da base, na função de pesquisa):
public record FiltroConsulta(string codigo, string nomeMedico, string nomePaciente, string data, string horario, string retorno, string dataRetorno);
public record FiltroMedico(string codigo, string nomeMedico, string telefone, string valorConsulta);
public record FiltroPaciente(string codigo, string nomePaciente, string endereco, string numero, string bairro, string cidade, string cep, string sexo, string telefone, string celular);