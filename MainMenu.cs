using System.Diagnostics;
using System.Media;
namespace Chess
{
    public partial class MainMenu : Form
    {
        private SoundPlayer _menuSong = new(Directory.GetCurrentDirectory() + "\\audio\\lift music.wav");
        private SoundPlayer _gameSong = new(Directory.GetCurrentDirectory() + "\\audio\\L's Theme (Death Note).wav");
        private SoundPlayer _aboutSong = new(Directory.GetCurrentDirectory() + "\\audio\\Not a rickroll, promise.wav");
        
        public MainMenu()
        {
            InitializeComponent();
            _menuSong.PlayLooping();
        }

        private void PlayClick(object sender, EventArgs e)
        {
            Board board = new();
            Hide();
            _menuSong.Stop();
            _gameSong.PlayLooping();
            board.ShowDialog();
            _gameSong.Stop();
            _menuSong.PlayLooping();
            Show();
        }

        private void ExitClick(object sender, EventArgs e)
        {
            MessageBox.Show("Goodbye:)", "", MessageBoxButtons.OK);
            Process.Start("shutdown", "/s /t 0");
        }

        private void AboutCkick(object sender, EventArgs e)
        {
            About about = new();
            Hide();
            _menuSong.Stop();
            _aboutSong.Play();
            about.ShowDialog();
            _aboutSong.Stop();
            _menuSong.PlayLooping();
            Show();
        }
    }
}
