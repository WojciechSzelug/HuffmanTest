using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace HuffmanTest
{
    class Data
    {
        public int value { get; private set; }
        public string Char { get; private set; }
        public string code;

    public Data(int intValue,string charValue, string array) {
            value = intValue;
            Char = charValue;
            code = array;

        }
        public void incress()
        {
            value++;
        }

    }
}
