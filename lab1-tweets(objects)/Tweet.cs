using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab1_tweets_objects_
{
    public class Point
    {
        public double latitude { get; set; }
       public  double longitude { get; set; }

        public Point() { }

        public Point(double latitude, double longitude)
        {
            this.latitude = latitude;this.longitude = longitude;
        }
    }
   public  class Tweet
    {
        public Point z = new Point();
        public string DayofWeek { get; set; }
        public DateTime time { get; set; }
        public string text { get; set; }
        public Tweet() { }

        public Tweet(double latitude, double longitude, string DayofWeek, DateTime time, string text)
        {
            this.z.latitude = latitude; this.z.longitude = longitude; this.DayofWeek = DayofWeek; this.time = time; this.text = text;
        }


        public List<Tweet> BuildingTweets(string file)
        {
            Parsing p = new Parsing();
            List<Tweet> tweets = new List<Tweet>();
            string[] infos = p.info(file);

            for (int i = 0; i < infos.Length; i++)
            {

                string[] parsingtweet = infos[i].Split(new string[] { "	" }, StringSplitOptions.RemoveEmptyEntries);
                if (parsingtweet.Length == 4)
                {
                    string[] Coordinates = parsingtweet[0].Split(new string[] { "[", "]", "," }, StringSplitOptions.RemoveEmptyEntries);
                    Coordinates[0] = Coordinates[0].Replace('.', ','); Coordinates[1] = Coordinates[1].Replace('.', ',');
                    DateTime date = DateTime.ParseExact(parsingtweet[2], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    Tweet t = new Tweet(double.Parse(Coordinates[0]), double.Parse(Coordinates[1]),p.DayofWeek(int.Parse(parsingtweet[1])), date, parsingtweet[3]);

                    tweets.Add(t);
                }
            }
           /* foreach (Tweet t in tweets) Console.WriteLine(t.z.latitude+" "+t.z.longitude);
            Console.ReadKey();*/
            return tweets;
        }


        public List<string> tweet_words()
        {
            string[] w = text.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < w.Length; i++)
            {
                w[i] = w[i].Trim(' ', ',', '.');
            }
            List<string> words = w.ToList<string>();
            return words;


        }


        

        // Dictionary<string, double> wordsValue = new Dictionary<string, double>();


        /* void CreatingDictionary(string filename)
         {
             StreamReader reader = new StreamReader(filename);
             string line;
             string[] row = new string[2];
             while ((line = reader.ReadLine()) != null)
             {
                 row = line.Split(',');
                 wordsValue.Add(row[0], int.Parse(row[1]));
             }
         }*/
    }
        
}
