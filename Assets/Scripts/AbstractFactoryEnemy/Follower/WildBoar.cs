using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WildBoar : AFollower
{
    // Start is called before the first frame update
    public Transform thor;
    public Transform boar;
    int speed = -4;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        boar.transform.position = boar.transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform.tag.Equals("Thor"))
        {
            speed = -speed;
            StartCoroutine(Back());
        }
    }

    IEnumerator Back()
    {
        yield return new WaitForSeconds(2);
        speed = -1 * speed;
    }
    
}
