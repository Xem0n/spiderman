using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spiderman.Rendering
{
    internal static class BatchManager
    {
        static List<Batch> _batches = new();

        public static void Add(float[] vertices, uint[] indices, BatchConfig config)
        {
            var batch = _batches.Find(
                x => x.EqualsBatchConfig(config) && x.CanFit(vertices, indices)
            );

            if (batch == null)
            {
                batch = new(config);
                _batches.Add(batch);
            }

            batch.Add(vertices, indices);
        }

        public static void Render()
        {
            foreach (var batch in _batches)
            {
                if (!batch.IsEmpty())
                    batch.Render();
            }
        }

        public static void CleanUp()
        {
            foreach (var batch in _batches)
            {
                batch.Dispose();
            }

            _batches.Clear();
        }
    }
}
