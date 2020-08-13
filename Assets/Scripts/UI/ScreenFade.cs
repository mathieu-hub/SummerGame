using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace GameCanvas
{
    /// <summary>
    /// BlackScreenFade
    /// </summary>

    public class ScreenFade : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject blackScreen;
        private SpriteRenderer screenRenderer;
        public bool fadeFinish = false;
        #endregion
        private void Start()
        {
            screenRenderer = blackScreen.GetComponent<SpriteRenderer>();

            Color c = screenRenderer.material.color;
            c.a = 0f;
            screenRenderer.material.color = c;
        }

        public void startFadingOUT()
        {
            StartCoroutine("FadeOut");
        }
        public void startFadingIN()
        {
            StartCoroutine("FadeIn");
        }

        #region Enumerator
        IEnumerator FadeOut()
       {
            fadeFinish = false;
            for (float f = 1f; f >= -0.05f; f -= 0.05f)
            {
                Color c = screenRenderer.material.color;
                c.a = f;
                screenRenderer.material.color = c;
                yield return new WaitForSeconds(0.1f);
            }
       }

        IEnumerator FadeIn()
        {
           
            for (float f = 0.1f; f <= 1.1; f += 0.1f)
            {
                Color d = screenRenderer.material.color;
                d.a = f;
                screenRenderer.material.color = d;
                yield return new WaitForSeconds(0.1f);
            }

            fadeFinish = true;
        }
        #endregion
    }
}
