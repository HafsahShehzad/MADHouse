using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    GameObject parent;
    bool opened = false;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent.gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !opened)
        {
            parent.transform.Rotate(0, -75, 0);

            if (parent.transform.name == "DoorX")
                parent.transform.position += new Vector3(-0.65f, 0, 1.45f);

            else if (parent.transform.name == "DoorZ" && !opened)
                parent.transform.position += new Vector3(1.45f, 0, 0.65f);

            opened = true;
        }
    }
}
