using UnityEngine;
using UnityEngine.UI;

public class GenerateMap : MonoBehaviour
{
    public int Width, Height;

    public float StartPosX, StartPosY;

    public float StepX, StepY;
    public int Max_X, Max_Y;
    public GameObject PrefabCell, PrefabCellCube;
    public Transform ParentCells;
    RaycastHit hit;
    public Matrix matrix;
    public Image[,] ImagesCell;
    public FindPatch findPatch;
    public Vector3[,]  PointCells;
    public Vector3[] PointsPatch;
    public float StartPosX_Save, StartPosY_Save;


    void Start()
    {
        Regenerate();
    }
    public void Regenerate ()
    {
        findPatch.CountFreePoints = 0;
        int countChild = ParentCells.childCount;
        for (int i = 0; i < countChild; i++)
        {
            Destroy(ParentCells.GetChild(i).gameObject);
        }
        matrix.x = Max_Y;
        matrix.y = Max_X;
        matrix.ArrayMatrix = new int[Max_Y, Max_X];
        ImagesCell = new Image[Max_Y, Max_X];
        StartPosX_Save = StartPosX;
        StartPosY_Save = StartPosY;
        PointCells = new Vector3[Max_Y, Max_X];

        int nm = 0;

        for (int i = 0; i < Max_Y; i++)
        {
            for (int j = 0; j < Max_X; j++)
            {
                //Instantiate(PrefabCellCube, new Vector3(StartPosX, StartPosY, -1f), PrefabCell.transform.rotation);
                int numWall = -1;
                GameObject gm = Instantiate(PrefabCell, ParentCells);
                gm.name = "Cell " + nm;
                nm++;
                ImagesCell[i, j] = gm.GetComponent<Image>();
                PointCells[i, j] = new Vector3(StartPosX, StartPosY, 0f);
                if (Physics.Raycast(new Vector3(StartPosX, StartPosY, -1f), Vector3.forward, out hit))
                {
                    if (hit.collider)
                    {
                        if (hit.collider.CompareTag("Wall"))
                        {
                            numWall = 99;

                            ImagesCell[i, j].color = Color.red;
                        }
                    }
                }
                if (numWall == -1)
                {
                    // if(findPatch.CountFreePoints < 84)
                    findPatch.CountFreePoints++;
                }
                StartPosX += StepX;
                matrix.ArrayMatrix[i, j] = numWall;
            }
            StartPosX = StartPosX_Save;
            StartPosY -= StepY;
        }
        StartPosX = StartPosX_Save;
        StartPosY = StartPosY_Save;
     
        findPatch.FreePointX = new int[findPatch.CountFreePoints];
        findPatch.FreePointY = new int[findPatch.CountFreePoints];
        print("add");
        int n = 0;
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (matrix.ArrayMatrix[i, j] == -1)
                {
                    findPatch.FreePointX[n] = j;
                    findPatch.FreePointY[n] = i;
                    n++;
                }
            }
        }
        matrix.printMatrix();
    }
}
