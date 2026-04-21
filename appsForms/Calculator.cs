
using Sistema_De_Aplicativos_Simples__.NET.appsForms;

// TODO: 
// 1. Operações básicas [+ - x /] (ok)
// 2. Implementar uso de vírgula (ok)
// 3. Operações intermediárias [sqrt, pow, %, 1/x, +-] (x) 
// 4. Testar limites dos cálculos, procurar por erros ()
// 5. Implementar funcionalidade de histórico de operações ()
// 6. Rever nomenclaturas, principalmente onde houver comentários explicando código ()
// 7. Implementar keyboard, de modo que seja permitido digitar números pelo teclado numérico ()

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
            // Calculation.CalculationTests();
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
    public static bool canUseDot { get; set; }

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
        canUseDot = true;
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
                "1/x", "x²", "²√x", "/",
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
    public static void OnDigit(Label calculatorVisor, Label visorTop, string operand)
    {
        if (calculatorVisor.Text.Length >= 15) return; // limita quantidade de dígitos para a quantidade normal suportada por um double ~15.

        if (calculatorVisor.Text == "0")
        {
            calculatorVisor.Text = operand;
        }
        else
        {
            calculatorVisor.Text += operand;
        }

        if (visorTop.Text == "0")
        {
            visorTop.Text = operand;
        }
        else
        {
            visorTop.Text += operand;
        }
    }
    public static void AfterOperation(Label calculatorVisor, Label visorTop, double result, string operatorr)
    {
        visorTop.Text = $"{result} {operatorr} ";
        calculatorVisor.Text = "0";
    }

    public static void OnOperation(Label calculatorVisor, Label visorTop, string operatorr)
    {
        if (visorTop.Text == "0")
        {
            visorTop.Text = $" {operatorr} ";
        }
        else
        {
            visorTop.Text += $" {operatorr} ";
        }
        calculatorVisor.Text = "0";
    }

    public static void OnDel(Label calculatorVisor, Label visorTop)
    {
        if (calculatorVisor.Text == "0") return; // impede que visorTop seja deletado para além do que é deletado em calculatorVisor.

        string currentNumber = calculatorVisor.Text;

        if (currentNumber.Length > 1)
        {
            calculatorVisor.Text = calculatorVisor.Text.Remove(calculatorVisor.Text.Length - 1);
        }
        else
        {
            calculatorVisor.Text = "0";
        }

        if (visorTop.Text.Length == 1) visorTop.Text = "0";
        else visorTop.Text = visorTop.Text.Remove(visorTop.Text.Length - 1);
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

    public static void OnDot(Label calculatorVisor, Label visorTop, bool canUseDot)
    {
        if (canUseDot)
        {
            if (visorTop.Text[visorTop.Text.Length - 1] == ' ') visorTop.Text += "0,";
            else visorTop.Text += ",";
            calculatorVisor.Text += ",";
        }
    }

    public static void OnSqrt(Label calculatorVisor, Label visorTop, double result, string operand)
    {
        visorTop.Text = $"√({operand})";
        calculatorVisor.Text = $"{result}";
    }

    public static void OnOneBy(Label calculatorVisor, Label visorTop, double result, string operand)
    {
        visorTop.Text = $"1 / {operand}";
        calculatorVisor.Text = $"{result}";
    }

    public static void OnOperatorChange(Label visorTop, string operatorr)
    {
        string CopyText = visorTop.Text;
        visorTop.Text = CopyText.Substring(0, CopyText.Length - 2) + $"{operatorr} ";
    }

    public static void OnReverseOperator(Label calculatorVisor, Label visorTop)
    {
        calculatorVisor.Text = $"{-Double.Parse(Components.calculatorVisor.Text)}";
        string number = calculatorVisor.Text;
        // pega o índice do último espaço da expressão, que deve ser logo após o sinal do operador.
        int lastSpace = visorTop.Text.LastIndexOf(' ');

        if (lastSpace == -1)
        {
            visorTop.Text = number;
        }
        else
        {
            // corta a expressão até o último espaço.
            string expressionBeforeLastSpace = visorTop.Text.Substring(0, lastSpace);
            visorTop.Text = $"{expressionBeforeLastSpace} {number}"; // junta o corte ao número.
        }
    }

    public static void OnPercentage(Label calculatorVisor, Label visorTop, List<double> operands, string operatorr)
    {
        if (operands.Count == 0)
        {
            visorTop.Text = "0";
            calculatorVisor.Text = "0";
        }
        else
        {
            visorTop.Text = $"{operands[0]} {operatorr} {operands[1]}";
            calculatorVisor.Text = $"{operands[1]}";
        }
    }

}

public class AppEvents
{
    public static void InitializeAppEvents()
    {
        ApplyEventsToKeys(Components.btnList); // "key" se refere aos botões da calculadora.
        Calculator.Instance.KeyPreview = true; // Permite que eventos de teclado sejam primeiro capturados pelo Form, ao invés de pelos componentes em foco.
        Calculator.Instance.KeyDown += CalculatorKeyDownEvent;
    }

    public static void ChangeOperator(string operatorr)
    {
        GUIUpdate.OnOperatorChange(Components.visorTop, operatorr);
        Calculation.SetOperator(operatorr);
        AppState.expression[AppState.expression.Count - 1] = operatorr;
        Console.WriteLine($"Changed operation to {AppState.operatorr}");
    }

    private static void DispatchOperation(string operatorr)
    {
        bool onOperatorDuplicationRisk = !AppState.canConcatOperation; // verifica se há risco de duplicar operador.
        bool isOperationTooBig = AppState.operands.Count >= 4; // basicamente cria um limite de operandos para a operação.

        // se houver risco de duplicar operador, por exemplo, se o usuário já clicou num operador, ou então se a operação for
        // já grande demais (maior igual a 4 operandos), então apenas muda o operador e sai da função.
        if (isOperationTooBig) return;
        if (onOperatorDuplicationRisk)
        {
            ChangeOperator(operatorr); // apenas muda o operador, tanto na operação como no visor.
            return;
        }

        string operand = Components.calculatorVisor.Text;
        Calculation.AddOperand(operand, operatorr);
        Calculation.SetOperator(operatorr);

        AppState.canConcatOperation = false;
        AppState.canUseDot = true;
        GUIUpdate.OnOperation(Components.calculatorVisor, Components.visorTop, AppState.operatorr);
    }

    private static void DispatchOperationResult()
    {
        string operand = Components.calculatorVisor.Text;
        Calculation.AddOperand(operand, "");

        if (AppState.operatorr == "^")
        {
            Calculation.Pow(AppState.operands[0], AppState.operands[1]);
        }
        else
        {
            Calculation.Calculate();
        }


        AppState.canConcatOperation = false;
        AppState.canUseDot = true;
        GUIUpdate.AfterOperation(Components.calculatorVisor, Components.visorTop, AppState.result, AppState.operatorr);
    }

    private static void InputValidation(string text)
    {
        List<string> numbers = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
        List<string> operations = ["+", "-", "x", "/", "DEL", "C", "CE", "=", ",", "²√x", "x²", "%", "1/x", "+/-"];

        if (Components.calculatorVisor.Text.Length == 20) return; // Pra pôr limite no número de caracteres no visor.

        if (numbers.Contains(text))
        {
            AppState.canConcatOperation = true;
            GUIUpdate.OnDigit(Components.calculatorVisor, Components.visorTop, text);
        }
        else if (operations.Contains(text))
        {
            bool operationNotPossible = AppState.operands.Count < 1;
            string operatorr; // não deve ser confundido com AppState.operatorr. Seu uso também não é intercambiável.

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
                case "x²":
                    operatorr = "^";
                    DispatchOperation(operatorr);
                    break;
                case "%":
                    double lastOperand = Double.Parse(Components.calculatorVisor.Text);
                    Calculation.Percent(lastOperand);
                    GUIUpdate.OnPercentage(Components.calculatorVisor, Components.visorTop, AppState.operands, AppState.operatorr);
                    break;
                case "²√x":
                    string operand = Components.calculatorVisor.Text;
                    Calculation.AddOperand(operand, "");
                    Calculation.Sqrt();
                    Calculation.ResetOperands();
                    GUIUpdate.OnSqrt(Components.calculatorVisor, Components.visorTop, AppState.result, operand);
                    break;
                case "1/x":
                    operand = Components.calculatorVisor.Text;
                    Calculation.AddOperand(operand, "");
                    Calculation.OneBy();
                    Calculation.ResetOperands();
                    GUIUpdate.OnOneBy(Components.calculatorVisor, Components.visorTop, AppState.result, operand);
                    break;
                case "+/-":
                    GUIUpdate.OnReverseOperator(Components.calculatorVisor, Components.visorTop);
                    break;
                case ",":
                    GUIUpdate.OnDot(Components.calculatorVisor, Components.visorTop, AppState.canUseDot);
                    AppState.canUseDot = false;
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
                    if (operationNotPossible) break;
                    DispatchOperationResult();
                    break;
            }
        }

    }

    public static void CalculatorKeyDownEvent(object sender, KeyEventArgs e)
    {
        int numberPressed = -1;

        // Keypad Numérico
        if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
        {
            numberPressed = e.KeyCode - Keys.NumPad0;
        }
        // Números do teclado QWERTY
        else if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
        {
            numberPressed = e.KeyCode - Keys.D0;
        }

        if (numberPressed != -1)
        {
            Console.WriteLine($"Number pressed: {numberPressed}");

            AppState.canConcatOperation = true;
            GUIUpdate.OnDigit(Components.calculatorVisor, Components.visorTop, $"{numberPressed}");
        }

        // Demais operações:
        string operatorr;
        var operationNotPossible = AppState.operands.Count < 1;

        if (e.KeyCode == Keys.Oemcomma)
        {
            Console.WriteLine("You pressed the 'Comma' key!");

            GUIUpdate.OnDot(Components.calculatorVisor, Components.visorTop, AppState.canUseDot);
            AppState.canUseDot = false;
        }
        if (e.KeyCode == Keys.Add)
        {
            Console.WriteLine("You pressed the '+' key!");

            operatorr = "+";
            DispatchOperation(operatorr);
        }
        if (e.KeyCode == Keys.Subtract)
        {
            Console.WriteLine("You pressed the '-' key!");

            operatorr = "-";
            DispatchOperation(operatorr);
        }
        if (e.KeyCode == Keys.Multiply)
        {
            Console.WriteLine("You pressed the '*' key!");

            operatorr = "x";
            DispatchOperation(operatorr);
        }
        if (e.KeyCode == Keys.Divide)
        {
            Console.WriteLine("You pressed the '/' key!");

            operatorr = "/";
            DispatchOperation(operatorr);
        }
        if (e.KeyCode == Keys.Oemplus)
        {
            Console.WriteLine("You pressed the '=' key!");

            if (operationNotPossible) return;

            DispatchOperationResult();
        }
        if (e.KeyCode == Keys.Back)
        {
            Console.WriteLine("You pressed the 'Back' key!");

            GUIUpdate.OnDel(Components.calculatorVisor, Components.visorTop);
        }
        if (e.KeyCode == Keys.Delete)
        {
            Console.WriteLine("You pressed the 'Delete' key!");

            Calculation.ResetOperands();
            GUIUpdate.OnC(Components.calculatorVisor, Components.visorTop);
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
    public static void SetOperator(string operation)
    {
        AppState.operatorr = operation;
    }

    public static void AddOperand(string value, string operatorr)
    {
        AppState.operands.Add(double.Parse(value));
        AppState.expression.Add(operatorr);
    }

    public static void RemoveOperandAndOperatorAt(int index)
    {
        AppState.operands.RemoveAt(index + 1);
        AppState.expression.RemoveAt(index);
    }

    public static void Calculate()
    {
        // Prioridade: resolver operaçõed de * e /
        for (int i = 0; i < AppState.expression.Count;)
        {
            switch (AppState.expression[i])
            {
                case "x":
                    AppState.operands[i] = AppState.operands[i] * AppState.operands[i + 1];
                    RemoveOperandAndOperatorAt(i);
                    break;
                case "/":
                    AppState.operands[i] = AppState.operands[i] / AppState.operands[i + 1];
                    RemoveOperandAndOperatorAt(i);
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
                    RemoveOperandAndOperatorAt(i);
                    break;
                case "-":
                    AppState.operands[i] = AppState.operands[i] - AppState.operands[i + 1];
                    RemoveOperandAndOperatorAt(i);
                    break;
                default:
                    i++;
                    break;
            }
        }

        AppState.result = AppState.operands[0];
        UpdateOperation();
    }

    // Pow é uma operação que exige dois inputs, então deve seguir a lógica das quatro operações básicas.
    public static void Pow(double basee, double expo)
    {
        AppState.result = Math.Pow(basee, expo);
        UpdateOperation();
    }

    // já Sqrt é uma função que exige apenas um input, então sua lógica é um pouco diferente.
    // primeiramente pegamos o resultado de qualquer operação e usamos este resultado.
    public static void Sqrt()
    {
        if (AppState.operatorr == "^")
        {
            Pow(AppState.operands[0], AppState.operands[1]);
        }
        else
        {
            Calculate();
        }

        AppState.result = Math.Sqrt(AppState.operands[0]);
    }

    public static void OneBy()
    {
        AppState.result = 1 / AppState.operands[0];
    }

    public static void Percent(double lastOperand)
    {
        if (AppState.operands.Count >= 1)
        {
            if (AppState.operands.Count == 0) return; // se não há nada no registro, não faz nada.

            // Para evitar "index out of range" (pois ainda que o registro contenha um operando, o segundo, que é lastOperand, não entrará para o registro, somente percentageOfResult que irá entrar para o registro do próximo cálculo):
            if (AppState.operatorr == "x" || AppState.operatorr == "/")
                AppState.operands.Add(1); // porque qualquer número multiplicado ou dividido por um é igual a ele mesmo.
            else AppState.operands.Add(0); // porque qualquer número adicionado ou subtraído em zero é igual a ele mesmo.

            // realiza o cálculo e upa o resultado ao registro como novo operando, bem como a última operação:
            Calculate();

            // upa então o resultado do cálculo da porcentagem sobre o resultado da expressão anterior:
            var percentageOfResult = AppState.result / 100 * lastOperand;
            AppState.operands.Add(percentageOfResult);
        }

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
}