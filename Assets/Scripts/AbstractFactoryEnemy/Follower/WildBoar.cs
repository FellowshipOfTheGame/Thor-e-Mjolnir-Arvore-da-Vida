using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WildBoar : AFollower
{
    // Start is called before the first frame update
    public Transform thor;
    public int speed = 1;
    float smooth;
    float distance;
    Vector3 target;
    void Start()
    {
        if (transform.position.x < thor.position.x)
        {
            transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
        }
        else
        {
            transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
        }
    }

    // Update is called once per frame
    void Update()
    {  

        distance = Vector3.Distance(transform.position, target);

        if(distance > 5.0f)
        {           
            smooth = 1.0f - Mathf.Pow(0.5f, Time.deltaTime * speed);
            target = thor.position;
            transform.position = Vector3.Lerp(transform.position, target, smooth);
        } 
        else
        {
            speed *= 2;
            smooth = 1.0f - Mathf.Pow(0.5f, Time.deltaTime * speed);
            target = thor.position;
            transform.position = Vector3.Lerp(transform.position, target, smooth);
        }

    }
}
