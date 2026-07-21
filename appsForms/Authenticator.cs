
// NOTE: Esta interface servirá apenas para dar suporte visual das operações de autenticação. O foco deste projeto é autenticação,
// então será feito uso do prompt de comando para printar os resultados, e também messageboxes no form. Basicamente o Usuário tentará
// criar um cadastro, e poderá então acessar com senha e usuário o sistema (somente isto, para o nível básico).
namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class Authenticator : Form
    {
        public static Authenticator Instance { get; private set; }

        public Authenticator()
        {
            Instance = this;
            InitializeForm();
            InitializeComponents();
            this.CenterToScreen();
        }

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
                btn_cadastrar.DialogResult = DialogResult.OK;
                MessageBox.Show("Cadastrado!");
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
    }
}