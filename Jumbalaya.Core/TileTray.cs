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
        public Tile[] Tiles { get; set; }

        public TileTray(params string[] tiles)
        {
            if (tiles == null) throw new ArgumentNullException("tiles");
            Tiles = tiles.Select(tileText => new Tile(tileText)).ToArray();
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
