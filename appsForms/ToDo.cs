// FontAwesome é uma lib interessante pra se utilizar ícones.
// pra instalar essa lib, basta digitar no console: dotnet add package FontAwesome.Sharp
using FontAwesome.Sharp;
using Sistema_De_Aplicativos_Simples__.NET.appsForms;
using System.CodeDom;
using System.ComponentModel;

namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class ToDo : Form
    {
        public static ToDo Instance { get; private set; }
        private bool IsMaximized = false;

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
            Instance.Resize += OnFormResize;
        }

        private void OnFormResize(Object sender, EventArgs e)
        {
            if (!IsMaximized) // se o estado não estiver maximizado.
            {
                IsMaximized = !IsMaximized; // muda o estado para maximizado.

                // realiza operações em maximizado:

                Components3.appGrid.Size = new Size(600, 650);
                Components3.appGrid.Left = (ToDo.Instance.ClientSize.Width - Components3.appGrid.Width) / 2;//ToDo.Instance.Width / 2 - (Components3.appGrid.Width / 2);
                Components3.taskDescription.Size = new Size(400, 50);
                Components3.addBtn.Left = Components3.appControlPanel.Width - 150;

                // Panel taskContainer = (Panel)Components3.appFlowPanel.Controls["taskContainer"];
                // taskContainer.Size = new Size(585, 100);
                Components3.taskContainerWidth = 585;
                Components3.taskContainerHeigth = 100;

                if (Components3.appFlowPanel.Controls.Count != 0) // se a lista de componentes contidos em appFlow não for 0:
                {
                    for (int i = 0; i < Components3.appFlowPanel.Controls.Count; i++)
                    {
                        Panel taskContainer = (Panel)Components3.appFlowPanel.Controls[i];
                        taskContainer.Size = new Size(Components3.taskContainerWidth, Components3.taskContainerHeigth); // redimensiona todos eles.
                        taskContainer.Controls["Edit"].Left = 535;
                        taskContainer.Controls["Delete"].Left = 535;
                    }
                }
                return;
            }

            IsMaximized = !IsMaximized; // reverte estado.

            // reverte operações:
            Components3.appGrid.Size = new Size(400, 500);
            Components3.appGrid.Left = (ToDo.Instance.ClientSize.Width - Components3.appGrid.Width) / 2;
            Components3.taskDescription.Size = new Size(250, 40);
            Components3.addBtn.Left = Components3.appControlPanel.Width - 55;

            Components3.taskContainerWidth = 385;
            Components3.taskContainerHeigth = 100;

            if (Components3.appFlowPanel.Controls.Count != 0) // se a lista de componentes contidos em appFlow não for 0:
            {
                for (int i = 0; i < Components3.appFlowPanel.Controls.Count; i++)
                {
                    Panel taskContainer = (Panel)Components3.appFlowPanel.Controls[i];
                    taskContainer.Size = new Size(Components3.taskContainerWidth, Components3.taskContainerHeigth);
                    taskContainer.Controls["Edit"].Left = Components3.taskContainerWidth - 45;
                    taskContainer.Controls["Delete"].Left = Components3.taskContainerWidth - 45;
                }
            }

            return;
        }
    }
}

// Tudo inicializado, falta só configurar cada componente:
public class Components3
{
    // componentes relacionados ao painel e seu menu:
    public static TableLayoutPanel appGrid;
    public static Panel appControlPanel;
    public static FlowLayoutPanel appFlowPanel;
    public static IconButton addBtn;
    public static TextBox taskName;
    public static TextBox taskDescription;
    public static Label nameTxt;
    public static Label descriptionTxt;

    // componentes relacionados aos itens de task:
    private static Panel taskContainer;
    public static int taskContainerWidth = 385; // este componente é público pois seu valor deverá ser acessado pelo form.
    public static int taskContainerHeigth = 100;
    private static EditableLabel taskNameLbl;
    private static EditableLabel taskDescriptionLbl;
    private static IconButton taskEditBtn;
    private static IconButton taskDelBtn;
    public static int taskCount = 0;

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
            Left = 10,
            MaxLength = 50
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
            Left = 10,
            MaxLength = 250 // max length of the input
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
        addBtn.Click += HandleClick;

        appControlPanel.Controls.Add(nameTxt);
        appControlPanel.Controls.Add(taskName);
        appControlPanel.Controls.Add(descriptionTxt);
        appControlPanel.Controls.Add(taskDescription);
        appControlPanel.Controls.Add(addBtn);

    }

    private static void HandleClick(Object sender, EventArgs e)
    {
        if (taskName.Text.Length == 0 || taskDescription.Text.Length == 0) return;

        AddTaskTo(appFlowPanel, taskName.Text, taskDescription.Text);
        taskName.Text = "";
        taskDescription.Text = "";
    }

    private static void InitAppFlowPanel()
    {
        appFlowPanel = new FlowLayoutPanel
        {
            BackColor = Color.FromArgb(62, 85, 85),
            Dock = DockStyle.Fill,
            AutoScroll = true,
        };

        appGrid.Controls.Add(appFlowPanel);
    }

    private static void AddTaskTo(FlowLayoutPanel flowPanel, string taskN, string taskD)
    {
        // toda vez que uma task for criada, taskCount deve aumentar.
        // toda vez que uma task for destruida, taskCount deve diminuir.
        taskCount++;

        // para uma task ser modificada ou destruída, deverá ser acessada pela Tag.
        taskContainer = new Panel
        {
            Tag = $"{taskCount}", // id da task parte do contador, mas poderia ser algo randomico, mas se for randomico há a chance de duplicata, ainda que baixa, por isso prefiro usar o próprio contador como id.
            Size = new Size(taskContainerWidth, taskContainerHeigth),
            BackColor = Color.FromArgb(29, 49, 49),
        };

        DateTime date = DateTime.Now;
        string formatedDate = date.ToString("dd/MM/yy");

        taskNameLbl = new EditableLabel
        {
            Text = $"{formatedDate} - " + taskN,
            AutoSize = false, //pra respeitar o Width e o Height do label, e permitir quebra de linha.
            Width = 200,
            Top = 5,
            Left = 5,
            Font = new Font("Segoe UI", 10f, FontStyle.Bold),
            ForeColor = Color.White,
            AutoEllipsis = true,

        };

        taskDescriptionLbl = new EditableLabel
        {
            Text = taskD,
            AutoSize = false,
            Width = 260,
            Height = 60,
            Top = 30,
            Left = 5,
            Font = new Font("Segoe UI", 10f, FontStyle.Regular),
            ForeColor = Color.White,
            AutoEllipsis = true,
        };

        taskEditBtn = new IconButton
        {
            Name = "Edit",
            Size = new Size(40, 40),
            Left = taskContainer.Width - 45,
            Top = 5,
            IconChar = IconChar.Edit,
            IconFont = IconFont.Solid,
            IconColor = Color.LightGreen,
            ImageAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.FromArgb(29, 49, 49),
            BackColor = Color.FromArgb(29, 49, 49),
            FlatStyle = FlatStyle.Flat,
        };
        taskEditBtn.FlatAppearance.BorderSize = 0;

        taskDelBtn = new IconButton
        {
            Name = "Delete",
            Size = new Size(40, 40),
            Left = taskContainer.Width - 45,
            Top = 50,
            IconChar = IconChar.Trash,
            IconFont = IconFont.Solid,
            IconColor = Color.Red,
            ImageAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.FromArgb(29, 49, 49),
            BackColor = Color.FromArgb(29, 49, 49),
            FlatStyle = FlatStyle.Flat,

        };
        taskDelBtn.FlatAppearance.BorderSize = 0;

        taskContainer.Controls.Add(taskNameLbl);
        taskContainer.Controls.Add(taskDescriptionLbl);
        taskContainer.Controls.Add(taskDelBtn);
        taskContainer.Controls.Add(taskEditBtn);

        flowPanel.Controls.Add(taskContainer);

        Console.WriteLine($"Task added!\nID: {taskContainer.Tag}\nName: {taskNameLbl.Text}\nDescription: {taskDescriptionLbl.Text}");
    }

    private static void OnClickEditBnt(object sender, EventArgs e)
    {
        // taskNameLbl.Text;
        // taskDescriptionLbl.Text;
    }

    private static void OnClickDelBnt(object sender, EventArgs e)
    {
        // TODO...
    }

}

// Essa classe lidará com funções que irão se encarregar do CRUD:
public class AppData
{

}


// Essa classe cria um componente label editável:
// NOTA.: UserControl possui propriedades padrão diferentes, então é necessário redefinir, no construtor
// as propriedades padrão para este novo componente. Isto também se aplica a outros comportamentos, que devem então ser configurados
// manualmente nesta classe.
public class EditableLabel : UserControl
{
    private Label lbl;
    private TextBox txt;

    // EXPOSIÇÃO DE PROPRIEDADES:

    // Qualquer que for o conteúdo passado como valor à propriedade Text na criação do componente, será guardado no componente.
    // do contrário não aparece nada de texto na tela.
    public override string Text
    {
        get => lbl.Text; // "captura" a propriedade Text do label.
        set { lbl.Text = value; txt.Text = value; } // "captura" o valor e passa este valor.
    }

    public override Font Font
    {
        get => base.Font;
        set
        {
            base.Font = value;
            if (lbl != null) lbl.Font = value;
            if (txt != null) txt.Font = value;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AutoEllipsis
    {
        get => lbl.AutoEllipsis;
        set => lbl.AutoEllipsis = value;
    }

    // protected override void OnFontChanged(EventArgs e)
    // {
    //     base.OnFontChanged(e);
    //     lbl.Font = this.Font;
    //     txt.Font = this.Font;
    // }

    // protected override void OnForeColorChanged(EventArgs e)
    // {
    //     base.OnForeColorChanged(e);
    //     lbl.ForeColor = this.ForeColor;
    //     txt.ForeColor = this.ForeColor;
    // }

    // CONSTRUCTOR DO COMPONENTE:
    public EditableLabel()
    {
        lbl = new Label();
        txt = new TextBox();

        this.AutoScaleMode = AutoScaleMode.None;
        this.AutoSize = false;
        this.Size = new Size(100, 23); // define um tamanho padrão para o elemento.

        lbl.Dock = DockStyle.Fill;
        lbl.AutoEllipsis = true; // por padrão é "true", ou seja, os três pontos (...) serão adicionados ao final de um texto truncado.
        lbl.AutoSize = false;
        lbl.TextAlign = ContentAlignment.TopLeft;

        txt.Dock = DockStyle.Fill;
        txt.Visible = false;
        txt.Multiline = true;

        txt.Leave += (s, e) => EndEdit(true);
        txt.KeyDown += Txt_KeyDown;

        Controls.Add(txt);
        Controls.Add(lbl);
    }


    // MÉTODOS DE INSTÂNCIA:
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