using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIControll : MonoBehaviour
{

    
     


    
    GameObject MouseClick()
    {
        GameObject resObject=null;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
    
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, 100, ~LayerMask.GetMask("Environment", "IgnoreRaycast")))
            {
              
                    resObject = hit.transform.gameObject;


                    Debug.Log(resObject);
                

            }


        }

        return resObject;


    }

    
    
     void KeyClickListener()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");

        }

    }

  
    void Update()
    {
        KeyClickListener();
        MouseClick();
    }



}
