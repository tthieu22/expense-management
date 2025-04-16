using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json.Linq;

public class MapHandler
{
    private string apiKey;
    private GMapControl gMap;
    private GMapOverlay markersOverlay;

    public MapHandler(GMapControl mapControl, string apiKey)
    {
        this.gMap = mapControl;
        this.apiKey = apiKey;
        this.markersOverlay = new GMapOverlay("markers");
        gMap.Overlays.Add(markersOverlay);
        InitializeMap();
    }

    private void InitializeMap()
    {
        gMap.MapProvider = GMapProviders.GoogleMap;
        gMap.MinZoom = 2;
        gMap.MaxZoom = 20;
        gMap.Zoom = 15;
        gMap.ShowCenter = false;
        gMap.DragButton = System.Windows.Forms.MouseButtons.Left;
    }

    public Tuple<double, double> GetCurrentLocation()
    {
        try
        {
            string url = $"https://www.googleapis.com/geolocation/v1/geolocate?key={apiKey}";
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                string json = client.UploadString(url, "{}");
                JObject data = JObject.Parse(json);

                double lat = (double)data["location"]["lat"];
                double lng = (double)data["location"]["lng"];

                return new Tuple<double, double>(lat, lng);
            }
        }
        catch
        {
            return null;
        }
    }

    public string GetAddressFromCoordinates(double lat, double lng)
    {
        try
        {
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{lng}&key={apiKey}";
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                string json = client.DownloadString(url);
                JObject data = JObject.Parse(json);

                if (data["status"].ToString() == "OK")
                {
                    return (string)data["results"][0]["formatted_address"];
                }
            }
        }
        catch { }

        return "Không xác định được địa chỉ";
    }

    public Tuple<double, double> GetCoordinatesFromAddress(string address)
    {
        try
        {
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={apiKey}";
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                string json = client.DownloadString(url);
                JObject data = JObject.Parse(json);

                if (data["status"].ToString() == "OK")
                {
                    double lat = (double)data["results"][0]["geometry"]["location"]["lat"];
                    double lng = (double)data["results"][0]["geometry"]["location"]["lng"];
                    return new Tuple<double, double>(lat, lng);
                }
            }
        }
        catch { }

        return null;
    }

    public void AddMarker(double lat, double lng, string title)
    {
        markersOverlay.Markers.Clear();
        GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.red_dot);
        marker.ToolTipText = title;
        marker.ToolTipMode = MarkerTooltipMode.Always;
        markersOverlay.Markers.Add(marker);
    }

    public void SetPosition(double lat, double lng)
    {
        gMap.Position = new PointLatLng(lat, lng);
    }
    public List<string> GetAddressSuggestions(string input)
    {
        List<string> suggestions = new List<string>();

        string url = $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={Uri.EscapeDataString(input)}&language=vi&key={apiKey}";
        using (WebClient client = new WebClient())
        {
            client.Encoding = Encoding.UTF8;
            string json = client.DownloadString(url);
            JObject data = JObject.Parse(json);

            if (data["status"].ToString() == "OK")
            {
                foreach (var item in data["predictions"])
                {
                    suggestions.Add(item["description"].ToString());
                }
            }
        }

        return suggestions;
    }

}
