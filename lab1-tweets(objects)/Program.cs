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

            /*   geoParsing geo = new geoParsing();
                  geo.OutPutAllMoods("all_tweets");

                  Console.ReadKey();*/


                  // run as windows app
                  Application.EnableVisualStyles();

                  Map p = new Map();
                  p.file = "texas";
                  Application.Run(p);
              
         
              
 
        }
    
    }

}
