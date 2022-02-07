using System.Collections.Generic;
using System.IO;
using System.Linq;
using TanksGame.Base;
using TanksGame.Services.Abstractions;

namespace TanksGame.Services
{
    public class CollisionTreeBuilder : ICollisionTreeBuilder
    {
        public void FromFile(string path)
        {
            var fileName = Path.GetFileNameWithoutExtension(path);
            var dataset = ReadDataset(path);

            var tree = IoC.Resolve<Dictionary<int, object>>("collisionTree", fileName);

            foreach (var row in dataset)
            {
                Dictionary<int, object> subTree = tree;

                foreach (var number in row)
                {
                    if (!subTree.ContainsKey(row[0]))
                    {
                        subTree[number] = new Dictionary<int, object>();
                    }

                    subTree = (Dictionary<int, object>)subTree[number];
                }
            }
        }

        private IEnumerable<List<int>> ReadDataset(string dataPath)
        {
            return File.ReadAllLines(dataPath)
                .Select(line => line.Split().Select(int.Parse).ToList())
                .ToList();
        }
    }
}
