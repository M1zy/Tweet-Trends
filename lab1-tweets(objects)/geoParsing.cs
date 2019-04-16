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
                return JsonConvert.DeserializeObject<Dictionary<string, List<List<List<double>>>>>(jsonstring);
            
        }
        
        public Dictionary<string, List<GMapPolygon>> Polygons()
        {
            Dictionary<string, List<List<List<double>>>> states = FromJson("states.json");
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
            Parsing parsing = new Parsing();
            Dictionary<string, List<GMapPolygon>>states = Polygons();
            List<Tweet> tweets = parsing.BuildingTweets(file);
            Dictionary<Tweet, Poly> moodstates = new Dictionary<Tweet, Poly>();
            foreach (Tweet t in tweets)
            {
               foreach(var v in states)
                {
                    for(int i = 0; i < v.Value.Count; i++)
                    {
                        if (v.Value[i].IsInside(t.latLng))
                        {

                            if (trends.AverageMood(t) >= -1 && trends.AverageMood(t) <= 1)
                            {
                                moodstates.Add(t, new Poly(v.Key, v.Value[i], trends.AverageMood(t))); break;
                            }
                           
                        }
                    }
                }
                continue;
            }
            return moodstates;
        }

        public Dictionary<GMapPolygon, double> AverageMood_OF_States(string file)
        {
            Dictionary<string, List<GMapPolygon>> polygons = Polygons();
            int count_OF_polygons = 0;
            foreach(var v in polygons)
            {
                count_OF_polygons += v.Value.Count;
            }

            Dictionary<GMapPolygon, double> states = new Dictionary<GMapPolygon, double>();
            Dictionary<GMapPolygon, int> count_tweets = new Dictionary<GMapPolygon, int>();

            Dictionary<Tweet, Poly> tweets =MoodOfStates(file);

                foreach(var v in tweets)
                {

                if (states.ContainsKey(v.Value.polygon)) { states[v.Value.polygon] += v.Value.mood; count_tweets[v.Value.polygon]++; }

                else { states.Add(v.Value.polygon, v.Value.mood);count_tweets.Add(v.Value.polygon, 1); }
     
                }

                foreach(var v in count_tweets) { states[v.Key]/=v.Value; }
            return states;

        }

        public void OutPutAllMoods(string file)
        {
            /*  Dictionary<string, List<double>> ave = AverageMood_OF_States(file);
              foreach(var v in ave)
              {
                  Console.WriteLine(v.Key);
                  for (int i = 0; i < v.Value.Count; i++)
                  {
                      Console.Write(v.Value[i] + " ");
                  }
                  Console.WriteLine("-------------------");
              }*/
            
          Dictionary<GMapPolygon, double> d = AverageMood_OF_States(file);
          foreach(var v in d) Console.WriteLine(v.Key + " " + v.Value );
           Dictionary<Tweet, Poly> z = MoodOfStates(file);
            foreach (var v in z) { Console.WriteLine(v.Key.text + " " + v.Value.mood + " " + v.Value.name); }
        }


    }
    public class Poly
    {
        public string name;
        public GMapPolygon polygon;
        public double mood;
        public Poly() { }
        public Poly(string name,GMapPolygon polygon,double mood)
        {
            this.name = name;this.polygon = polygon; this.mood = mood;
        }
    }




}
