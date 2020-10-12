using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour
{
    public  int[,] ArrayMatrix; // матрица местности

    public  int x, y; // ширина и высота поля


    public GenerateMap generateMap;



    public  void printMatrix()
    {
        string arr = "";
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
             if(ArrayMatrix[i, j] == 99)
                {
                    arr += "#";
                }
             else
                {
                    arr += "$";
                }
                
            }
            arr += "\n";
        }
        print(arr);
    }


}
