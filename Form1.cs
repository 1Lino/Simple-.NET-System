using Sistema_De_Aplicativos_Simples__.NET.appsForms;

namespace Sistema_De_Aplicativos_Simples__.NET
{
    public partial class Form1 : Form
    {
        // Criação dos componentes do Form (esses botões acessarão outros forms):
        private Button toDo;
        private Button mediaPlayer;
        private Button mathTable;
        private Button calculator;

        private Button unitConverter;

        // Inicialização dos componentes no form:
        public Form1()
        {
            InitializeComponent();
            InitializeMainWindow();
        }


        // Configuração dos componentes do Form:
        private void InitializeMainWindow()
        {
            this.Text = "Simple System";
            this.Width = 600;
            this.Height = 400;

            // Grid para pôr os componentes:
            var grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 3,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single, // temporário: só para ver as bordas das células.
                AutoScroll = true
            };

            // Configura o tamanho de cada coluna e de cada linha de forma percentual e relativa ao container (Form). O percentual é em float (F), pra maior precisão.
            for (int i = 0; i < grid.ColumnCount; i++)
            {
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }

            for (int i = 0; i < grid.RowCount; i++)
            {
                grid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            }

            // Configuração individual dos componentes que farão parte do grid:
            toDo = new Button()
            {
                Text = "To Do",
                Width = 100,
                Height = 100
            };

            mediaPlayer = new Button()
            {
                Text = "Media",
                Width = 100,
                Height = 100
            };

            mathTable = new Button()
            {
                Text = "Math Table",
                Width = 100,
                Height = 100
            };

            calculator = new Button()
            {
                Text = "Calculator",
                Width = 100,
                Height = 100
            };

            unitConverter = new Button()
            {
                Text = "Unit Converter",
                Width = 100,
                Height = 100
            };

            // Reseta a âncora de cada componente, de modo que sejam renderizados no centro de suas respectivas células no grid.
            toDo.Anchor = AnchorStyles.None;
            mediaPlayer.Anchor = AnchorStyles.None;
            mathTable.Anchor = AnchorStyles.None;
            calculator.Anchor = AnchorStyles.None;
            unitConverter.Anchor = AnchorStyles.None;

            // Por último, adiciona os componentes ao grid:
            grid.Controls.Add(toDo);
            grid.Controls.Add(mediaPlayer);
            grid.Controls.Add(mathTable);
            grid.Controls.Add(calculator);
            grid.Controls.Add(unitConverter);

            this.Controls.Add(grid); // adiciona o grid ao Form.

            //TIP: (_, _) são argumentos nulos, mas normalmente seriam "e" e "s", onde "e" é evento (click, no caso) e "s" é sender (objeto que causa o evento, no caso o botão), que fazem parte da exigência do event handler da linguagem. Mesmo não usando esses argumentos, eles têm de existir aqui só pra satisfazer essa exigência da linguagem.
            calculator.Click += (_, _) => OpenForm<Calculator>();
            mathTable.Click += (_, _) => OpenForm<MathTable>();
            mediaPlayer.Click += (_, _) => OpenForm<MediaPlayerForm>();
            toDo.Click += (_, _) => OpenForm<ToDo>();
            unitConverter.Click += (_, _) => OpenForm<UnitConverter>();

        }

        // Método genérico restritivo, onde T (Type) deve ser qualquer objeto que seja Form, e deve ser permitido uso de construtor, new():
        private void OpenForm<T>() where T : Form, new()
        {
            T form = new T(); // constrói um novo Form com as características passadas em T.
            form.Show(); // mostra o objeto.
        }

        // Isto aqui é a estrutura comum exigida pelo event handler:
        // private void OpenFormEvent(object sender, EventArgs e)
        // {
        //     MathTable mathTable = new MathTable();
        //     mathTable.Show();
        // }
    }
}


