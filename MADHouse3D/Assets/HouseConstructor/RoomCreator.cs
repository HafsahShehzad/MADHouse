using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.HouseConstructor
{
    class RoomCreator
    {
        GameObject room;
        GameObject[] walls;
        //WallCreator[] walls;
        GameObject[,] bricks;

        private float x1;
        private float y1;
        private float x2;
        private float y2;
        private int length;
        private int width;

        static int roomNo = 1;
        int wallNo = 1;

        public float X1 { get => x1; set => x1 = value; }
        public float Y1 { get => y1; set => y1 = value; }
        public float X2 { get => x1; set => x1 = value; }
        public float Y2 { get => y1; set => y1 = value; }
        public int Length { get => length; set => length = value; }
        public int Width { get => width; set => width = value; }
        public GameObject Room { get => room; set => room = value; }
        public GameObject[] Walls { get => walls; set => walls = value; }
        public GameObject[,] Bricks { get => bricks; set => bricks = value; }
        
        public RoomCreator(float x, float y, int length, int width)
        {
            Room = GameObject.Instantiate(GameObject.Find("EmptyObject"));
            Walls = new GameObject[4];
            for (int i = 0; i < 4; i++)
                Walls[i] = GameObject.Instantiate(GameObject.Find("EmptyObject"));
            Debug.Log("Walls Created");
            //walls = new WallCreator[4];
            X1 = x;
            Y1 = y;
            Length = length;
            Width = width;
            Bricks = new GameObject[(2 * Length) + (2 * Width) + 1, 6];
            CreateRoom();
        }

        private void CreateRoom()
        {
            Room.name = "Room " + roomNo++;
            Room.transform.position = new Vector3(X1, 0, Y1);

            GameObject roof = GameObject.Instantiate(GameObject.Find("EmptyObject"));
            for (int a = 1; a < Length - 1; a++)
            {
                for (int b = 1; b < Width - 1; b++)
                {
                    GameObject roofBrick = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    roofBrick.tag = "roof";
                    roofBrick.transform.localScale -= new Vector3(0, 0.5f, 0);
                    roofBrick.transform.position = new Vector3(X1 + a, 5, Y1 + b);
                    //UnityEditor.EditorUtility.DisplayDialog("Coords", X1.ToString() + " " + Y1.ToString(), "OK");
                    roofBrick.transform.parent = roof.transform;
                }
            }

            GameObject floor = GameObject.Instantiate(GameObject.Find("EmptyObject"));
            for (int a = 1; a < Length - 1; a++)
            {
                for (int b = 1; b < Width - 1; b++)
                {
                    GameObject floorBrick = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    floorBrick.tag = "floor";
                    floorBrick.transform.localScale -= new Vector3(0, 0.9f, 0);
                    floorBrick.transform.position = new Vector3(X1 + a, 0, Y1 + b);
                    //UnityEditor.EditorUtility.DisplayDialog("Coords", X1.ToString() + " " + Y1.ToString(), "OK");
                    floorBrick.transform.parent = floor.transform;
                }
            }

            int i;
            int j;
            int k;
            int I;

            for (i = 0; i < Length; i++)
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

                    if (i == 0 || i == Length - 1)
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

            for (; i < I + Width; i++, k++)
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

                    if (i == I || i == I + Width - 1)
                        Bricks[i, j] = GameObject.Instantiate(GameObject.Find("CornerBrickZ"));
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

            for (; i < I + Length; i++, k++)
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

                    if (i == I || i == I + Length - 1)
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

            for (; i < I + Width; i++, k++)
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

                    if (i == I || i == I + Width - 1)
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
