using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WildBoar : AFollower
{
    // Start is called before the first frame update
    public Transform thor;
    public Transform boar;
    public Transform lifeBar;
    float dano = 1.0f;
    Vector3 scaleChange;
    int speed = -4;
    int life;
    void Start()
    {
        life = lifeWildBoarBite/10;
        scaleChange = new Vector3(dano, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        boar.transform.position = boar.transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform.CompareTag("Thor"))
        {           
            speed = -speed;
            StartCoroutine(Back());           
        }

        if (collision2D.transform.CompareTag("hammer"))       
        {
            life -= 1;
            lifeBar.transform.localScale -= scaleChange;

            if (life <= 0)
            {
                Destroy(boar.gameObject);
            }
        }
    }

    IEnumerator Back()
    {
        yield return new WaitForSeconds(2);
        speed = -1 * speed;
    }
    
}
