using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab1_tweets_objects_
{

   public  class Tweet
    {
        public PointLatLng latLng;
        public string DayofWeek { get; set; }
        public DateTime time { get; set; }
        public string text { get; set; }
        public Tweet() { }

        public Tweet(PointLatLng latLng, string DayofWeek, DateTime time, string text)
        {
            this.latLng = latLng ; this.DayofWeek = DayofWeek; this.time = time; this.text = text;
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
