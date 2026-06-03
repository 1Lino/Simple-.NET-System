// As libs abaixo são do VLC open source, instaladas via comando:
// dotnet add package LibVLCSharp.WinForms && dotnet add package VideoLAN.LibVLC.Windows
// são necessárias por oferecer suporte moderno a reprodução de mídias, melhor do que o nativo Windows Media Player.
using FontAwesome.Sharp;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using Sistema_De_Aplicativos_Simples__.NET.appsForms;

namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public partial class MediaPlayerForm : Form
    {
        public static MediaPlayerForm Instance { get; private set; }
        private System.Windows.Forms.Timer inactivityTimer;
        private const int PollInterval = 100;
        private Point _lastMousePos;
        private DateTime _lastActivityTime = DateTime.Now;
        private bool _controlsVisible = true;
        private bool IsMaximized = false;

        public MediaPlayerForm()
        {
            Instance = this;

            InitiateMediaPlayer();
            Components4.InitializeAppComponents();

            this.FormClosed += OnFormClosed;
            this.Resize += OnFormResize;

            inactivityTimer = new System.Windows.Forms.Timer();
            inactivityTimer.Interval = PollInterval;
            inactivityTimer.Tick += InactivityTimer_Tick;
            inactivityTimer.Start();

            _lastMousePos = Cursor.Position;
        }

        private void OnFormResize(object sender, EventArgs e)
        {
            if (!IsMaximized)
            {
                IsMaximized = !IsMaximized;
                ResizeComponents(new Size(800, 60), MediaPlayerForm.Instance.Width / 2 - 400);

                return;
            }

            IsMaximized = !IsMaximized;
            ResizeComponents(new Size(500, 60), MediaPlayerForm.Instance.Width / 2 - 255);

            return;
        }

        private static void ResizeComponents(Size panelSize, int panelLeft)
        {
            Components4.appControlPanel.Size = panelSize;
            Components4.appControlPanel.Left = panelLeft;
            Components4.appControlPanel.Top = MediaPlayerForm.Instance.Height - 110;

            Components4.seekBar.Left = Components4.appControlPanel.Width / 2 - (Components4.seekBar.Width / 2);
            Components4.previousBtn.Left = (Components4.appControlPanel.Width / 2 - 30) - 40;
            Components4.playBtn.Left = Components4.appControlPanel.Width / 2 - 15;
            Components4.nextBtn.Left = (Components4.appControlPanel.Width / 2 - 15) + 50;
            Components4.volumeBtn.Left = Components4.appControlPanel.Width - 160;
            Components4.volumeBar.Left = Components4.appControlPanel.Width - 115;
        }

        private void InactivityTimer_Tick(object sender, EventArgs e)
        {
            if (Components4.videoViewer == null)
                return;

            Point current = Cursor.Position;

            Rectangle videoRect = Components4.videoViewer.RectangleToScreen(Components4.videoViewer.ClientRectangle);

            bool mouseOverVideo = videoRect.Contains(current);

            if (current != _lastMousePos)
            {
                _lastMousePos = current;

                if (mouseOverVideo)
                {
                    _lastActivityTime = DateTime.Now;

                    if (!_controlsVisible)
                    {
                        Components4.appControlPanel.Visible = true;
                        Components4.appControlPanel.BringToFront();

                        _controlsVisible = true;
                    }
                }
            }

            if (_controlsVisible && DateTime.Now - _lastActivityTime > TimeSpan.FromSeconds(2))
            {
                Components4.appControlPanel.Visible = false;
                _controlsVisible = false;
            }
        }

        private void InitiateMediaPlayer()
        {
            Text = "Media Player";
            Size = new Size(600, 400);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.FromArgb(62, 85, 85);
        }

        private static void OnFormClosed(object sender, EventArgs e)
        {
            Components4.mediaPlayer?.Stop();
            Components4.mediaPlayer?.Dispose();
        }
    }
}

public class Components4
{
    // public static IconButton mediaList;
    public static TrackBar seekBar;
    public static Label timeLabel;
    private static System.Windows.Forms.Timer mediaTimer;
    public static Panel appControlPanel;
    public static IconButton playBtn;
    public static IconButton nextBtn;
    public static IconButton previousBtn;
    public static IconButton openFileBtn;
    public static IconButton volumeBtn;
    public static TrackBar volumeBar;
    private static int previousVolume = 50;
    public static LibVLC libVLC;
    public static MediaPlayer mediaPlayer;
    public static VideoView videoViewer;

    public static void InitializeAppComponents()
    {
        Core.Initialize();

        libVLC = new LibVLC();

        mediaPlayer = new MediaPlayer(libVLC)
        {
            Volume = 50
        };

        InitVideoView();
        InitControlPanel();
        InitMediaTimer(); // timer do track de vídeo

        appControlPanel.BringToFront();
        timeLabel.BringToFront();

        mediaPlayer.EndReached += MediaPlayer_EndReached;
    }

    private static void InitVideoView()
    {
        videoViewer = new VideoView
        {
            MediaPlayer = mediaPlayer,
            Dock = DockStyle.Fill,
            BackColor = Color.Black
        };

        MediaPlayerForm.Instance.Controls.Add(videoViewer);
        videoViewer.BringToFront();
    }

    private static void MediaPlayer_EndReached(object sender, EventArgs e)
    {
        MediaPlayerForm.Instance.BeginInvoke(() =>
        {
            mediaPlayer.Position = 0;
            mediaPlayer.Stop();

            playBtn.IconChar = IconChar.Play;
        });
    }

    private static void InitMediaTimer()
    {
        mediaTimer = new System.Windows.Forms.Timer();
        mediaTimer.Interval = 500;
        mediaTimer.Tick += MediaTimer_Tick;
        mediaTimer.Start();
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

        seekBar = new TrackBar
        {
            Width = appControlPanel.Width - 20,
            Left = 10,
            Top = -10,
            Minimum = 0,
            Maximum = 1000,
            TickStyle = TickStyle.None
        };

        timeLabel = new Label
        {
            AutoSize = true,
            ForeColor = Color.White,
            Text = "00:00 / 00:00",
            Left = 60,
            Top = appControlPanel.Height - 35,
            BackColor = Color.Transparent
        };

        volumeBar = new TrackBar
        {
            Width = 120,
            Height = 30,
            Left = appControlPanel.Width - 125,
            Top = appControlPanel.Height / 2 - 15,
            Minimum = 0,
            Maximum = 100,
            Value = 50,
            TickStyle = TickStyle.None
        };

        openFileBtn = new IconButton();
        SetIconBtnProps(openFileBtn, new Size(40, 40), 10, appControlPanel.Height / 2 - 20, IconChar.Folder);

        playBtn = new IconButton();
        SetIconBtnProps(playBtn, new Size(50, 50), appControlPanel.Width / 2 - 25, appControlPanel.Height / 2 - 25, IconChar.Play);

        nextBtn = new IconButton();
        SetIconBtnProps(nextBtn, new Size(50, 50), (appControlPanel.Width / 2 - 25) + 50, appControlPanel.Height / 2 - 25, IconChar.CaretRight);

        previousBtn = new IconButton();
        SetIconBtnProps(previousBtn, new Size(50, 50), (appControlPanel.Width / 2 - 30) - 50, appControlPanel.Height / 2 - 25, IconChar.CaretLeft);

        volumeBtn = new IconButton();
        SetIconBtnProps(volumeBtn, new Size(40, 40), appControlPanel.Width - 170, appControlPanel.Height / 2 - 20, IconChar.VolumeHigh);
        volumeBtn.FlatAppearance.BorderSize = 0;

        appControlPanel.Controls.Add(openFileBtn);
        appControlPanel.Controls.Add(playBtn);
        appControlPanel.Controls.Add(nextBtn);
        appControlPanel.Controls.Add(previousBtn);

        appControlPanel.Controls.Add(volumeBtn);
        appControlPanel.Controls.Add(volumeBar);
        appControlPanel.Controls.Add(seekBar);
        appControlPanel.Controls.Add(timeLabel);

        openFileBtn.Click += OpenFileBtn_Click;
        playBtn.Click += PlayBtn_Click;
        volumeBtn.Click += VolumeBtn_Click;
        volumeBar.Scroll += VolumeBar_Scroll;
        seekBar.MouseUp += SeekBar_MouseUp;

        MediaPlayerForm.Instance.Controls.Add(appControlPanel);
    }

    private static void SetIconBtnProps(IconButton IconBtn, Size size, int left, int top, IconChar iconChar)
    {
        IconBtn.Size = size;
        IconBtn.Left = left;
        IconBtn.Top = top;
        IconBtn.IconChar = iconChar;
        IconBtn.IconFont = IconFont.Solid;
        IconBtn.IconColor = Color.FromArgb(62, 85, 85);
        IconBtn.ForeColor = Color.FromArgb(29, 49, 49);
        IconBtn.BackColor = Color.FromArgb(29, 49, 49);
        IconBtn.FlatStyle = FlatStyle.Flat;
    }

    private static void SeekBar_MouseUp(object sender, MouseEventArgs e)
    {
        if (mediaPlayer == null) return;

        mediaPlayer.Position = seekBar.Value / 1000f;
    }

    private static void MediaTimer_Tick(object sender, EventArgs e)
    {
        if (mediaPlayer == null) return;

        if (mediaPlayer.Length <= 0) return;

        seekBar.Value = (int)(mediaPlayer.Position * 1000);

        TimeSpan current = TimeSpan.FromMilliseconds(mediaPlayer.Time);

        TimeSpan total = TimeSpan.FromMilliseconds(mediaPlayer.Length);

        timeLabel.Text = $"{current:mm\\:ss} / {total:mm\\:ss}";
    }

    private static void PlayBtn_Click(object sender, EventArgs e)
    {
        if (mediaPlayer.Media == null) return;

        if (mediaPlayer.IsPlaying)
        {
            mediaPlayer.Pause();
            playBtn.IconChar = IconChar.Play;
        }
        else
        {
            mediaPlayer.Play();
            playBtn.IconChar = IconChar.Pause;
        }
    }

    private static void VolumeBar_Scroll(object sender, EventArgs e)
    {
        mediaPlayer.Volume = volumeBar.Value;
        UpdateVolumeIcon();
    }

    private static void VolumeBtn_Click(object sender, EventArgs e)
    {
        if (volumeBar.Value > 0)
        {
            previousVolume = volumeBar.Value;
            volumeBar.Value = 0;
            mediaPlayer.Volume = 0;
        }
        else
        {
            volumeBar.Value = previousVolume;
            mediaPlayer.Volume = previousVolume;
        }

        UpdateVolumeIcon();
    }

    private static void UpdateVolumeIcon()
    {
        int volume = volumeBar.Value;

        if (volume == 0)
            volumeBtn.IconChar = IconChar.VolumeMute;
        else if (volume < 40)
            volumeBtn.IconChar = IconChar.VolumeLow;
        else
            volumeBtn.IconChar = IconChar.VolumeHigh;
    }

    private static void OpenFileBtn_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "Video Files|*.mp4;*.avi;*.mkv|Audio Files|*.mp3;*.wav;*.flac|Image Files|*.jpeg;*.png;*.jpg|All Files|*.*",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            Title = "Select a file to open",
            CheckFileExists = true,
            CheckPathExists = true,
            Multiselect = false,
            RestoreDirectory = true
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            var media = new Media(libVLC, new Uri(dialog.FileName));

            volumeBar.Value = 50;
            mediaPlayer.Volume = 50;

            UpdateVolumeIcon();

            mediaPlayer.Play(media);

            playBtn.IconChar = IconChar.Pause;
        }
    }
}