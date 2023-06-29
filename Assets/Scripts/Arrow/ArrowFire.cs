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
    float dist;
    Vector3 viewPosArrow;
    Vector3 target;
    public Camera mainCam;
    bool fire = false;
    int speed = 0;
    float smooth;
    float angle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        dist = Vector3.Distance(thor.position, elfo.position);        
        
        if (dist < 8 && !fire) 
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
            arrow.transform.position = elfo.position;
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
            // Destroy(arrow.transform.gameObject);
            arrow.transform.position = elfo.position;
        }

    }

    IEnumerator Example()
    {       
        yield return new WaitForSeconds(2);
        //Destroy(arrow.transform.gameObject);
        arrow.transform.position = elfo.position;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {       
        if (collision2D.transform.tag.Equals("Thor"))
        {
            //Destroy(arrow.transform.gameObject);
            arrow.transform.position = elfo.position;
        }
    }
}
