using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.GraphicsLibraryFramework;
using spiderman.Rendering;

namespace spiderman.Entities.Components.Player
{
    internal class InputComponent : IInputComponent
    {
        PhysicsComponent _physics;

        public InputComponent(PhysicsComponent physics)
        {
            _physics = physics;
        }

        public void Update(Entity player, Game game)
        {
            if (!game.MouseState[MouseButton.Left])
            {
                _physics.IsShooting = false;
                return;
            }

            if (_physics.IsShooting)
                return;

            _physics.LineFinish = new(
                (int)game.MouseState.X,
                Renderer.Resolution.Y - (int)game.MouseState.Y
            );
            _physics.IsShooting = true;
        }
    }
}
