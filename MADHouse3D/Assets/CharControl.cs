using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 4.0f;

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float straffe = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        Vector3 moveVector = Camera.main.transform.forward * move;
        Vector3 straffeVector = Camera.main.transform.right * straffe;

        transform.Translate(moveVector + straffeVector);
    }
}
