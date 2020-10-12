using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float SpeedMove, MinDist;
    public FindPatch findPatch;
    public GenerateMap generateMap;
    public Matrix matrix;
    public enum States
    {
        Idle,
        PatrolMove,
        EatMove
    }
    public States states;
    private int NumPoint;
    public int MyX, MyY;
    public float TimeEat, TimeIdle;
    private float tmEat, tm;
    public Text textUI;

    private void Start()
    {
        tmEat = TimeEat;
        tm = TimeIdle;
    }
    private void RegenerateMap ()
    {
        generateMap.Regenerate();
    }
    void Update()
    {
       /* if (states != States.EatMove)
        {
            tmEat -= Time.deltaTime;
            textUI.text = Mathf.RoundToInt(tmEat).ToString();
            if (tmEat <= 0)
            {
                if (MyX == 0 && MyY == 0)
                {
                    findPatch.GetMyPoint(this.gameObject.name, out MyX, out MyY);
                }
                print("My point " + MyX + " " + MyY);

                int eatX = 0, eatY = 0;
                findPatch.GetEatPoint("Eat", out eatX, out eatY);
               // print("Num rand " + rand + " " + "Rand point " + findPatch.FreePointX[rand] + " " + findPatch.FreePointY[rand]);
                findPatch.CheckMap(matrix.ArrayMatrix, generateMap.Max_X, generateMap.Max_Y, MyX, MyY, eatX, eatY);

                MyX = findPatch.FreePointX[eatX];
                MyY = findPatch.FreePointY[eatY];

                states = States.PatrolMove;
                generateMap.Regenerate();
                // Invoke("RegenerateMap", 2f);
                tmEat = TimeEat;
                states = States.PatrolMove;
            }
        }*/
        if (states == States.Idle)
        {
            tm += Time.deltaTime;
            if (tm >= TimeIdle)
            {
                if (MyX == 0 && MyY == 0)
                {
                    findPatch.GetMyPoint(this.gameObject.name, out MyX, out MyY);
                }
               // print("My point " + MyX + " " + MyY);
             
                int rand = findPatch.GetRandomPoint();
              //  print("Num rand " + rand + " " + "Rand point " + findPatch.FreePointX[rand] + " " + findPatch.FreePointY[rand]);
                findPatch.CheckMap(matrix.ArrayMatrix, generateMap.Max_X, generateMap.Max_Y, MyX, MyY, findPatch.FreePointX[rand], findPatch.FreePointY[rand]);
           
                MyX = findPatch.FreePointX[rand];
                MyY = findPatch.FreePointY[rand];
              
                states = States.PatrolMove;
                generateMap.Regenerate();
               // Invoke("RegenerateMap", 2f);
                tm = 0;
            }
            
        }
        if (states == States.PatrolMove)
        {
            if(NumPoint < generateMap.PointsPatch.Length)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, generateMap.PointsPatch[NumPoint], SpeedMove * Time.deltaTime);
                if(Vector3.Distance(this.transform.position, generateMap.PointsPatch[NumPoint]) < MinDist)
                {
                    NumPoint++;
                    if (NumPoint >= generateMap.PointsPatch.Length)
                    {
                        NumPoint = 0;
                        //generateMap.Regenerate();
                        states = States.Idle;
                    }
                }
            }
        }
     
    }
}
