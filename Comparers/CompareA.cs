using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace opg_201910_interview.Comparers
{
    public class CompareA : IComparer<string>
    {
        private Dictionary<string, int> fileOrder = new Dictionary<string, int>();
        
        public CompareA()
        {
            fileOrder.Add("shovel", 1);
            fileOrder.Add("waghor", 2);
            fileOrder.Add("blaze", 3);
            fileOrder.Add("discus", 4);
        }

        public int Compare(string x, string y)
        {
            string[] splitX = x.Split(new char[] { '-' }, 2);
            string[] splitY = y.Split(new char[] { '-' }, 2);
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
                DateTime dateX = DateTime.Parse(xDateOnly);
                DateTime dateY = DateTime.Parse(yDateOnly);
                return (dateX - dateY).TotalSeconds > 1 ? 1 : -1;
            }
        }
    }
}
