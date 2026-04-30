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
        unitCategory = ComboBoxConstructor(0, 1, new string[] { "m", "cm" }, 2);
        fromUnit = ComboBoxConstructor(2, 2, new string[] { "m" }, 2);
        toUnit = ComboBoxConstructor(2, 0, new string[] { "cm" }, 2);
        fromNumber = TextBoxConstructor(4, 1, "box 1");
        toNumber = TextBoxConstructor(4, 0, "box 2");

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

    private static ComboBox ComboBoxConstructor(int topOffset = 0, int leftOffset = 0, Array items = null, int maxItems = 1)
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
        comboBox.MaxDropDownItems = maxItems;

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

        return textBox;
    }
}


public class Converter
{

}