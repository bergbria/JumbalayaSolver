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
            if (string.IsNullOrEmpty(text) || text.Length > 2)
            {
                throw new ArgumentException("Shenanigans! Invalid tile text: " + text);
            }
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

        protected bool Equals(Tile other)
        {
            return string.Equals(_text, other._text) && char2 == other.char2 && char1 == other.char1;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (_text != null ? _text.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ char2.GetHashCode();
                hashCode = (hashCode*397) ^ char1.GetHashCode();
                return hashCode;
            }
        }
    }
}
