// FontAwesome é uma lib interessante pra se utilizar ícones.
// pra instalar essa lib, basta digitar no console: dotnet add package FontAwesome.Sharp
using FontAwesome.Sharp;
using Sistema_De_Aplicativos_Simples__.NET.appsForms;

namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class ToDo : Form
    {
        public static ToDo Instance { get; private set; }
        public ToDo()
        {
            Instance = this;
            InitializeForm();
            Components3.InitializeAppComponents();
        }

        private void InitializeForm()
        {
            Text = "To Do List";
            Width = 450;
            Height = 600;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.FromArgb(29, 49, 49);
        }
    }
}

// Tudo inicializado, falta só configurar cada componente:
public class Components3
{
    public static TableLayoutPanel appGrid;
    public static Panel appControlPanel;
    public static FlowLayoutPanel appFlowPanel;
    public static IconButton addBtn;
    public static TextBox taskName;
    public static TextBox taskDescription;
    public static Label nameTxt;
    public static Label descriptionTxt;

    public static void InitializeAppComponents()
    {
        InitializeAppGrid();
        InitAppControlPanel();
        InitAppFlowPanel();
    }

    private static void InitializeAppGrid()
    {
        // ############### APP GRID  ###############
        appGrid = new TableLayoutPanel
        {
            ColumnCount = 1,
            RowCount = 2,
            Dock = DockStyle.None,
            // CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
            Size = new Size(400, 500)
        };

        appGrid.RowStyles.Add(
            new RowStyle(SizeType.Percent, 30)); // a primeira linha ocupará 30% da altura total do appGrid

        appGrid.RowStyles.Add(
            new RowStyle(SizeType.Percent, 70));

        int margin = 35;

        if (ToDo.Instance != null)
        {
            appGrid.Left = (ToDo.Instance.ClientSize.Width - appGrid.Width) / 2;
            appGrid.Top = ToDo.Instance.ClientSize.Height - appGrid.Height - margin;
        }

        ToDo.Instance.Controls.Add(appGrid);
        // ############### ############### ###############
    }

    private static void InitAppControlPanel()
    {
        appControlPanel = new Panel
        {
            BackColor = Color.FromArgb(62, 85, 85),
            Dock = DockStyle.Fill
        };

        appGrid.Controls.Add(appControlPanel);

        // Os componentes abaixo vão tudo pro appControlPanel:
        nameTxt = new Label
        {
            Width = 100,
            Text = "Task Name",
            Font = new Font("Segoe UI", 10f, FontStyle.Bold),
            ForeColor = Color.White,
            Top = 5,
            Left = 10
        };

        taskName = new TextBox
        {
            Width = 200,
            Height = 20,
            Top = 30,
            Left = 10
        };

        descriptionTxt = new Label
        {
            Width = 200,
            Text = "Task Description",
            Font = new Font("Segoe UI", 10f, FontStyle.Bold),
            ForeColor = Color.White,
            Top = appControlPanel.Height / 2 - 5,
            Left = 10
        };

        taskDescription = new TextBox
        {
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            Width = 250,
            Height = 40,
            Top = appControlPanel.Height / 2 + 20,
            Left = 10
        };

        addBtn = new IconButton
        {
            Size = new Size(50, 50),
            Left = appControlPanel.Width - 55,
            Top = appControlPanel.Height - 55,
            IconChar = IconChar.Plus,
            IconFont = IconFont.Solid,
            IconColor = Color.LightGreen,
            ImageAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.FromArgb(62, 85, 85),
            BackColor = Color.FromArgb(62, 85, 85),
            FlatStyle = FlatStyle.Flat,
        };
        addBtn.FlatAppearance.BorderSize = 0;

        appControlPanel.Controls.Add(nameTxt);
        appControlPanel.Controls.Add(taskName);
        appControlPanel.Controls.Add(descriptionTxt);
        appControlPanel.Controls.Add(taskDescription);
        appControlPanel.Controls.Add(addBtn);

    }

    private static void InitAppFlowPanel()
    {
        appFlowPanel = new FlowLayoutPanel
        {
            BackColor = Color.FromArgb(62, 85, 85),
            Dock = DockStyle.Fill
        };

        appGrid.Controls.Add(appFlowPanel);
    }

    public static void AddTaskTo(FlowLayoutPanel flowPanel)
    {
        Panel taskContainer = new Panel
        {
            Size = new Size(400, 400)
        };

        Label taskName = new Label
        {
            Text = "Task 1"
        };

        Label taskDescription = new Label
        {
            Text = "Task description"
        };

        IconButton taskDelBtn = new IconButton
        {
            Text = "Delete"
        };

        // basicamente, este botão faz o mesmo que
        IconButton taskEditBtn = new IconButton
        {
            Text = "Edit"
        };

        taskContainer.Controls.Add(taskName);
        taskContainer.Controls.Add(taskDescription);
        taskContainer.Controls.Add(taskDelBtn);
        taskContainer.Controls.Add(taskEditBtn);

        flowPanel.Controls.Add(taskContainer);
    }
}

// Essa classe lidará com funções que irão se encarregar do CRUD:
public class AppData
{

}


// Essa classe cria um componente label editável:
public class EditableLabel : UserControl
{
    private Label lbl;
    private TextBox txt;

    public EditableLabel()
    {
        lbl = new Label();
        txt = new TextBox();

        lbl.Dock = DockStyle.Fill;
        lbl.TextAlign = ContentAlignment.MiddleLeft;

        txt.Dock = DockStyle.Fill;
        txt.Visible = false;

        txt.Leave += (s, e) => EndEdit(true);
        txt.KeyDown += Txt_KeyDown;

        this.Controls.Add(txt);
        this.Controls.Add(lbl);
    }

    public override string Text
    {
        get => lbl.Text;
        set
        {
            lbl.Text = value;
            txt.Text = value;
        }
    }

    // Inicia edição
    public void StartEdit()
    {
        txt.Text = lbl.Text;
        lbl.Visible = false;
        txt.Visible = true;
        txt.Focus();
        txt.SelectAll();
    }

    // Finaliza edição
    public void EndEdit(bool commit)
    {
        if (commit)
            lbl.Text = txt.Text;
        else
            txt.Text = lbl.Text;

        lbl.Visible = true;
        txt.Visible = false;
    }

    private void Txt_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            EndEdit(true); // salva edição
            // TODO... alguma ação pra salvar em banco de dados ou coisa assim
        }
        else if (e.KeyCode == Keys.Escape)
        {
            EndEdit(false); // cancela edição
        }
    }
}