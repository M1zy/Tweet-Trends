using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace lab1_tweets_objects_
{
   

    public class Parsing
    {
        string[] daysofWeek = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        public string[] info(string file)
        {
            StreamReader reader = new StreamReader(file+".txt");
            string FullText = reader.ReadToEnd();
            reader.Close();
            string[] tweets = FullText.Split(new string[] { "\n" },StringSplitOptions.RemoveEmptyEntries );
           
     
         //   foreach (string s in tweets) { Console.WriteLine(s); }
           
            return tweets;
            
         /*   foreach(string s in sentences)
            { 
                if(!s.Contains())
            }*/
        }
        

        public  string DayofWeek(int n)
        {
            return daysofWeek[n];
        }

    }



}
