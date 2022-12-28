using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TwentyOneServer.modules;
using System.Collections.Generic;

namespace TwentyOneServer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class handController : ControllerBase
    {
        System.Random rnd = new System.Random();

        private const int winResult = 21;
        private readonly static List<int> deck = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        private readonly static List<int> playerHand = new List<int> { };
        private readonly static List<int> enemyHand = new List<int> { };
        bool[] Arr = { false, true };

        Hands hands = new Hands(deck, playerHand, enemyHand);
        Pass pass = new Pass(false, false);

        private readonly ILogger<handController> _logger;

        public handController(ILogger<handController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Hands getDeckValue()
        {
            return hands;
        }

        [HttpPut("player")]
        public List<int> AddPlayerCard()
        {
            bool index = true;
            if(!pass.playerPass)
            {
                CheckWinResult(hands.playerHand, index, pass.playerPass);
                return hands.playerHand;
            }
            else
            {
                return hands.playerHand;
            }
        }

        [HttpPut("enemy")]
        public List<int> AddEnemyCard()
        {
            bool index = false;
            if (!pass.enemyPass)
            {
                CheckWinResult(hands.enemyHand, index, pass.enemyPass);
                return hands.enemyHand;
            }
            else
            {
                return hands.enemyHand;
            }      
        }

        [HttpGet("pass")]
        public bool[] CheckPass()
        {

            return Arr;
        }
        [HttpGet("playerPass")]
        public bool CheckPlayerPass()
        {
            Arr[0] = true;
            return Arr[0];
        }
        [HttpGet("enemyPass")]
        public bool CheckEnemyPass()
        {
            return pass.enemyPass;
        }

        [HttpPost("default")]
        public void returnDefaultState()
        {
            hands.deck.AddRange(hands.playerHand);
            hands.deck.AddRange(hands.enemyHand);
            pass.playerPass = false;
            pass.enemyPass = false;
            hands.playerHand.Clear();
            hands.enemyHand.Clear();
        }

        [HttpPost("openHand")]
        public void endOfGame()
        {
            pass.enemyPass = true;
            pass.playerPass = true;
        }

        private void CheckWinResult(List<int> hand, bool handIndex, bool pass)
        {
            int sum = 0;
            foreach(int number in hand)
            {
                sum += number;
            }
            if (sum < winResult && handIndex)
            {
                getCard(hand);
            }
            else if (sum < winResult && !handIndex)
            {
                chanceOfTakingCard(ref sum, pass);
            }
            else if (sum > winResult)
            {
                pass = true;
            }
            else
            {
                pass = true;
            }
        }

        private void chanceOfTakingCard(ref int sum, bool pass)
        {
            int negativeResult = 1;
            int positiveResult = 1;
            List<int> newDeck = new List<int>();
            List<int> buffer = hands.playerHand;
            buffer.Remove(0);
            newDeck.AddRange(buffer);
            newDeck.AddRange(hands.enemyHand);
            List<int> fullDeck = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

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

            double result = ((double)positiveResult - (double)negativeResult - 1 / 11) / hands.deck.Count * 100;

            if (result >= 50)
            {
                getCard(hands.enemyHand);
            }
            else
            {
                pass = true;
            }
        }

        private void getCard(List<int> hand)
        {
            int index = hands.deck[rnd.Next(hands.deck[0], hands.deck.Count)];
            hand.Add(index);
            hands.deck.Remove(index);
        }
    }
}