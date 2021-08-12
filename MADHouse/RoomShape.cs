using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MADHouse
{
    class RoomShape  : Shape
    {
        protected override Geometry DefiningGeometry
        {
            get { return GenerateBaseRoom(70, 100); }
        }

        private Geometry GenerateBaseRoom(int length, int width)
        {
            StreamGeometry geom = new StreamGeometry();
            using (StreamGeometryContext gc = geom.Open())
            {
                // isFilled = false, isClosed = true
                gc.BeginFigure(new Point(50.0, 50.0), false, true);
                gc.LineTo(new Point(50.0, 50.0 + length), false, true);
                gc.LineTo(new Point(50.0-width, 50.0 + length), false, true);
                gc.LineTo(new Point(50.0-width, 50.0 ), false, true);

            }

            return geom;
        }
    }
}
