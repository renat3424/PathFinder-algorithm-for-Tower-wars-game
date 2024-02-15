using System;
using System.Collections.Generic;
using System.Numerics;


public class GameLogic
{
   
    public bool[][] field; //������� ����
    
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



    public bool IfPath() //s - ������� �������
    {
        p = new Dictionary<Vector2, Vector2>();
        //����� � ������
        Queue<Vector2> q = new Queue<Vector2>();
        q.Enqueue(start); //������� ���� ������ �����
       
       HashSet<Vector2> l1 = new HashSet<Vector2>();//??add � contains � List ������� ��������� ���������� ������ �� ����� ������� ���������

        l1.Add(start); //������� �����
        Vector2[] l2; //�������� ������ (4 ��������)

        Vector2 n; //������, ���� ����������� �������
        while (q.Count != 0)
        {
            n = q.Dequeue(); //��������� �������
            l2 = AvailableCells(n); //�������� �� ��������� ������ 

            for (int i = 0; i < l2.Length; i++)
            {
                if (!l1.Contains(l2[i]) && l2[i]!=NotVector)
                { //��������, ��� �� �� ������ � ��� ������

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


    public List<Vector2> CreatePath(Vector2 e)//??Insert � ����� ������� ��������� �������� O(n) �������� ����� ����� ��������
    {

        List<Vector2> l = new List<Vector2>();

        while (p.ContainsKey(e)) //���� �� ������ �� e
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
            //true - ��������, false - ������
            if (field[(int)t.X][(int)t.Y])
            {
                return true;
            }
        }

        return false;


    }

    //List<Vector2> AvailableCells(Vector2 s)//??����� ������� ������ ������� �� 4 ������-4 �������� 4 ������ 
    //{

    //    List<Vector2> l = new List<Vector2>();

    //    Vector2 dir = end - s; //������ ���� ���� ����

    //    //���������� �������
    //    float x = Math.Sign(dir.X);
    //    float y = Math.Sign(dir.Y);

    //    Vector2 v1;
    //    Vector2 v2;
    //    //����� ������������ ����
    //    //����������� ����� ����� ������� ��� ����, ���������� �� ����������� �������, �.�. ��� ��������� ����������� ����� ������ ��������� � ����� ����������� ��� ��� ����.
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
    //        //��� ������� ���� ����� ������ ���, ����� ��������� ����� �� ������ �� ���� �����.
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
    //    { //����� ����������� ������ ����������� ������
    //        l.Add(s + v1);
    //    }
    //    //���� �� ��������� �����������, �� ��������� ���� �� ���� �� ���������
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
    //    //�������� �������, ���� ����� �����, ��� ������ ������ ����� ���� ����� �����������
    //    return l;
    //}


    Vector2[] AvailableCells(Vector2 s)//??����� ������� ������ ������� �� 4 ������-4 �������� 4 ������ 
    {

        
        Vector2[] l = new Vector2[4];

        Vector2 dir = end - s; //������ ���� ���� ����

        //���������� �������
        float x = Math.Sign(dir.X);
        float y = Math.Sign(dir.Y);

        Vector2 v1;
        Vector2 v2;
        //����� ������������ ����
        //����������� ����� ����� ������� ��� ����, ���������� �� ����������� �������, �.�. ��� ��������� ����������� ����� ������ ��������� � ����� ����������� ��� ��� ����.
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
            //��� ������� ���� ����� ������ ���, ����� ��������� ����� �� ������ �� ���� �����.
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
        { //����� ����������� ������ ����������� ������
            l[0]=s + v1;
        }
        else
        {

            l[0] = NotVector;
        }
        
        //���� �� ��������� �����������, �� ��������� ���� �� ���� �� ���������
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

        //�������� �������, ���� ����� �����, ��� ������ ������ ����� ���� ����� �����������
        return l;
    }





}