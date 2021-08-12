using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    int matNo = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse is down");

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit))
            {
                Material mat;
                GameObject hitObject = raycastHit.transform.gameObject;
                //GameObject hitObject = raycastHit.transform.gameObject.transform.parent.transform.gameObject;
                Debug.Log("Hit " + hitObject.name + " " + hitObject.transform.position.x + " " + hitObject.transform.position.z);

                //UnityEditor.EditorUtility.DisplayDialog("Raycast", "It's working", "OK");
                //if (hitObject.name == "Wall1")
                //{
                //    //UnityEditor.EditorUtility.DisplayDialog("Raycast", "It's working", "OK");

                //    //hitObject.GetComponentsInChildren<Renderer>()[0].material.color = Color.red;
                //    var children = hitObject.GetComponentsInChildren<Renderer>();

                //    foreach (var child in children)
                //    {
                //        child.material.color = Color.red;
                //    }
                //}

                //UnityEditor.EditorUtility.DisplayDialog("Coords", hitObject.transform.position.x + " " + hitObject.transform.position.y + " " + hitObject.transform.position.z, "Okay");

                if (matNo == 0)
                {
                    mat = Resources.Load("brownish", typeof(Material)) as Material;
                    matNo++;
                }
                else if (matNo == 1)
                {
                    mat = Resources.Load("wood", typeof(Material)) as Material;
                    matNo++;
                }
                else if (matNo == 2)
                {
                    mat = Resources.Load("wood2", typeof(Material)) as Material;
                    matNo++;
                }
                else if (matNo == 3)
                {
                    mat = Resources.Load("wood3", typeof(Material)) as Material;
                    matNo++;
                }
                else if (matNo == 4)
                {
                    mat = Resources.Load("wallpaper", typeof(Material)) as Material;
                    matNo++;
                }
                else
                {
                    mat = Resources.Load("white", typeof(Material)) as Material;
                    matNo = 0;
                }


                //UnityEditor.EditorUtility.DisplayDialog("Coords", hitObject.transform.parent.transform.name, "Okay");

                var texEdits = GameObject.FindGameObjectsWithTag("tex");

                int x = 0;
                int z = 0;
                bool isX = false;
                bool isZ = false;

                //string name = hitObject.transform.parent.parent.name;
                //Debug.Log("Wall Name: " + name);
                //int wallNo = name[5] - '0';
                //wallNo = (wallNo - 1 < 1) ? 4 : wallNo - 1;
                //string name1 = name.Substring(0, 5) + wallNo;
                //Debug.Log("2nd Wall Name: " + name1);
                //wallNo = (wallNo + 2 > 4) ? 1 : wallNo + 2;
                //string name2 = name.Substring(0, 5) + wallNo;
                //Debug.Log("3rd Wall Name: " + name2);

                foreach (var tex in texEdits)
                {
                    //string texName = tex.transform.parent.parent.name;
                    if (
                        tex.transform.position.x == hitObject.transform.position.x
                        && tex.transform.parent.parent == hitObject.transform.parent.parent
                        //&& (tex.transform.parent.parent.name == name
                        //    || tex.transform.parent.parent.name == name1
                        //    || tex.transform.parent.parent.name == name2)
                        //&& tex.transform.parent.parent.parent == hitObject.transform.parent.parent.parent
                       )
                    {
                        x++;
                    }
                    if (
                        tex.transform.position.z == hitObject.transform.position.z
                        && tex.transform.parent.parent == hitObject.transform.parent.parent
                        //&& (tex.transform.parent.parent.name == name
                        //    || tex.transform.parent.parent.name == name1
                        //    || tex.transform.parent.parent.name == name2)
                        //&& tex.transform.parent.parent.parent == hitObject.transform.parent.parent.parent
                       )
                    {
                        z++;
                    }
                    if (x > 2 || z > 2)
                        break;
                }

                if (x > 2)
                { isX = true; }
                else if (z > 2)
                { isZ = true; }


                foreach (var tex in texEdits)
                {
                    if (isX)
                    {
                        Debug.Log("In X");
                        if (
                            tex.transform.position.x == hitObject.transform.position.x
                            && tex.transform.parent.parent == hitObject.transform.parent.parent
                            //&& (tex.transform.parent.parent.name == name
                            //    || tex.transform.parent.parent.name == name1
                            //    || tex.transform.parent.parent.name == name2)
                            //&& tex.transform.parent.parent.parent == hitObject.transform.parent.parent.parent
                           )
                        {
                            tex.GetComponent<Renderer>().material = mat;
                        }
                    }

                    if (isZ)
                    {
                        Debug.Log("In Z");
                        if (
                            tex.transform.position.z == hitObject.transform.position.z
                            && tex.transform.parent.parent == hitObject.transform.parent.parent
                            //&& (tex.transform.parent.parent.name == name
                            //    || tex.transform.parent.parent.name == name1
                            //    || tex.transform.parent.parent.name == name2)
                            //&& tex.transform.parent.parent.parent == hitObject.transform.parent.parent.parent
                           )
                        {
                            tex.GetComponent<Renderer>().material = mat;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("No hit");
            }
            Debug.Log("Mouse is down");
        }
    }
}
