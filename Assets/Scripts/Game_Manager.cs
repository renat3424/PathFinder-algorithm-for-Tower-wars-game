using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    int fieldWidth, fieldHeigth;
    public GameObject fieldCell; //клетка
    public GameObject[,] fieldCells; //массив клеток
    public GameLogic gameLogic;
    public List<GameObject> activeUnits;
    public Material pathCol;
    public Material fieldCol;
    public void Awake()
    {
        instance = this;
        activeUnits = new List<GameObject>();
        fieldWidth = int.Parse(FieldGeneration.instance.width.text);
        fieldHeigth = int.Parse(FieldGeneration.instance.height.text);
        fieldCells = new GameObject[fieldHeigth, fieldWidth];
        gameLogic = new GameLogic(fieldWidth, fieldHeigth, new System.Numerics.Vector2(0, 0), new System.Numerics.Vector2(fieldHeigth-1, fieldWidth-1));
        for (int i = 0; i < fieldHeigth; i++)
        {
            for (int j = 0; j < fieldWidth; j++)
            {
                //z - высота, x - ширина
                fieldCells[i, j] = Instantiate(fieldCell, new Vector3((fieldCell.transform.localScale.x + 0.05f) * j, 0, (fieldCell.transform.localScale.z + 0.05f) * i), fieldCell.transform.rotation);
                //Debug.Log($"{fieldCells[i, j].transform.position}");
                fieldCells[i, j].GetComponent<CellControl>().x = i;
                fieldCells[i, j].GetComponent<CellControl>().y = j;
            }
        }

        ShowPath();

   
    }


    public void ClearPath()
    {
        if (gameLogic.path != null)
        {
            foreach (System.Numerics.Vector2 vec in gameLogic.path)
            {
                int x = (int)vec.X, y = (int)vec.Y;
                GameObject fieldCube = fieldCells[x, y].transform.GetChild(0).gameObject;
                fieldCube.GetComponent<Renderer>().material = fieldCol;
            }

        }


    }
    public void ShowPath()
    {
       
        
        foreach (System.Numerics.Vector2 vec in gameLogic.path)
        {
            int x = (int)vec.X, y = (int)vec.Y;
            GameObject fieldCube = fieldCells[x, y].transform.GetChild(0).gameObject;
            fieldCube.GetComponent<Renderer>().material = pathCol;
        }


    }

    public void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }


   public void ClearUnits()
    {

        for (int i = 0; i < activeUnits.Count; i++)
        {
            
            Destroy(activeUnits[i]);
        }
        ClearPath();
        gameLogic = new GameLogic(fieldWidth, fieldHeigth, new System.Numerics.Vector2(0, 0), new System.Numerics.Vector2(fieldHeigth - 1, fieldHeigth - 1));
        ShowPath();
    }
}
