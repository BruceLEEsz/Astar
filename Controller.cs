using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackerInterface;
using ljqLog;

namespace AI1
{
    public class Controller : HackerInterface.IControl
    {
        Log logger = new Log("1");
        private bool x;
        //队名录入位置
        public string GetTeamName()
        {
            return "1";
        }

        //逻辑代码编写
        int lenX;//地图长
        int lenY;//地图层数
        int lenZ;//地图宽
        Grid start = new Grid();//当前位置
        Grid end = new Grid();//公共门
        Grid[,] map1 = new Grid[100,100];//一楼地图
        Grid[,] map2 = new Grid[100,100];//二楼地图
        public void InitMap(Hacker hacker)
        {
            int[] _map = hacker.GetMapInfo();//获取地图info
            lenX = _map[0];
            lenY = _map[1];
            lenZ = _map[2];
            //初始化map1
            for (int i = 0; i <= lenX; i++)
                for (int j = 0; j <= lenZ; j++)
                {
                    map1[i, j].x = i;
                    map1[i, j].y = 1;
                    map1[i, j].z = j;
                    map1[i, j].gridType = hacker.GetMapType(i, lenY, j);
                }
            //初始化map2
            for (int i = 0; i <= lenX; i++)
                for (int j = 0; j <= lenZ; j++)
                {
                    map2[i, j].x = i;
                    map2[i, j].y = 2;
                    map2[i, j].z = j;
                    map2[i, j].gridType = hacker.GetMapType(i, lenY, j);
                    
                }

        }

        public void InitStart(Hacker hacker)
        {
            int[] _pos = new int[3];
            _pos = hacker.GetPosition();
            start.x = _pos[0];
            start.y = _pos[1];
            start.z = _pos[2];
        }
        public void Initend(Hacker hacker)
        {
            int[] p = hacker.GetExitPosition(0);
            end.x = p[0];
            end.y = p[1];
            end.z = p[2];
        }
        public void Update(Hacker hacker)
        {
            InitStart(hacker);
            InitMap(hacker);
            Astar astar = new Astar(map1, start, end, lenX, lenZ);
            List<Grid> path = astar.Run();
            if (path[1].x - path[0].x == 1)
                hacker.MoveEast();
            if (path[1].x - path[0].x == -1)
                hacker.MoveWest();
            if (path[1].z - path[0].z == 1)
                hacker.MoveNorth();
            if (path[1].z - path[0].z == -1)
                hacker.MoveSouth();

        }

    }

}
