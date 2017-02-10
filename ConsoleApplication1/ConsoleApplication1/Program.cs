using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan tempo = new TimeSpan(0, 32, 48);

            decimal voltas = (decimal)10.5;
            var mediaTicks = (long)(tempo.Ticks / voltas);

            var media = new TimeSpan(mediaTicks);

            Console.WriteLine(media);
        }
    }
}
