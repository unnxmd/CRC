using System;
using System.Collections;
using System.Collections.Generic;

namespace CRC
{
    class Program
    {
        static void Main(string[] args)
        {
            int gx = 67; //1+x^1+x^6 = 1000011 = 67
            Dictionary<int, int> result_counts = new Dictionary<int, int>();
            
            for (int i = 0; i <= 255; i++)
            {
                int res = CRC(i, gx);
                if (result_counts.ContainsKey(res)) result_counts[res] += 1;
                else result_counts.Add(res, 1);
            }

            int collision = 0;
            foreach (KeyValuePair<int, int> pair in result_counts)
            {
                if (pair.Value != 1) collision += pair.Value;
                Console.WriteLine(pair.Key + " - " + pair.Value);
            }
            Console.WriteLine("Коллизия: " + collision);
            Console.ReadLine();
        }

        static int CRC(int message, int gx)
        {
            if (message < gx) return message;
            if (message == gx) return 0;
            message = message << BinaryLenght(gx) - 1;
            message = message ^ (gx << (BinaryLenght(message) - BinaryLenght(gx)));
            while (BinaryLenght(message) >= BinaryLenght(gx))  message = message ^ (gx << (BinaryLenght(message) - BinaryLenght(gx)));
            return message;
        }

        static int BinaryLenght(int num)
        {
            int i = 0;
            if (num == 1) return 1;
            while (num >= 1)
            {
                if (num % 2 == 1)
                {
                    num -= 1;
                    num /= 2;
                    i++;
                }
                else
                {
                    num /= 2;
                    i++;
                }
            }
            return i;
        }
    }
}
