using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueJavali : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform thor;
    public Transform java;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        java.transform.position = java.transform.position + new Vector3(-4 * Time.deltaTime, 0, 0);

    }
}
