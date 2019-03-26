using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsForms;

namespace lab1_tweets_objects_
{
    class Program :Form
    {
        static void Main(string[] args)
        {

         geoParsing geo = new geoParsing();
            geo.OutPutAllMoods("all_tweets");

            Console.ReadKey();
           /*
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
            }*/
        }
    
    }

}
