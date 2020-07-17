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
        private SpriteRenderer screenFade;
        public bool fadeFinish = false;
        #endregion

        public void startFadingOUT()
        {
            FadeOut();
        }
        public void startFadingIN()
        {
            FadeIn();
        }

        #region Enumerator
        IEnumerator FadeOut()
       {
            for (float f = 1f; f >= -0.05f; f -= 0.05f)
            {
                Color c = screenFade.material.color;
                c.a = f;
                screenFade.material.color = c;
                yield return new WaitForSeconds(0.1f);
            }
       }

        IEnumerator FadeIn()
        {
            for (float f = 0.1f; f <= 1.1; f += 0.1f)
            {
                Color d = screenFade.material.color;
                d.a = f;
                screenFade.material.color = d;
                yield return new WaitForSeconds(0.1f);
            }
            fadeFinish = true;
           
        }
        #endregion
    }
}
