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

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(0.5f);
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        transitionAnim.SetTrigger("Start");
        yield return null;
    }
}