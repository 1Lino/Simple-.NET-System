namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class Consultorio : Form
    {
        public static Consultorio Instance { get; private set; }
        private TabControl tabControl;

        public Consultorio()
        {
            Instance = this;
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

            GroupBox grp1 = NewGroupBox("grp_consultas", "Pesquisar Consultas");
            Label lbl_consulta = NewLabel("Nº Consulta", 40);
            Label lbl_medico = NewLabel("Nome Médico", 80);
            Label lbl_paciente = NewLabel("Nome Paciente", 120);
            Label lbl_data = NewLabel("Data: 10/09/2022", 160);
            Label lbl_horario = NewLabel("Horário: 10:30", 160, 200);
            CheckBox chk_retorno = new CheckBox { Text = "Retorno", Top = 160 - 5, Left = 350 };
            TextBox txt_consulta = NewTextBox("box_consulta", 100, 40);
            TextBox txt_medico = NewTextBox("box_medico", 200, 80);
            TextBox txt_paciente = NewTextBox("box_paciente", 200, 120);


            grp1.Controls.Add(lbl_consulta);
            grp1.Controls.Add(lbl_medico);
            grp1.Controls.Add(lbl_paciente);
            grp1.Controls.Add(lbl_data);
            grp1.Controls.Add(lbl_horario);
            grp1.Controls.Add(chk_retorno);
            grp1.Controls.Add(txt_consulta);
            grp1.Controls.Add(txt_medico);
            grp1.Controls.Add(txt_paciente);
            tab1.Controls.Add(grp1);

            TabPage tab2 = new TabPage();
            tab2.Name = "medicos";
            tab2.Text = "Médicos";
            tab2.BackColor = Color.White;

            GroupBox grp2 = NewGroupBox("grp_medicos", "Encontrar Médicos");
            // Label lbl_consulta2 = NewLabel("Nº Consulta", 40);
            // Label lbl_medico2 = NewLabel("Nome Médico", 80);
            // Label lbl_paciente2 = NewLabel("Nome Paciente", 120);

            // grp2.Controls.Add(lbl_consulta2);
            // grp2.Controls.Add(lbl_medico2);
            // grp2.Controls.Add(lbl_paciente2);
            tab2.Controls.Add(grp2);

            TabPage tab3 = new TabPage();
            tab3.Name = "pacientes";
            tab3.Text = "Pacientes";
            tab3.BackColor = Color.White;

            GroupBox grp3 = NewGroupBox("grp_pacientes", "Encontrar Pacientes");
            // Label lbl_consulta3 = NewLabel("Nº Consulta", 40);
            // Label lbl_medico3 = NewLabel("Nome Médico", 80);
            // Label lbl_paciente3 = NewLabel("Nome Paciente", 120);

            // grp3.Controls.Add(lbl_consulta3);
            // grp3.Controls.Add(lbl_medico3);
            // grp3.Controls.Add(lbl_paciente3);
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