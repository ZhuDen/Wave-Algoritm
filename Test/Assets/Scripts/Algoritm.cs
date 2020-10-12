using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algoritm : MonoBehaviour
{
    int width;
    int height;
    int wall = 99;
    int[,] map;
    List<Point> wave = new List<Point>();
    public int[] NumsX, NumsY;
    public Algoritm(int[,] _map, int width, int height)
    {
        this.width = width;
        this.height = height;
        map = _map;

    }

    public void block(int x, int y)
    {
        //заполняем карту препятствиями
        map[y, x] = wall;
    }
   

    public void findPath(int x, int y, int nx, int ny)
    {
        if (map[y, x] == wall || map[ny, nx] == wall)
        {
            print("Вы выбрали препятствие");
            return;
        }

        //волновой алгоритм поиска пути (заполнение значений достижимости) начиная от конца пути
        int[,] cloneMap = (int[,])map.Clone();
        List<Point> oldWave = new List<Point>();
        oldWave.Add(new Point(nx, ny));
        int nstep = 0;
        map[ny, nx] = nstep;

        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { -1, 0, 1, 0 };

        while (oldWave.Count > 0)
        {
            nstep++;
            wave.Clear();
            foreach (Point i in oldWave)
            {
                for (int d = 0; d < 4; d++)
                {
                    nx = i.x + dx[d];
                    ny = i.y + dy[d];


                    if (map[ny, nx] == -1)
                    {
                        wave.Add(new Point(nx, ny));
                        map[ny, nx] = nstep;
                    }
                }
            }
            oldWave = new List<Point>(wave);
        }
        //traceOut(); //посмотреть распространение волны

        bool flag = true;
        wave.Clear();
        wave.Add(new Point(x, y));
        while (map[y, x] != 0)
        {
            flag = true;
            for (int d = 0; d < 4; d++)
            {
                nx = x + dx[d];
                ny = y + dy[d];
                if (map[y, x] - 1 == map[ny, nx])
                {
                    x = nx;
                    y = ny;
                    wave.Add(new Point(x, y));
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                print("Пути нет!");
                break;
            }
        }

        map = cloneMap;
        int count = 0;

        wave.ForEach(delegate (Point i)
        {
            print("count");
            count++;
        });
        NumsX = new int[count];
        NumsY = new int[count];
        int n = 0;
        wave.ForEach(delegate (Point i)
        {
            NumsX[n] = i.x;
            NumsY[n] = i.y;
            map[i.y, i.x] = 0;
            n++;
        });
    }
    public void ClearPath()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if(map[i, j] == 0)
                {
                    map[i, j] = -1;
                }
            }
        }
    }
    public void ReturnFullPatch (out int[] xC, out int[] yC)
    {
        xC = NumsX;
        yC = NumsY;
    }
    struct Point
    {
        public Point(int x, int y)
            : this()
        {
            this.x = x;
            this.y = y;
        }
        public int x;
        public int y;
    }

    public void waveOut() // вывод координат пути
    {
        wave.ForEach(delegate (Point i)
        {
           // print("x = " + i.x + ", y = " + i.y);
        });
      
    }

    public void traceOut() // вывод таблицы
    {


       /* string m = null;
        print("   ");

        print("\n");
        for (int i = 0; i < width; i++)
        {

            for (int j = 0; j < height; j++)
            {
                m += map[i, j] > 9 || map[i, j] < 0 ? map[i, j] + " " : map[i, j] + "  ";
            }
            m += "\n";
         
        }
           print(m);*/
    }

}

