using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofHider : MonoBehaviour
{
    bool roofHidden = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("hideRoofs").GetComponentInChildren<TextMesh>().text = "Toggle Roof";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideRoof()
    {

        GameObject[] objs = GameObject.FindGameObjectsWithTag("roof");
        if (roofHidden == false)
        {
            foreach (GameObject obj in objs)
            {
                obj.GetComponent<Renderer>().enabled = false;
            }
            roofHidden = true;
        }
        else
        {
            foreach (GameObject obj in objs)
            {
                obj.GetComponent<Renderer>().enabled = true;
            }
            roofHidden = false;
        }
    }
}
