using System;

namespace grade_scores
{
    public class Line
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Score { get; set; }

        public static string WriteLine(Line dataItem)
        {
            return dataItem.LastName + "," + dataItem.FirstName + "," + dataItem.Score;
        }

        public static Line ReadLine(string line)
        {
            var item = new Line();
            var lineItems = line.Trim().Split(',');
            if (lineItems.Length != 3)
            {
                Console.WriteLine("Invalid line " + line);
                return item;
            }
            item.LastName = lineItems[0];
            item.FirstName = lineItems[1];
            var scoreString = lineItems[2];
            int score;
            if (int.TryParse(scoreString, out score))
            {
                item.Score = score;
            }
            else
            {
                item.Score = 0;
                Console.WriteLine("Invalid score " + scoreString + " on line " + line);
            }
            return item;
        }
    }

}
