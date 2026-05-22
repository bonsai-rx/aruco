using Aruco.Net;
using Bonsai;
using Bonsai.Aruco.Design;
using Bonsai.Design;

[assembly: TypeVisualizer(typeof(MarkerVisualizer), Target = typeof(Marker))]

namespace Bonsai.Aruco.Design
{
    /// <summary>
    /// Provides a type visualizer that displays a textual representation of a <see cref="Marker"/>.
    /// </summary>
    public class MarkerVisualizer : ObjectTextVisualizer
    {
    }
}
