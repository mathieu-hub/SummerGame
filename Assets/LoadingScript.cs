using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class LoadingScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dot1;
    [SerializeField] private TextMeshProUGUI dot2;
    [SerializeField] private TextMeshProUGUI dot3;

    // Start is called before the first frame update
    void Start()
    {
        dot1.enabled = false;
        dot2.enabled = false;
        dot3.enabled = false;

        StartCoroutine(FirstAppering());
    }

    IEnumerator FirstAppering()
    {
        yield return new WaitForSeconds(0.7f);
        dot1.enabled = true;
        yield return new WaitForSeconds(0.7f);
        dot2.enabled = true;
        yield return new WaitForSeconds(0.7f);
        dot3.enabled = true;
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(SecondAppering());
    }

    IEnumerator SecondAppering()
    {
        dot1.enabled = false;
        dot2.enabled = false;
        dot3.enabled = false;
        yield return new WaitForSeconds(0.7f);
        dot1.enabled = true;
        yield return new WaitForSeconds(0.7f);
        dot2.enabled = true;
        yield return new WaitForSeconds(0.7f);
        dot3.enabled = true;
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(2);
    }

}
