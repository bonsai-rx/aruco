using Aruco.Net;
using Bonsai;
using Bonsai.Aruco.Design;
using Bonsai.Design;

[assembly: TypeVisualizer(typeof(MarkerVisualizer), Target = typeof(Marker))]

namespace Bonsai.Aruco.Design
{
    public class MarkerVisualizer : ObjectTextVisualizer
    {
    }
}
