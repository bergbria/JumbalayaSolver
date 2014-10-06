using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumbalaya.Core
{
    public class TileTray
    {
        private string[] _distinctLetterPairs;
        private List<LinkedList<char>> _sortedDistinctLetterPairs;
        public Tile[] Tiles { get; set; }

        public TileTray(params string[] tiles)
        {
            if (tiles == null) throw new ArgumentNullException("tiles");
            Tiles = tiles.Select(tileText => new Tile(tileText)).ToArray();
        }

        public string Text
        {
            get
            {
                if (!Tiles.Any())
                {
                    return string.Empty;
                }
                return Tiles.Select(t => t.Text).Aggregate((a, b) => a + b);
            }
            set
            {
                Tiles = value.ToCharArray().Select(c => new Tile(c.ToString())).ToArray();
                _distinctLetterPairs = null;
                _sortedDistinctLetterPairs = null;
            }
        }

        public string[] DistinctTilePairStrings
        {
            get
            {
                if (_distinctLetterPairs == null)
                {
                    _distinctLetterPairs = GenerateDistinctLetterPairs();
                }
                return _distinctLetterPairs;
            }
        }

        public List<LinkedList<char>> SortedDistinctLetterPairs
        {
            get
            {
                if (_sortedDistinctLetterPairs == null)
                {
                    _sortedDistinctLetterPairs = new List<LinkedList<char>>(this.DistinctTilePairStrings.Length);
                    foreach (string str in this.DistinctTilePairStrings)
                    {
                        var chars = str.ToCharArray();
                        Array.Sort(chars);
                        _sortedDistinctLetterPairs.Add(new LinkedList<char>(chars));
                    }
                }
                return _sortedDistinctLetterPairs;
            }
        }

        private string[] GenerateDistinctLetterPairs()
        {
            var pairs = new HashSet<string>();
            for (int i = 0; i < Tiles.Length - 1; i++)
            {
                for (int j = i + 1; j < Tiles.Length; j++)
                {
                    pairs.Add(Tiles[i].Text + Tiles[j].Text);
                }
            }
            return pairs.ToArray();
        }
    }
}
