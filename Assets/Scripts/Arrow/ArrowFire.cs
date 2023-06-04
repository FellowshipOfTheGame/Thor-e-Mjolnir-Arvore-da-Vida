using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFire : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform thor;
    public Transform arrow;
    Vector3 viewPosArrow;
    public Camera mainCam;
    bool fire = false;
    int speed = 0;
    int speedGo = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.RightArrow) && !fire)
        {
            fire = true;           
            arrow.position = thor.position;
            speed = speedGo;
            arrow.transform.Rotate(new Vector3(0, 0, 0));           
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !fire)
        {
            fire = true;
            speed = -1*speedGo;
            arrow.position = thor.position;
            arrow.transform.Rotate(new Vector3(0, -180, 0));          

        }

        arrow.transform.position = arrow.transform.position + new Vector3(speed * Time.deltaTime, 0, 0);

        viewPosArrow = mainCam.WorldToViewportPoint(arrow.position);

        if (!(viewPosArrow.x >= 0 && viewPosArrow.x <= 1 && viewPosArrow.y >= 0 && viewPosArrow.y <= 1))
        {
            arrow.position = new Vector3(100,100,0);
            speed = 0;
            fire = false;
        }

    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {       
        if (collision2D.transform.tag.Equals("Enemy"))
        {
            speed = 0;
            arrow.position = new Vector3(100, 100, 0);
            fire = false;
        }
    }
}
