using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spiderman.Entities.Components
{
    internal interface IPhysicsComponent
    {
        public void Update(Entity entity, World world);
    }
}
