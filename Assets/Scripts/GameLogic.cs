using System;
using System.Collections.Generic;
using System.Numerics;


public class GameLogic
{
   
    public bool[][] field; //матрица поля
    
    public List<Vector2> path=null; 
    Dictionary<Vector2, Vector2> p; //
    Vector2 start;
    Vector2 end;
    int width;
    int height;
    Vector2 NotVector;
    public GameLogic(int width, int height,  Vector2 start, Vector2 end)
    {
        this.start=start;
        this.end=end;

        this.height = height;
        this.width = width;
        NotVector = new Vector2(width, height);
        field = new bool[width][];
        for (int i = 0; i < field.Length; i++)
        {
            field[i] = new bool[height];
            for(int j = 0; j < field[i].Length; j++)
            field[i][j] = true;
        }
       
        IfPath();
        PathToList();
        

    }
    

    public void PathToList()
    {
        path = CreatePath(end);

    }



    public bool IfPath() //s - текущая позиция
    {
        p = new Dictionary<Vector2, Vector2>();
        //поиск в ширину
        Queue<Vector2> q = new Queue<Vector2>();
        q.Enqueue(start); //очередь куда сможем пойти
       
       HashSet<Vector2> l1 = new HashSet<Vector2>();//??add и contains у List слишком медленные необходима замена на более быструю коллекцию

        l1.Add(start); //история ходов
        Vector2[] l2; //соседние клетки (4 соседних)

        Vector2 n; //вектор, куда выгружается очередь
        while (q.Count != 0)
        {
            n = q.Dequeue(); //выгружаем очередь
            l2 = AvailableCells(n); //проверка на доступные клетки 

            for (int i = 0; i < l2.Length; i++)
            {
                if (!l1.Contains(l2[i]) && l2[i]!=NotVector)
                { //проверка, что мы не ходили в эту клетку

                    q.Enqueue(l2[i]); //
                    l1.Add(l2[i]);
                    p[l2[i]] = n;
                    if (l2[i] == end)
                    {
                        return true;
                    }


                }


            }



        }

        return false;


    }


    public List<Vector2> CreatePath(Vector2 e)//??Insert в листе слишком медленная операция O(n) наверное также лучше заменить
    {

        List<Vector2> l = new List<Vector2>();

        while (p.ContainsKey(e)) //пока не дойдем до e
        {

            l.Insert(0, e);
            e = p[e];
        }

        l.Insert(0, e);


        return l;


    }

    bool CheckCell(Vector2 t)
    {

        if (t.X >= 0 && t.X < field.Length && t.Y >= 0 && t.Y < field[0].Length)
        {
            //true - свободно, false - занято
            if (field[(int)t.X][(int)t.Y])
            {
                return true;
            }
        }

        return false;


    }

    //List<Vector2> AvailableCells(Vector2 s)//??можно разбить данную функцию на 4 потока-4 проверки 4 потока 
    //{

    //    List<Vector2> l = new List<Vector2>();

    //    Vector2 dir = end - s; //вектор куда надо идти

    //    //координаты вектора
    //    float x = Math.Sign(dir.X);
    //    float y = Math.Sign(dir.Y);

    //    Vector2 v1;
    //    Vector2 v2;
    //    //выбор оптимального пути
    //    //оптимальным путем будем считать тот путь, полученный из кратчайшего вектора, т.к. для бысмтрого преодоления нежно пройти наименьшй и самый оптимальный для нас путь.
    //    if (x == 0)
    //    {
    //        v1 = new Vector2(0, y);
    //        v2 = new Vector2(1, 0);
    //    }
    //    else if (y == 0)
    //    {
    //        v1 = new Vector2(x, 0);
    //        v2 = new Vector2(0, 1);

    //    }
    //    else
    //    {
    //        //для баланса игры будем делать так, чтобы вражеские юниты не ходили по краю карты.
    //        if (end.X == 0 || end.X == field.Length - 1)
    //        {
    //            v1 = new Vector2(0, y);
    //            v2 = new Vector2(x, 0);
    //        }
    //        else
    //        {
    //            v1 = new Vector2(x, 0);
    //            v2 = new Vector2(0, y);
    //        }
    //    }
    //    if (CheckCell(s + v1))
    //    { //самый оптимальный вектор добавляется первым
    //        l.Add(s + v1);
    //    }
    //    //если не добавится оптимальный, то добавится хотя бы один из следующих
    //    if (CheckCell(s + v2))
    //    {
    //        l.Add(s + v2);
    //    }
    //    if (CheckCell(s - v2))
    //    {
    //        l.Add(s - v2);
    //    }

    //    if (CheckCell(s - v1))
    //    {
    //        l.Add(s - v1);
    //    }
    //    //получили позиции, куда можно пойти, где первый вектор может быть самым оптимальным
    //    return l;
    //}


    Vector2[] AvailableCells(Vector2 s)//??можно разбить данную функцию на 4 потока-4 проверки 4 потока 
    {

        
        Vector2[] l = new Vector2[4];

        Vector2 dir = end - s; //вектор куда надо идти

        //координаты вектора
        float x = Math.Sign(dir.X);
        float y = Math.Sign(dir.Y);

        Vector2 v1;
        Vector2 v2;
        //выбор оптимального пути
        //оптимальным путем будем считать тот путь, полученный из кратчайшего вектора, т.к. для бысмтрого преодоления нежно пройти наименьшй и самый оптимальный для нас путь.
        if (x == 0)
        {
            v1 = new Vector2(0, y);
            v2 = new Vector2(1, 0);
        }
        else if (y == 0)
        {
            v1 = new Vector2(x, 0);
            v2 = new Vector2(0, 1);

        }
        else
        {
            //для баланса игры будем делать так, чтобы вражеские юниты не ходили по краю карты.
            if (end.X == 0 || end.X == field.Length - 1)
            {
                v1 = new Vector2(0, y);
                v2 = new Vector2(x, 0);
            }
            else
            {
                v1 = new Vector2(x, 0);
                v2 = new Vector2(0, y);
            }
        }

       
        if (CheckCell(s + v1))
        { //самый оптимальный вектор добавляется первым
            l[0]=s + v1;
        }
        else
        {

            l[0] = NotVector;
        }
        
        //если не добавится оптимальный, то добавится хотя бы один из следующих
        if (CheckCell(s + v2))
        {
            l[1]=s + v2;
        }
        else
        {

            l[1] = NotVector;
        }

        if (CheckCell(s - v2))
        {
            l[2]=s - v2;
        }
        else
        {

            l[2] = NotVector;
        }


        if (CheckCell(s - v1))
        {
            l[3]=s - v1;
        }
        else
        {

            l[3] = NotVector;
        }

        //получили позиции, куда можно пойти, где первый вектор может быть самым оптимальным
        return l;
    }





}