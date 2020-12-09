using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Views
{
    static class UI
    {
        public static Rectangle HealthBar = new Rectangle(-12, -16, 60, 12); // Relative to GameChar.Bounds
        public static Rectangle LeftInventory = new Rectangle(-12, 24, 8, 8);
        public static Rectangle RightInventory = new Rectangle(40, 24, 8, 8);
        public static int InventorySpacing = -12;
        public static Rectangle LeftLungeBar = new Rectangle(-12, 36, 8, 24);
        public static Rectangle RightLungeBar = new Rectangle(40, 36, 8, 24);

        // Pause menu virtual locations
        public static Rectangle RestartOption = new Rectangle(272, 208, 480, 80);
        public static Rectangle MainMenuOption = new Rectangle(272, 288, 480, 80);
        public static Rectangle BackOption = new Rectangle(272, 368, 480, 80);
    }
}
