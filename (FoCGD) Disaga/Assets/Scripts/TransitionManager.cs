using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public Animator transition;

    void Start()
    {
        //Check data
        //LoadEnd();
    }

    public void LoadEnd()
    {
        StartCoroutine(LoadEndDelay());
    }

    public void LoadScene(int i)
    {
        StartCoroutine(LoadDelay(i));
    }

    IEnumerator LoadEndDelay()
    {
        transition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        foreach (Button b in GetComponentsInChildren<Button>())
        {
            b.interactable = true;
        }
    }

    IEnumerator LoadDelay(int i)
    {
        foreach (Button b in GetComponentsInChildren<Button>())
        {
            b.interactable = false;
        }
        transition.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(i);
    }
}