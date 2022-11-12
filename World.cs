using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using spiderman.Entities;

namespace spiderman
{
    internal class World
    {
        Game _game;
        List<Entity> _entities = new();

        public Game Game => _game;

        public World(Game game)
        {
            _game = game;

            _entities.Add(EntityFactory.Player());
        }

        public void Update()
        {
            List<int> toRemove = new();

            for (int i = 0; i < _entities.Count; i++)
            {
                Entity entity = _entities[i];

                entity.Update(this);

                if (!entity.IsValid)
                    toRemove.Add(i);
            }

            foreach (int i in toRemove)
                _entities.RemoveAt(i);
        }

        public void Draw()
        {
            foreach (Entity entity in _entities)
                entity.Draw();
        }

        public bool CheckCollision(Vector2i pos)
        {
            return false;
        }
    }
}
