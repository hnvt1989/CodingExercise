using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise
{
    public class Deck
    {
        private List<Card> _cards;
        public Deck()
        {

        }

        public List<Card> Cards
        {
            get
            {
                if (_cards == null)
                    _cards = populateDeckOfCards();
                return _cards;
            }
        }
        private List<Card> populateDeckOfCards()
        {
            var ret = new List<Card>();

            foreach (var suit in SuitAndRank.Suits)
            {
                foreach (var rank in SuitAndRank.Ranks)
                {
                    ret.Add(new Card(suit.Key, rank.Key));
                }
            }
            return ret;
        }

        public List<Card> Sort(List<Card> cards)
        {
            cards.Sort((c1, c2) => c1.Point.CompareTo(c2.Point));
            return cards;
        }

        public List<Card> Shuffle(List<Card> cards,int nSwap = 20)
        {

            Random rnd = new Random();
            
            for (int i = 0; i < nSwap; i++)
            {
                int r1 = rnd.Next(0, 51);
                int r2 = -1;
                while (r2 < 0)
                {
                    r2 = rnd.Next(0, 51);
                    if (r2 == r1)
                        r2 = -1;
                }

                var tmp = cards[r1];
                cards[r1] = cards[r2];
                cards[r2] = tmp;
            }

            return cards;

        }
    }

    public class Card
    {
        private string _suit;
        private string _rank;

        public Card(string suit, string rank)
        {
            if (!SuitAndRank.Suits.ContainsKey(suit))
                throw new KeyNotFoundException("Invalid suit:" + suit);
            if (!SuitAndRank.Ranks.ContainsKey(rank))
                throw new KeyNotFoundException("Invalid rank:" + rank);
            _suit = suit;
            _rank = rank;
        }

        public string Suit { get { return _suit; } }
        public string Rank { get { return _rank; } }

        public override string ToString()
        {
            return _suit + "[" + _rank + "]";
        }

        public int Point
        {
            get
            {
                return SuitAndRank.Suits[_suit] * 13 + SuitAndRank.Ranks[_rank];
            }
        }
    }

    public static class SuitAndRank
    {
        public static Dictionary<string, int> Suits = new Dictionary<string, int>()
        {
            {"Space", 1 },
            {"Club", 2 },
            {"Diamond", 3 },
            {"Heart", 4 }

        };

        public static Dictionary<string, int> Ranks = new Dictionary<string, int>()
        {
            {"Two", 2 },
            {"Three", 3 },
            {"Four", 4 },
            {"Five", 5 },
            {"Six", 6 },
            {"Seven", 7 },
            {"Eight", 8 },
            {"Nine", 9 },
            {"Ten", 10 },
            {"Jack", 11 },
            {"Queen", 12 },
            {"King", 13 },
            {"Ace", 14 }
        };
    }
}
