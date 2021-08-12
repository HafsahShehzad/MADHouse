using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelected : MonoBehaviour
{
    [SerializeField]
    private Material defaultTex, whiteTex, brownTex;

    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void isClicked()
    {
        meshRenderer.material = brownTex;
    }
}
