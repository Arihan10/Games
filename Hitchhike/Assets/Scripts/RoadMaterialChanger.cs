using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMaterialChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0f, Random.Range(0f, 1f)); 
    }
}
