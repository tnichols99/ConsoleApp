using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{

    public class Move
    {

        public int num { get; set; }
        public int right { get; set; }
        public int up { get; set; }
        public int left { get; set; }
        public int down { get; set; }

        public Move(int a, int b, int c, int d, int num)
        {
            right = a;
            up = b;
            left = c;
            down = d;
            num = 0;
        }
    }
}