using System.Windows.Controls;

namespace Lissajous
{
    /// <summary>
    /// Logica di interazione per LissaCircle.xaml
    /// </summary>
    public partial class LissaSetCircle : UserControl
    {
        public LissaSetCircle()
        {
            InitializeComponent();
        }

        public void SetGenerator(Generator generator)
        {
            this.LissaOnCircle.TheGenerator = generator;
            this.TopCircle.Lissajous = this.LissaOnCircle;
            this.RightCircle.Lissajous = this.LissaOnCircle;
        }

        public void RenderToGif(string path)
        {
            System.Diagnostics.Debug.WriteLine("Qui");
            this.RenderToGif(this.LissaOnCircle.TheGenerator, path, 12);
        }
    }
}
