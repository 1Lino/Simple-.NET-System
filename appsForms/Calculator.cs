
using System.Reflection.Metadata.Ecma335;
using Sistema_De_Aplicativos_Simples__.NET.appsForms;

namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{

    public partial class Calculator : Form
    {
        public static Calculator Instance { get; private set; } // Classe Components precisa acessar este Form.

        public Calculator()
        {
            Instance = this; // define Instance como este Form atual.
            InitializeForm();
            Components.InitializeAppComponents();
            AppState.InitializeAppState();
            AppEvents.InitializeAppEvents();

            // tests:
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
    }
}

// Varáveis de estado, modificadas pela classe Calculations, e lidas pela classe GUIUpdate.
public class AppState
{
    public static List<double> operands { get; set; }
    public static List<string> expression { get; set; }
    public static string operatorr { get; set; }
    public static double result { get; set; }

    public static bool canConcatOperation { get; set; }

    public static void InitializeAppState()
    {
        operands = [];
        expression = [];
        operatorr = "";
        result = 0;
        // controla se a operação pode ou não ser concatenada, no caso, isso impede que o usuário concatene o mesmo número
        // várias vezes para a operação ao clicar repetidamente o mesmo botão do operador (+ - x / etc). Isto também impede
        // de vários operadores seguidos serem concatenados pro visor, o que gera erro na operação (retorna NaN).
        canConcatOperation = true;
    }
}

public class Components
{
    private static TableLayoutPanel keyboardLayout;
    public static Panel calculatorVisorPanel;
    public static Label calculatorVisor;
    public static Label visorTop;
    private static List<string> keyListTxt;
    public static IEnumerable<Button> btnList;

    public static void InitializeAppComponents()
    {
        InitializeAppLayout();
        InitializeVisorPanelUI();

        keyListTxt = InitializeKeyTexts();
        calculatorVisor = InitializeVisor(0);
        visorTop = InitializeVisor(calculatorVisor.Height);

        calculatorVisorPanel.Controls.Add(calculatorVisor);
        calculatorVisorPanel.Controls.Add(visorTop);
        Calculator.Instance.Controls.Add(calculatorVisorPanel);
        Calculator.Instance.Controls.Add(keyboardLayout);
        AppendCalculatorKeysToAppLayout(keyboardLayout, keyListTxt);

        btnList = GetCalculatorKeys(keyboardLayout);
        // Events.ApplyEventsToKeys(btnList);
    }

    private static List<string> InitializeKeyTexts()
    {
        return new List<string> {
                "%", "CE", "C", "DEL",
                "1/x", "x²", "sqrt", "/",
                "7", "8", "9", "x",
                "4", "5", "6", "-",
                "1", "2", "3", "+",
                "+/-", "0", ",", "="};
    }

    private static Label InitializeVisor(int verticalOffset)
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
            Top = calculatorVisorPanel.Height / 2 - verticalOffset
        };

        return visor;
    }

    private static void InitializeAppLayout()
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

        if (Calculator.Instance != null)
        {
            keyboardLayout.Left = (Calculator.Instance.ClientSize.Width - keyboardLayout.Width) / 2;
            keyboardLayout.Top = Calculator.Instance.ClientSize.Height - keyboardLayout.Height - margin;
        }

        for (int i = 0; i < keyboardLayout.ColumnCount; i++)
        {
            keyboardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        }
        for (int i = 0; i < keyboardLayout.RowCount; i++)
        {
            keyboardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        }
    }

    private static void InitializeVisorPanelUI()
    {
        var formWidth = Calculator.Instance.Width; // pra não confundir o Width do componente com o Width do form.
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
    // NOTA: pesquisar melhor sobre recursões e sobre yield.
    private static IEnumerable<Button> GetCalculatorKeys(Control parent)
    {
        // para cada componente do parente.
        foreach (Control c in parent.Controls)
        {
            // se tal componente for um botão, "guarda" o retorno deste componente na lista enumerável (yield faz isso).
            if (c is Button btn)
            {
                yield return btn;
            }

            // em seguida chama novamente a função GetCalculatorKeys e itera sobre cada 
            // elemento, de modo que a função efetivamente fará novamente o passo acima. Isto, na prática, faz com que todos os elementos botão do parente sejam capturados. Se a função fosse chamada uma vez apenas, iria parar no primeiro elemento botão que encontrasse, e já que precisamos de uma lista, então é necessário uso de recursão.

            foreach (var child in GetCalculatorKeys(c))
            {
                yield return child;
            }
        }
    }
}

public class GUIUpdate
{
    public static void OnDigit(Label calculatorVisor, Label visorTop, string text)
    {
        if (calculatorVisor.Text == "0")
        {
            calculatorVisor.Text = text;
        }
        else
        {
            calculatorVisor.Text += text;
        }

        if (visorTop.Text == "0")
        {
            visorTop.Text = text;
        }
        else
        {
            visorTop.Text += text;
        }
    }
    public static void AfterOperation(Label calculatorVisor, Label visorTop)
    {
        visorTop.Text = $"{AppState.result} {AppState.operatorr} ";
        calculatorVisor.Text = "0";
    }

    public static void OnOperation(Label calculatorVisor, Label visorTop)
    {
        if (visorTop.Text == "0")
        {
            visorTop.Text = $" {AppState.operatorr} ";
        }
        else
        {
            visorTop.Text += $" {AppState.operatorr} ";
        }
        calculatorVisor.Text = "0";
    }

    public static void OnDel(Label calculatorVisor, Label visorTop)
    {
        string currentNumber = calculatorVisor.Text;
        int charsToRemove = currentNumber.Length;

        if (currentNumber.Length > 1)
        {
            calculatorVisor.Text = calculatorVisor.Text.Remove(calculatorVisor.Text.Length - 1);
        }
        else
        {
            calculatorVisor.Text = "0";
        }

        // TODO: 0 no visorTop deve sumir aqui ou ser substituído pelo operando ao digitar.
        if (visorTop.Text.Length > 1)
        {
            int newLength = Math.Max(visorTop.Text.Length - charsToRemove, 0);
            visorTop.Text = visorTop.Text.Substring(0, newLength) + calculatorVisor.Text;
        }
    }

    public static void OnCE(Label calculatorVisor)
    {
        calculatorVisor.Text = "0";
    }

    public static void OnC(Label calculatorVisor, Label visorTop)
    {
        calculatorVisor.Text = "0";
        visorTop.Text = "0";
    }

}

public class AppEvents
{
    private static Action operation = () => { };
    public static void InitializeAppEvents()
    {
        ApplyEventsToKeys(Components.btnList);
    }

    private static void DispatchOperation(string operatorr)
    {
        if (!AppState.canConcatOperation) return;
        var operand = Components.calculatorVisor.Text;
        Calculation.AddOperand(operand, operatorr);
        Calculation.SetOperator(operatorr);

        AppState.canConcatOperation = false;
        GUIUpdate.OnOperation(Components.calculatorVisor, Components.visorTop);
    }

    private static void DispatchOperationResult(string operatorr)
    {
        var operand = Components.calculatorVisor.Text;
        Calculation.AddOperand(operand, operatorr);
        Calculation.Calculate();

        AppState.canConcatOperation = false;
        GUIUpdate.AfterOperation(Components.calculatorVisor, Components.visorTop);
    }

    private static void InputValidation(string text)
    {
        List<string> numbers = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
        List<string> operations = ["+", "-", "x", "/", "DEL", "C", "CE", "=", ","];

        if (Components.calculatorVisor.Text.Length == 20) return; // Pra pôr limite no número de caracteres no visor.

        if (numbers.Contains(text))
        {
            AppState.canConcatOperation = true;
            GUIUpdate.OnDigit(Components.calculatorVisor, Components.visorTop, text);
        }
        else if (operations.Contains(text))
        {
            var operatorr = ""; // não deve ser confundido com AppState.operatorr. Seu uso também não é intercambiável.

            switch (text)
            {
                case "+":
                    operatorr = "+";
                    DispatchOperation(operatorr);
                    break;

                case "-":
                    operatorr = "-";
                    DispatchOperation(operatorr);
                    break;
                case "x":
                    operatorr = "x";
                    DispatchOperation(operatorr);
                    break;
                case "/":
                    operatorr = "/";
                    DispatchOperation(operatorr);
                    break;
                case "DEL":
                    GUIUpdate.OnDel(Components.calculatorVisor, Components.visorTop);
                    break;

                case "CE":
                    GUIUpdate.OnCE(Components.calculatorVisor);
                    break;

                case "C":
                    Calculation.ResetOperands();
                    GUIUpdate.OnC(Components.calculatorVisor, Components.visorTop);
                    break;

                case "=":
                    var isSumNotPossibleYet = AppState.operands.Count < 1;
                    if (isSumNotPossibleYet) break;

                    DispatchOperationResult(operatorr);

                    break;
            }
        }
    }

    public static void ApplyEventsToKeys(IEnumerable<Button> btnList)
    {
        foreach (Button btn in btnList)
        {
            btn.Click += (_, _) => InputValidation(btn.Text);
        }
    }
}


public class Calculation
{
    public static void CalculationTests()
    {
        // Fazer todos os testes aqui antes de mandar conectar a lógica com a interface.
        var a = 100;
        var b = 2;

        // TESTES:
        Console.WriteLine($"Sum: {a} + {b} = {a + b}");
        Console.WriteLine($"Sub: {a} - {b} = {a - b}");
        Console.WriteLine($"Mult: {a} x {b} = {a * b}");
        Console.WriteLine($"Div: {a} / {b} = {a / b}");
        Console.WriteLine($"Sqrt of {a} = {Math.Sqrt(a)}");
        Console.WriteLine($"Pow of {b} by 2 = {Pow(b, 2)}");
    }

    public static void SetOperator(string operation)
    {
        AppState.operatorr = operation;
    }

    public static void AddOperand(string value, string operatorr)
    {
        AppState.operands.Add(int.Parse(value));
        AppState.expression.Add(operatorr);
    }

    // TODO: talvez seja possível refatorar este método, pois há algumas repetições.
    public static void Calculate()
    {
        Console.WriteLine($"Operands: {string.Join(", ", AppState.operands)}");
        Console.WriteLine($"Operators: {string.Join(" ", AppState.expression)}");

        // Prioridade: resolver operaçõed de * e /
        for (int i = 0; i < AppState.expression.Count;)
        {
            switch (AppState.expression[i])
            {
                case "x":
                    AppState.operands[i] = AppState.operands[i] * AppState.operands[i + 1];
                    AppState.operands.RemoveAt(i + 1);
                    AppState.expression.RemoveAt(i);
                    break;
                case "/":
                    AppState.operands[i] = AppState.operands[i] / AppState.operands[i + 1];
                    AppState.operands.RemoveAt(i + 1);
                    AppState.expression.RemoveAt(i);
                    break;
                default:
                    i++; // só incrementa o iterador caso os casos acima não ocorram.
                    break;
            }
        }

        // depois resolve operações de + e -
        for (int i = 0; i < AppState.expression.Count;)
        {
            switch (AppState.expression[i])
            {
                case "+":
                    AppState.operands[i] = AppState.operands[i] + AppState.operands[i + 1];
                    AppState.operands.RemoveAt(i + 1);
                    AppState.expression.RemoveAt(i);
                    break;
                case "-":
                    AppState.operands[i] = AppState.operands[i] - AppState.operands[i + 1];
                    AppState.operands.RemoveAt(i + 1);
                    AppState.expression.RemoveAt(i);
                    break;
                default:
                    i++;
                    break;
            }
        }

        AppState.result = AppState.operands[0];
        UpdateOperation();

        Console.WriteLine($"Operands: {string.Join(", ", AppState.operands)}");
        Console.WriteLine($"Operators: {string.Join(" ", AppState.expression)}");
    }

    // TODO: deve ser ResetOperation
    public static void ResetOperands()
    {
        AppState.operands = [];
        AppState.expression = [];
    }

    // Atualiza a operação com os valores do resultado:
    private static void UpdateOperation()
    {
        AppState.operands = [];
        AppState.expression = [];
        AppState.operands.Add(AppState.result);
        AppState.expression.Add(AppState.operatorr);
    }

    public static double Pow(double a, double b)
    {
        return Math.Pow(a, b);
    }
}