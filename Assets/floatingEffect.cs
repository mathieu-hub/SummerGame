using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingEffect : MonoBehaviour
{
    private float floatingEffet = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        floatingEffet += 0.03f;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.005f * Mathf.Sin(floatingEffet) * 0.2f, 0f);
    }
}
