using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public interface ICombatSprite
    {
        Rectangle FutureRec { get; }
        bool IsDead { get; }
        int Health { get; }
        int MaxHealth { get; }
    }
}
