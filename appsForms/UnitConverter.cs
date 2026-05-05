using System.Drawing.Text;
using Sistema_De_Aplicativos_Simples__.NET.appsForms;

namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class UnitConverter : Form
    {
        public static UnitConverter Instance { get; private set; }
        public UnitConverter()
        {
            Instance = this;
            InitializeForm();
            Components2.InitializeAppComponents();
        }

        private void InitializeForm()
        {
            Text = "Unit Converteer";
            Width = 600;
            Height = 400;
            BackColor = Color.FromArgb(29, 49, 49);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }
    }
}

// O ideal é que houvesse apenas uma classe Components compartilhada entre todos os forms do App, pois a manutenção geral do app
// seria melhor.
public class Components2
{
    public static Panel appPanel;
    public static ComboBox unitCategory;
    public static ComboBox fromUnit;
    public static ComboBox toUnit;
    public static TextBox fromNumber;
    public static TextBox toNumber;

    public static void InitializeAppComponents()
    {
        AppPanelConstructor();
        unitCategory = ComboBoxConstructor(0, 1, new string[] { "Comprimento", "Area", "Volume", "Massa", "Temperatura" });
        unitCategory.SelectedIndex = 0;
        unitCategory.SelectedIndexChanged += ComboBox_CategoryChange;

        fromUnit = ComboBoxConstructor(2, 2, new string[] { "km", "hm", "dam", "m", "dm", "cm", "mm" });
        toUnit = ComboBoxConstructor(2, 0, new string[] { "km", "hm", "dam", "m", "dm", "cm", "mm" });
        fromUnit.SelectedIndex = 3;
        toUnit.SelectedIndex = 3;
        fromNumber = TextBoxConstructor(4, 1, "1");
        toNumber = TextBoxConstructor(4, 0, "1");

        appPanel.Controls.Add(unitCategory);
        appPanel.Controls.Add(fromUnit);
        appPanel.Controls.Add(toUnit);
        appPanel.Controls.Add(fromNumber);
        appPanel.Controls.Add(toNumber);
        UnitConverter.Instance.Controls.Add(appPanel);
    }

    private static void AppPanelConstructor()
    {
        var formWidth = UnitConverter.Instance.Width;
        appPanel = new Panel
        {
            Width = formWidth / 100 * 80, // 80% do tamanho do form.
            Height = 240,
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.FromArgb(62, 85, 85)
        };
        appPanel.Left = formWidth / 100 * 10; // 10% do tamanho do form.
        appPanel.Top = formWidth / 100 * 10;
    }

    private static ComboBox ComboBoxConstructor(int topOffset = 0, int leftOffset = 0, Array items = null)
    {
        // left e top offsets controlam o deslocamento dos componentes para a esquerda do painel. Quanto maior o offset
        // maior o deslocamento à esquerda.
        var comboBox = new ComboBox
        {
            Size = new Size(100, 20),
        };

        int adjustFactor = 4; // 0 - abaixo do centro do painel; 2 - no centro do painel; 4 - acima do centro do painel; 
        comboBox.Left = (appPanel.Width + comboBox.Width) / 2 - (comboBox.Width * leftOffset);
        comboBox.Top = appPanel.Height / 2 + comboBox.Height * (topOffset - adjustFactor);

        comboBox.Items.AddRange(items != null ? items.Cast<Object>().ToArray() : new string[] { "" });
        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBox.SelectedIndex = 0;

        return comboBox;
    }

    private static TextBox TextBoxConstructor(int topOffset = 0, int leftOffset = 0, string text = "")
    {
        var textBox = new TextBox
        {
            Size = new Size(150, 20),
            Text = text
        };
        int adjustFactor = 4;
        int gap = leftOffset != 0 ? 10 : -10; // 10, elemento se desloca 10 pixels à esquerda. -10 se desloca 10 pixels à direita.
        textBox.Left = appPanel.Width / 2 - textBox.Width * leftOffset - gap;
        textBox.Top = appPanel.Height / 2 + textBox.Height * (topOffset - adjustFactor);
        textBox.KeyPress += TextBox_KeyPress;

        return textBox;
    }

    private static void TextBox_KeyPress(Object sender, KeyPressEventArgs e)
    {
        // se o caractere não for de controle (ex.: tab, backspace, enter, etc)
        // e não for um dígito numérico (decimal), nem for o símbolo '.', então ignora o caractere:
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
        {
            e.Handled = true; // "Handled" diz se o evento foi tratado ou não. Quando true, basicamente diz que o evento já foi tratado, e que não precisa prosseguir. No caso, o que acontece é que o caractere será ignorado.
        }

        // mas se for o símbolo for '.' e na caixa de texto já houver tal símbolo, então ignora o caractere:
        if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
        {
            e.Handled = true;
        }

        // se for enter:
        if (e.KeyChar == (char)Keys.Enter)
        {
            string value = fromNumber.Text != "" ? fromNumber.Text : "0";
            string category = unitCategory.SelectedItem.ToString();
            string sourceUnit = fromUnit.SelectedItem.ToString();
            string targetUnit = toUnit.SelectedItem.ToString();
            toNumber.Text = Converter.ConvertUnit(category, value, sourceUnit, targetUnit);
            e.Handled = true; // previne o som de "beep" que ocorre ao teclar enter. Este som ocorre porque e.Handled precisa ser acionado para que se entenda que o evento terminou.
        }


    }

    public static string[] rangeUnits = ["km", "hm", "dam", "m", "dm", "cm", "mm"];
    private static void ComboBox_CategoryChange(Object sender, EventArgs e)
    {
        string category = unitCategory.SelectedItem.ToString();
        // string[] range = [];

        switch (category)
        {
            case "Comprimento":
                rangeUnits = ["km", "hm", "dam", "m", "dm", "cm", "mm"];
                break;
            case "Area":
                rangeUnits = ["km²", "hm²", "dam²", "m²", "dm²", "cm²", "mm²"];
                break;
            case "Volume":
                rangeUnits = ["km³", "hm³", "dam³", "m³", "dm³", "cm³", "mm³"];
                break;
            case "Massa":
                rangeUnits = ["kg", "hg", "dag", "g", "dg", "cg", "mg"];
                break;
            case "Temperatura":
                rangeUnits = ["ºC", "ºF", "K"];
                break;
        }

        fromUnit.Items.Clear();
        toUnit.Items.Clear();
        fromUnit.Items.AddRange(rangeUnits);
        toUnit.Items.AddRange(rangeUnits);
        fromUnit.SelectedIndex = 0;
        toUnit.SelectedIndex = 0;
    }
}


public class Converter
{
    public static string ConvertUnit(string type, string input, string sourceUnit, string targetUnit)
    {
        string convertedUnit = "";

        switch (type)
        {
            case "Comprimento":
                convertedUnit = $"{Converter.ConvertDimension(input, sourceUnit, targetUnit, 1)}";
                break;
            case "Area":
                convertedUnit = $"{Converter.ConvertDimension(input, sourceUnit, targetUnit, 2)}";
                break;
            case "Volume":
                convertedUnit = $"{Converter.ConvertDimension(input, sourceUnit, targetUnit, 3)}";
                break;
            case "Massa":
                convertedUnit = $"{Converter.ConvertDimension(input, sourceUnit, targetUnit, 1)}";
                break;
            case "Temperatura":
                convertedUnit = $"{Converter.ConvertTemperature(input, sourceUnit, targetUnit)}";
                break;
        }

        return convertedUnit;
    }

    /// <summary>
    /// Recebe um valor em uma dada unidade e o converte para outra unidade em uma dada dimensão.
    /// </summary>
    /// <param name="input">O valor a ser convertido.</param>
    /// <param name="sourceUnit">Unidade do valor a ser convertido.</param>
    /// <param name="targetUnit">Unidade alvo para a conversão.</param>
    /// <param name="dimension">Dimensão da conversão: 1 - comprimento ou massa; 2 - area; 3 - volume</param>
    /// <returns>O valor convertido, em comprimento, área ou volume.</returns>
    public static double ConvertDimension(string input, string sourceUnit, string targetUnit, int dimension = 1)
    {
        if (!Components2.rangeUnits.Contains(sourceUnit) || !Components2.rangeUnits.Contains(targetUnit))
        {
            throw new Exception("One of the units provided is not supported for this conversion!");
        }

        double valueToConvert = double.Parse(input);
        int sourceUnitIndex = Components2.rangeUnits.IndexOf(sourceUnit);
        int targetUnitIndex = Components2.rangeUnits.IndexOf(targetUnit);
        int indexDifference = targetUnitIndex - sourceUnitIndex;
        double conversionFactor = Math.Pow(10, indexDifference);

        return valueToConvert * Math.Pow(conversionFactor, dimension);
    }

    public static double ConvertTemperature(string input, string sourceUnit, string targetUnit)
    {
        if (!Components2.rangeUnits.Contains(sourceUnit) || !Components2.rangeUnits.Contains(targetUnit))
        {
            throw new Exception("One of the units provided is not supported for this conversion!");
        }

        double valueToConvert = double.Parse(input);

        // "ºC", "ºF", "K"
        // Celsius para Fahrenheit: °F = (°C × 5 / 9) +32
        // Celsius para Kelvin: K = °C + 273,15
        // Fahrenheit para Celsius: °C = (°F - 32) × 9 / 5 	​
        // Kelvin para Celsius: °C = K - 273,15

        switch (sourceUnit)
        {
            case "ºC":
                double celsius = valueToConvert;
                if (targetUnit == "ºF")
                {
                    return (celsius * 9 / 5) + 32; // celsius para fahrenheit
                }
                else if (targetUnit == "K")
                {
                    return celsius + 273.15; // celsius para kelvin
                }

                return celsius;

            case "ºF":
                double fahrenheit = valueToConvert;
                if (targetUnit == "ºC")
                {
                    return (fahrenheit - 32) * 5 / 9; // fahrenheit para celsius
                }
                else if (targetUnit == "K")
                {
                    return ((fahrenheit - 32) * 5 / 9) + 273.15; // fahrenheit para celsius -> celsius para kelvin
                }
                return fahrenheit;

            case "K":
                double kelvin = valueToConvert;
                if (targetUnit == "ºC")
                {
                    return kelvin - 273.15; // kelvin para celsius;
                }
                else if (targetUnit == "ºF")
                {
                    return ((kelvin - 273.15) * 9 / 5) + 32; // kelvin para celsius -> celsius para fahrenheit
                }

                return kelvin;
        }

        return 1;
    }
}