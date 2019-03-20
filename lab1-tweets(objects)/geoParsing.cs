using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

namespace lab1_tweets_objects_
{

    public class geoParsing
    {


        public Dictionary<string, List<List<List<double>>>> FromJson(string json)
        {
            string jsonstring = new StreamReader(json).ReadToEnd();
                return JsonConvert.DeserializeObject<Dictionary<string, List<List<List<double>>>>>(jsonstring,lab1_tweets_objects_.Converter.Settings);
            
        }
        
        public Dictionary<string, List<GMapPolygon>> Polygons(string json)
        {
            Dictionary<string, List<List<List<double>>>> states = FromJson(json);
            Dictionary<string, List<GMapPolygon>> z = new Dictionary<string, List<GMapPolygon>>();

            foreach (var v in states)
            {
                List<GMapPolygon> poly = new List<GMapPolygon>();
                for (int i = 0; i < v.Value.Count; i++)
                {
                    List<PointLatLng> points = new List<PointLatLng>();
                    for (int y = 0; y < v.Value[i].Count; y++)
                    {
                        PointLatLng point = new PointLatLng(v.Value[i][y][1], v.Value[i][y][0]);
                        points.Add(point);
                    }
                    GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
                    poly.Add(polygon);
                }
                z.Add(v.Key, poly);
            }

            return z;
        }
        
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            }
        };
    }



}
