using System.Collections;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);

    }
}
