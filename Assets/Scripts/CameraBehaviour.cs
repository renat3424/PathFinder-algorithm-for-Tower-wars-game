﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public float movementSpeed;//скорость передвижения камеры
    public float movementTime;//время перемещения от одной позиции к другой
    public Transform cameraPoint;//камера(позиция и поворот)
    
    Vector3 curPos;//текущая позиция камеры
    // Start is called before the first frame update
    void Awake()
    {
        transform.position = new Vector3(-5/2, 5, -5/2);//начальное положение камеры
        
        cameraPoint.rotation= Quaternion.Euler(45, 0, 0);//поворот камеры, так чтобы она смотрела под углом 45
        transform.rotation = Quaternion.Euler(0, 45, 0);//поворот камеры на угол 45 от оси x и z
        movementSpeed = 25;
        curPos = transform.position;
        movementTime = 10;
        Cursor.lockState = CursorLockMode.Confined;//установка курсора, так чтобы он находился в пределах экрана
    }

     Vector3 Movements()//движение в плоскости с помощью мыши
    {
        Vector3 x = Vector3.zero;
        Vector3 y = Vector3.zero;

        if (Input.mousePosition.y <= 1)
        {

            y = -transform.forward;
        }else if (Input.mousePosition.y >= Screen.height-1)
        {
            y = transform.forward;

        }


        if (Input.mousePosition.x <= 1)
        {

            x = -transform.right;
        }
        else if (Input.mousePosition.x >= Screen.width - 1)
        {
            x = transform.right;

        }

        return x+y;

    }


    // Update is called once per frame
    void Update()
    {


        curPos = curPos + (cameraPoint.forward*Input.mouseScrollDelta.y + Movements()).normalized * movementSpeed * Time.deltaTime;//скролл мышью и/или движение в плоскости(? возможно будет лишь или)
        transform.position = Vector3.Lerp(transform.position, curPos, movementTime);//плавное передвижение от одной позиции к другой в течении времени movementTime
        
        

    }
}
