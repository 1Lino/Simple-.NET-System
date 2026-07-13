using System.ComponentModel.Design;
using Sistema_De_Aplicativos_Simples__.NET.appsForms;

namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class Consultorio : Form
    {
        public static TabControl tabControl;
        private TabPage tab_consultas;
        private TabPage tab_medicos;
        private TabPage tab_pacientes;
        private FiltroConsulta filtroConsulta = new FiltroConsulta();
        private FiltroMedico filtroMedico = new FiltroMedico();
        private FiltroPaciente filtroPaciente = new FiltroPaciente();
        public static RegistroConsulta registroConsulta; // Os dados do formulário de registro de consulta serão passados pra este objeto.

        public Consultorio()
        {
            InitializeConsultorio();
            InitializeTabControl();
            InitializeTabComponents();

            // tc = tabela consultas; tm = tabela medicos; tp = tabela pacientes
            Builder.AddDataGridViewToTab(tab_consultas,
            [
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
            UI.LoadConsultasToTable((DataGridView)tab_consultas.Controls[$"table_{tab_consultas.Name}"], DBTest.dadosConsulta);
            UI.LoadMedicosToTable((DataGridView)tab_medicos.Controls[$"table_{tab_medicos.Name}"], DBTest.dadosMedico);
            UI.LoadPacientesToTable((DataGridView)tab_pacientes.Controls[$"table_{tab_pacientes.Name}"], DBTest.dadosPaciente);
        }

        private void InitializeConsultorio()
        {
            Text = "Consultório (ERP Software)";
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

            CheckBox chk_retorno = new CheckBox { Name = "chk_retorno", Text = "Retorno", Top = 160 - 5, Left = 400 };

            var txt_consulta = Builder.NewTextBox("txt_consulta", 100, 120, 40);
            var txt_medico = Builder.NewTextBox("txt_medico", 200, 120, 80);
            var txt_paciente = Builder.NewTextBox("txt_paciente", 200, 120, 120);

            DateTimePicker dtp_data = Builder.NewDateTimePicker(160, 60, "dd/MM/yyyy");
            dtp_data.Name = "dtp_data";
            DateTimePicker dtp_time = Builder.NewDateTimePicker(160, 260, "HH:mm");
            dtp_time.Name = "dtp_time";
            dtp_data.MinDate = new DateTime(2026, 1, 1);
            dtp_data.MaxDate = new DateTime(2026, 12, 31);
            dtp_time.ShowUpDown = true;

            // ## ABA MÉDICOS ##
            GroupBox grp_medicos = Builder.NewGroupBox("grp_medicos", "Buscar Médico");

            var lbl_medico_id = Builder.NewLabel("ID Médico", 100, 10, 40);
            var lbl_medico_nome = Builder.NewLabel("Nome Médico", 100, 10, 80);

            var txt_medico_id = Builder.NewTextBox("txt_medico_id", 100, 120, 40);
            var txt_medico_nome = Builder.NewTextBox("txt_medico_nome", 200, 120, 80);

            // ## ABA PACIENTES ##
            GroupBox grp_pacientes = Builder.NewGroupBox("grp_pacientes", "Buscar Paciente");

            var lbl_paciente_id = Builder.NewLabel("ID Paciente", 100, 10, 40);
            var lbl_paciente_nome = Builder.NewLabel("Nome Paciente", 100, 10, 80);

            var txt_paciente_id = Builder.NewTextBox("txt_paciente_id", 100, 120, 40);
            var txt_paciente_nome = Builder.NewTextBox("txt_paciente_nome", 200, 120, 80);

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

            InitConsultasChangeEvents(grp_consultas);
            InitMedicosChangeEvents(grp_medicos);
            InitPacientesChangeEvents(grp_pacientes);
        }

        // TODO: Considerar mandar todos os métodos de eventos abaixo pra classe Eventos.
        private void InitConsultasChangeEvents(GroupBox grp_consulta)
        {
            // Inicializa os eventos das texboxes da tab Consultas:

            //dados tipo string:
            grp_consulta.Controls["txt_consulta"].TextChanged += (_, _) =>
            {
                // pra resetar o filtro de datas e de horario, já que, do contrário, seria aplicado permanentemente, já que DateOnly e TimeSpan são fixos.
                filtroConsulta.data = null;
                filtroConsulta.horario = null;

                filtroConsulta.codigo = grp_consulta.Controls["txt_consulta"].Text;
                UI.FiltrarConsulta(filtroConsulta);
            };

            //dados tipo string:
            grp_consulta.Controls["txt_medico"].TextChanged += (_, _) =>
            {
                filtroConsulta.data = null;
                filtroConsulta.horario = null;

                filtroConsulta.nomeMedico = grp_consulta.Controls["txt_medico"].Text;
                UI.FiltrarConsulta(filtroConsulta);
            };

            //dados tipo string:
            grp_consulta.Controls["txt_paciente"].TextChanged += (_, _) =>
            {
                filtroConsulta.data = null;
                filtroConsulta.horario = null;

                filtroConsulta.nomePaciente = grp_consulta.Controls["txt_paciente"].Text;
                UI.FiltrarConsulta(filtroConsulta);
            };

            //dados tipo DateOnly
            // O casting de DateTimePicker aqui é necessário, pois Controls não possui por si só a propriedade ValueChanged:
            ((DateTimePicker)grp_consulta.Controls["dtp_data"]).ValueChanged += (_, _) =>
            {
                filtroConsulta.horario = null;
                filtroConsulta.data = DateOnly.Parse(((DateTimePicker)grp_consulta.Controls["dtp_data"]).Text);
                Console.WriteLine(filtroConsulta.data);
                UI.FiltrarConsulta(filtroConsulta);
            };

            //dados tipo TimeSpan
            ((DateTimePicker)grp_consulta.Controls["dtp_time"]).ValueChanged += (_, _) =>
            {
                filtroConsulta.data = null;

                filtroConsulta.horario = TimeSpan.Parse(((DateTimePicker)grp_consulta.Controls["dtp_time"]).Text);
                Console.WriteLine(filtroConsulta.horario);
                UI.FiltrarConsulta(filtroConsulta);
            };

            //dados tipo bool
            ((CheckBox)grp_consulta.Controls["chk_retorno"]).CheckedChanged += (_, _) =>
            {
                filtroConsulta.data = null;
                filtroConsulta.horario = null;

                filtroConsulta.retorno = ((CheckBox)grp_consulta.Controls["chk_retorno"]).Checked;
                UI.FiltrarConsulta(filtroConsulta);
            };
        }

        private void InitMedicosChangeEvents(GroupBox grp_medico)
        {
            grp_medico.Controls["txt_medico_id"].TextChanged += (_, _) =>
            {
                filtroMedico.codigo = grp_medico.Controls["txt_medico_id"].Text;
                UI.FiltrarMedicos(filtroMedico);
            };

            grp_medico.Controls["txt_medico_nome"].TextChanged += (_, _) =>
            {
                filtroMedico.nomeMedico = grp_medico.Controls["txt_medico_nome"].Text;
                UI.FiltrarMedicos(filtroMedico);
            };
        }

        private void InitPacientesChangeEvents(GroupBox grp_paciente)
        {
            grp_paciente.Controls["txt_paciente_id"].TextChanged += (_, _) =>
            {
                filtroPaciente.codigo = grp_paciente.Controls["txt_paciente_id"].Text;
                UI.FiltrarPacientes(filtroPaciente);
            };

            grp_paciente.Controls["txt_paciente_nome"].TextChanged += (_, _) =>
            {
                filtroPaciente.nomePaciente = grp_paciente.Controls["txt_paciente_nome"].Text;
                UI.FiltrarPacientes(filtroPaciente);
            };
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

        var comboNomePaciente = new ComboBox
        {
            Tag = "paciente",
            Width = 240,
            Left = 80,
            Top = 10,
            AutoCompleteMode = AutoCompleteMode.SuggestAppend,
            AutoCompleteSource = AutoCompleteSource.ListItems,
            DataSource = DBTest.dadosConsulta.ToList(), // ToList é usado aqui apenas para criar uma cópia, do contrário os dados seriam compartilhados entre todos os comboboxes, fazendo com que a edição em um refletisse no outro.
            DisplayMember = "nomePaciente",
            ValueMember = "codigo"
        };
        var comboNomeMedico = new ComboBox
        {
            Tag = "médico",
            Width = 240,
            Left = 80,
            Top = 40,
            AutoCompleteMode = AutoCompleteMode.SuggestAppend,
            AutoCompleteSource = AutoCompleteSource.ListItems,
            DataSource = DBTest.dadosConsulta.ToList(),
            DisplayMember = "nomeMedico",
            ValueMember = "codigo"
        };

        var dtpData = Builder.NewDateTimePicker(75, 80, "dd/MM/yyyy");
        dtpData.MinDate = DateTime.Now; // a data mínima deve ser sempre o dia atual.
        dtpData.MaxDate = new DateTime(2026, 12, 31); // a data máxima deve ser a data final do período de operações anual da empresa.

        var dtpTime = Builder.NewDateTimePicker(75, 260, "HH:mm");
        // puxa o horário atual como horário mínimo caso a data da consulta coincida com a de hoje, afinal, se a data da consulta é hoje, por exemplo, e estamos às 14 horas, não é possível marcar para as 10 horas do dia.
        dtpTime.MinDate = dtpData.Value.Date == DateTime.Now.Date ? DateTime.Now : DateTime.MinValue;
        dtpTime.Width = 60;
        dtpTime.ShowUpDown = true;

        dtpData.ValueChanged += (_, _) =>
        {
            // toda vez que mudarmos a data da consulta, atualiza os limites mínimos de horário:
            dtpTime.MinDate = dtpData.Value.Date == DateTime.Now.Date ? DateTime.Now : new DateTime(2026, 01, 01);
            return;
        };

        var chkRetorno = new CheckBox
        {
            Text = "Retorno",
            Checked = false,
            Top = 105,
            Left = 260
        };

        var dtpRetorno = Builder.NewDateTimePicker(105, 80, "dd/MM/yyyy");
        dtpRetorno.MinDate = dtpData.Value;
        dtpRetorno.MaxDate = new DateTime(2026, 12, 31);
        dtpRetorno.Enabled = false;

        chkRetorno.CheckedChanged += (_, _) =>
        {
            if (chkRetorno.Checked)
            {
                dtpRetorno.Enabled = true;
                return;
            }
            dtpRetorno.Enabled = false;
            return;
        };

        var btnOk = Builder.AddButton("Salvar", 90, 150);
        var btnCancel = Builder.AddButton("Cancelar", 230, 150);
        btnOk.DialogResult = DialogResult.None;
        btnCancel.DialogResult = DialogResult.Cancel;

        formDialog.Controls.Clear(); // esta limpeza deve ser feita a cada chamada, pois o formDialog é apenas um único componente reutilizado em todas as situações.

        formDialog.Controls.Add(lblNomePaciente);
        formDialog.Controls.Add(lblNomeMedico);
        formDialog.Controls.Add(lblData);
        formDialog.Controls.Add(lblHorario);
        formDialog.Controls.Add(comboNomePaciente);
        formDialog.Controls.Add(comboNomeMedico);
        formDialog.Controls.Add(dtpData);
        formDialog.Controls.Add(dtpTime);
        formDialog.Controls.Add(chkRetorno);
        formDialog.Controls.Add(dtpRetorno);
        formDialog.Controls.Add(btnOk);
        formDialog.Controls.Add(btnCancel);

        //TODO: além de ValidateComboBox receber uma lista de componentes, seria melhor que recebece uma lista contendo todos os dados de todos os campos, assim o método o método isConsultaValid faria a verificação somente de dados, já que se trata de um método backend.
        btnOk.Click += (_, _) =>
        {
            Consultorio.registroConsulta = new RegistroConsulta(
                (int.Parse(DBTest.dadosConsulta[DBTest.dadosConsulta.Count - 1].codigo) + 1).ToString(), // pega a última consulta da lista e acrescenta 1 ao código.
                comboNomeMedico.Text,
                comboNomePaciente.Text,
                DateOnly.Parse(dtpData.Text),
                TimeSpan.Parse(dtpTime.Text),
                chkRetorno.Checked,
                DateOnly.Parse(dtpRetorno.Text)
            );

            // Print dos dados que deverão ir para a validação:
            Console.WriteLine($"ID: {Consultorio.registroConsulta.codigo}\nMédico: {Consultorio.registroConsulta.nomeMedico}\nPaciente: {Consultorio.registroConsulta.nomePaciente}\nData: {Consultorio.registroConsulta.data} - {Consultorio.registroConsulta.horario}\nRetorno: {Consultorio.registroConsulta.retorno} - {Consultorio.registroConsulta.dataRetorno}");

            Eventos.ValidateComboBox(btnOk, new List<ComboBox> { comboNomePaciente, comboNomeMedico }, formDialog);
        };

        btnCancel.Click += (_, _) =>
        {
            Console.WriteLine("Operação Cancelada!");
        };

        formDialog.ShowDialog();

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
        txtNomeMedico.Tag = "Nome";
        txtTelefone.Tag = "Telefone";
        txtValorConsulta.Tag = "Valor Consulta";

        var btnOk = Builder.AddButton("Salvar", 90, 150);
        var btnCancel = Builder.AddButton("Cancelar", 230, 150);
        btnOk.DialogResult = DialogResult.None;
        btnCancel.DialogResult = DialogResult.Cancel;

        formDialog.Controls.Clear();

        formDialog.Controls.Add(lblNomeMedico);
        formDialog.Controls.Add(lblTelefone);
        formDialog.Controls.Add(lblValorConsulta);
        formDialog.Controls.Add(txtNomeMedico);
        formDialog.Controls.Add(txtTelefone);
        formDialog.Controls.Add(txtValorConsulta);
        formDialog.Controls.Add(btnOk);
        formDialog.Controls.Add(btnCancel);

        btnOk.Click += (_, _) => Eventos.ValidateTextBox(btnOk, new List<TextBox> { txtNomeMedico, txtTelefone, txtValorConsulta }, formDialog);

        btnCancel.Click += (_, _) =>
        {
            Console.WriteLine("Operação Cancelada!");
        };

        formDialog.ShowDialog();
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
        radioMasculino.Checked = true;

        var txtTelefone = Builder.NewTextBox("paciente_telefone", 150, 100, 160);
        var txtCelular = Builder.NewTextBox("paciente_celular", 150, 100, 190);

        txtNomePaciente.Tag = "Nome";
        txtEndereco.Tag = "Endereço";
        txtNumero.Tag = "Número";
        txtBairro.Tag = "Bairro";
        txtCidade.Tag = "Cidade";
        txtCep.Tag = "CEP";
        txtTelefone.Tag = "Telefone";
        txtCelular.Tag = "Celular";

        var btnOk = Builder.AddButton("Salvar", 90, 250);
        var btnCancel = Builder.AddButton("Cancelar", 230, 250);
        btnOk.DialogResult = DialogResult.None;
        btnCancel.DialogResult = DialogResult.Cancel;

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

        btnOk.Click += (_, _) => Eventos.ValidateTextBox(btnOk, new List<TextBox>
        {
            txtNomePaciente,
            txtEndereco,
            txtNumero,
            txtBairro,
            txtCidade,
            txtCep,
            txtTelefone,
            txtCelular
        }, formDialog);

        btnCancel.Click += (_, _) =>
        {
            Console.WriteLine("Operação Cancelada!");
        };

        formDialog.ShowDialog();
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
        var dtpRetorno = Builder.NewDateTimePicker(105, 80, "dd/MM/yyyy");
        dtpRetorno.MinDate = DateTime.Parse(DBTest.dadosConsulta[Eventos.GetCurrentSelectedRowId()].data.ToString());
        dtpRetorno.MaxDate = new DateTime(2026, 12, 31);
        Console.WriteLine(dtpData.Value);

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

        // Texto do datimepicker de retorno é puxado somente se de fato houver retorno para aquela consulta, do contrário puxa uma data padrão. Ademais, o estado Enabled deste componente é definido também pelo retorno, enquanto que o evento de CheckedChanged do checkbox controla também se esse datetimepicker está ativo ou não no momento da edição de entrada.
        dtpRetorno.Text = retorno ? DBTest.dadosConsulta[Eventos.GetCurrentSelectedRowId()].dataRetorno.ToString() : dataConsulta;
        dtpRetorno.Enabled = retorno ? true : false;
        chkRetorno.CheckedChanged += (_, _) =>
        {
            dtpRetorno.Enabled = chkRetorno.Checked ? true : false;
        };

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
    // Objetos do tipo "RegistroConsulta", "RegistroMedico" e "RegistroPaciente". Estes valores são apenas teste (hardcoded), mas o que deve ocorrer de verdade é que um carregamento deve ser feito da base de dados para cá, para isso deve haver um método para cada tipo de objeto. Como este projeto é de "playground", só isto aqui basta pra simulação.

    // Regra de negócio: data de retorno jamais pode ser anterior à data da consulta.
    public static List<RegistroConsulta> dadosConsulta = new List<RegistroConsulta>
    {
        new("1029",  "Marcela Andrade", "João da Silva",  DateOnly.Parse("14/07/2026"), TimeSpan.Parse("14:30"), true, DateOnly.Parse("30/07/2026")),
        new("1030",  "Pedro Alcantara", "Maria Cruz",  DateOnly.Parse("15/07/2026"), TimeSpan.Parse("15:30"), false, DateOnly.Parse("15/07/2026")),
        new("1031",  "Marcos Almeida", "Jacinta Ribeiro",  DateOnly.Parse("18/07/2026"), TimeSpan.Parse("16:30"), true, DateOnly.Parse("05/08/2026"))
    };

    public static List<RegistroMedico> dadosMedico = new List<RegistroMedico>
    {
        new("1",  "Marcela Andrade", "0800-9090", 170),
        new("2",  "Pedro Alcantara", "0500-9092", 250),
        new("3",  "Marcos Almeida", "0400-9095", 180)
    };

    public static List<RegistroPaciente> dadosPaciente = new List<RegistroPaciente>
    {
        new("40", "João da Silva", "R. Terra das Dores", 157, "Jabuti", "Vilalopolis", 12300000, "Masculino", "-", "(65) 99345-7657"),
        new("45", "Maria Cruz", "R. Francisco Polo", 161, "Mapuio", "Areias", 17800000, "Feminino", "0500-0880", "(85) 97385-1359"),
        new("49", "Jacinta Ribeiro", "R. Ivo de Almeida", 240, "Jangada", "Verdes Prados", 19500000, "Feminino", "-", "(50) 98445-2656")
    };

    public static List<RegistroConsulta> SearchConsultas(List<RegistroConsulta> dados, FiltroConsulta filtro)
    {
        // string codigo, string nomeMedico, string nomePaciente, string data, string horario, bool retorno)
        IEnumerable<RegistroConsulta> SearchFiltering = dados;

        // passa cada um dos filtros ao SearchFiltering, verificando antes se os campos não estão vazios:
        if (!string.IsNullOrWhiteSpace(filtro.codigo))
            SearchFiltering = SearchFiltering.Where(entry => entry.codigo.Contains(filtro.codigo, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filtro.nomeMedico))
            SearchFiltering = SearchFiltering.Where(entry => entry.nomeMedico.Contains(filtro.nomeMedico, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filtro.nomePaciente))
            SearchFiltering = SearchFiltering.Where(entry => entry.nomePaciente.Contains(filtro.nomePaciente, StringComparison.OrdinalIgnoreCase));

        if (filtro.data != null)
            SearchFiltering = SearchFiltering.Where(entry => entry.data == filtro.data);

        if (filtro.horario != null)
            SearchFiltering = SearchFiltering.Where(entry => entry.horario == filtro.horario);

        if (filtro.retorno)
            SearchFiltering = SearchFiltering.Where(entry => entry.retorno == filtro.retorno);

        List<RegistroConsulta> SearchResult = SearchFiltering.ToList();

        return SearchResult;
    }
    public static List<RegistroMedico> SearchMedico(List<RegistroMedico> dados, FiltroMedico filtro)
    {
        IEnumerable<RegistroMedico> SearchFiltering = dados;

        if (!string.IsNullOrWhiteSpace(filtro.codigo))
            SearchFiltering = SearchFiltering.Where(entry => entry.codigo.Contains(filtro.codigo, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filtro.nomeMedico))
            SearchFiltering = SearchFiltering.Where(entry => entry.nomeMedico.Contains(filtro.nomeMedico, StringComparison.OrdinalIgnoreCase));

        List<RegistroMedico> SearchResult = SearchFiltering.ToList();

        return SearchResult;
    }
    public static List<RegistroPaciente> SearchPaciente(List<RegistroPaciente> dados, FiltroPaciente filtro)
    {
        IEnumerable<RegistroPaciente> SearchFiltering = dados;

        if (!string.IsNullOrWhiteSpace(filtro.codigo))
            SearchFiltering = SearchFiltering.Where(entry => entry.codigo.Contains(filtro.codigo, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filtro.nomePaciente))
            SearchFiltering = SearchFiltering.Where(entry => entry.nomePaciente.Contains(filtro.nomePaciente, StringComparison.OrdinalIgnoreCase));

        List<RegistroPaciente> SearchResult = SearchFiltering.ToList();

        return SearchResult;
    }

    //TODO: funções de validação backend (verifica cada item de txtList, checa se está conforme os dados existentes na base)
    public static bool isConsultaValid(List<ComboBox> txtList)
    {
        // FirstOrDefault procura a primeira ocorrência que satisfaz as condições. Se não encontrar, retorna null;
        //Nota: é necessário prestar atenção à ordem com que os valores são passados para o método. No caso, nomeMédico está no índice 1 da txtList, mas poderia estar no índice 0. Depois mudarei isso, para que ordem não importe.
        var medico = dadosConsulta.FirstOrDefault(p => p.nomeMedico == txtList[1].Text);
        var paciente = dadosConsulta.FirstOrDefault(p => p.nomePaciente == txtList[0].Text);

        if (medico != null && paciente != null)
        {
            return true;
        }
        return false;
    }
    public static void isMedicoValid(List<ComboBox> txtList)
    {

    }
    public static void isPacienteValid(List<ComboBox> txtList)
    {

    }
    //TODO: funções de criação de novos dados.
    public static void NewConsulta()
    {
        // Uma vez que as validações passem, é hora de registrar os dados.
    }
    public static void NewMedico() { }
    public static void NewPaciente() { }

    //TODO: funções de edição de dados.
    public static void UpdateConsulta() { }
    public static void UpdateMedico() { }
    public static void UpdatePaciente() { }

    //TODO: funções de deleção de dados.
    public static void RemoveConsulta() { }
    public static void RemoveMedico() { }
    public static void RemovePaciente() { }
}

public class UI
{
    public static void FiltrarConsulta(FiltroConsulta filtroConsulta)
    {
        TabPage currentPage = Consultorio.tabControl.SelectedTab;
        DataGridView dgv = (DataGridView)currentPage.Controls[$"table_{currentPage.Name}"];

        List<RegistroConsulta> FilteredData = DBTest.SearchConsultas(DBTest.dadosConsulta, filtroConsulta);

        // O método Any() verifica se SearchResult possui algo na lista, do contrário retorna os dados iniciais mesmo, no caso de nada ser encontrado pelo filtro, isto é para evitar que a tabela fique vazia caso nada seja.
        List<RegistroConsulta> FinalResult = FilteredData.Any() ? FilteredData : DBTest.dadosConsulta;

        LoadConsultasToTable(dgv, FinalResult);
    }

    public static void FiltrarMedicos(FiltroMedico filtroMedico)
    {
        TabPage currentPage = Consultorio.tabControl.SelectedTab;
        DataGridView dgv = (DataGridView)currentPage.Controls[$"table_{currentPage.Name}"];

        List<RegistroMedico> FilteredData = DBTest.SearchMedico(DBTest.dadosMedico, filtroMedico);

        // O método Any() verifica se SearchResult possui algo na lista, do contrário retorna os dados iniciais mesmo, no caso de nada ser encontrado pelo filtro, isto é para evitar que a tabela fique vazia caso nada seja.
        List<RegistroMedico> FinalResult = FilteredData.Any() ? FilteredData : DBTest.dadosMedico;

        LoadMedicosToTable(dgv, FinalResult);
    }

    public static void FiltrarPacientes(FiltroPaciente filtroPaciente)
    {
        TabPage currentPage = Consultorio.tabControl.SelectedTab;
        DataGridView dgv = (DataGridView)currentPage.Controls[$"table_{currentPage.Name}"];

        List<RegistroPaciente> FilteredData = DBTest.SearchPaciente(DBTest.dadosPaciente, filtroPaciente);

        // O método Any() verifica se SearchResult possui algo na lista, do contrário retorna os dados iniciais mesmo, no caso de nada ser encontrado pelo filtro, isto é para evitar que a tabela fique vazia caso nada seja.
        List<RegistroPaciente> FinalResult = FilteredData.Any() ? FilteredData : DBTest.dadosPaciente;

        LoadPacientesToTable(dgv, FinalResult);
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
        table.Rows.Clear();
        for (int i = 0; i < dados.Count; i++)
        {
            table.Rows.Add(dados[i].codigo, dados[i].nomeMedico, dados[i].telefone, dados[i].valorConsulta);
        }
    }

    public static void LoadPacientesToTable(DataGridView table, List<RegistroPaciente> dados)
    {
        table.Rows.Clear();
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

    public static void ValidateTextBox(Button btnOk, List<TextBox> txtList, Form formDiag)
    {
        foreach (TextBox textBox in txtList)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show($"Campo '{textBox.Tag}' não pode ficar vazio!");
                textBox.Focus();
                return;
            }
        }

        // TODO: chama a função de validação de backend aqui. Se for bem sucedida, aciona a linha abaixo:
        btnOk.DialogResult = DialogResult.OK;

        // TODO: então chama a função que registra o novo cadastro na base.

        Console.WriteLine("Operação completa!");
        formDiag.Close(); // fecha o form especificado
    }

    public static void ValidateComboBox(Button btnOk, List<ComboBox> txtList, Form formDiag)
    {
        foreach (ComboBox comboBox in txtList)
        {
            if (string.IsNullOrWhiteSpace(comboBox.Text))
            {
                MessageBox.Show($"Campo '{comboBox.Tag}' deve conter um valor válido.");
                comboBox.Focus();
                return;
            }
        }

        // Se for verificado que tanto o nome do médico como do paciente, passados na validação de Front, não existem porém na base de dados, então não procede com cadastro. 
        if (!DBTest.isConsultaValid(txtList))
        {
            MessageBox.Show($"Valor inválido!");
            return;
        }

        btnOk.DialogResult = DialogResult.OK;

        // TODO: então chama a função que registra o novo cadastro na base.

        Console.WriteLine("Operação completa!");
        formDiag.Close(); // fecha o form especificado
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


// TODO: Ver viabilidade de transformar os blocos a seguir em uma interface, já que compartilham de muitas propriedades similares, fazendo com que as classes individuais (record) herdassem essa interface e fosse declarado nelas apenas o que há de diferente (polimorfismo).

// record é basicamente uma classe, só que simplificada para contexto de dados.
// Modelo dos dados que serão apresentados. No caso, nomeMedico e nomePaciente serão puxados de tabelas diferentes da base, já que a tabela consulta só possui ids, e tais ids serão utilizados para puxar exatamente o nome que queremos.
public record RegistroConsulta(string codigo, string nomeMedico, string nomePaciente, DateOnly data, TimeSpan horario, bool retorno, DateOnly dataRetorno);

// public record RegistroConsulta
// {
//     public string codigo { get; set; }
//     public string nomeMedico { get; set; }
//     public string nomePaciente { get; set; }
//     public DateOnly data { get; set; }
//     public TimeSpan horario { get; set; }
//     public bool retorno { get; set; }
//     public DateOnly dataRetorno { get; set; }

//     public RegistroConsulta(string codigo, string nomeMedico, string nomePaciente, DateOnly data, TimeSpan horario, bool retorno, DateOnly dataRetorno)
//     {

//     }
// };

public record RegistroMedico(string codigo, string nomeMedico, string telefone, double valorConsulta);

public record RegistroPaciente(string codigo, string nomePaciente, string endereco, int numero, string bairro, string cidade, double cep, string sexo, string telefone, string celular);


// filtros (basicamente, o conteúdo Text das textBoxes, que deverão ser usados pra comparar com os dados da base, na função de pesquisa):
// public record FiltroConsulta(string codigo, string nomeMedico, string nomePaciente, string data, string horario, string retorno, string dataRetorno);

public record FiltroConsulta
{
    // como record é naturalmente imutável, "set" normalmente seria "init" (ou seja, só pode definir na inicialização), por isso é preciso usar "set" explicitamente aqui, para permitir modificação de propriedades da instância
    public string codigo { get; set; }
    public string nomeMedico { get; set; }
    public string nomePaciente { get; set; }
    // "?" torna o valor anulável, de modo que seja possível que o campo seja nulo. Isto é necessário aqui porque, para DateOnly e TimeSpan nunca podem estar nulos normalmente, sendo assim isto significa que o filtro já seria inicializado com um valor padrão (ex: 01/01/0001 00:00:00), o que atrapalharia o funcionamento do filtro. Então tornamos o valor anulável para que isto não ocorra e o filtro comece tendo "null" por valor inicial. Com o booleano "retorno" isto também se aplicaria, já que bool só pode ser "true" ou "false" por padrão, mas não o anularemos porque não é necessário, neste contexto.
    public DateOnly? data { get; set; }
    public TimeSpan? horario { get; set; }
    public bool retorno { get; set; }
    // public DateOnly? dataRetorno { get; set; }
}
public record FiltroMedico
{
    public string codigo { get; set; }
    public string nomeMedico { get; set; }
    // public string telefone { get; set; }
    // public double valorConsulta { get; set; }
}
public record FiltroPaciente
{
    public string codigo { get; set; }
    public string nomePaciente { get; set; }
    // public string endereco { get; set; }
    // public string numero { get; set; }
    // public string bairro { get; set; }
    // public string cidade { get; set; }
    // public string cep { get; set; }
    // public string sexo { get; set; }
    // public string telefone { get; set; }
    // public string celular { get; set; }
}