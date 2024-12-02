using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] Animator transitionAnim;

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());

    }

    public void Tuto()
    {
        StartCoroutine(LoadTuto());
    }

    public void Jugar()
    {
        StartCoroutine(TutoJugar());
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(0.5f);
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        transitionAnim.SetTrigger("Start");
        yield return null;
    }
    
    IEnumerator LoadTuto()
    {
        yield return new WaitForSeconds(0.5f);
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

        transitionAnim.SetTrigger("Start");
        yield return null;
    }
    IEnumerator TutoJugar()
    {
        yield return new WaitForSeconds(0.5f);
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        transitionAnim.SetTrigger("Start");
        yield return null;
    }
}