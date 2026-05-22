using System;
using System.Linq;
using OpenCV.Net;
using Aruco.Net;
using System.ComponentModel;
using System.Reactive.Linq;
using System.IO;

namespace Bonsai.Aruco
{
    /// <summary>
    /// Represents an operator that detects planar fiducial markers in the input image sequence.
    /// </summary>
    [Description("Detects planar fiducial markers in the input image sequence.")]
    public class DetectMarkers : Transform<IplImage, MarkerFrame>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetectMarkers"/> class.
        /// </summary>
        public DetectMarkers()
        {
            Param1 = 7.0;
            Param2 = 7.0;
            MinSize = 0.04f;
            MaxSize = 0.5f;
            ThresholdMethod = ThresholdMethod.AdaptiveThreshold;
            CornerRefinement = CornerRefinementMethod.Lines;
            MarkerSize = 10;
        }

        /// <summary>
        /// Gets or sets the name of the YAML file storing the camera calibration parameters.
        /// </summary>
        [FileNameFilter("YAML Files (*.yml)|*.yml|All Files (*.*)|*.*")]
        [Description("The name of the YAML file storing the camera calibration parameters.")]
        [Editor("Bonsai.Design.OpenFileNameEditor, Bonsai.Design", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string CameraParameters { get; set; }

        /// <summary>
        /// Gets or sets the threshold method used for marker detection.
        /// </summary>
        [Description("The threshold method used for marker detection.")]
        public ThresholdMethod ThresholdMethod { get; set; }

        /// <summary>
        /// Gets or sets the first parameter of the threshold method.
        /// </summary>
        [Description("The first parameter of the threshold method.")]
        public double Param1 { get; set; }

        /// <summary>
        /// Gets or sets the second parameter of the threshold method.
        /// </summary>
        [Description("The second parameter of the threshold method.")]
        public double Param2 { get; set; }

        /// <summary>
        /// Gets or sets the minimum marker size as a fraction of the image size.
        /// </summary>
        [Description("The minimum marker size as a fraction of the image size.")]
        public float MinSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum marker size as a fraction of the image size.
        /// </summary>
        [Description("The maximum marker size as a fraction of the image size.")]
        public float MaxSize { get; set; }

        /// <summary>
        /// Gets or sets the method used to refine marker corners.
        /// </summary>
        [Description("The method used to refine marker corners.")]
        public CornerRefinementMethod CornerRefinement { get; set; }

        /// <summary>
        /// Gets or sets the size of the marker sides, in meters.
        /// </summary>
        [Description("The size of the marker sides, in meters.")]
        public float MarkerSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the Y axis as the marker normal.
        /// </summary>
        [Description("True to use the Y axis as the marker normal; otherwise the Z axis is used.")]
        public bool SetYPerpendicular { get; set; }

        /// <summary>
        /// Detects planar fiducial markers in an observable sequence of images.
        /// </summary>
        /// <param name="source">The sequence of images to scan for markers.</param>
        /// <returns>
        /// A sequence of <see cref="MarkerFrame"/> values containing the markers
        /// detected in each input image.
        /// </returns>
        public override IObservable<MarkerFrame> Process(IObservable<IplImage> source)
        {
            return Observable.Using(
                () => new MarkerDetector(),
                detector =>
                {
                    CameraParameters parameters = null;
                    Mat cameraMatrix = null;
                    Mat distortion = null;

                    var parametersFileName = CameraParameters;
                    if (!string.IsNullOrEmpty(parametersFileName))
                    {
                        if (!File.Exists(parametersFileName))
                        {
                            throw new InvalidOperationException("Failed to open the camera parameters at the specified path.");
                        }

                        cameraMatrix = new Mat(3, 3, Depth.F32, 1);
                        distortion = new Mat(1, 4, Depth.F32, 1);
                        parameters = new CameraParameters();
                        parameters.ReadFromXmlFile(parametersFileName);
                        parameters.CopyParameters(cameraMatrix, distortion, out _);
                    }

                    return source.Select(input =>
                    {
                        detector.ThresholdMethod = ThresholdMethod;
                        detector.Param1 = Param1;
                        detector.Param2 = Param2;
                        detector.MinSize = MinSize;
                        detector.MaxSize = MaxSize;
                        detector.CornerRefinement = CornerRefinement;

                        var detectedMarkers = detector.Detect(input, cameraMatrix, distortion, MarkerSize, SetYPerpendicular);
                        return new MarkerFrame(parameters, detectedMarkers);
                    });
                });
        }
    }
}
