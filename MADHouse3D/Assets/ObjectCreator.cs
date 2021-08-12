using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HouseConstructor;

public class ObjectCreator : MonoBehaviour
{
    public GameObject[,] wall;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //wall.transform.localScale = new Vector3(10f, 5f, 1f);
        //wall.transform.position = new Vector3(0, 2f, 0);
        //wall.GetComponent<Renderer>().material.color = new Color(0.85f, 0.75f, 0.65f);
        //wall.AddComponent<BoxCollider>();

        //List<List<GameObject>> wall = new List<List<GameObject>>(10);

        //wall = new GameObject[10, 5];

        //for (int i = 0; i < 10; i++)
        //{
        //    for (int j = 0; j < 5; j++)
        //    {
        //        wall[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //        wall[i, j].transform.localScale = new Vector3(1f, 1f, 1f);
        //        wall[i, j].transform.localPosition = new Vector3(i, j + 0.5f, 0);
        //        wall[i, j].GetComponent<Renderer>().material.color = new Color(0.85f, 0.75f, 0.65f);
        //        wall[i, j].AddComponent<BoxCollider>();
        //    }
        //}

        float x = 0;
        float y = 0;
        int length = 0;
        int width = 0;
        int length2 = 0;
        int width2 = 0;
        //int direction;

        //List<WallCreator> walls = new List<WallCreator>();

        String path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        path += "\\Documents\\MADHouse";
        if (!Directory.Exists(path))
        {
            //UnityEditor.EditorUtility.DisplayDialog("Error", "Can't read file!", "Close");
            return;
        }
        path += "\\coords.txt";
        if (!File.Exists(path))
        {
            //UnityEditor.EditorUtility.DisplayDialog("Error", "Can't read file!", "Close");
            return;
        }

        string[] lines = File.ReadAllLines(path);
        foreach (string line in lines)
        {
            string[] col = line.Split(' ');

            //UnityEditor.EditorUtility.DisplayDialog("Coords", col[0], "OK");
            if (col[0] == "Room")
            {
                //UnityEditor.EditorUtility.DisplayDialog("Coords", col[0], "OK");
                x = (float)System.Convert.ToDouble(col[1]);
                x /= 30;
                x *= 2;
                //UnityEditor.EditorUtility.DisplayDialog("X", x.ToString(), "OK");
                y = (float)System.Convert.ToDouble(col[2]);
                y /= 30;
                y *= 2;
                //UnityEditor.EditorUtility.DisplayDialog("Y", y.ToString(), "OK");
                length = (int)System.Convert.ToDouble(col[3]);
                length /= 30;
                length *= 2;
                //UnityEditor.EditorUtility.DisplayDialog("Length", length.ToString(), "OK");
                width = (int)System.Convert.ToDouble(col[4]);
                width /= 30;
                width *= 2;
                //UnityEditor.EditorUtility.DisplayDialog("Width", width.ToString(), "OK");
                _ = new RoomCreator(x, y, length, width);
            }
            else if (col[0] == "Room1")
            {
                //UnityEditor.EditorUtility.DisplayDialog("Coords", col[0], "OK");
                x = (float)System.Convert.ToDouble(col[1]);
                x /= 30;
                x *= 2;
                //UnityEditor.EditorUtility.DisplayDialog("X", x.ToString(), "OK");
                y = (float)System.Convert.ToDouble(col[2]);
                y /= 30;
                y *= 2;
                //UnityEditor.EditorUtility.DisplayDialog("Y", y.ToString(), "OK");
                length = (int)System.Convert.ToDouble(col[3]);
                length /= 30;
                length *= 2;
                //UnityEditor.EditorUtility.DisplayDialog("Length", length.ToString(), "OK");
                width = (int)System.Convert.ToDouble(col[4]);
                width /= 30;
                width *= 2;

                length2 = (int)System.Convert.ToDouble(col[5]);
                length2 /= 30;
                length2 *= 2;
                //UnityEditor.EditorUtility.DisplayDialog("Length", length2.ToString(), "OK");


                width2 = (int)System.Convert.ToDouble(col[6]);
                width2 /= 30;
                width2 *= 2;
                //UnityEditor.EditorUtility.DisplayDialog("Length", width2.ToString(), "OK");
                //UnityEditor.EditorUtility.DisplayDialog("Width", width.ToString(), "OK");
                _ = new Room1Creator(x, y, length, width, length2, width2);
            }
            else if (col[0] == "Window")
            {
                x = (float)System.Convert.ToDouble(col[1]);
                x /= 30;
                x *= 2;
                
                y = (float)System.Convert.ToDouble(col[2]);
                y /= 30;
                y *= 2;

                _ = new WindowCreator(x, y);
            }
            else if (col[0] == "Door")
            {
                x = (float)System.Convert.ToDouble(col[1]);
                x /= 30;
                x *= 2;

                y = (float)System.Convert.ToDouble(col[2]);
                //y += 30;
                y /= 30;
                y *= 2;

                _ = new DoorCreator(x, y);
            }
            //UnityEditor.EditorUtility.DisplayDialog("Coords", x + " " + y + " " + length + " " + width, "OK");
            //_ = new DoorCreator(x, y + 5);
        }

        //_ = new RoomCreator(10, 10, 15, 15);
        //_ = new RoomCreator(24, 10, 15, 20);
        //_ = new DoorCreator(15, 10);
        //_ = new DoorCreator(24, 15);
        //WindowCreator window = new WindowCreator(walls[0], 2);
        //DoorCreator door = new DoorCreator(walls[1], 3);
    }
}
