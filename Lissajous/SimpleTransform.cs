using System.Windows;

namespace Lissajous
{
    public class SimpleTransform
    {
        private double translateX;
        private double translateY;
        private double scaleX;
        private double scaleY;
        public SimpleTransform(double translateX, double translateY, double scaleX, double scaleY)
        {
            this.translateX = translateX;
            this.translateY = translateY;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
        }

        public Point Direct(Point pt)
        {
            double x = this.DirectX(pt.X);
            double y = this.DirectY(pt.Y);
            return new Point(x, y);
        }

        public double DirectX(double x)
        {
            return x * this.scaleX + this.translateX;
        }

        public double DirectY(double y)
        {
            return y * this.scaleY + this.translateY;
        }
    }
}
