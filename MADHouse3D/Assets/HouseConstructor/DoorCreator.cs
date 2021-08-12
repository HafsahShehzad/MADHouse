using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.HouseConstructor
{
    class DoorCreator
    {
        GameObject door;
        WallCreator wall;
        float x;
        float y;
        int length;
        int height;

        bool isX = false;
        bool isZ = false;

        public DoorCreator(float x, float y, int length = 2, int height = 4)
        {
            //this.wall = wall;
            this.x = x;
            this.y = y;
            this.length = length;
            this.height = height;

            CreateDoor();
        }

        [Obsolete]
        public void CreateDoor()
        {
            Vector3 position;
            GameObject[] objs = GameObject.FindGameObjectsWithTag("brick");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (isX)
                        position = new Vector3(x + j, 0 + i, y);
                    else if (isZ)
                        position = new Vector3(x, 0 + i, y + j);
                    else
                        position = new Vector3(x, 0 + i, y);

                    for (int k = 0; k < 2; k++)
                    {
                        foreach (GameObject obj in objs)
                        {
                            if (obj.transform.position == position)
                            {
                                //if (obj.transform.name == "CornerBrickX(Clone)" || obj.transform.name == "CornerBrickZ(Clone)")
                                //{
                                //    //UnityEditor.EditorUtility.DisplayDialog("Corner Brick", obj.transform.name, "OK");
                                //    continue;
                                //}

                                //obj.transform.Find("Texture1").GetComponent<Renderer>().material = Resources.Load("wood", typeof(Material)) as Material;
                                //obj.GetComponentInChildren<Material>().SetTexture("_MainTex", Resources.Load("wood", typeof(Texture)) as Texture);
                                GameObject.Destroy(obj);
                                //GameObject newobj = GameObject.Instantiate(obj);
                                //UnityEditor.EditorUtility.DisplayDialog("Something", obj.transform.localPosition.x.ToString(), "OK");
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
            }

            door = GameObject.Instantiate(GameObject.Find("Door"));
            GameObject floorBrick = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floorBrick.tag = "floor";
            if (isX)
            {
                floorBrick.transform.localScale += new Vector3(1f, -0.9f, 0);
                floorBrick.transform.position = new Vector3(((x + x + length) / 2) - 0.5f, 0, y);

                door.transform.position = new Vector3(((x + x + length) / 2) - 0.5f, ((0 + 4) / 2) - 0.5f, y);
                door.transform.name = "DoorX";
            }
            else
            {
                floorBrick.transform.localScale += new Vector3(0f, -0.9f, 1f);
                floorBrick.transform.position = new Vector3(x, 0, ((y + y + length) / 2) - 0.5f);

                door.transform.position = new Vector3(x, ((0 + 4) / 2) - 0.5f, ((y + y + length) / 2) - 0.5f);
                door.transform.Rotate(0, 90, 0);
                door.transform.name = "DoorZ";
            }
        }
    }
}
