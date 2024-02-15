using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Material col1;
    GameObject parent;
    bool activeCell=true;
    GameObject cellObj;
    public Material col2;
    public Material col3;
    public GameObject obj;

    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (cellObj == null)
        {

            activeCell = true;
     
        }


    }
    void OnMouseEnter()
    {
        
        gameObject.GetComponent<Renderer>().material = col1;
            
    }

void OnMouseExit()
    {
        int x = parent.GetComponent<CellControl>().x;
        int y= parent.GetComponent<CellControl>().y;
        if (Game_Manager.instance.gameLogic.path.Contains(new System.Numerics.Vector2(x, y)))
        {
            gameObject.GetComponent<Renderer>().material = col3;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = col2;
        }
        
    }

       void OnMouseDown()
    {
        if (activeCell)
        {

           

            gameObject.transform.parent.gameObject.GetComponent<CellControl>().OnTriggerEnter(obj.GetComponent<Collider>());
            if (gameObject.transform.parent.gameObject.GetComponent<CellControl>().allowPut)
            {
                cellObj = Instantiate(obj, transform.position + new Vector3(0, 1, 0), obj.transform.rotation);
                Game_Manager.instance.activeUnits.Add(cellObj);
                activeCell = false;
            }


        }
    }
}
