using System.Collections.Generic;

namespace TwentyOneServer.modules
{
    public struct Hands
    {
        public List<int> deck { get; set; }
        public List<int> playerHand { get; set; }
        public List<int> enemyHand { get; set; }

        public Hands(List<int> deck, List<int> playerHand, List<int> enemyHand)
        {
            this.deck = deck;
            this.playerHand = playerHand;
            this.enemyHand = enemyHand;
        }
    }
}
