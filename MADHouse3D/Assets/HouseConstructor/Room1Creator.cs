using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.HouseConstructor
{
    class Room1Creator
    {
        GameObject room;
        GameObject[] walls;
        GameObject[,] bricks;

        private float x1;
        private float y1;
        private float x2;
        private float y2;
        private float xm;
        private float ym;
        private int length1;
        private int width1;
        private int length2;
        private int width2;

        static int roomNo = 1;
        int wallNo = 1;

        public float X1 { get => x1; set => x1 = value; }
        public float Y1 { get => y1; set => y1 = value; }
        public float X2 { get => x1; set => x1 = value; }
        public float Y2 { get => y1; set => y1 = value; }
        public float Xm { get => xm; set => xm = value; }
        public float Ym { get => ym; set => ym = value; }
        public int Length1 { get => length1; set => length1 = value; }
        public int Width1 { get => width1; set => width1 = value; }
        public int Length2 { get => length2; set => length2 = value; }
        public int Width2 { get => width2; set => width2 = value; }
        public GameObject Room { get => room; set => room = value; }
        public GameObject[] Walls { get => walls; set => walls = value; }
        public GameObject[,] Bricks { get => bricks; set => bricks = value; }

        public Room1Creator(float x, float y, int length1, int width1, int length2, int width2)
        {
            Room = GameObject.Instantiate(GameObject.Find("EmptyObject"));
            Walls = new GameObject[6];
            for (int i = 0; i < 4; i++)
                Walls[i] = GameObject.Instantiate(GameObject.Find("EmptyObject"));
            Debug.Log("Walls Created");
            //walls = new WallCreator[4];
            X1 = x;
            Y1 = y;
            Length1 = length1;
            Width1 = width1;
            Length2 = length2;
            Width2 = width2;
            Bricks = new GameObject[(2 * (Length1 + Length2)) + (2 * (Width1 + Width2)) + 1, 6];
            CreateRoom();
        }

        private void CreateRoom()
        {
            Room.name = "Room " + roomNo++;
            Room.transform.position = new Vector3(X1, 0, Y1);

            int i;
            int j;
            int k;
            int I;

            for (i = 0; i < Length1; i++)
            {
                for (j = 0; j < 6; j++)
                {
                    Vector3 position = new Vector3(X1 + i, j, Y1);
                    GameObject[] objs = GameObject.FindGameObjectsWithTag("brick");
                    foreach (GameObject obj in objs)
                    {
                        if (obj.transform.position == position && obj.transform.parent.parent.name != Room.name)
                            goto NextBrick;
                    }

                    if (i == 0 || i == Length1 - 1)
                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("CornerBrickX"));

                    else
                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("EdgeBrick"));

                    //Bricks[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //Bricks[i, j].transform.localScale = new Vector3(1f, 1f, 1f);
                    Bricks[i, j].transform.position = position;
                    //Bricks[i, j].GetComponent<Renderer>().material.color = new Color(0.85f, 0.75f, 0.65f);
                    //Bricks[i, j].AddComponent<BoxCollider>();

                    Bricks[i, j].transform.parent = Walls[0].transform;

                NextBrick:
                    ;
                }
            }

            Walls[0].name = "Wall " + wallNo;
            Walls[0].transform.parent = Room.transform;
            wallNo++;

            I = i;
            X2 = X1 + i - 1;
            k = 0;

            for (; i < I + Width1; i++, k++)
            {
                for (j = 0; j < 6; j++)
                {
                    Vector3 position = new Vector3(X2, j, Y1 + k);
                    GameObject[] objs = GameObject.FindGameObjectsWithTag("brick");
                    foreach (GameObject obj in objs)
                    {
                        if (obj.transform.position == position && obj.transform.parent.parent.name != Room.name)
                            goto NextBrick;
                    }

                    if (i == I)
                    {

                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("CornerBrickZ"));
                    }
                    else if (i == I + Width1 - 1)
                    {
                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("CornerBrickZ"));
                        Bricks[i, j].transform.Rotate(0, 180, 0);
                    }
                    else
                    {
                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("EdgeBrick"));
                        Bricks[i, j].transform.Rotate(0, -90, 0);
                    }
                    //Bricks[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //Bricks[i, j].transform.localScale = new Vector3(1f, 1f, 1f);
                    Bricks[i, j].transform.position = position;

                    //Bricks[i, j].GetComponent<Renderer>().material.color = new Color(0.85f, 0.75f, 0.65f);
                    //Bricks[i, j].AddComponent<BoxCollider>();

                    Bricks[i, j].transform.parent = Walls[1].transform;

                NextBrick:
                    ;
                }
            }

            Walls[1].name = "Wall " + wallNo;
            Walls[1].transform.parent = Room.transform;
            wallNo++;

            I = i;
            Y2 = Y1 + k - 1;
            k = 0;

            for (; i < I + Length2; i++, k++)
            {
                for (j = 0; j < 6; j++)
                {
                    Vector3 position = new Vector3(X2 + k, j, Y2);
                    GameObject[] objs = GameObject.FindGameObjectsWithTag("brick");
                    foreach (GameObject obj in objs)
                    {
                        if (obj.transform.position == position && obj.transform.parent.parent.name != Room.name)
                            goto NextBrick;
                    }

                    if (i == I)
                    {
                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("CornerBrickX"));
                        Bricks[i, j].transform.Rotate(0, 180, 0);
                    }
                    else if (i == I + Length2 - 1)
                    {
                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("CornerBrickX"));
                        Bricks[i, j].transform.Rotate(0, 180, 0);
                    }
                    else
                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("EdgeBrick"));

                    //Bricks[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //Bricks[i, j].transform.localScale = new Vector3(1f, 1f, 1f);
                    Bricks[i, j].transform.position = position;
                    Bricks[i, j].transform.Rotate(0, -180, 0);
                    //Bricks[i, j].GetComponent<Renderer>().material.color = new Color(0.85f, 0.75f, 0.65f);
                    //Bricks[i, j].AddComponent<BoxCollider>();

                    Bricks[i, j].transform.parent = Walls[2].transform;

                NextBrick:
                    ;
                }
            }

            Walls[2].name = "Wall " + wallNo;
            Walls[2].transform.parent = Room.transform;
            wallNo++;

            I = i;
            X2 += (k - 1);
            k = 0;

            for (; i < I + Width2; i++, k++)
            {
                for (j = 0; j < 6; j++)
                {
                    Vector3 position = new Vector3(X2, j, Y2 + k);
                    GameObject[] objs = GameObject.FindGameObjectsWithTag("brick");
                    foreach (GameObject obj in objs)
                    {
                        if (obj.transform.position == position && obj.transform.parent.parent.name != Room.name)
                            goto NextBrick;
                    }

                    if (i == I || i == I + Width2 - 1)
                    {
                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("CornerBrickZ"));
                    }
                    else
                    {
                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("EdgeBrick"));
                        Bricks[i, j].transform.Rotate(0, -270, 0);
                    }
                    //Bricks[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //Bricks[i, j].transform.localScale = new Vector3(1f, 1f, 1f);
                    Bricks[i, j].transform.position = position;
                    //Bricks[i, j].GetComponent<Renderer>().material.color = new Color(0.85f, 0.75f, 0.65f);
                    //Bricks[i, j].AddComponent<BoxCollider>();

                    Bricks[i, j].transform.parent = Walls[3].transform;

                NextBrick:
                    ;
                }
            }

            I = i;
            Y2 += k;
            k = 0;

            for (; i < I + Length1 + Length2 - 1; i++, k++)
            {
                for (j = 0; j < 6; j++)
                {
                    Vector3 position = new Vector3(X2 - k, j, Y2);
                    GameObject[] objs = GameObject.FindGameObjectsWithTag("brick");
                    foreach (GameObject obj in objs)
                    {
                        if (obj.transform.position == position && obj.transform.parent.parent.name != Room.name)
                            goto NextBrick;
                    }

                    if (i == I || i == I + Length1 + Length2 - 1)
                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("CornerBrickX"));
                    else
                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("EdgeBrick"));

                    //Bricks[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //Bricks[i, j].transform.localScale = new Vector3(1f, 1f, 1f);
                    Bricks[i, j].transform.position = position;
                    Bricks[i, j].transform.Rotate(0, -180, 0);
                    //Bricks[i, j].GetComponent<Renderer>().material.color = new Color(0.85f, 0.75f, 0.65f);
                    //Bricks[i, j].AddComponent<BoxCollider>();

                    Bricks[i, j].transform.parent = Walls[2].transform;

                NextBrick:
                    ;
                }
            }

            Walls[2].name = "Wall " + wallNo;
            Walls[2].transform.parent = Room.transform;
            wallNo++;

            I = i;
            X2 -= k;
            k = 0;

            for (; i < I + Width1 + Width2 - 1; i++, k++)
            {
                for (j = 0; j < 6; j++)
                {
                    Vector3 position = new Vector3(X1 + 1, j, Y2 - k);
                    GameObject[] objs = GameObject.FindGameObjectsWithTag("brick");
                    foreach (GameObject obj in objs)
                    {
                        if (obj.transform.position == position && obj.transform.parent.parent.name != Room.name)
                            goto NextBrick;
                    }

                    if (i == I || i == I + Width1 + Width2 - 1)
                    {
                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("CornerBrickZ"));
                        Bricks[i, j].transform.Rotate(0, 180, 0);
                    }
                    else
                    {
                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("EdgeBrick"));
                        Bricks[i, j].transform.Rotate(0, -270, 0);
                    }
                    //Bricks[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //Bricks[i, j].transform.localScale = new Vector3(1f, 1f, 1f);
                    Bricks[i, j].transform.position = position;
                    //Bricks[i, j].GetComponent<Renderer>().material.color = new Color(0.85f, 0.75f, 0.65f);
                    //Bricks[i, j].AddComponent<BoxCollider>();

                    Bricks[i, j].transform.parent = Walls[3].transform;

                NextBrick:
                    ;
                }
            }

            Walls[3].name = "Wall " + wallNo;
            Walls[3].transform.parent = Room.transform;

        }
    }
}
