namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class MathTable : Form
    {
        private TableLayoutPanel appLayout;
        private Panel mathTableControlPanel;
        private ComboBox comboBox;

        public MathTable()
        {
            InitializeForm();
            InitializeFormComponents();
            HandleEvents();
        }

        private void InitializeForm()
        {
            Text = "Math Table";
            Width = 450;
            Height = 550;
            BackColor = Color.Aquamarine;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void InitializeFormComponents()
        {
            InitializeAppLayout();
            InitializeControlPanelUI();
            InitializeComboBox();
            comboBox.Location = new Point((mathTableControlPanel.Width - comboBox.Width) / 2, (mathTableControlPanel.Height - comboBox.Height) / 2);
            mathTableControlPanel?.Controls.Add(comboBox);

            appLayout.Controls.Add(mathTableControlPanel, 0, 0);
            appLayout.SetColumnSpan(mathTableControlPanel, appLayout.ColumnCount);

            AppendMathTablesToAppLayout(InitializeMathTable, appLayout);

            Controls.Add(appLayout);
        }

        private void HandleEvents()
        {
            Resize += (_, _) => ResizeAndCenterAppLayout(appLayout);
            comboBox.SelectedIndexChanged += (_, _) => OnSelectedOption();
        }

        public void InitializeAppLayout()
        {
            appLayout = new TableLayoutPanel
            {
                Dock = DockStyle.None,
                ColumnCount = 3,
                RowCount = 4,
                AutoScroll = true,
                MinimumSize = new Size(400, 500),
            };
            appLayout.Left = (ClientSize.Width - appLayout.Width) / 2;
            appLayout.Top = (ClientSize.Height - appLayout.Height) / 2;

            for (int i = 0; i < appLayout.ColumnCount; i++)
            {
                appLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }
            for (int i = 0; i < appLayout.RowCount; i++)
            {
                appLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            }
        }

        private void InitializeControlPanelUI()
        {
            mathTableControlPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Aqua
            };
        }

        private void InitializeComboBox()
        {

            comboBox = new ComboBox
            {
                Dock = DockStyle.None,
                Size = new Size(100, 0),
                Anchor = AnchorStyles.None,
            };
            comboBox.Items.AddRange(["Adição", "Subtração", "Multiplicação", "Divisão"]);
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.SelectedIndex = 0;
            comboBox.MaxDropDownItems = 4;

            Panel? parent = comboBox.Parent as Panel;
            if (parent != null)
            {
                comboBox.Location = new Point((parent.Width - comboBox.Width) / 2, (parent.Height - comboBox.Height) / 2);
            }
        }

        private DataGridView InitializeMathTable()
        {
            var table = new DataGridView
            {
                Dock = DockStyle.None,
                ColumnCount = 1,
                RowHeadersVisible = false,
                ColumnHeadersVisible = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false
            };
            table.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            table.AllowUserToAddRows = false;
            table.Anchor = AnchorStyles.None;

            return table;
        }

        private static void AppendMathTablesToAppLayout(Func<DataGridView> initMathTable, TableLayoutPanel layout)
        {
            int counter = 1;

            for (int row = 1; row < layout.RowCount; row++)
            {
                for (int col = 0; col < layout.ColumnCount; col++)
                {
                    AppendMathTable(initMathTable, layout, counter, col, row);
                    counter++;
                }
            }
        }

        private static void AppendMathTable(Func<DataGridView> initMathTable, TableLayoutPanel layout, int counter, int col, int row)
        {
            var mathTable = initMathTable();

            for (int i = 1; i <= 10; i++)
            {
                mathTable.Rows.Add($"{counter} + {i} = {counter + i}");
            }

            layout.Controls.Add(mathTable, col, row);
        }

        private void OnSelectedOption()
        {
            int selectedOption = comboBox.SelectedIndex;
            UpdateMathTable(selectedOption);
        }
        private void UpdateMathTable(int operationId)
        {
            int a = 1;
            int b;

            foreach (Control control in appLayout.Controls)
            {
                if (control is DataGridView mathTable)
                {
                    b = 1;
                    foreach (DataGridViewRow row in mathTable.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cell.Value = GetOperationResult(a, b, operationId);
                        }
                        b++;
                    }
                    a++;
                }
            }
        }

        private static string GetOperationResult(int a, int b, int operationId)
        {
            int c = 0;
            string mathExpression = "";

            switch (operationId)
            {
                case 0:
                    c = a + b;
                    mathExpression = $"{a} + {b} = {c}";
                    break;
                case 1:
                    c = a + b; // em subtração, o cálculo da adição permanece, muda-se apenas a apresentação dos dados.
                    mathExpression = $"{c} - {a} = {b}";
                    break;
                case 2:
                    c = a * b;
                    mathExpression = $"{a} x {b} = {c}";
                    break;
                case 3:
                    c = b * a; // em divisão, o cálculo da multiplicação permanece, muda-se apenas a apresentação dos dados.
                    mathExpression = $"{c} / {a} = {b}";
                    break;
            }
            return mathExpression;
        }

        private void ResizeAndCenterAppLayout(TableLayoutPanel layout)
        {
            ResizeLayout(layout);
            CenterLayout(layout);
        }

        private void ResizeLayout(TableLayoutPanel layout)
        {
            double scaleW = 0.5;
            double scaleH = 0.8;

            int newTableWidth = (int)(ClientSize.Width * scaleW);
            int newTableHeight = (int)(ClientSize.Height * scaleH);

            newTableWidth = Math.Min(newTableWidth, 800);
            newTableHeight = Math.Min(newTableHeight, 600);

            layout.Size = new Size(newTableWidth, newTableHeight);
        }

        private void CenterLayout(TableLayoutPanel layout)
        {
            layout.Left = (ClientSize.Width - layout.Width) / 2;
            layout.Top = (ClientSize.Height - layout.Height) / 2;
        }

    }
}