using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Player;

public class Cinématique : MonoBehaviour
{
    [SerializeField] private GameObject repere1;
    [SerializeField] private GameObject repere2;

    [SerializeField] private Transform targetMovement;

    [SerializeField] private float speed;

    [SerializeField] private GameObject SpaceSprite;

    [SerializeField] private CamScale camScale;

    public bool firstBool = false;
    public bool secondBool = false;
    public static bool lastBool = false;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.vCam.Follow = gameObject.transform;

        GameManager.Instance.vCam.m_Lens.OrthographicSize = 9;

        targetMovement = repere1.transform;

        PlayerManager.Instance.controller.needToStop = true;

        camScale.camMode = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!lastBool)
        {
            Vector3 direction = targetMovement.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }

       


        if(transform.position.x <= targetMovement.position.x && firstBool == false)
        {
            firstBool = true;
        }

        if (firstBool)
        {
            targetMovement = repere2.transform;
            SpaceSprite.GetComponent<floatingEffect>().enabled = false;
            speed = 2;
        }


        if (transform.position.y <= repere2.transform.position.y  && firstBool && !lastBool)
        {
            lastBool = true;
        }

        if (lastBool)
        {
            GameManager.Instance.vCam.m_Lens.OrthographicSize = 7;
            GameManager.Instance.vCam.Follow = PlayerManager.Instance.transform;
            camScale.camMode = 0;
            PlayerManager.Instance.controller.needToStop = false;
        }
    }
}
