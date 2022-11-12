using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Mathematics;
using System.Threading.Tasks;

namespace spiderman.Entities.Components.Player
{
    internal class PhysicsComponent : IPhysicsComponent
    {
        bool _isShooting = false;
        Vector2i _lineStart = new(0, 0);
        Vector2i _lineFinish = new(0, 0);

        public Vector2i LineStart => _lineStart;
        
        public Vector2i LineFinish
        {
            get => _lineFinish;
            set => _lineFinish = value;
        }

        public bool IsShooting
        {
            get => _isShooting;
            set => _isShooting = value;
        }

        public void Update(Entity player, World world)
        {
            _lineStart.X = player.Position.X + player.Size.X / 2;
            _lineStart.Y = player.Position.Y + player.Size.Y / 2;
        }
    }
}
