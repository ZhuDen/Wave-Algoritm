using UnityEngine;
using System;

public class FindPatch : MonoBehaviour
{
    int x = 0;
    int y = 0;
    int nx = 0;
    int ny = 0;
    public GenerateMap generateMap;
    public int[] NumsX, NumsY;
    public float[] PosX, PosY;
    RaycastHit hit;
    public int[] FreePointX, FreePointY;
    public int CountFreePoints;
    public Algoritm algoritm;

    public void CheckMap(int[,] _map, int _width, int _height, int startX, int startY, int endX, int endY)
    {
        Algoritm Path = new Algoritm(_map, _width, _height);
        Path.traceOut();
   
        
            x = startX;

            y = startY;

            nx = endX;
   
            ny = endY;
        print("Start: " + x + " " + y + " End: " + nx + " " + ny);
        Path.findPath(x, y, nx, ny);

        Path.traceOut();

        Path.waveOut();
        Path.ReturnFullPatch(out NumsX, out NumsY);

        PosX = new float[NumsX.Length];
        PosY = new float[NumsY.Length];
        generateMap.PointsPatch = new Vector3[0];
        generateMap.PointsPatch = new Vector3[NumsX.Length];

        for (int i = 0; i < NumsX.Length; i++)
        {
            generateMap.ImagesCell[NumsY[i], NumsX[i]].color = Color.cyan;
            generateMap.PointsPatch[i] = generateMap.PointCells[NumsY[i], NumsX[i]];
        }
      
    }
    public void GetMyPoint(string NamePlayer, out int _x, out int _y)
    {
        generateMap.StartPosX = generateMap.StartPosX_Save;
        generateMap.StartPosY = generateMap.StartPosY_Save;
        int x = 0, y = 0;
        for (int i = 0; i < generateMap.Width; i++)
        {
            for (int j = 0; j < generateMap.Height; j++)
            {
            
               
                if (Physics.Raycast(new Vector3(generateMap.StartPosX, generateMap.StartPosY, -1f), Vector3.forward, out hit))
                {
                    if (hit.collider)
                    {
                       
                        if (hit.collider.name == NamePlayer)
                        {
                            print(j + " " + i);
                            x = j;
                            y = i;
                            break;
                        }
                    }
                }
                
                generateMap.StartPosX += generateMap.StepX;
        
            }
            generateMap.StartPosX = generateMap.StartPosX_Save;
            generateMap.StartPosY -= generateMap.StepY;
        }
        generateMap.StartPosX = generateMap.StartPosX_Save;
        generateMap.StartPosY = generateMap.StartPosY_Save;
        _x = x;
        _y = y;
    }
    public void GetEatPoint(string NameEat, out int _x, out int _y)
    {
        generateMap.StartPosX = generateMap.StartPosX_Save;
        generateMap.StartPosY = generateMap.StartPosY_Save;
        int x = 0, y = 0;
        for (int i = 0; i < generateMap.Width; i++)
        {
            for (int j = 0; j < generateMap.Height; j++)
            {


                if (Physics.Raycast(new Vector3(generateMap.StartPosX, generateMap.StartPosY, -1f), Vector3.forward, out hit))
                {
                    if (hit.collider)
                    {

                        if (hit.collider.name == NameEat)
                        {
                            print(j + " " + i);
                            x = j;
                            y = i;
                            break;
                        }
                    }
                }

                generateMap.StartPosX += generateMap.StepX;

            }
            generateMap.StartPosX = generateMap.StartPosX_Save;
            generateMap.StartPosY -= generateMap.StepY;
        }
        generateMap.StartPosX = generateMap.StartPosX_Save;
        generateMap.StartPosY = generateMap.StartPosY_Save;
        _x = x;
        _y = y;
    }
    public int GetRandomPoint ()
    {
        print("Длина рандомного: 0 - " + FreePointX.Length);
        return UnityEngine.Random.Range(0, FreePointX.Length);
    }

}


