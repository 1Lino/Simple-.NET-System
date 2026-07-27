
// Esse pequeno projeto simula a estrutura base de projeto fullstack, no caso, focando apenas no que se refere ao serviço de autenticação de usuário. Estrutura:
// programa.cs
// │
// ├── Authenticator/LoginForm  -> Interface (WinForms)
// ├── Usuario                  -> Modelo para a base
// ├── UsuarioRepository        -> Armazenamento/base
// ├── AuthService              -> Regras de login (validação backend)
// └── Validator                -> Validação (validação frontend)
// Na camada de AuthService e na de cadastro de usuário, é possível também testar diversos métodos de salvamento de dados em hash, etc.

namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class Authenticator : Form
    {
        public static Authenticator Instance { get; private set; }
        public static UsuarioRepository userRepo = new UsuarioRepository();
        public static AuthService authService = new AuthService(userRepo);

        public Authenticator()
        {
            Instance = this;
            InitializeForm();
            InitializeComponents();
            this.CenterToScreen();
        }

        // LOGIN FORM
        private void InitializeForm()
        {
            Text = "Login App - Teste de Autenticação";
            Width = 600;
            Height = 400;
            BackColor = Color.FromArgb(29, 49, 49);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void InitializeComponents()
        {
            var panelWidth = 350;
            var panelHeight = 250;

            Panel panel = new Panel
            {
                Anchor = AnchorStyles.None,
                BackColor = Color.White,
                Width = panelWidth,
                Height = panelHeight,
                Left = this.ClientSize.Width / 2 - (panelWidth / 2),
                Top = this.ClientSize.Height / 2 - (panelHeight / 2)
            };

            Label lbl_email = new Label
            {
                Text = "E-Mail:",
                Left = (panel.Width - 200) / 2,
                Top = 20
            };

            Label lbl_senha = new Label
            {
                Text = "Senha:",
                Left = (panel.Width - 200) / 2,
                Top = 80
            };

            TextBox txt_email = new TextBox
            {
                Width = 200,
                Left = (panel.Width - 200) / 2,
                Top = 45
            };

            TextBox txt_senha = new TextBox
            {
                Width = 200,
                Left = (panel.Width - 200) / 2,
                Top = 105
            };

            LinkLabel link_criar_conta = new LinkLabel
            {
                Text = "criar conta",
                Left = (panel.Width - 200) / 2,
                Top = 145
            };
            link_criar_conta.Click += (_, _) => FormCadastro();

            LinkLabel link_esqueci_senha = new LinkLabel
            {
                Text = "esqueci senha",
                Left = (panel.Width - 200) / 2 + 120,
                Top = 145
            };

            Button btn_login = new Button
            {
                Size = new Size(50, 30),
                Text = "Login",
                Left = (panel.Width - 50) / 2,
                Top = 190,
                Cursor = Cursors.Hand
            };

            btn_login.Click += (_, _) => btnLogin_Click(txt_email, txt_senha, authService);

            panel.Controls.Add(lbl_email);
            panel.Controls.Add(txt_email);

            panel.Controls.Add(lbl_senha);
            panel.Controls.Add(txt_senha);

            panel.Controls.Add(link_criar_conta);
            panel.Controls.Add(link_esqueci_senha);

            panel.Controls.Add(btn_login);

            this.Controls.Add(panel);
        }

        private void FormCadastro()
        {
            Form formDialog = new Form
            {
                Text = "Cadastrar",
                Size = new Size(350, 400),
                FormBorderStyle = FormBorderStyle.FixedSingle,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false
            };

            Label lbl_email = new Label
            {
                Width = 200,
                Text = "E-Mail:",
                Left = (formDialog.Width - 200) / 2,
                Top = 20
            };

            Label lbl_email_confirm = new Label
            {
                Width = 200,
                Text = "Confirme o E-Mail",
                Left = (formDialog.Width - 200) / 2,
                Top = 80
            };

            Label lbl_senha = new Label
            {
                Width = 200,
                Text = "Senha",
                Left = (formDialog.Width - 200) / 2,
                Top = 140
            };

            Label lbl_senha_confirm = new Label
            {
                Width = 200,
                Text = "Confirme a senha",
                Left = (formDialog.Width - 200) / 2,
                Top = 200
            };

            TextBox txt_email = new TextBox
            {
                Width = 200,
                Left = (formDialog.Width - 200) / 2,
                Top = 45
            };

            TextBox txt_email_confirm = new TextBox
            {
                Width = 200,
                Left = (formDialog.Width - 200) / 2,
                Top = 105
            };

            TextBox txt_senha = new TextBox
            {
                Width = 200,
                Left = (formDialog.Width - 200) / 2,
                Top = 165
            };

            TextBox txt_senha_confirm = new TextBox
            {
                Width = 200,
                Left = (formDialog.Width - 200) / 2,
                Top = 225
            };

            Button btn_cadastrar = new Button
            {
                Size = new Size(100, 30),
                Text = "Cadastrar",
                Left = (formDialog.Width - 100) / 2,
                Top = 280,
                Cursor = Cursors.Hand
            };
            btn_cadastrar.DialogResult = DialogResult.None;

            btn_cadastrar.Click += (_, _) =>
            {
                btnCadastrar_Click(txt_email, txt_senha, authService);
                formDialog.Close();
            };

            formDialog.Controls.Add(lbl_email);
            formDialog.Controls.Add(txt_email);

            formDialog.Controls.Add(lbl_email_confirm);
            formDialog.Controls.Add(txt_email_confirm);

            formDialog.Controls.Add(lbl_senha);
            formDialog.Controls.Add(txt_senha);

            formDialog.Controls.Add(lbl_senha_confirm);
            formDialog.Controls.Add(txt_senha_confirm);

            formDialog.Controls.Add(btn_cadastrar);

            formDialog.ShowDialog();
        }

        private void btnCadastrar_Click(TextBox txtLogin, TextBox txtSenha, AuthService auth)
        {
            if (!Validator.CamposValidos(txtLogin.Text, txtSenha.Text))
            {
                MessageBox.Show("Preencha todos os campos.");
                return;
            }

            bool sucesso = auth.Cadastrar(txtLogin.Text, txtSenha.Text);

            MessageBox.Show(sucesso ? "Usuário cadastrado." : "Usuário já existe.");
        }

        private void btnLogin_Click(TextBox txtLogin, TextBox txtSenha, AuthService auth)
        {
            bool sucesso = auth.Login(txtLogin.Text, txtSenha.Text);

            MessageBox.Show(sucesso ? "Login realizado." : "Login inválido.");
        }
    }
}

// classe que confere todos os parâmetros básicos de cadastro do usuário. É o que deve ser registrado e salvo na base.
public class Usuario
{
    public string Login { get; set; }
    public string Senha { get; set; }
}

// Simulação da base de usuários. A camada de userRepository é uma camada de dados, somente acessada pelo backend da aplicação
// qualquer coisa só pode ser registrada nos dados se passar pelas validações e pelo processo de autenticação.
public class UsuarioRepository
{
    private static List<Usuario> usuarios = new();

    public void Adicionar(Usuario usuario)
    {
        usuarios.Add(usuario);
    }

    public Usuario Buscar(string login)
    {
        return usuarios.FirstOrDefault(u => u.Login == login);
    }
}

// Isso aqui lida com o processo de autenticação. No caso, esta camada tem acesso aos dados da base.
public class AuthService
{
    private static UsuarioRepository Repository;

    public AuthService(UsuarioRepository repository)
    {
        Repository = repository;
    }

    public bool Cadastrar(string login, string senha)
    {
        if (Repository.Buscar(login) != null)
            return false;

        Repository.Adicionar(new Usuario
        {
            Login = login,
            Senha = senha
        });

        return true;
    }

    public bool Login(string login, string senha)
    {
        var usuario = Repository.Buscar(login);

        return usuario != null && usuario.Senha == senha;
    }
}

// isto aqui lida com a validação dos campos. É um recurso básico de frontend.
public class Validator
{
    public static bool CamposValidos(string login, string senha)
    {
        return !string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(senha);
    }
}