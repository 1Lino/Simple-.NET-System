
using Sistema_De_Aplicativos_Simples__.NET.appsForms;

// TODO: 
// 1. Operações básicas [+ - x /] (ok)
// 2. Implementar uso de vírgula (ok)
// 3. Operações intermediárias [sqrt, pow, %, 1/x, +-] (x) 
// 4. Testar limites dos cálculos, procurar por erros ()
// 5. Implementar funcionalidade de histórico de operações ()
// 6. Rever nomenclaturas, principalmente onde houver comentários explicando código (x)
// 7. Implementar keyboard, de modo que seja permitido digitar números pelo teclado numérico (x)

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

public class AppState
{
    public static List<double> operands { get; set; }
    public static List<string> operators { get; set; }
    public static string operatorr { get; set; }
    public static double result { get; set; }

    public static bool canConcatOperation { get; set; }
    public static bool canUseDot { get; set; }

    public static void InitializeAppState()
    {
        operands = [];
        operators = [];
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
    private static TableLayoutPanel appGrid;
    public static Panel visorPanel;
    public static Label visorBottom;
    public static Label visorTop;
    private static List<string> btnTxtList;
    public static IEnumerable<Button> btnList;

    public static void InitializeAppComponents()
    {
        InitializeAppGrid();
        InitializeVisorPanel();

        btnTxtList = GetBtnTexts();
        visorBottom = InitializeVisor(0);
        visorTop = InitializeVisor(visorBottom.Height);

        visorPanel.Controls.Add(visorBottom);
        visorPanel.Controls.Add(visorTop);
        Calculator.Instance.Controls.Add(visorPanel);
        Calculator.Instance.Controls.Add(appGrid);
        AddCalculatorBtnsToAppGrid(appGrid, btnTxtList);

        btnList = GetCalculatorBtns(appGrid);
    }

    private static List<string> GetBtnTexts()
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
            Left = visorPanel.Width / 2 - visorCentralOffset,
            Top = visorPanel.Height / 2 - verticalOffset
        };

        return visor;
    }

    private static void InitializeAppGrid()
    {
        appGrid = new TableLayoutPanel
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
            appGrid.Left = (Calculator.Instance.ClientSize.Width - appGrid.Width) / 2;
            appGrid.Top = Calculator.Instance.ClientSize.Height - appGrid.Height - margin;
        }

        for (int gridCell = 0; gridCell < appGrid.ColumnCount; gridCell++)
        {
            appGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        }
        for (int gridCell = 0; gridCell < appGrid.RowCount; gridCell++)
        {
            appGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        }
    }

    private static void InitializeVisorPanel()
    {
        var formWidth = Calculator.Instance.Width; // pra não confundir o Width do componente com o Width do form.
        visorPanel = new Panel
        {
            Width = formWidth,
            Height = 180,
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.FromArgb(62, 85, 85)
        };
    }

    private static void AddCalculatorBtnsToAppGrid(TableLayoutPanel appGrid, List<string> btnTxtList)
    {
        int btnTxtListId = 0;

        for (int gridRow = 0; gridRow < appGrid.RowCount; gridRow++)
        {
            for (int gridCol = 0; gridCol < appGrid.ColumnCount; gridCol++)
            {
                AddCalculatorBtn(appGrid, gridCol, gridRow, btnTxtList, btnTxtListId);
                btnTxtListId++;
            }
        }
    }

    private static void AddCalculatorBtn(TableLayoutPanel appGrid, int gridCol, int gridRow, List<string> btnTxtList, int btnTxtListId)
    {
        var btn = CreateCalculatorBtn(btnTxtListId, btnTxtList);
        appGrid.Controls.Add(btn, gridCol, gridRow);
    }

    private static Button CreateCalculatorBtn(int btnTxtListId, List<string> btnTxtList)
    {
        // Tag é pra assegurar algum metadado para o componente. Será usado para eventos.
        // Como Tag é um objeto sem tipo específico, é necessário que, em seu uso, seja especificado o tipo do dado.
        // Ex.: "btn.Tag as string"
        var key = new Button
        {
            Dock = DockStyle.Fill,
            Text = btnTxtList[btnTxtListId],
            Tag = btnTxtList[btnTxtListId],
            Font = new Font("Segoe UI", 12f, FontStyle.Bold),
            ForeColor = Color.White
        };
        return key;
    }

    // Uso de recursão para capturar todos os elementos Button de um dado Control:
    // NOTA: pesquisar melhor sobre recursões e sobre yield.
    private static IEnumerable<Button> GetCalculatorBtns(Control appGrid)
    {
        // para cada componente do parente.
        foreach (Control component in appGrid.Controls)
        {
            // se tal componente for um botão, "guarda" o retorno deste componente na lista enumerável (yield faz isso).
            if (component is Button btn)
            {
                yield return btn;
            }

            // em seguida chama novamente a função GetCalculatorBtns e itera sobre cada 
            // elemento, de modo que a função efetivamente fará novamente o passo acima. Isto, na prática, faz com que todos os elementos botão do parente sejam capturados. Se a função fosse chamada uma vez apenas, iria parar no primeiro elemento botão que encontrasse, e já que precisamos de uma lista, então é necessário uso de recursão.

            foreach (var child in GetCalculatorBtns(component))
            {
                yield return child;
            }
        }
    }
}

public class GUIUpdate
{
    public static void OnDigit(Label visorBottom, Label visorTop, string operand)
    {
        if (visorBottom.Text.Length >= 15) return; // limita quantidade de dígitos para a quantidade normal suportada por um double ~15.

        if (visorBottom.Text == "0")
        {
            visorBottom.Text = operand;
        }
        else
        {
            visorBottom.Text += operand;
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
    public static void OnResult(Label visorBottom, Label visorTop, double result, string operatorr)
    {
        visorTop.Text = $"{result} {operatorr} ";
        visorBottom.Text = "0";
    }
    public static void OnOperationSet(Label visorBottom, Label visorTop, string operatorr)
    {
        if (visorTop.Text == "0")
        {
            visorTop.Text = $" {operatorr} ";
        }
        else
        {
            visorTop.Text += $" {operatorr} ";
        }
        visorBottom.Text = "0";
    }
    public static void OnDel(Label visorBottom, Label visorTop)
    {
        if (visorBottom.Text == "0") return; // impede que visorTop seja deletado para além do que é deletado em visorBottom.

        string currentNumber = visorBottom.Text;

        if (currentNumber.Length > 1)
        {
            visorBottom.Text = visorBottom.Text.Remove(visorBottom.Text.Length - 1);
        }
        else
        {
            visorBottom.Text = "0";
        }

        if (visorTop.Text.Length == 1) visorTop.Text = "0";
        else visorTop.Text = visorTop.Text.Remove(visorTop.Text.Length - 1);
    }
    public static void OnCE(Label visorBottom)
    {
        visorBottom.Text = "0";
    }
    public static void OnC(Label visorBottom, Label visorTop)
    {
        visorBottom.Text = "0";
        visorTop.Text = "0";
    }
    public static void OnDot(Label visorBottom, Label visorTop, bool canUseDot)
    {
        if (canUseDot)
        {
            if (visorTop.Text[visorTop.Text.Length - 1] == ' ') visorTop.Text += "0,";
            else visorTop.Text += ",";
            visorBottom.Text += ",";
        }
    }
    public static void OnSqrt(Label visorBottom, Label visorTop, double result, string operand)
    {
        visorTop.Text = $"√({operand})";
        visorBottom.Text = $"{result}";
    }
    public static void OnOneBy(Label visorBottom, Label visorTop, double result, string operand)
    {
        visorTop.Text = $"1 / {operand}";
        visorBottom.Text = $"{result}";
    }
    public static void OnOperationChange(Label visorTop, string operatorr)
    {
        string expression = visorTop.Text;
        visorTop.Text = expression.Substring(0, expression.Length - 2) + $"{operatorr} ";
    }
    public static void OnOperandSignalChange(Label visorBottom, Label visorTop)
    {
        visorBottom.Text = $"{-Double.Parse(Components.visorBottom.Text)}";
        string value = visorBottom.Text;
        // pega o índice do último espaço da expressão, que deve ser logo após o sinal do operador.
        int lastSpace = visorTop.Text.LastIndexOf(' ');

        if (lastSpace == -1)
        {
            visorTop.Text = value;
        }
        else
        {
            // corta a expressão até o último espaço.
            string expressionBeforeLastSpace = visorTop.Text.Substring(0, lastSpace);
            visorTop.Text = $"{expressionBeforeLastSpace} {value}"; // junta o corte ao número.
        }
    }
    public static void OnPercentage(Label visorBottom, Label visorTop, List<double> operands, string operatorr)
    {
        if (operands.Count == 0)
        {
            visorTop.Text = "0";
            visorBottom.Text = "0";
        }
        else
        {
            visorTop.Text = $"{operands[0]} {operatorr} {operands[1]}";
            visorBottom.Text = $"{operands[1]}";
        }
    }

}

public class AppEvents
{
    public static void InitializeAppEvents()
    {
        ApplyEventsToBtns(Components.btnList);
        Calculator.Instance.KeyPreview = true; // Permite que eventos de teclado sejam primeiro capturados pelo Form, ao invés de pelos componentes em foco.
        Calculator.Instance.KeyDown += CalculatorKeyDownEvent;
    }

    public static void ChangeOperator(string operatorr)
    {
        GUIUpdate.OnOperationChange(Components.visorTop, operatorr);
        Calculation.SetOperator(operatorr);
        AppState.operators[AppState.operators.Count - 1] = operatorr;
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

        string operand = Components.visorBottom.Text;
        Calculation.AddOperand(operand, operatorr);
        Calculation.SetOperator(operatorr);

        AppState.canConcatOperation = false;
        AppState.canUseDot = true;
        GUIUpdate.OnOperationSet(Components.visorBottom, Components.visorTop, AppState.operatorr);
    }

    private static void DispatchOperationResult()
    {
        string operand = Components.visorBottom.Text;
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
        GUIUpdate.OnResult(Components.visorBottom, Components.visorTop, AppState.result, AppState.operatorr);
    }

    private static void InputValidation(string operation)
    {
        List<string> numbers = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
        List<string> operations = ["+", "-", "x", "/", "DEL", "C", "CE", "=", ",", "²√x", "x²", "%", "1/x", "+/-"];

        if (Components.visorBottom.Text.Length == 20) return; // Pra pôr limite no número de caracteres no visor.

        if (numbers.Contains(operation))
        {
            AppState.canConcatOperation = true;
            GUIUpdate.OnDigit(Components.visorBottom, Components.visorTop, operation);
        }
        else if (operations.Contains(operation))
        {
            bool operationNotPossible = AppState.operands.Count < 1;
            string operatorr; // não deve ser confundido com AppState.operatorr. Seu uso também não é intercambiável.

            switch (operation)
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
                    double lastOperand = Double.Parse(Components.visorBottom.Text);
                    Calculation.Percent(lastOperand);
                    GUIUpdate.OnPercentage(Components.visorBottom, Components.visorTop, AppState.operands, AppState.operatorr);
                    break;
                case "²√x":
                    string operand = Components.visorBottom.Text;
                    Calculation.AddOperand(operand, "");
                    Calculation.Sqrt();
                    Calculation.ResetOperands();
                    GUIUpdate.OnSqrt(Components.visorBottom, Components.visorTop, AppState.result, operand);
                    break;
                case "1/x":
                    operand = Components.visorBottom.Text;
                    Calculation.AddOperand(operand, "");
                    Calculation.OneBy();
                    Calculation.ResetOperands();
                    GUIUpdate.OnOneBy(Components.visorBottom, Components.visorTop, AppState.result, operand);
                    break;
                case "+/-":
                    GUIUpdate.OnOperandSignalChange(Components.visorBottom, Components.visorTop);
                    break;
                case ",":
                    GUIUpdate.OnDot(Components.visorBottom, Components.visorTop, AppState.canUseDot);
                    AppState.canUseDot = false;
                    break;
                case "DEL":
                    GUIUpdate.OnDel(Components.visorBottom, Components.visorTop);
                    break;
                case "CE":
                    GUIUpdate.OnCE(Components.visorBottom);
                    break;
                case "C":
                    Calculation.ResetOperands();
                    GUIUpdate.OnC(Components.visorBottom, Components.visorTop);
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
            GUIUpdate.OnDigit(Components.visorBottom, Components.visorTop, $"{numberPressed}");
        }

        // Demais operações:
        string operatorr;
        var operationNotPossible = AppState.operands.Count < 1;

        if (e.KeyCode == Keys.Oemcomma)
        {
            Console.WriteLine("You pressed the 'Comma' key!");

            GUIUpdate.OnDot(Components.visorBottom, Components.visorTop, AppState.canUseDot);
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

            GUIUpdate.OnDel(Components.visorBottom, Components.visorTop);
        }
        if (e.KeyCode == Keys.Delete)
        {
            Console.WriteLine("You pressed the 'Delete' key!");

            Calculation.ResetOperands();
            GUIUpdate.OnC(Components.visorBottom, Components.visorTop);
        }
    }

    public static void ApplyEventsToBtns(IEnumerable<Button> btnList)
    {
        foreach (Button btn in btnList)
        {
            btn.Click += (_, _) => InputValidation(btn.Tag as string);
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
        AppState.operators.Add(operatorr);
    }

    public static void RemoveOperandAndOperatorAt(int index)
    {
        AppState.operands.RemoveAt(index + 1);
        AppState.operators.RemoveAt(index);
    }

    public static void Calculate()
    {
        // Prioridade: resolver operaçõed de * e /
        for (int i = 0; i < AppState.operators.Count;)
        {
            switch (AppState.operators[i])
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
        for (int i = 0; i < AppState.operators.Count;)
        {
            switch (AppState.operators[i])
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
        AppState.operators = [];
    }

    // Atualiza a operação com os valores do resultado:
    private static void UpdateOperation()
    {
        AppState.operands = [];
        AppState.operators = [];
        AppState.operands.Add(AppState.result);
        AppState.operators.Add(AppState.operatorr);
    }
}