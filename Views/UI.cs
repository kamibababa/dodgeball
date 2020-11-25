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
    }
}
