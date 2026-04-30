
namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class ToDo : Form
    {
        public static ToDo Instance { get; private set; }
        public ToDo()
        {
            Instance = this;
            Components3.InitializeAppComponents();
        }
    }
}

// Tudo inicializado, falta só configurar cada componente:
public class Components3
{
    public static Panel appControlPanel;
    public static FlowLayoutPanel appFlowPanel;
    public static Button addBtn;
    public static TextBox taskName;
    public static TextBox taskDescription;
    public static Label nameTxt;
    public static Label descriptionTxt;

    public static void InitializeAppComponents()
    {
        InitAppControlPanel();
        InitAppFlowPanel();
    }

    private static void InitAppControlPanel()
    {
        appControlPanel = new Panel
        {

        };

        nameTxt = new Label
        {

        };

        descriptionTxt = new Label
        {

        };

        taskName = new TextBox
        {

        };

        taskDescription = new TextBox
        {

        };

        addBtn = new Button
        {

        };
    }

    private static void InitAppFlowPanel()
    {
        appFlowPanel = new FlowLayoutPanel
        {

        };
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

        Button taskDelBtn = new Button
        {
            Text = "Delete"
        };

        // basicamente, este botão faz o mesmo que
        Button taskEditBtn = new Button
        {
            Text = "Edit"
        };

        taskContainer.Controls.Add(taskName);
        taskContainer.Controls.Add(taskDescription);
        taskContainer.Controls.Add(taskDelBtn);

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