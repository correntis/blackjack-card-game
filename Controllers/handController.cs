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

        private readonly static bool playerPass = false;
        private readonly static bool enemyPass = false;

        private readonly static List<int> deck = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        private readonly static List<int> playerHand = new List<int> { };
        private readonly static List<int> enemyHand = new List<int> { };

        private readonly static List<bool> passes = new List<bool>() { playerPass, enemyPass };

        Hands hands = new Hands(deck, playerHand, enemyHand);
        Pass pass = new Pass(playerPass, enemyPass, passes);

        AI ai = new AI();

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
            if (!pass.playerPass)
            {
                ai.CheckWinResult(hands.playerHand, ref index, ref hands, ref pass);
                return hands.playerHand;
            }
            else
            {
                return null;
            }
        }

        [HttpPut("enemy")]
        public List<int> AddEnemyCard()
        {
            bool index = false;
            if (!pass.enemyPass)
            {
                ai.CheckWinResult(hands.enemyHand, ref index, ref hands, ref pass);
                pass.passes[1] = pass.enemyPass;
                return hands.enemyHand;
            }
            else
            {
                return null;
            }
        }

        [HttpGet("playerPass")]
        public bool CheckPlayerPass()
        {
            pass.playerPass = true;
            pass.passes[0] = pass.playerPass;
            bool index = false;
            while (!pass.enemyPass)
            {
                ai.CheckWinResult(hands.enemyHand, ref index, ref hands, ref pass);
                pass.passes[1] = pass.enemyPass;
            }
            return pass.playerPass;
        }

        [HttpPost("default")]
        public void returnDefaultState()
        {
            hands.deck.AddRange(hands.playerHand);
            hands.deck.AddRange(hands.enemyHand);
            pass.passes[0] = false;
            pass.passes[1] = false;
            hands.playerHand.Clear();
            hands.enemyHand.Clear();
        }

        [HttpGet("Pass")]
        public List<bool> CheckPass()
        {
            return pass.passes;
        }

    }
}