using Aruco.Net;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;

namespace Bonsai.Aruco
{
    /// <summary>
    /// Represents an operator that selects the marker with the specified id from the input frame,
    /// or an invalid marker if no match is found.
    /// </summary>
    [Description("Selects the marker with the specified id from the input frame, or an invalid marker if no match is found.")]
    public class SelectMarker : Transform<MarkerFrame, Marker>
    {
        /// <summary>
        /// Gets or sets the id of the marker to select.
        /// </summary>
        [Description("The id of the marker to select.")]
        public int Id { get; set; }

        /// <summary>
        /// Selects the marker with the specified <see cref="Id"/> from each input frame,
        /// or returns an invalid marker if no match is found.
        /// </summary>
        /// <param name="source">The sequence of marker frames to filter.</param>
        /// <returns>
        /// A sequence of <see cref="Marker"/> values containing the selected marker from
        /// each input frame, or <see cref="Marker.Empty"/> if no match is found.
        /// </returns>
        public override IObservable<Marker> Process(IObservable<MarkerFrame> source)
        {
            return source.Select(input =>
            {
                var selectedMarker = input.DetectedMarkers.FirstOrDefault(marker => marker.Id == Id);
                return selectedMarker ?? Marker.Empty;
            });
        }
    }
}
