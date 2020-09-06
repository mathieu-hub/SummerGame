using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSeller : MonoBehaviour
{

    public SpriteRenderer sprRenderer;
    public GameObject storedItem = null;
    public bool placed = false;


    // Start is called before the first frame update
    void Start()
    {
        storedItem = gameObject;
        sprRenderer = gameObject.GetComponent<SpriteRenderer>();
        sprRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (placed && storedItem == null)
        {
            
          sprRenderer.enabled = true;
            
           
        }
      
    }
}
