using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Name
{
    internal class Sprite
    {
        public Texture2D texture;
        public Vector2 position;
        private Microsoft.Xna.Framework.Vector2 zero;

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public Sprite(Texture2D texture, Microsoft.Xna.Framework.Vector2 zero)
        {
            this.texture = texture;
            this.zero = zero;
        }
    }
}

// I believe none of this code ended up being useful to the assignment, but just incase I am leaving it here.