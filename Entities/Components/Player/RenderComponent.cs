using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using spiderman.Rendering;

namespace spiderman.Entities.Components.Player
{
    internal class RenderComponent : IRenderComponent
    {
        PhysicsComponent _physics;

        Color4 _color = new(0.9f, 0.22f, 0.27f, 1f);

        public RenderComponent(PhysicsComponent physics)
        {
            _physics = physics;
        }

        public void Draw(Entity player)
        {
            Drawing.Rectangle(player.Position, player.Size, _color);

            if (_physics.IsShooting)
                Drawing.Line(_physics.LineStart, _physics.LineFinish, 2, _color);
        }
    }
}
