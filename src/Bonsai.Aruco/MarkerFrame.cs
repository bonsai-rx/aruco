﻿using System.Collections.Generic;
using Aruco.Net;

namespace Bonsai.Aruco
{
    public class MarkerFrame
    {
        public MarkerFrame(CameraParameters cameraParameters, IList<Marker> detectedMarkers)
        {
            CameraParameters = cameraParameters;
            DetectedMarkers = detectedMarkers;
        }

        public CameraParameters CameraParameters { get; private set; }

        public IList<Marker> DetectedMarkers { get; private set; }
    }
}
