// TIP: o Designer não será necessário aqui, pois como ele apenas inicializa configurações do formulário, não seria conveniente um designer pra cada formulário, então as configurações de cada formulário serão locais mesmo. Sendo assim "InitializeComponent" será local mesmo (normalmente fica no designer).
using Microsoft.VisualBasic;

namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class MathTable : Form
    {
        private TableLayoutPanel tableLayout;
        private Panel mathTableControlPanel;
        private ComboBox mathTableOperationSelection;
        private DataGridView mathTableOperations;


        // Método principal da classe (Main ou constructor):
        public MathTable()
        {
            InitializeForm();
            InitializeFormComponents();
            InitializeEvents();
        }

        // Inicializadores de Form, de componentes e eventos:
        private void InitializeForm()
        {
            Text = "Math Table";
            Width = 450;
            Height = 550;
            FormBorderStyle = FormBorderStyle.FixedSingle; // impede que o form seja redimensionado pelas bordas.
        }

        private void InitializeFormComponents()
        {
            tableLayout = InitializeAppLayout();

            mathTableControlPanel = InitializeControlPanel();
            tableLayout.Controls.Add(mathTableControlPanel, 0, 0);
            tableLayout.SetColumnSpan(mathTableControlPanel, tableLayout.ColumnCount);

            InitializeMathTables();

            // adiciona a tabela no form.
            Controls.Add(tableLayout);
        }

        private void InitializeMathTables()
        {
            int counter = 1;

            for (int row = 1; row < tableLayout.RowCount; row++)
            {
                for (int col = 0; col < tableLayout.ColumnCount; col++)
                {
                    mathTableOperations = new DataGridView
                    {
                        Dock = DockStyle.None,
                        Name = $"dgv_{row}_{col}",
                        Tag = counter,
                        ColumnCount = 1,
                        RowHeadersVisible = false,
                        ColumnHeadersVisible = false,
                    };
                    mathTableControlPanel.Anchor = AnchorStyles.None;

                    for (int i = 1; i <= 10; i++)
                    {
                        // TODO: nessa parte da operação, um método deve ser adicionado pra fazer essa operação de acordo
                        // com o parâmetro de operação (+ - * /).
                        mathTableOperations.Rows.Add($"{counter} + {i} = {counter + i}");
                    }

                    counter++;

                    tableLayout.Controls.Add(mathTableOperations, col, row);
                }
            }
        }

        private Panel InitializeControlPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Aqua
            };
            return panel;
        }

        // "inicializar" eventos não é uma expressão muito precisa, mas está assim apenas para manter coerência de nomenclatura. Já que o app é simples, isto não é problema.
        private void InitializeEvents()
        {
            Resize += (_, _) => ResizeAndCenterTable(tableLayout); // quando for dado resize no Form, a tabela será centralizada.
        }

        // COMPONENTES
        public TableLayoutPanel InitializeAppLayout()
        {
            var table = new TableLayoutPanel
            {
                Dock = DockStyle.None,
                ColumnCount = 3,
                RowCount = 4,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                AutoScroll = true,
                MinimumSize = new Size(400, 500),
            };
            table.Left = (ClientSize.Width - table.Width) / 2; // não pode ser configurado diretamente na inicialização acima porque necessita que a definição esteja concluída para então puxar table.width;
            table.Top = (ClientSize.Height - table.Height) / 2;

            for (int i = 0; i < table.ColumnCount; i++)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }
            for (int i = 0; i < table.RowCount; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            }

            return table;
        }

        private void ResizeAndCenterTable(TableLayoutPanel table)
        {
            ResizeTable(table);
            CenterTable(table);
        }

        private void ResizeTable(TableLayoutPanel table)
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

            table.Size = new Size(newTableWidth, newTableHeight);
        }

        private void CenterTable(TableLayoutPanel table)
        {
            // Centraliza a tabela:
            table.Left = (ClientSize.Width - table.Width) / 2;
            table.Top = (ClientSize.Height - table.Height) / 2;
        }


    }
}