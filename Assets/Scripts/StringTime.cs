using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class StringTime
{
    public static string SecondToTimeString(float seconds)
    {
        return TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss\.ff");
    }
}
