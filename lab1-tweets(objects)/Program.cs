using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace lab1_tweets_objects_
{
    class Program :Form
    {
        static void Main(string[] args)
        {
            /*   Tweet t = new Tweet();
               t.Building("my_life");
              /* Trends trends = new Trends();
               Console.WriteLine(trends.get_word_sentiment("get"));
               Console.ReadKey();
               */
            /* geoParsing p = new geoParsing();
             Dictionary<string, List<List<List<double>>>> z  = p.FromJson("states.json");
             foreach (var v in z)
             {
                 Console.WriteLine(v.Key + "  ");
                 for(int i = 0; i < v.Value.Count; i++)
                 {
                     for(int y = 0; y < v.Value[i].Count; y++)
                     {
                         Console.WriteLine(v.Value[i][y][0]+"  "+ v.Value[i][y][1]);
                     }
                 }
             }
         */

            /*   Trends trends = new Trends();
               trends.CreatingDictionary();
               foreach(var v in trends.wordsValue)
               {
                   Console.WriteLine(v.Key + "  " + v.Value);
               }
               */
            /* Trends d = new Trends();
             Tweet t = new Tweet();
             List<Tweet> z = t.BuildingTweets("my_life");
             d.CreatingDictionary();
             for (int i = 0; i < 4; i++)
             {
                 Console.WriteLine(z[i].text);
                 Console.WriteLine(d.AverageMood(z[i]));
             }*/

            if (args.Length == 0)
            {
                // run as windows app
                Application.EnableVisualStyles();
                Application.Run(new Map());
            }
            else
            {
                // run as console app
                
                Console.WriteLine("Hello World");
                Console.ReadLine();
            }
        }
    
    }

}
