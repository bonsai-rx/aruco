using Aruco.Net;
using Bonsai;
using Bonsai.Aruco.Design;
using Bonsai.Design;
using Bonsai.Vision.Design;
using OpenCV.Net;
using System;

[assembly: TypeVisualizer(typeof(MarkerOverlay), Target = typeof(MashupSource<IplImageVisualizer, MarkerVisualizer>))]

namespace Bonsai.Aruco.Design
{
    /// <summary>
    /// Provides a type visualizer that overlays a marker over an existing image visualizer.
    /// </summary>
    public class MarkerOverlay : DialogTypeVisualizer
    {
        IplImageVisualizer visualizer;

        /// <inheritdoc/>
        public override void Show(object value)
        {
            var marker = (Marker)value;
            var visualizerImage = visualizer.VisualizerImage;
            if (visualizerImage != null && marker != null && marker.IsValid)
            {
                marker.Draw(visualizerImage, Scalar.Rgb(0, 0, 255), 2, true);
            }
        }

        /// <inheritdoc/>
        public override void Load(IServiceProvider provider)
        {
            visualizer = (IplImageVisualizer)provider.GetService(typeof(MashupVisualizer));
        }

        /// <inheritdoc/>
        public override void Unload()
        {
        }
    }
}
