using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellControl : MonoBehaviour
{
    public int x, y; //координаты клетки
    public bool allowPut;
  
    //public CellControl(int x, int y)
    //{
    //    this.x = x;
    //    this.y = y;
    //}
    
    public void OnTriggerEnter(Collider other)
    {

      
        
        if (other.gameObject.tag == "Player")
        {

            Game_Manager.instance.gameLogic.field[x][y] = false;
            if (Game_Manager.instance.gameLogic.IfPath() && (x!=0 || y!=0))
            {   
                Game_Manager.instance.ClearPath();
                Game_Manager.instance.gameLogic.PathToList();
                Game_Manager.instance.ShowPath();
                allowPut = true;

            }
            else
            {
                allowPut = false;
                Game_Manager.instance.gameLogic.field[x][y] = true;
            }
           
        }
    }
    
}


