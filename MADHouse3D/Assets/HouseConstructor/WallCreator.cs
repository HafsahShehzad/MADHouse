using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.HouseConstructor
{
    class WallCreator
    {
        public GameObject wall;
        public GameObject[,] bricks;
        private static int wallNo = 1;

        // Coordinates
        float x;
        float y;
        int length;
        int direction;

        public WallCreator(float x, float y, int length, int direction)
        {
            this.x = x;
            this.y = y;
            this.length = length;
            this.direction = direction;

            CreateWall();
        }

        private void CreateWall()
        {
            bricks = new GameObject[length, 6];

            wall = GameObject.Instantiate(GameObject.Find("EmptyObject"));
            wall.name = "Wall" + wallNo;
            wall.transform.position = new Vector3(x, 0, y);

            wallNo++;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    bricks[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    bricks[i, j].transform.localScale = new Vector3(1f, 1f, 0.5f);
                    bricks[i, j].transform.position = new Vector3(x + i, j, y);
                    bricks[i, j].GetComponent<Renderer>().material.color = new Color(0.85f, 0.75f, 0.65f);
                    bricks[i, j].AddComponent<BoxCollider>();

                    bricks[i, j].transform.parent = wall.transform;
                }
            }

            //wall.AddComponent<MeshRenderer>();
            wall.transform.Rotate(0, direction, 0);
        }
    }
}
