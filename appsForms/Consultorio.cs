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
            BackColor = Color.FromArgb(62, 85, 85);
        }

        // TODO: Esse método pode ser subdividido em vários, pra melhor manuntenção.
        // Idealmente, este método é pra inicializar somente o TabControl e suas tabs.
        // O resto deve ser adicionado via outro método.
        private void InitializeTabControl()
        {
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
            };

            TabPage tab1 = new TabPage();
            tab1.Name = "consultas";
            tab1.Text = "Consultas";
            tab1.BackColor = Color.White;

            // TODO: grp1 deve ser grp_consulta
            GroupBox grp1 = NewGroupBox("grp_consultas", "Buscar Consulta");
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

            grp1.Controls.Add(lbl_consulta);
            grp1.Controls.Add(lbl_medico);
            grp1.Controls.Add(lbl_paciente);
            grp1.Controls.Add(lbl_data);
            grp1.Controls.Add(dtp_data);
            grp1.Controls.Add(lbl_horario);
            grp1.Controls.Add(dtp_time);
            grp1.Controls.Add(chk_retorno);
            grp1.Controls.Add(txt_consulta);
            grp1.Controls.Add(txt_medico);
            grp1.Controls.Add(txt_paciente);
            tab1.Controls.Add(grp1);

            TabPage tab2 = new TabPage();
            tab2.Name = "medicos";
            tab2.Text = "Médicos";
            tab2.BackColor = Color.White;

            // TODO: grp2 deve ser grp_medico
            GroupBox grp2 = NewGroupBox("grp_medicos", "Buscar Médico");
            Label lbl_medico_id = NewLabel("Nº Médico", 40);
            Label lbl_medico_nome = NewLabel("Nome Médico", 80);
            TextBox txt_medico_id = NewTextBox("box_medico_id", 100, 40);
            TextBox txt_medico_nome = NewTextBox("box_medico_nome", 200, 80);

            grp2.Controls.Add(lbl_medico_id);
            grp2.Controls.Add(lbl_medico_nome);
            grp2.Controls.Add(txt_medico_id);
            grp2.Controls.Add(txt_medico_nome);
            tab2.Controls.Add(grp2);

            TabPage tab3 = new TabPage();
            tab3.Name = "pacientes";
            tab3.Text = "Pacientes";
            tab3.BackColor = Color.White;

            // TODO: grp3 deve ser grp_paciente
            GroupBox grp3 = NewGroupBox("grp_pacientes", "Buscar Paciente");
            Label lbl_paciente_id = NewLabel("Nº Paciente", 40);
            Label lbl_paciente_nome = NewLabel("Nome Paciente", 80);
            TextBox txt_paciente_id = NewTextBox("box_paciente_id", 100, 40);
            TextBox txt_paciente_nome = NewTextBox("box_paciente_nome", 200, 80);

            grp3.Controls.Add(lbl_paciente_id);
            grp3.Controls.Add(lbl_paciente_nome);
            grp3.Controls.Add(txt_paciente_id);
            grp3.Controls.Add(txt_paciente_nome);
            tab3.Controls.Add(grp3);

            tabControl.TabPages.Add(tab1);
            tabControl.TabPages.Add(tab2);
            tabControl.TabPages.Add(tab3);

            this.Controls.Add(tabControl);
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