namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class MediaPlayer : Form
    {
        public static MediaPlayer Instance { get; private set; }
        public MediaPlayer()
        {
            Instance = this;
        }
    }
}

public class Components4
{
    public static Panel appControlPanel;
}