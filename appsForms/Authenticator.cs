using Sistema_De_Aplicativos_Simples__.NET.appsForms;


// NOTE: Esta interface servirá apenas para dar suporte visual das operações de autenticação. O foco deste projeto é autenticação,
// então será feito uso do prompt de comando para printar os resultados, e também messageboxes no form. Basicamente o Usuário tentará
// acessar um sistema fictício utilizando uma senha, ou então criar um novo cadastro, etc.
namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class Authenticator : Form
    {
        public static Authenticator Instance { get; private set; }

        public Authenticator()
        {
            Instance = this;
            InitializeForm();
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
    }
}