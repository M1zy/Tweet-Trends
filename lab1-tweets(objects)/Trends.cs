using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace lab1_tweets_objects_
{
    public class Trends
    {

        public Dictionary<string, double> wordsValue = new Dictionary<string, double>();
        bool creating = false;

       public void CreatingDictionary()
        {
            creating = true;
            StreamReader reader = new StreamReader("sentiments.csv");
            string line;
            string[] row = new string[2];
            while ((line = reader.ReadLine()) != null)
            {
                row = line.Split(',');
                row[1]=row[1].Replace('.', ',');
                wordsValue.Add(row[0].ToLower(), double.Parse(row[1]));
            }
            creating = true;
        }


        double get_word_sentiment(string word)
        {
            if (creating == false) { CreatingDictionary(); }
            return wordsValue[word.ToLower()];
        }

        bool IsExistword(string word)
        {
            if (creating == false) {  CreatingDictionary(); }
            if (wordsValue.ContainsKey(word.ToLower())) return true;
            return false;
        }


       public double AverageMood(Tweet t)
        {
            List<string> words = t.tweet_words();
            double mood = 0; int countofWords = 0;
             bool exist_expression = false;
            for (int i = 0; i < words.Count; i++)
            {
                double value = 0;
                int count = 1;
                string CheckString = words[i];
                for (int y = i; y < words.Count-1; y++)
                {
                    if (IsExistword(CheckString))
                    {
                        exist_expression = true;
                        value = get_word_sentiment(CheckString);
                        count = CheckString.Split(' ').Length;
                        
                    }
                    CheckString += " " + words[y+1];
                }
                mood += value;
                if (exist_expression == true) { countofWords++; exist_expression = false; }
                for(int z = 1; z < count; z++)
                {
                    i++;
                }
            }
            return (mood / countofWords);
        }



    }

}

