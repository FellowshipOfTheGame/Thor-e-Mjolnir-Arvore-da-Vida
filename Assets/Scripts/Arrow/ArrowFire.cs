using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ArrowFire : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform thor;
    public Transform arrow;
    public Transform elfo;
    public Camera mainCam;
    int ajusteY = 2;
    float dist;
    Vector3 viewPosArrow;    
    bool fire = false;
    int speed = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        dist = Vector3.Distance(thor.position, elfo.position);        
        
       if (dist < 20) 
       {
            if (!fire)
            {
                arrow.transform.position = new Vector3(elfo.position.x, elfo.position.y + ajusteY, 0);
                fire = true;
            }
            arrow.transform.position = arrow.transform.position + new Vector3(-speed * Time.deltaTime, 0, 0);
       }       
        
       if(dist < 2)
        {

            arrow.transform.position = new Vector3(100, 100, 0);
            speed = 0;
        }

        if (!(viewPosArrow.x >= 0 && viewPosArrow.x <= 1 && viewPosArrow.y >= 0 && viewPosArrow.y <= 1))
        {
            arrow.transform.position = new Vector3(elfo.position.x, elfo.position.y + ajusteY, 0);
        }

    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {       
        if (collision2D.transform.tag.Equals("Thor"))
        {
            arrow.transform.position = new Vector3(elfo.position.x, elfo.position.y + ajusteY, 0);
        }
    }
}
