using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using Newtonsoft.Json;

namespace lab1_tweets_objects_
{
   

    public class Parsing
    {
        string[] daysofWeek = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

 
        public List<Tweet> BuildingTweets(string file)
        {
            Parsing p = new Parsing();
            List<Tweet> tweets = new List<Tweet>();
            string tweet = "";
            StreamReader reader = new StreamReader(file + ".txt");
            while ((tweet = reader.ReadLine()) != null)
            {
                string[] parsingtweet = tweet.Split(new string[] { "	" }, StringSplitOptions.RemoveEmptyEntries);
                if (parsingtweet.Length == 4)
                {
                    string[] Coordinates = parsingtweet[0].Split(new string[] { "[", "]", "," }, StringSplitOptions.RemoveEmptyEntries);
                    Coordinates[0] = Coordinates[0].Replace('.', ','); Coordinates[1] = Coordinates[1].Replace('.', ',');
                    DateTime date = DateTime.ParseExact(parsingtweet[2], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    PointLatLng latLng = new PointLatLng(double.Parse(Coordinates[0]), double.Parse(Coordinates[1]));
                    Tweet t = new Tweet(latLng, p.DayofWeek(int.Parse(parsingtweet[1])), date, parsingtweet[3]);

                    tweets.Add(t);
                }
            }
            reader.Close();


            /* foreach (Tweet t in tweets) Console.WriteLine(t.z.latitude+" "+t.z.longitude);
             Console.ReadKey();*/

            return tweets;
        }

        public  string DayofWeek(int n)
        {
            return daysofWeek[n];
        }

    }



}
