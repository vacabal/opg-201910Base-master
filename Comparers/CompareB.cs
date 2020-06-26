using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace opg_201910_interview.Comparers
{
    public class CompareB : IComparer<string>
    {
        private Dictionary<string, int> fileOrder = new Dictionary<string, int>();
        
        public CompareB()
        {
            fileOrder.Add("orca", 1);
            fileOrder.Add("widget", 2);
            fileOrder.Add("eclair", 3);
            fileOrder.Add("talon", 4);
        }

        public int Compare(string x, string y)
        {
            string[] splitX = x.Split(new char[] { '_' }, 2);
            string[] splitY = y.Split(new char[] { '_' }, 2);
            string xName = Path.GetFileNameWithoutExtension(splitX[0]);
            string yName = Path.GetFileNameWithoutExtension(splitY[0]);
            int result = fileOrder[xName] - fileOrder[yName];
            if (result != 0)
            {
                return result;
            }
            else
            {
                string xDateOnly = Path.GetFileNameWithoutExtension(splitX[1]);
                string yDateOnly = Path.GetFileNameWithoutExtension(splitY[1]);
                int dateX = Int32.Parse(xDateOnly);
                int dateY = Int32.Parse(yDateOnly);
                return (dateX - dateY) > 1 ? 1 : -1;
            }
        }
    }
}
