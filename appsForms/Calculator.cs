namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class Calculator : Form
    {
        private TableLayoutPanel keyboardLayout;
        private Panel calculatorVisorPanel;
        private Label calculatorVisor;
        private Label visorTop;
        private List<string> keyListTxt;
        private IEnumerable<Button> btnList;

        // Estas duas variáveis abaixo estão num escopo inadequado, temporariamente. Devem ficar fora de eventos, muito embora sejam usadas somente nos eventos.
        private List<double> operands = new List<double>();
        private string operatorr = "";

        public Calculator()
        {
            InitializeForm();
            InitializeFormComponents();
            InitializeEvents();
            Calculation.CalculationTests();
        }

        private void InitializeForm()
        {
            Text = "Standard Calculator";
            Width = 450;
            Height = 550;
            BackColor = Color.FromArgb(29, 49, 49);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void InitializeFormComponents()
        {
            InitializeAppLayout();
            InitializeVisorPanelUI();

            keyListTxt = new List<string> {
                "%", "CE", "C", "DEL",
                "1/x", "x²", "sqrt", "/",
                "7", "8", "9", "x",
                "4", "5", "6", "-",
                "1", "2", "3", "+",
                "+/-", "0", ",", "="};

            calculatorVisor = InitializeVisor();
            visorTop = InitializeVisor();
            calculatorVisor.Top = calculatorVisorPanel.Height / 2;
            visorTop.Top = calculatorVisorPanel.Height / 2 - calculatorVisor.Height;

            calculatorVisorPanel.Controls.Add(calculatorVisor);
            calculatorVisorPanel.Controls.Add(visorTop);
            Controls.Add(calculatorVisorPanel);
            Controls.Add(keyboardLayout);
            AppendCalculatorKeysToAppLayout(keyboardLayout, keyListTxt);

            btnList = GetCalculatorKeys(keyboardLayout);
        }

        private void InitializeEvents()
        {
            ApplyEventsToKeys(btnList);
        }

        private Label InitializeVisor()
        {
            var visorWidth = 300;
            var visorCentralOffset = visorWidth / 2;

            Label visor = new Label
            {
                BackColor = Color.FromArgb(29, 49, 49),
                ForeColor = Color.White,
                Width = visorWidth,
                Height = 50,
                Text = "0",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Left = calculatorVisorPanel.Width / 2 - visorCentralOffset,
            };

            return visor;
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
                BackColor = Color.FromArgb(62, 85, 85)
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
            var formWidth = Width; // pra não confundir o Width do componente com o Width do form.
            calculatorVisorPanel = new Panel
            {
                Width = formWidth,
                Height = 180,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(62, 85, 85)
            };
        }

        private static void AppendCalculatorKeysToAppLayout(TableLayoutPanel layout, List<string> keyListTxt)
        {
            int counter = 0;

            for (int row = 0; row < layout.RowCount; row++)
            {
                for (int col = 0; col < layout.ColumnCount; col++)
                {
                    AppendCalculatorKey(layout, counter, col, row, keyListTxt);
                    counter++;
                }
            }
        }

        private static void AppendCalculatorKey(TableLayoutPanel layout, int counter, int col, int row, List<string> keyListTxt)
        {
            var key = CreateCalculatorKey(counter, keyListTxt);
            layout.Controls.Add(key, col, row);
        }

        private static Button CreateCalculatorKey(int counter, List<string> keyListTxt)
        {
            var key = new Button
            {
                Dock = DockStyle.Fill,
                Text = keyListTxt[counter],
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White
            };
            return key;
        }

        // Uso de recursão para capturar todos os elementos Button de um dado Control:
        private IEnumerable<Button> GetCalculatorKeys(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button btn)
                {
                    yield return btn;
                }

                foreach (var child in GetCalculatorKeys(c))
                {
                    yield return child;
                }
            }
        }

        //TODO: a validação de input presente neste método deverá ocorrer em método separado. Outra coisa, UI e os dados para o cálculo das operações deverão ser separados, ou seja: puxa o valor de input, valida tais valores, realiza a operação e atualiza a UI conforme. Por exemplo, após digitar 40, e clicar em +, 40 deverá ir para uma variável de dado que deverá então ser validado e convertido em dado puro para uma função matemática trabalhar sobre... e assim por diante.
        private void ClickKeyEvent(string text)
        {
            // Estas variáveis, bem como os if/else e switches concernem à camada de validação:
            List<string> numbers = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
            List<string> operations = ["+", "-", "x", "/"];

            if (calculatorVisor.Text.Length == 20) return; // Pra pôr limite no número de caracteres no visor.

            if (numbers.Contains(text))
            {
                // concerns UI layer:
                if (calculatorVisor.Text == "0") calculatorVisor.Text = text;
                else calculatorVisor.Text += text;

                //Console.WriteLine($"{text} is a number!");
            }
            else if (operations.Contains(text))
            {
                switch (text)
                {
                    case "+":
                        // concerns operation layer:
                        operands.Add(int.Parse(calculatorVisor.Text));

                        // concerns UI layer:
                        operatorr = "+";
                        if (visorTop.Text == "0") visorTop.Text = calculatorVisor.Text + $" {operatorr} ";
                        else visorTop.Text += calculatorVisor.Text + $" {operatorr} ";
                        calculatorVisor.Text = "0";

                        break;

                    case "-":
                        //TODO...
                        break;
                    case "x":
                        //TODO...
                        break;
                    case "/":
                        //TODO...
                        break;
                }
            }
            else
            {
                switch (text)
                {
                    case "DEL":

                        // concerns UI layer:
                        if (calculatorVisor.Text.Length != 1)
                            calculatorVisor.Text = calculatorVisor.Text.Remove(calculatorVisor.Text.Length - 1);
                        else calculatorVisor.Text = "0";

                        break;

                    case "CE":
                        // concerns UI layer:
                        calculatorVisor.Text = "0";

                        break;

                    case "C":
                        // concerns UI layer:
                        calculatorVisor.Text = "0";
                        visorTop.Text = "0";
                        // concerns operation layer:
                        operands = [];

                        break;

                    case "=":
                        if (operands.Count < 1) break;

                        // concerns operation layer:
                        operands.Add(int.Parse(calculatorVisor.Text));
                        var result = Calculation.Sum(operands[0], operands[1]);

                        // testes:
                        // operands.ForEach(e => Console.WriteLine($"Operand: {e}"));
                        // Console.WriteLine($"{operands[0]} {operatorr} {operands[1]} = {result}");

                        // concerns operation layer:
                        operands = [];
                        operands.Add(result);

                        // concerns UI layer:
                        visorTop.Text = $"{result} {operatorr} ";
                        calculatorVisor.Text = $"{result}";

                        break;
                }
            }


        }

        private void ApplyEventsToKeys(IEnumerable<Button> btnList)
        {
            foreach (Button btn in btnList)
            {
                btn.Click += (_, _) => ClickKeyEvent(btn.Text);
            }
        }
    }
}


// TIP: um exercício interessante seria tentar implementar todas estas operações usando matemática pura, ao invés de simplesmente usar apoio de libs, mas isto está em um escopo diferente desse projeto. Mas é algo a ser pensado.
public class Calculation
{
    public static void CalculationTests()
    {
        // Fazer todos os testes aqui antes de mandar conectar a lógica com a interface.
        var a = 100;
        var b = 2;

        // TESTES:
        Console.WriteLine($"Sum: {a} + {b} = {Sum(a, b)}");
        Console.WriteLine($"Sub: {a} - {b} = {Sub(a, b)}");
        Console.WriteLine($"Mult: {a} x {b} = {Mult(a, b)}");
        Console.WriteLine($"Div: {a} / {b} = {Div(a, b)}");
        Console.WriteLine($"Sqrt of {a} = {Sqrt(a)}");
        Console.WriteLine($"Pow of {b} by 2 = {Pow(b, 2)}");
    }

    // TODO: Aperfeiçoar estas funções quando estiverem prontas.
    public static double Sum(double a, double b)
    {
        return a + b;
    }

    private static double Sub(double a, double b)
    {
        return a - b;
    }

    private static double Mult(double a, double b)
    {
        return a * b;
    }

    private static double Div(double a, double b)
    {
        return a / b;
    }

    private static double Sqrt(double a)
    {
        return Math.Sqrt(a);
    }

    private static double Pow(double a, double b)
    {
        return Math.Pow(a, b);
    }
}