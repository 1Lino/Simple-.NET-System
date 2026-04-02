using System.CodeDom;

namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class Calculator : Form
    {
        private TableLayoutPanel keyboardLayout;
        private Panel calculatorVisorPanel;

        private List<string> keyList;

        public Calculator()
        {
            InitializeForm();
            InitializeFormComponents();
        }

        private void InitializeForm()
        {
            Text = "Standard Calculator";
            Width = 450;
            Height = 550;
            BackColor = Color.Aquamarine;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }


        private void InitializeFormComponents()
        {
            InitializeAppLayout();
            InitializeVisorPanelUI();

            keyList = new List<string> {
                "%", "CE", "C", "DEL",
                "1/x", "x²", "sqrt", "/",
                "7", "8", "9", "X",
                "4", "5", "6", "-",
                "1", "2", "3", "+",
                "+/-", "0", ",", "="};

            Controls.Add(calculatorVisorPanel);
            Controls.Add(keyboardLayout);
            AppendCalculatorKeysToAppLayout(InitializeCalculatorKey, keyboardLayout, keyList);
        }

        private void InitializeAppLayout()
        {
            keyboardLayout = new TableLayoutPanel
            {
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset,
                Dock = DockStyle.None,
                ColumnCount = 4,
                RowCount = 6,
                AutoScroll = true,
                MinimumSize = new Size(400, 300),
            };
            int margin = 20;
            keyboardLayout.Left = (ClientSize.Width - keyboardLayout.Width) / 2;
            keyboardLayout.Top = ClientSize.Height - keyboardLayout.Height - margin;

            for (int i = 0; i < keyboardLayout.ColumnCount; i++)
            {
                keyboardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }
            for (int i = 0; i < keyboardLayout.RowCount; i++)
            {
                keyboardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            }
        }

        private void InitializeVisorPanelUI()
        {
            calculatorVisorPanel = new Panel
            {
                Width = 500,
                Height = 180,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Aqua
            };
        }

        private static void AppendCalculatorKeysToAppLayout(Func<Button> initCalculatorKey, TableLayoutPanel layout, List<string> keyList)
        {
            int counter = 0;

            for (int row = 0; row < layout.RowCount; row++)
            {
                for (int col = 0; col < layout.ColumnCount; col++)
                {
                    AppendCalculatorKey(initCalculatorKey, layout, counter, col, row, keyList);
                    counter++;
                }
            }
        }

        private static void AppendCalculatorKey(Func<Button> initCalculatorKey, TableLayoutPanel layout, int counter, int col, int row, List<string> keyList)
        {
            //var key = initCalculatorKey();
            var key = new Button
            {
                Dock = DockStyle.Fill,
                Text = keyList[counter]
            };
            layout.Controls.Add(key, col, row);
        }

        private Button InitializeCalculatorKey()
        {
            var key = new Button
            {
                Dock = DockStyle.Fill
            };

            return key;
        }
    }
}