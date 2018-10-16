using System;

namespace CollideGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CollideGame game = new CollideGame())
            {
                game.Run();
            }
        }
    }
#endif
}

