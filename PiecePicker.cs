namespace Chess
{
    public partial class PiecePicker : Form
    {
        public PiecePicker() => InitializeComponent();
        
        public Transformations Transformations { get; private set; }
        
        private void Pick(object sender, EventArgs e)
        {
            if (_queen.Checked)
                Transformations = Transformations.Queen;
            else if (_knight.Checked)
                Transformations = Transformations.Knight;
            else if (_castle.Checked)
                Transformations = Transformations.Castle;
            else if (_bishop.Checked)
                Transformations = Transformations.Bishop;
            Close();
        }
    }
}
