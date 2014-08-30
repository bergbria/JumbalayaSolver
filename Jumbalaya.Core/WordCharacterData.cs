using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumbalaya.Core
{
    public class WordCharacterData :IEquatable<WordCharacterData>
    {
        public WordCharacterData(string word)
        {
            CharacterCounts = new Dictionary<char, ushort>();
            foreach (char c in word)
            {
                if (CharacterCounts.ContainsKey(c))
                {
                    CharacterCounts[c]++;
                }
                else
                {
                    CharacterCounts.Add(c, 1);
                }
            }
        }

        public Dictionary<char, ushort> CharacterCounts { get; set; }

        public override int GetHashCode()
        {
            int sum = 0;
            foreach (var kvp in CharacterCounts)
            {
                sum += kvp.Key.GetHashCode();
                sum += kvp.Key.GetHashCode();
            }
            return sum.GetHashCode();
        }

        public bool Equals(WordCharacterData other)
        {
            if (other == null || other.CharacterCounts.Count != CharacterCounts.Count)
            {
                return false;
            }
            foreach (var kvp in CharacterCounts)
            {
                ushort otherVal;
                if (!other.CharacterCounts.TryGetValue(kvp.Key, out otherVal)) return false;
                if (kvp.Value != otherVal) return false;
            }
            return true;
        }

        public override string ToString()
        {
            var keyValPairs = CharacterCounts.Select(kvp => kvp.Key + ":" + kvp.Value);
            return "Count: " + CharacterCounts.Count + "{" + string.Join(", ", keyValPairs) + "}";
        }
    }
}
