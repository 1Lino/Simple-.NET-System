// TIP: o Designer não será necessário aqui, pois como ele apenas inicializa configurações do formulário, não seria conveniente um designer pra cada formulário, então as configurações de cada formulário serão locais mesmo. Sendo assim "InitializeComponent" será local mesmo (normalmente fica no designer).

namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class MathTable : Form
    {
        private TableLayoutPanel appLayout;
        private Panel mathTableControlPanel;
        private ComboBox comboBox;

        // Método principal da classe (Main ou constructor):
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
            FormBorderStyle = FormBorderStyle.FixedSingle; // impede que o form seja redimensionado pelas bordas.
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

            // adiciona a tabela ao form.
            Controls.Add(appLayout);
        }

        private void HandleEvents()
        {
            Resize += (_, _) => ResizeAndCenterAppLayout(appLayout); // quando for dado resize no Form, a tabela será centralizada.
            comboBox.SelectedIndexChanged += (_, _) => OnSelectedOption();
        }

        public void InitializeAppLayout()
        {
            appLayout = new TableLayoutPanel
            {
                Dock = DockStyle.None,
                ColumnCount = 3,
                RowCount = 4,
                //CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                AutoScroll = true,
                MinimumSize = new Size(400, 500),
            };
            appLayout.Left = (ClientSize.Width - appLayout.Width) / 2; // não pode ser configurado diretamente na inicialização acima porque necessita que a definição esteja concluída para então puxar table.width;
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
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // evita edição do texto pelo usuário.
            comboBox.SelectedIndex = 0;
            comboBox.MaxDropDownItems = 4;

            // Todo este bloco "complicado" abaixo é só pra garantir que o parente deste componente é de fato um painel, antes de configurar a posição do comboBox ao centro dele.
            Panel? parent = comboBox.Parent as Panel; // ? aqui é pra garantir que o código continue mesmo se "parent" for null.
            if (parent != null)  // se "parent" não for null, então prossegue definir a posição do combox.
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

        // DICA: pesquisar a fundo sobre Action<T> e Func<T>. São formas bastante interessantes de se fazer referências a métodos externos.
        private static void AppendMathTablesToAppLayout(Func<DataGridView> initMathTable, TableLayoutPanel layout)
        {
            // atualização do counter precisa persistir através do loop, por isso deve ser declarado aqui fora.
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
                // TODO: nessa parte da operação, um método deve ser adicionado pra fazer essa operação de acordo
                // com o parâmetro de operação (+ - * /).
                mathTable.Rows.Add($"{counter} + {i} = {counter + i}");
            }

            layout.Controls.Add(mathTable, col, row);
        }

        // Métodos relacionados a eventos:
        private void OnSelectedOption()
        {
            //string selectedOption = comboBox.SelectedItem?.ToString() ?? "";
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
            double scaleW = 0.5; // escala para a largura. 50%
            double scaleH = 0.8; // escala para a altura. 80%

            // converter em int é necessário para essa operação, do contrário, erro, pois não se pode atribuir
            // resultado double ou float para uma variável tipo int.
            int newTableWidth = (int)(ClientSize.Width * scaleW);
            int newTableHeight = (int)(ClientSize.Height * scaleH);

            // Math.Min retorna o menor de dois valores. Isto efetivamente impede que newTableWidth fique maior que 800.
            newTableWidth = Math.Min(newTableWidth, 800);
            newTableHeight = Math.Min(newTableHeight, 600);

            layout.Size = new Size(newTableWidth, newTableHeight);
        }

        private void CenterLayout(TableLayoutPanel layout)
        {
            // Centraliza a tabela:
            layout.Left = (ClientSize.Width - layout.Width) / 2;
            layout.Top = (ClientSize.Height - layout.Height) / 2;
        }

    }
}