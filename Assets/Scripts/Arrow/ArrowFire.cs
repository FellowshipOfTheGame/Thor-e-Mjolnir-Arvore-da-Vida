using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ArrowFire : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform thor;
    public Transform arrow;
    float dist;
    Vector3 viewPosArrow;
    Vector3 target;
    public Camera mainCam;
    bool fire = false;
    int speed = 0;
    float smooth;
    float angle;
    int speedGo = 10;
    bool foco = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        dist = Vector3.Distance(thor.position, arrow.position);        
        
        if (dist < 5 && !fire) 
        {           
            
            if(arrow.position.y > thor.position.y && arrow.position.x < thor.position.x)
            {
                angle = 180 - Vector3.Angle(arrow.transform.position, thor.position);
            }
            else
            {
                if(arrow.position.y < thor.position.y && arrow.position.x < thor.position.x)
                {
                    angle = Vector3.Angle(arrow.transform.position, thor.position);
                }
                else
                {
                    if (arrow.position.y < thor.position.y && arrow.position.x > thor.position.x)
                    {
                        angle =  -Vector3.Angle(arrow.transform.position, thor.position) ;
                    }
                    else
                    {
                        if (arrow.position.y > thor.position.y && arrow.position.x > thor.position.x)
                        {
                            angle = 180 + Vector3.Angle(arrow.transform.position, thor.position);
                        }
                    }
                }                
            }
            arrow.Rotate(0, 0, angle);
            speed = 10;
            target = thor.position;
            fire = true;
            smooth =  1.0f - Mathf.Pow(0.5f, Time.deltaTime * speed);
            StartCoroutine(Example());
        } 

        arrow.position = Vector3.Lerp(arrow.position, target, smooth);
        

        if (!(viewPosArrow.x >= 0 && viewPosArrow.x <= 1 && viewPosArrow.y >= 0 && viewPosArrow.y <= 1))
        {
            Destroy(arrow.transform.gameObject);
        }

    }

    IEnumerator Example()
    {
       
        yield return new WaitForSeconds(2);
        Destroy(arrow.transform.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {       
        if (collision2D.transform.tag.Equals("Thor"))
        {
            Destroy(arrow.transform.gameObject);
        }
    }
}
