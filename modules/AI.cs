using System.Collections.Generic;

namespace TwentyOneServer.modules
{
    public class AI
    {
        private System.Random rnd = new System.Random();
        private const int winResult = 21;
        private const int chance = 1 / 11;

        public AI() { }

        public void CheckWinResult(List<int> hand, ref bool handIndex, ref Hands hands, ref Pass pass)
        {
            int sum = 0;
            foreach (int number in hand)
            {
                sum += number;
            }
            if (sum < winResult && handIndex)
            {
                getCard(hand, ref hands);
            }
            else if (sum < winResult && !handIndex)
            {
                chanceOfTakingCard(ref sum, ref hands, ref pass);
            }
            else if (sum >= winResult && handIndex)
            {
                pass.playerPass = true;
            }
            else if (sum >= winResult && !handIndex)
            {
                pass.enemyPass = true;
            }
        }

        private void chanceOfTakingCard(ref int sum, ref Hands hands, ref Pass pass)
        {
            int negativeResult = 1;
            int positiveResult = 1;

            List<int> fullDeck = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            List<int> newDeck = new List<int>();
            List<int> buffer = hands.playerHand;

            buffer.Remove(0);
            newDeck.AddRange(buffer);
            newDeck.AddRange(hands.enemyHand);

            double enemyProfitability = winResult - sum;

            foreach (int number in fullDeck)
            {
                if (number <= enemyProfitability)
                {
                    positiveResult++;
                }
            }
            foreach (int number in newDeck)
            {
                if (number <= enemyProfitability)
                {
                    negativeResult++;
                }
            }

            double result = ((double)positiveResult - (double)negativeResult - chance) / hands.deck.Count * 100;

            if (result >= 50)
            {
                getCard(hands.enemyHand, ref hands);
            }
            else
            {
                pass.enemyPass = true;
            }
        }

        private void getCard(List<int> hand, ref Hands hands)
        {
            int index = hands.deck[rnd.Next(hands.deck[0], hands.deck.Count)];
            hand.Add(index);
            hands.deck.Remove(index);
        }
    }
}