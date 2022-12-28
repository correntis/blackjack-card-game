namespace TwentyOneServer.modules
{
    public struct Pass
    {
        public bool playerPass { get; set; }
        public bool enemyPass { get; set; }

        public Pass(bool playerPass, bool enemyPass)
        {
            this.playerPass = playerPass;
            this.enemyPass = enemyPass;
        }
    }
}
