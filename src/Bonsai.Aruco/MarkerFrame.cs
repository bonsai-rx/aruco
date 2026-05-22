using System.Collections.Generic;
using Aruco.Net;

namespace Bonsai.Aruco
{
    /// <summary>
    /// Represents the markers detected in an image, together with the associated camera parameters.
    /// </summary>
    public class MarkerFrame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkerFrame"/> class with the specified
        /// camera parameters and detected markers.
        /// </summary>
        /// <param name="cameraParameters">
        /// The camera parameters, or <see langword="null"/> if no calibration was provided.
        /// </param>
        /// <param name="detectedMarkers">The markers detected in the image.</param>
        public MarkerFrame(CameraParameters cameraParameters, IList<Marker> detectedMarkers)
        {
            CameraParameters = cameraParameters;
            DetectedMarkers = detectedMarkers;
        }

        /// <summary>
        /// Gets the camera parameters, or <see langword="null"/> if no calibration was provided.
        /// </summary>
        public CameraParameters CameraParameters { get; private set; }

        /// <summary>
        /// Gets the markers detected in the image.
        /// </summary>
        public IList<Marker> DetectedMarkers { get; private set; }
    }
}
