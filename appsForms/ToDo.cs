// FontAwesome é uma lib interessante pra se utilizar ícones.
// pra instalar essa lib, basta digitar no console: dotnet add package FontAwesome.Sharp
using FontAwesome.Sharp;
using Sistema_De_Aplicativos_Simples__.NET.appsForms;
using System.ComponentModel;
using System.Collections;
using Microsoft.Data.Sqlite; // dotnet add package Microsoft.Data.Sqlite


// TODO: realizar todas as limpezas necessárias no código.

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
                Components3.appGrid.Left = (Instance.ClientSize.Width - Components3.appGrid.Width) / 2;//ToDo.Instance.Width / 2 - (Components3.appGrid.Width / 2);
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
            Components3.appGrid.Left = (Instance.ClientSize.Width - Components3.appGrid.Width) / 2;
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
    public static Panel taskContainer;
    public static int taskContainerWidth = 385; // este componente é público pois seu valor deverá ser acessado pelo form.
    public static int taskContainerHeigth = 100;
    public static EditableLabel taskNameLbl;
    public static EditableLabel taskDescriptionLbl;
    private static IconButton taskEditBtn; // acho que deve ser uma lista
    private static IconButton taskDelBtn; // deve ser uma lista
    public static int taskCount = 0;

    public static void InitializeAppComponents()
    {
        InitializeAppGrid();
        InitAppControlPanel();
        InitAppFlowPanel();
        LoadItemsFromDataBase();
    }

    private static void LoadItemsFromDataBase()
    {
        try
        {
            Hashtable loadedData = TaskData.LoadData();

            foreach (DictionaryEntry item in loadedData)
            {
                TaskData itemData = (TaskData)item.Value;
                AddTaskTo(appFlowPanel, itemData.TaskId, itemData.TaskName, itemData.TaskDescription);
                Console.WriteLine($"TaskId: {itemData.TaskId}\nTaskName: {itemData.TaskName}\nTaskDescription: {itemData.TaskDescription}");
            }
        }
        catch (Exception NoDataBaseToLoad) // Pode ocorrer de a base de dados não existir ainda, então o bloco acima retornaria erro.
        {
            Console.WriteLine(NoDataBaseToLoad);
        }


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
        taskName.KeyDown += HandleKeyDown;

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
        taskDescription.KeyDown += HandleKeyDown;

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
        addBtn.Click += HandleAddClick;

        appControlPanel.Controls.Add(nameTxt);
        appControlPanel.Controls.Add(taskName);
        appControlPanel.Controls.Add(descriptionTxt);
        appControlPanel.Controls.Add(taskDescription);
        appControlPanel.Controls.Add(addBtn);

    }

    private static void HandleKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true; // Stops the ding sound
            e.Handled = true;

            // Your Enter logic here
        }
    }

    private static void HandleAddClick(Object sender, EventArgs e)
    {
        if (taskName.Text.Length == 0 || taskDescription.Text.Length == 0) return;

        // toda vez que uma task for criada, taskCount deve aumentar.
        // toda vez que uma task for destruida, taskCount deve diminuir.
        taskCount++;
        Random rnd = new Random();
        int firstRnd = rnd.Next(0, 9999);
        int secondRnd = rnd.Next(0, 9999);
        string id = $"{taskCount}{firstRnd}{secondRnd}";

        AddTaskTo(appFlowPanel, id, taskName.Text, taskDescription.Text);
        TaskData.SaveData((string)taskContainer.Tag, taskName.Text, taskDescription.Text);

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

    private static void AddTaskTo(FlowLayoutPanel flowPanel, string taskId, string taskN, string taskD)
    {
        // para uma task ser modificada ou destruída, deverá ser acessada pela Tag.
        taskContainer = new Panel
        {
            Tag = taskId, // id da task parte do contador, acrescido de dois nº pseudo-randomicos.
            Size = new Size(taskContainerWidth, taskContainerHeigth),
            BackColor = Color.FromArgb(29, 49, 49),
        };

        taskNameLbl = new EditableLabel
        {
            Name = "taskNameLbl",
            Text = taskN,
            AutoSize = false, //pra respeitar o Width e o Height do label, e permitir quebra de linha.
            Width = 200,
            Top = 5,
            Left = 5,
            Font = new Font("Segoe UI", 10f, FontStyle.Bold),
            ForeColor = Color.White,
            AutoEllipsis = true,
        };
        taskNameLbl.MaxLength(50);
        taskNameLbl.Multiline(false);

        taskDescriptionLbl = new EditableLabel
        {
            Name = "taskDescriptionLbl",
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
        taskDescriptionLbl.MaxLength(250);

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

        taskEditBtn.Click += OnClickEditBnt;
        taskDelBtn.Click += OnClickDelBnt;
    }

    private static void OnClickEditBnt(object sender, EventArgs e)
    {
        Button btn = sender as Button; // captura o próprio botão clicado
        if (btn == null) return;

        // captura o parente do botão clicado, que é o container da task:
        Panel taskContainer = btn.Parent as Panel;
        if (taskContainer == null) return;

        // itera sobre cada componente do taskContainer e verifica se tal componente é do tipo EditableLabel.
        foreach (Control ctrl in taskContainer.Controls)
        {
            if (ctrl is EditableLabel lb)
            {
                lb.StartEdit(); // executa o StartEdit de cada EditableLabel encontrado.
            }
        }

    }

    private static void OnClickDelBnt(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        if (btn == null) return;

        Panel taskContainer = btn.Parent as Panel;
        if (taskContainer == null) return;

        TaskData.DeleteData((string)taskContainer.Tag); // remove da base de dados o item com esta tag/id.

        appFlowPanel.Controls.Remove(taskContainer); // Remove a task do FlowLayoutPanel

        // descarta recursos do panel
        taskContainer.Dispose();
    }

}

// Essa classe lidará com funções que irão se encarregar do CRUD:
public class TaskData
{
    public string TaskId { get; set; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }

    // captura o caminho da pasta de dados de apps. Em windows, por exemplo, seria a:
    // %TaskData% -> C:\Users\User\TaskData\Roaming\...
    private static string TaskDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

    // cria um novo caminho para ToDo App no caminho TaskDataPath:
    private static string appFolderPath = Path.Combine(TaskDataPath, "ToDo App");

    // cria um novo diretório a partir do caminho, caso já não exista:
    static TaskData()
    {
        Directory.CreateDirectory(appFolderPath);
    }

    // elabora um caminho final pro diretório:
    private static string dbFilePath = Path.Combine(appFolderPath, "app_data.db");

    // NOTA: este método só deverá ser chamado quando o usuário tiver editado alguma task e teclado enter ou tab:
    public static void UpdateData(string taskId, string newTaskName, string newTaskDescription)
    {
        using var connection = new SqliteConnection($"Data Source={dbFilePath}");
        connection.Open();

        string updateQuery = @"
            UPDATE tasks
            SET 
                taskName = @newTaskName,
                taskDescription = @newTaskDescription
            WHERE 
                taskId = @taskId";

        using var command = new SqliteCommand(updateQuery, connection);

        command.Parameters.AddWithValue("@newTaskName", newTaskName);
        command.Parameters.AddWithValue("@newTaskDescription", newTaskDescription);
        command.Parameters.AddWithValue("@taskId", taskId);

        int rowsAffected = command.ExecuteNonQuery();

        Console.WriteLine($"{rowsAffected} row with the id of {taskId} updated!");
    }

    public static void DeleteData(string id)
    {
        using var connection = new SqliteConnection($"Data Source={dbFilePath}");
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText =
        @"
            DELETE FROM tasks
            WHERE taskId = $id
        ";

        command.Parameters.AddWithValue("$id", id);

        int rowsDeleted = command.ExecuteNonQuery();

        Console.WriteLine($"{rowsDeleted} row with the id of {id} deleted!");
    }

    public static Hashtable LoadData()
    {
        Hashtable hashObject = new Hashtable();

        using var connection = new SqliteConnection($"Data Source={dbFilePath}");
        connection.Open();

        // leitura do banco:
        string sqlSelect = "SELECT * FROM tasks";

        using var cmdSelect = new SqliteCommand(sqlSelect, connection);

        using var reader = cmdSelect.ExecuteReader();

        Console.WriteLine("------------------------------------------------------");
        Console.WriteLine("Banco de dados: ");
        while (reader.Read())
        {
            string taskId = (string)reader["taskId"];
            string taskName = (string)reader["taskName"];
            string taskDescription = (string)reader["taskDescription"];

            // Armazena um objeto do tipo TaskData:
            hashObject[taskId] = new TaskData { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription };

            // Console.WriteLine($"TaskId: {reader["taskId"]}\nTaskName: {reader["taskName"]}\nTaskDescription: {reader["taskDescription"]}");
        }

        return hashObject;
    }

    public static void SaveData(string id, string name, string description)
    {
        TaskData taskData = new TaskData()
        {
            TaskId = id,
            TaskName = name,
            TaskDescription = description
        };

        Console.WriteLine($"Saved data:\nId: {taskData.TaskId}\nName: {taskData.TaskName}\nDescription: {taskData.TaskDescription}");

        using var connection = new SqliteConnection($"Data Source={dbFilePath}");
        connection.Open();

        // Cria tabela se não existir
        string sqlCreate = @"
                CREATE TABLE IF NOT EXISTS tasks (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    taskId TEXT NOT NULL,
                    taskName TEXT NOT NULL,
                    taskDescription TEXT NOT NULL
                )";

        using var cmd = new SqliteCommand(sqlCreate, connection);
        cmd.ExecuteNonQuery();

        string sqlInsert = "INSERT INTO tasks (taskId, taskName, taskDescription) VALUES (@taskId, @taskName, @taskDescription)";

        using var cmdInsert = new SqliteCommand(sqlInsert, connection); // "using" fecha a conexão automaticamente após tudo terminar o processo do comando.

        cmdInsert.Parameters.AddWithValue("@taskId", id);
        cmdInsert.Parameters.AddWithValue("@taskName", name);
        cmdInsert.Parameters.AddWithValue("@taskDescription", description);

        cmdInsert.ExecuteNonQuery();

        // leitura do banco:
        string sqlSelect = "SELECT * FROM tasks";

        using var cmdSelect = new SqliteCommand(sqlSelect, connection);

        using var reader = cmdSelect.ExecuteReader();

        Console.WriteLine("------------------------------------------------------");
        Console.WriteLine("Banco de dados: ");
        while (reader.Read())
        {
            Console.WriteLine($"TaskId: {reader["taskId"]}\nTaskName: {reader["taskName"]}\nTaskDescription: {reader["taskDescription"]}");
        }

    }
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

    public void MaxLength(int length)
    {
        txt.MaxLength = length;
    }

    // public void InsertTextAtStart(string text)
    // {
    //     formatedDate = text;
    // }

    public void Multiline(bool b)
    {
        txt.Multiline = b;
    }

    // string formatedDate = "";
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
        txt.MaxLength = 50;

        txt.Leave += (s, e) => EndEdit(true); // ao sair do foco do componente, sai da edição.
        txt.KeyDown += Txt_KeyDown;

        Controls.Add(txt);
        Controls.Add(lbl);
    }


    // MÉTODOS DE INSTÂNCIA:
    // Inicia edição
    public void StartEdit()
    {
        // Caso não queira a data no modo edição: corta-a. já que o final da string de data termina em ' ', na posição 10, então:
        // txt.Text = lbl.Text.Length > 10 && lbl.Text[10] == ' ' ? lbl.Text.Substring(11) : lbl.Text;

        txt.Text = lbl.Text;

        lbl.Visible = false;
        txt.Visible = true;
        //txt.Focus(); // isto faz com que o texbox receba foco, mas não preciso disso agora.
        txt.SelectAll();
    }

    // Finaliza edição
    // TODO: criar validação que impede usuário de sair da edição se as caixas de texto estiverem vazias
    public void EndEdit(bool commit)
    {
        if (commit)
        {
            lbl.Text = txt.Text.Trim(); // .Trim pra remover qualquer espaço antes ou depois do texto
        }
        else
            txt.Text = lbl.Text;

        lbl.Visible = true;
        txt.Visible = false;
    }

    // TODO: criar validação que impede usuário de sair da edição se as caixas de texto estiverem vazias
    // fazer caixa de texto ficar com bordas vermelhas, ou alguma outra forma de "required"
    private void Txt_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            e.Handled = true;

            // verifica se o campo está vazio, se estiver, retorna a mensagem:
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                MessageBox.Show("O campo Nome é obrigatório.");

                txt.Focus();
                return;
            }
            Console.WriteLine($"The text that should show up: {txt.Text}");

            EndEdit(true); // salva edição caso não esteja vazio.

            Panel parent = (Panel)Parent;
            EditableLabel name = (EditableLabel)parent.Controls["taskNameLbl"];
            EditableLabel description = (EditableLabel)parent.Controls["taskDescriptionLbl"];

            Console.WriteLine($"Item Id: {(string)parent.Tag}\nUpdated Name: {name.Text}\nUpdated Description: {description.Text}");

            TaskData.UpdateData((string)parent.Tag, name.Text, description.Text);
        }
        else if (e.KeyCode == Keys.Escape)
        {
            EndEdit(false); // cancela edição
        }

    }
}