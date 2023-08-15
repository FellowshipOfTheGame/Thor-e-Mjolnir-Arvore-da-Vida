using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenarioCeu : MonoBehaviour
{
    public float velocidadeDoCenario;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarCenario();
    }

    private void MovimentarCenario()
    {
        Vector2 deslocamento = new Vector2(Time.time * velocidadeDoCenario, 0);
        GetComponent<Renderer>().material.mainTextureOffset = deslocamento;
    }
}
