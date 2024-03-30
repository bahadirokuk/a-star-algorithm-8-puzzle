using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astar
{
    public class PuzzleSolver
    {
        private int[][] goal;
        private int[][] start;

        public PuzzleSolver(int[][] start, int[][] goal)
        {
            this.start = start;
            this.goal = goal;
        }

        public Node? Solve()
        {
            var openList = new PriorityQueue<Node, int>();
            var closedList = new HashSet<Node>();
            Node startNode = new Node(start, null, 0, GetHeuristic(start));
            openList.Enqueue(startNode, startNode.F);
            while (openList.Count > 0)
            {
                var currentNode = openList.Dequeue();
                if (GetHeuristic(currentNode.State) == 0)
                {
                    return currentNode;
                }

                closedList.Add(currentNode);

                foreach (var neighbor in GetNeighbors(currentNode))
                {
                    if (closedList.Contains(neighbor)) continue;

                    if (!openList.UnorderedItems.Any(item => item.Element.StateEquals(neighbor.State)))
                    {
                        openList.Enqueue(neighbor, neighbor.F);
                    }
                }
            }

            MessageBox.Show("çözüm yok");
            return null;
        }

        private IEnumerable<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();
            int zeroRow = 0, zeroCol = 0;

            for (int i = 0; i < node.State.Length; i++)
                for (int j = 0; j < node.State[i].Length; j++)
                    if (node.State[i][j] == 0)
                    {
                        zeroRow = i;
                        zeroCol = j;
                    }
            var directions = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

            foreach (var (dr, dc) in directions)
            {
                int newRow = zeroRow + dr, newCol = zeroCol + dc;
                if (newRow >= 0 && newRow < node.State.Length && newCol >= 0 && newCol < node.State[0].Length)
                {
                    var newState = node.State.Select(a => (int[])a.Clone()).ToArray();
                    newState[zeroRow][zeroCol] = newState[newRow][newCol];
                    newState[newRow][newCol] = 0;
                    neighbors.Add(new Node(newState, node, node.G + 1, GetHeuristic(newState)));
                }
            }
            return neighbors;
        }

        private int GetHeuristic(int[][] state)
        {
            int misplacedTiles = 0;
            for (int i = 0; i < state.Length; i++)
            {
                for (int j = 0; j < state[i].Length; j++)
                {
                    var position = goal
                        .SelectMany((colm, row) => colm.Select((item, colm) => new { item, row, colm }))
                        .FirstOrDefault(x => x.item == state[i][j]);
                    misplacedTiles += Math.Abs(position.row - i) + Math.Abs(position.colm - j);
                }
            }
            return misplacedTiles;
        }
    }
    public class Node
    {
        public int[][] State { get; }
        public Node Parent { get; }
        public int G { get; } // Cost from start to current node
        public int H { get; } // Estimated cost from current node to goal
        public int F => G + H; // Total cost

        public Node(int[][] state, Node parent, int g, int h)
        {
            State = state;
            Parent = parent;
            G = g;
            H = h;
        }

        public bool StateEquals(int[][] otherState)
        {
            for (int i = 0; i < this.State.Length; i++)
                for (int j = 0; j < this.State[i].Length; j++)
                    if (this.State[i][j] != otherState[i][j])
                        return false;
            return true;
        }
    }
}
