using System;

namespace Dodgeball
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Dodgeball())
                game.Run();
        }
    }
}
