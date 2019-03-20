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

        public Dictionary<Tweet, Poly> MoodOfStates(string file)
        {
            Trends trends = new Trends();
            Tweet tweet = new Tweet();
            Dictionary<string, List<GMapPolygon>>states = Polygons("states.json");
            List<Tweet> tweets = tweet.BuildingTweets(file);
            Dictionary<Tweet, Poly> moodstates = new Dictionary<Tweet, Poly>();
            foreach (Tweet t in tweets)
            {
               foreach(var v in states)
                {
                    for(int i = 0; i < v.Value.Count; i++)
                    {
                        if (v.Value[i].IsInside(t.latLng))
                        {
                            moodstates.Add(t, new Poly(v.Key, i, trends.AverageMood(t)));break;
                        }
                    }
                }
                continue;
            }
            return moodstates;
        }

        public Dictionary<string, List<double>> AverageMood_OF_States(string file)
        {
            Dictionary<string, List<GMapPolygon>> polygons = Polygons("states.json");
            int countofpolygons = 0;
            foreach(var v in polygons)
            {
                countofpolygons += v.Value.Count;
            }
            double[] mood = new double[countofpolygons];
            int[] countofTweets = new int[countofpolygons];
            Dictionary<Tweet, Poly> tweets = MoodOfStates(file);
            for(int i = 0; i < countofpolygons; i++)
            {
                mood[i] = 0;countofTweets[i] = 0;
            }

            Dictionary<string, List<double>> states = new Dictionary<string, List<double>>();

            foreach(var v in tweets)
            {
                mood[NumberOfpolygonInGeneral(v.Value)] += v.Value.mood;
                countofTweets[NumberOfpolygonInGeneral(v.Value)]++;
            }
            double[] averageMoodofTweets = new double[countofpolygons];
            for(int i = 0; i < averageMoodofTweets.Length; i++)
            {
                averageMoodofTweets[i] = mood[i] / countofTweets[i];
            }
            int o = 0;
            foreach(var v in polygons)
            {
                List<double> moods = new List<double>();
                {
                    for(int i = 0; i < v.Value.Count; i++)
                    {
                        moods.Add(averageMoodofTweets[o]);o++;
                    }
                }
                states.Add(v.Key, moods);
                
            }
            return states;

        }

        public void OutPutAllMoods(string file)
        {
            Dictionary<string, List<double>> ave = AverageMood_OF_States(file);
            foreach(var v in ave)
            {
                Console.WriteLine(v.Key);
                for (int i = 0; i < v.Value.Count; i++)
                {
                    Console.Write(v.Value[i] + " ");
                }
                Console.WriteLine("-------------------");
            }
        }

        public int NumberOfpolygonInGeneral(Poly poly)
        {
            int number = 0;
            Dictionary<string, List<GMapPolygon>> p = Polygons("states.json");
            foreach (var v in p)
            {
                if (v.Key == poly.name) { number += poly.polygon; break; }
                else number += v.Value.Count;


            }
            return number;
        }
    }
    public class Poly
    {
        public string name;
        public int polygon;
        public double mood;
        public Poly() { }
        public Poly(string name,int polygon,double mood)
        {
            this.name = name;this.polygon = polygon; this.mood = mood;
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
