using Dodgeball.Models;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Views
{
    class TextureSet
    {
        public Dictionary<World.Enviro, Texture2D> Environments;

        public TextureSet(ContentManager content)
        {
            // Load environments
            Environments = new Dictionary<World.Enviro, Texture2D>();
            Environments.Add(World.Enviro.Gym, content.Load<Texture2D>("Images/Environments/gym"));
            Environments.Add(World.Enviro.Street, content.Load<Texture2D>("Images/Environments/street"));
            Environments.Add(World.Enviro.Field, content.Load<Texture2D>("Images/Environments/field"));
            Environments.Add(World.Enviro.Playground, content.Load<Texture2D>("Images/Environments/playground"));
        }
    }
}
