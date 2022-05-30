using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyBall
{
    public class Log
    {
        public static StringBuilder LogBuilder = new StringBuilder(10000);

        public static void Write()
        {
            var str = LogBuilder.ToString();
            var sw = new StreamWriter("Data.txt", false, Encoding.Default);
            sw.Write(str);
        }

        //Log.LogBuilder.Append($"\nstart Timer_Tick {DateTime.Now.Millisecond}\n");
        //Log.LogBuilder.Append($"end Timer_Tick {DateTime.Now.Millisecond}\n");

    }
}

