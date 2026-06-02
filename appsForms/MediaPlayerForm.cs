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
            Text = "Media Player";
            Size = new Size(600, 400);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.FromArgb(62, 85, 85);
            // Instance.Resize += OnFormResize; // configurar as dimensões dos componentes ao dar resize.
        }
    }
}

public class Components4
{
    public static Panel appControlPanel; // OK
    public static IconButton menuBtn;
    public static IconButton playBtn; // OK
    public static IconButton nextBtn; // OK
    public static IconButton previousBtn; // OK
    public static IconButton openFileBtn; // OK
    public static IconButton mediaList; // na verdade, este botão deve abrir uma "bandeja" com uma lista dos itens a vizualizar.

    // pictureViewer e videoViewer ficarão contido em panelViewer, que, por sua vez, ficará contido em appControlPanel 
    public static Panel panelViewer;

    public static LibVLC libVLC;
    public static MediaPlayer mediaPlayer;
    public static VideoView videoViewer;


    public static void InitializeAppComponents()
    {
        Core.Initialize();
        libVLC = new LibVLC();
        mediaPlayer = new MediaPlayer(libVLC);

        InitVideoPanel();
        InitVideoView();
        InitControlPanel();
        appControlPanel.BringToFront();
    }

    private static void InitVideoPanel()
    {
        panelViewer = new Panel
        {
            Size = new Size(600, 400),
            Dock = DockStyle.Fill,
            BackColor = Color.Black
        };

        MediaPlayerForm.Instance.Controls.Add(panelViewer);
    }

    private static void InitControlPanel()
    {
        appControlPanel = new Panel
        {
            Size = new Size(500, 60),
            Top = MediaPlayerForm.Instance.Height - 110,
            Left = MediaPlayerForm.Instance.Width / 2 - 255,
            BackColor = Color.FromArgb(29, 49, 49)
        };

        openFileBtn = new IconButton
        {
            Size = new Size(40, 40),
            Left = 10,
            Top = appControlPanel.Height / 2 - 20,
            IconChar = IconChar.Folder,
            IconFont = IconFont.Solid,
            IconColor = Color.FromArgb(62, 85, 85),
            ImageAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.FromArgb(29, 49, 49),
            BackColor = Color.FromArgb(29, 49, 49),
            FlatStyle = FlatStyle.Flat,
        };

        playBtn = new IconButton
        {
            Size = new Size(50, 50),
            Left = appControlPanel.Width / 2 - 25,
            Top = appControlPanel.Height / 2 - 25,
            IconChar = IconChar.Play,
            IconFont = IconFont.Solid,
            IconColor = Color.FromArgb(62, 85, 85),
            ImageAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.FromArgb(29, 49, 49),
            BackColor = Color.FromArgb(29, 49, 49),
            FlatStyle = FlatStyle.Flat,
        };

        nextBtn = new IconButton
        {
            Size = new Size(50, 50),
            Left = (appControlPanel.Width / 2 - 25) + 50,
            Top = appControlPanel.Height / 2 - 25,
            IconChar = IconChar.CaretRight,
            IconFont = IconFont.Solid,
            IconColor = Color.FromArgb(62, 85, 85),
            ImageAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.FromArgb(29, 49, 49),
            BackColor = Color.FromArgb(29, 49, 49),
            FlatStyle = FlatStyle.Flat,
        };

        previousBtn = new IconButton
        {
            Size = new Size(50, 50),
            Left = (appControlPanel.Width / 2 - 30) - 50,
            Top = appControlPanel.Height / 2 - 25,
            IconChar = IconChar.CaretLeft,
            IconFont = IconFont.Solid,
            IconColor = Color.FromArgb(62, 85, 85),
            ImageAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.FromArgb(29, 49, 49),
            BackColor = Color.FromArgb(29, 49, 49),
            FlatStyle = FlatStyle.Flat,
        };

        appControlPanel.Controls.Add(openFileBtn);
        appControlPanel.Controls.Add(playBtn);
        appControlPanel.Controls.Add(nextBtn);
        appControlPanel.Controls.Add(previousBtn);

        openFileBtn.Click += OpenFileBtn_Click;


        MediaPlayerForm.Instance.Controls.Add(appControlPanel);
    }

    private static void InitVideoView()
    {
        videoViewer = new VideoView
        {
            MediaPlayer = mediaPlayer,
            Size = new Size(600, 300),
            BackColor = Color.Black
        };

        MediaPlayerForm.Instance.Controls.Add(videoViewer);
        videoViewer.BringToFront();
    }

    private static void OpenFileBtn_Click(object sender, EventArgs e)
    {
        Environment.SpecialFolder myInitialDirectory = Environment.SpecialFolder.MyDocuments;

        // TODO: já que VLC suporta imagens, basta adicionar aqui no filtro também.
        using var dialog = new OpenFileDialog
        {
            Filter = "Video Files|*.mp4;*.avi;*.mkv|Audio Files|*.mp3;*.wav;*.flac|All Files|*.*",
            InitialDirectory = Environment.GetFolderPath(myInitialDirectory),
            Title = "Select a file to open",
            CheckFileExists = true,
            CheckPathExists = true,
            Multiselect = false, // se for true, seria interessante permitir selecionar vários arquivos e adicionar numa playlist local na memória.
            RestoreDirectory = true,
        };

        // TODO: tem que arrumar bug que faz o VLC continuar rodando mesmo após fechar tela do MediaPlayer.
        // TODO: tem que adicionar botão de volume ao vídeo.
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            var media = new Media(libVLC, new Uri(dialog.FileName));
            mediaPlayer.Volume = 50;
            mediaPlayer.Play(media);
        }
    }
}