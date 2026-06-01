using FontAwesome.Sharp;

// As libs abaixo são do VLC open source, instaladas via comando:
// dotnet add package LibVLCSharp.WinForms && dotnet add package VideoLAN.LibVLC.Windows
// são necessárias por oferecer suporte moderno a reprodução de mídias, melhor do que o nativo Windows Media Player.
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using Sistema_De_Aplicativos_Simples__.NET.appsForms;

namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class MediaPlayerForm : Form
    {
        public static MediaPlayerForm Instance { get; private set; }
        public MediaPlayerForm()
        {
            Instance = this;
            InitiateMediaPlayer();
            Components4.InitializeAppComponents();
        }

        private void InitiateMediaPlayer()
        {
            Text = "To Do List";
            Size = new Size(600, 400);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.FromArgb(62, 85, 85);
            // Instance.Resize += OnFormResize;
        }
    }
}

public class Components4
{
    public static Panel appControlPanel;
    public static IconButton menuBtn;
    public static IconButton playBtn;
    public static IconButton nextBtn;
    public static IconButton previousBtn;
    public static IconButton openFileBtn;
    public static IconButton mediaList; // na verdade, este botão deve abrir uma "bandeja" com uma lista dos itens a vizualizar.
    public static OpenFileDialog ofd; // deve ser associado ao openFileBtn

    // pictureViewer e videoViewer ficarão contido em panelViewer, que, por sua vez, ficará contido em appControlPanel 
    public static Panel panelViewer;
    public static PictureBox pictureViewer;
    public static VideoView videoViewer;


    public static void InitializeAppComponents()
    {
        InitializeControlPanel();
    }

    private static void InitializeControlPanel()
    {
        appControlPanel = new Panel
        {
            Size = new Size(600, 50),
            Top = MediaPlayerForm.Instance.Height - 120,
            BackColor = Color.FromArgb(29, 49, 49)
        };

        MediaPlayerForm.Instance.Controls.Add(appControlPanel);
    }
}