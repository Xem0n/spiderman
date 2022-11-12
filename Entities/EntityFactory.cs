using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using spiderman.Rendering;

namespace spiderman.Entities
{
    internal static class EntityFactory
    {
        static public Entity Player()
        {
            Components.Player.PhysicsComponent physics = new();
            Components.Player.InputComponent input = new(physics);
            Components.Player.RenderComponent render = new(physics);

            Entity player = new(input, physics, render);
            player.Size = new(40, 40);
            player.Position = new(
                Renderer.Resolution.X / 2 - player.Size.X / 2,
                Renderer.Resolution.Y / 2 - player.Size.Y / 2
            );

            return player;
        }
    }
}
