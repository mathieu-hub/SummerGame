﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class podsDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destroy());
    }

   IEnumerator Destroy()
    {

        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
