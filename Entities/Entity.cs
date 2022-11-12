using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using spiderman.Entities.Components;

namespace spiderman.Entities
{
    internal class Entity
    {
        bool _isValid = true;

        Vector2i _size;
        Vector2i _position;
        Vector2i _velocity;

        IInputComponent _input;
        IPhysicsComponent _physics;
        IRenderComponent _render;

        public bool IsValid
        {
            get => _isValid;
            set => _isValid = value;
        }

        public Vector2i Size
        {
            get => _size;
            set => _size = value;
        }

        public Vector2i Position
        {
            get => _position;
            set => _position = value;
        }

        public Vector2i Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }

        public Entity(IInputComponent input, IPhysicsComponent physics, IRenderComponent render)
        {
            _input = input;
            _physics = physics;
            _render = render;
        }

        public void Update(World world)
        {
            _input.Update(this, world.Game);
            _physics.Update(this, world);
        }

        public void Draw()
        {
            _render.Draw(this);
        }
    }
}
