using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI1
{
    class Astar
    {
        public List<Grid> open_set;
        public List<Grid> close_set;
        Grid[,] map;
        Grid start;
        Grid end;
        int lenX;
        int lenZ;
        public Astar(Grid[,] map,Grid start,Grid end,int lenX,int lenZ)
        {
            this.map = map;
            this.lenX = lenX;
            this.lenZ = lenZ;
            this.start = start;
            this.end = end;
            open_set = new List<Grid>();
            close_set = new List<Grid>();
        }
        /**f(n)=g(n)+h(n)
         * f(n)是节点n的综合优先级。当我们选择下一个要遍历的节点时，我们总会选取综合优先级最高（值最小）的节点。
         * g(n) 是节点n距离起点的代价。
         * h(n)是节点n距离终点的预计代价，这也就是A*算法的启发函数。关于启发函数我们在下面详细讲解。
        */
        public int BaseCost(Grid current)//节点到起点的移动代价，对应了g(n),g(n) 是节点n距离起点的代价。
        {
            int x_dis = Math.Abs(current.x - start.x);
            int y_dis = Math.Abs(current.z - start.z);
            return x_dis+y_dis;
        }
        public int HeuristicCost(Grid current)//节点到终点的启发函数，对应上文的h(n)
        {
            int x_dis = Math.Abs(end.x - current.x);
            int z_dis = Math.Abs(end.z - current.z);
            return x_dis+z_dis;
        }
        public int TotalCost(Grid current)
        {
            return BaseCost(current) + HeuristicCost(current);
        }
        public bool IsValidPoint(Grid grid)
        {
            if (grid.x < 0 || grid.z < 0)
                return false;
            if (grid.x > lenX || grid.z > lenZ)
                return false;
            if (grid.gridType == 1)
                return false;
            else
                return true;
        }
        public bool IsInPointList(Grid grid,List<Grid> list)
        {
            foreach(Grid g in list)
            {
                if (g.x == grid.x && g.z == grid.z)
                    return true;
            }
            return false;
        }
        public bool IsInOpenList(Grid grid)
        {
            return IsInPointList(grid, open_set);
        }
        public bool IsInCloseList(Grid grid)
        {
            return IsInPointList(grid, close_set);
        }
        public List<Grid> Run()
        {
            start.cost = 0;
            open_set.Add(start);
            while (true)
            {
                int index = SelectPointInOpenList();
                Grid g = open_set[index];
                if(g.x == end.x && g.z == end.z)
                {
                    return BuildPath(g);
                }
                open_set.Remove(open_set[index]);
                close_set.Add(g);
                //Process all neighbors
                int x = g.x;
                int z = g.z;
                ProcessPoint(map[x - 1, z], g);
                ProcessPoint(map[x + 1, z], g);
                ProcessPoint(map[x, z - 1], g);
                ProcessPoint(map[x, z + 1], g);
                
            }
        }
        public void ProcessPoint(Grid grid,Grid parent)
        {
            if (IsValidPoint(grid) == false)
                return;
            if (IsInCloseList(grid))
                return;
            if (IsInOpenList(grid) == false)
            {
                grid.parent = parent;
                grid.cost = TotalCost(grid);
                open_set.Add(grid);
                return;
            }
            else
            {
                Grid oldParent = grid.parent;
                int oldCost = grid.cost;
                grid.parent = parent;
                int curCost = TotalCost(grid);
                if (curCost < oldCost) {//更新
                    grid.parent = parent;
                    grid.cost = curCost;
                }
                else {//还原
                    grid.parent = oldParent;
                    grid.cost = oldCost;
                }
            }

        }
        public List<Grid> BuildPath(Grid g)
        {
            List<Grid> path=new List<Grid>();
            while (true)
            {
                path.Insert(0, g);
                if (g.x == start.x && g.y == start.y)
                    return path;
                else
                    g = g.parent;
                  
            }
        }

        public int SelectPointInOpenList()
        {
            int index = 0;
            int selected_index = -1;
            int min_cost = Int32.MaxValue;
            foreach(Grid g in open_set)
            {
                int cost = TotalCost(g);
                if(cost < min_cost)
                {
                    min_cost = cost;
                    selected_index = index;
                }
                index++;
            }
            return selected_index;
        }
    }
}
