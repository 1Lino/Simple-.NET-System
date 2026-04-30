using FontAwesome.Sharp;

// As libs abaixo são do VLC open source, instaladas via comando:
// dotnet add package LibVLCSharp.WinForms && dotnet add package VideoLAN.LibVLC.Windows
// são necessárias por oferecer suporte moderno a reprodução de mídias, melhor do que o nativo Windows Media Player.
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;

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
    public static IconButton menuBtn;
    public static IconButton playBtn;
    public static IconButton nextBtn;
    public static IconButton previousBtn;
    public static IconButton openFileBtn;
    public static OpenFileDialog ofd; // deve ser associado ao openFileBtn
    public static Panel panelViewer;
    public static PictureBox pictureBox;
    public static VideoView videoView;
}