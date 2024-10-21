using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarouselTextBox : MonoBehaviour
{
   [SerializeField] private TMP_Text description;
   
   [SerializeField] private bool fadeText = true;
   
   private float _fadeDuration = 0.5f;
   private float _halfFadeDuration => _fadeDuration * 0.5f;
   private Coroutine _fadeCoroutine;

   public void SetTextWithoutFade(string descriptionText)
   {
      description.SetText(descriptionText);

      description.alpha = 1;
   }

   public void SetText(string descriptionText, float fadingDuration = 0f)
   {
      if (!fadeText || fadingDuration <= 0)
      {
         SetTextWithoutFade(descriptionText);
         return;
      }

      if (_fadeCoroutine != null)
      {
         StopCoroutine(_fadeCoroutine);
         description.alpha = 1;
      }
      
      _fadeDuration = fadingDuration;
      _fadeCoroutine = StartCoroutine(FadeText(descriptionText));

   }
      private IEnumerator FadeText(string descriptionText)
      {
         float time = 0;
         while (time < _halfFadeDuration)
         {
            time += Time.deltaTime;
            float lerpValue = 1 - (time / _halfFadeDuration);
            description.alpha = lerpValue;
            yield return null;
         }
         
         description.SetText(descriptionText);
         
         time = 0;
            
         while (time < _halfFadeDuration)
         {
            time += Time.deltaTime;
            float lerpValue = time / _halfFadeDuration;
            description.alpha = lerpValue;
            yield return null;
         }
         
         description.alpha = 1;
      }
}
