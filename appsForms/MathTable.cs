// TIP: o Designer não será necessário aqui, pois como ele apenas inicializa configurações do formulário, não seria conveniente um designer pra cada formulário, então as configurações de cada formulário serão locais mesmo. Sendo assim "InitializeComponent" será local mesmo (normalmente fica no designer).
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class MathTable : Form
    {
        private TableLayoutPanel appLayout;
        private Panel mathTableControlPanel;
        private ComboBox mathTableOperationSelection;
        private DataGridView mathTableOperations;
        private DataGridView mathTable;


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
            FormBorderStyle = FormBorderStyle.FixedSingle; // impede que o form seja redimensionado pelas bordas.
        }

        private void InitializeFormComponents()
        {
            InitializeAppLayout();
            InitializeControlPanelUI();

            appLayout.Controls.Add(mathTableControlPanel, 0, 0);
            appLayout.SetColumnSpan(mathTableControlPanel, appLayout.ColumnCount);

            AppendMathTablesToAppLayout(InitializeMathTable, mathTable, appLayout);

            // adiciona a tabela ao form.
            Controls.Add(appLayout);
        }

        private void HandleEvents()
        {
            Resize += (_, _) => ResizeAndCenterAppLayout(appLayout); // quando for dado resize no Form, a tabela será centralizada.
        }

        // COMPONENTES

        private DataGridView InitializeMathTable()
        {
            var table = new DataGridView
            {
                Dock = DockStyle.None,
                ColumnCount = 1,
                RowHeadersVisible = false,
                ColumnHeadersVisible = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            table.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            table.AllowUserToAddRows = false;
            table.Anchor = AnchorStyles.None;

            return table;
        }

        // DICA: pesquisar mais a fundo sobre Action<T> e Func<T>. São formas bastante interessantes de se fazer referências a métodos externos.
        private void AppendMathTablesToAppLayout(Func<DataGridView> initMathTable, DataGridView mathTable, TableLayoutPanel layout)
        {
            // atualização do counter precisa persistir através do loop, por isso deve ser declarado aqui fora.
            int counter = 1;

            for (int row = 1; row < layout.RowCount; row++)
            {
                for (int col = 0; col < layout.ColumnCount; col++)
                {
                    mathTable = initMathTable();

                    for (int i = 1; i <= 10; i++)
                    {
                        // TODO: nessa parte da operação, um método deve ser adicionado pra fazer essa operação de acordo
                        // com o parâmetro de operação (+ - * /).
                        mathTable.Rows.Add($"{counter} + {i} = {counter + i}");
                    }

                    counter++;

                    layout.Controls.Add(mathTable, col, row);
                }
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

        public void InitializeAppLayout()
        {
            appLayout = new TableLayoutPanel
            {
                Dock = DockStyle.None,
                ColumnCount = 3,
                RowCount = 4,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
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