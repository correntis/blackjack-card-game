using System.Collections.Generic;

namespace TwentyOneServer.modules
{
    public struct Pass
    {
        public List<bool> passes { get; set; }
        public bool playerPass { get; set; }
        public bool enemyPass { get; set; }
        public Pass(bool playerPass, bool enemyPass, List<bool> passes)
        {
            this.passes = passes;
            this.playerPass = playerPass;
            this.enemyPass = enemyPass;
            
        }
    }
}
