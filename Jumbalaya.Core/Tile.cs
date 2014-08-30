using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumbalaya.Core
{
    public class Tile
    {
        private char char1;

        private char? char2;

        private string _text;

        public Tile(string text)
        {
            char1 = text[0];
            char2 = text.Length > 1 ? text[1] : (char?) null;
            _text = text;
        }

        public string Text
        {
            get
            {
                return _text;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Tile)
            {
                return ((Tile) obj).Text == this.Text;
            }
            return base.Equals(obj);
        }
    }
}
