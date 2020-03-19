using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI1
{
    class Grid
    {
        public int x;//x轴
        public int y;//y轴
        public int z;//z轴
        public int? gridType;//地板种类
        public Grid parent;//当前网格的父节点
        public int cost;//起点到该点的花费
        

        public Grid(int x, int y, int z, int? gridType, Grid parent, int cost)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.gridType = gridType;
            this.parent = parent;
            this.cost = cost;
        }

        public Grid()//默认构造函数
        {
            this.x = 0;
            this.y = 1;
            this.z = 0;
            this.gridType = null;
            this.parent = null;
            this.cost = Int32.MaxValue;
        }

       
    }
}
