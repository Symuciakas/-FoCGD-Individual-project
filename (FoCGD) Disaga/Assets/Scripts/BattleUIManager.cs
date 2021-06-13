using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    private Animator animator;
    public CanvasGroup canvasGroup;

    //Get better animations
    public void OnEnable()
    {
        canvasGroup.interactable = true;
    }

    public void OnDisable()
    {
        canvasGroup.interactable = false;
    }
}