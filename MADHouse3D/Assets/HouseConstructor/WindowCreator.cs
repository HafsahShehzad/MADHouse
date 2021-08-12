using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.HouseConstructor
{
    class WindowCreator
    {
        GameObject window;

        WallCreator wall;
        RoomCreator room;
        
        int pos; // remove later
        float x;
        float y;
        int length;
        int height;

        bool isX = false;
        bool isZ = false;

        public WindowCreator(float x, float y, int length = 4, int height = 3)
        {
            //this.wall = wall;
            this.x = x;
            this.y = y;
            this.length = length;
            this.height = height;

            CreateWindow();
        }

        public void CreateWindow()
        {
            Vector3 position;
            GameObject[] objs = GameObject.FindGameObjectsWithTag("brick");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (isX)
                        position = new Vector3(x + j, 2 + i, y);
                    else if (isZ)
                        position = new Vector3(x, 2 + i, y + j);
                    else
                        position = new Vector3(x, 2 + i, y);

                    foreach (GameObject obj in objs)
                    {
                        if (obj.transform.position == position)
                        {
                            if (obj.transform.name == "CornerBrickX(Clone)" || obj.transform.name == "CornerBrickZ(Clone)")
                            {
                                //UnityEditor.EditorUtility.DisplayDialog("Corner Brick", obj.transform.name, "OK");
                                break;
                            }
                            GameObject.Destroy(obj);
                            if (!isX || !isZ)
                            {
                                int wallNo = obj.transform.parent.name[5] - '0';
                                if (wallNo % 2 == 1)
                                    isX = true;
                                else
                                    isZ = true;
                            }
                        }
                    }
                }
            }

            window = GameObject.Instantiate(GameObject.Find("Window"));
            if (isX)
                window.transform.position = new Vector3(((x + x + length) / 2) - 0.5f, (2 + 5) / 2, y);
            else
            {
                window.transform.position = new Vector3(x, (2 + 5) / 2, ((y + y + length) / 2) - 0.5f);
                window.transform.Rotate(0, 90, 0);
            }
        }

        public void CreateWindowWall()
        {
            for (int i = pos; i < pos + length; i++)
            {
                int h = 3;
                if (height == 3)
                    h = 2;
                else if (height == 4)
                    h = 1;
                else if (height == 5)
                    h = 0;

                for (int j = h; j < h + height; j++)
                {
                    GameObject.Destroy(wall.bricks[i, j]);
                }
            }
        }

        public void CreateWindowRoom(int wallNo)
        {
            for (int i = pos; i < pos + length; i++)
            {
                int h = 3;
                if (height == 3)
                    h = 2;
                else if (height == 4)
                    h = 1;
                else if (height == 5)
                    h = 0;

                for (int j = h; j < h + height; j++)
                    GameObject.Destroy(room.Bricks[i, j]);
            }
        }
    }
}
