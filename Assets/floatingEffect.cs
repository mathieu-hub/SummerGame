using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingEffect : MonoBehaviour
{
    private float floatingEffet = 0f;


    [SerializeField] private float float1;
    [SerializeField] private float float2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        floatingEffet += 0.03f;
        transform.position = new Vector3(transform.position.x, transform.position.y + float1 /*0.005f*/ * Mathf.Sin(floatingEffet) * float2 /*0.2f*/, 0f);
    }
}
