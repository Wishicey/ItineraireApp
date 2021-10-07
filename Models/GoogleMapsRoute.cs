using System;
using System.Collections.Generic;
using System.Text;

namespace ItineraireApp.Models
{
    //Classes auto-générées à partir de la réponse JSON de l'API Google Maps Directions
    public class GoogleMapsRoute
    {
        public class GeocodedWaypoint
        {
            public GeocodedWaypoint(
                string geocoder_status,
                string place_id,
                List<string> types
            )
            {
                this.geocoder_status = geocoder_status;
                this.place_id = place_id;
                this.types = types;
            }

            public string geocoder_status { get; }
            public string place_id { get; }
            public List<string> types { get; }
        }

        public class Northeast
        {
            public Northeast(
                double lat,
                double lng
            )
            {
                this.lat = lat;
                this.lng = lng;
            }

            public double lat { get; }
            public double lng { get; }
        }

        public class Southwest
        {
            public Southwest(
                double lat,
                double lng
            )
            {
                this.lat = lat;
                this.lng = lng;
            }

            public double lat { get; }
            public double lng { get; }
        }

        public class Bounds
        {
            public Bounds(
                Northeast northeast,
                Southwest southwest
            )
            {
                this.northeast = northeast;
                this.southwest = southwest;
            }

            public Northeast northeast { get; }
            public Southwest southwest { get; }
        }

        public class Distance
        {
            public Distance(
                string text,
                int value
            )
            {
                this.text = text;
                this.value = value;
            }

            public string text { get; }
            public int value { get; }
        }

        public class Duration
        {
            public Duration(
                string text,
                int value
            )
            {
                this.text = text;
                this.value = value;
            }

            public string text { get; }
            public int value { get; }
        }

        public class EndLocation
        {
            public EndLocation(
                double lat,
                double lng
            )
            {
                this.lat = lat;
                this.lng = lng;
            }

            public double lat { get; }
            public double lng { get; }
        }

        public class StartLocation
        {
            public StartLocation(
                double lat,
                double lng
            )
            {
                this.lat = lat;
                this.lng = lng;
            }

            public double lat { get; }
            public double lng { get; }
        }

        public class Polyline
        {
            public Polyline(
                string points
            )
            {
                this.points = points;
            }

            public string points { get; }
        }

        public class Step
        {
            public Step(
                Distance distance,
                Duration duration,
                EndLocation end_location,
                string html_instructions,
                Polyline polyline,
                StartLocation start_location,
                string travel_mode,
                string maneuver
            )
            {
                this.distance = distance;
                this.duration = duration;
                this.end_location = end_location;
                this.html_instructions = html_instructions;
                this.polyline = polyline;
                this.start_location = start_location;
                this.travel_mode = travel_mode;
                this.maneuver = maneuver;
            }

            public Distance distance { get; }
            public Duration duration { get; }
            public EndLocation end_location { get; }
            public string html_instructions { get; }
            public Polyline polyline { get; }
            public StartLocation start_location { get; }
            public string travel_mode { get; }
            public string maneuver { get; }
        }

        public class Leg
        {
            public Leg(
                Distance distance,
                Duration duration,
                string end_address,
                EndLocation end_location,
                string start_address,
                StartLocation start_location,
                List<Step> steps,
                List<object> traffic_speed_entry,
                List<object> via_waypoint
            )
            {
                this.distance = distance;
                this.duration = duration;
                this.end_address = end_address;
                this.end_location = end_location;
                this.start_address = start_address;
                this.start_location = start_location;
                this.steps = steps;
                this.traffic_speed_entry = traffic_speed_entry;
                this.via_waypoint = via_waypoint;
            }

            public Distance distance { get; }
            public Duration duration { get; }
            public string end_address { get; }
            public EndLocation end_location { get; }
            public string start_address { get; }
            public StartLocation start_location { get; }
            public List<Step> steps { get; }
            public List<object> traffic_speed_entry { get; }
            public List<object> via_waypoint { get; }
        }

        public class OverviewPolyline
        {
            public OverviewPolyline(
                string points
            )
            {
                this.points = points;
            }

            public string points { get; }
        }

        public class Route
        {
            public Route(
                Bounds bounds,
                string copyrights,
                List<Leg> legs,
                OverviewPolyline overview_polyline,
                string summary,
                List<object> warnings,
                List<int> waypoint_order
            )
            {
                this.bounds = bounds;
                this.copyrights = copyrights;
                this.legs = legs;
                this.overview_polyline = overview_polyline;
                this.summary = summary;
                this.warnings = warnings;
                this.waypoint_order = waypoint_order;
            }

            public Bounds bounds { get; }
            public string copyrights { get; }
            public List<Leg> legs { get; }
            public OverviewPolyline overview_polyline { get; }
            public string summary { get; }
            public List<object> warnings { get; }
            public List<int> waypoint_order { get; }
        }

        public class Root
        {
            public Root(
                List<GeocodedWaypoint> geocoded_waypoints,
                List<Route> routes,
                string status
            )
            {
                this.geocoded_waypoints = geocoded_waypoints;
                this.routes = routes;
                this.status = status;
            }

            public List<GeocodedWaypoint> geocoded_waypoints { get; }
            public List<Route> routes { get; }
            public string status { get; }
        }
    }
}
