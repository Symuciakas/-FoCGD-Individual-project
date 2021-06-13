using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextBox : MonoBehaviour
{
    public string text, nextLine;
    private bool mouseEnabled;
    public bool ready = false;
    private Animator animator;
    public TextMeshProUGUI textDisplay;
    public IEnumerator writePart;

    private void Start()
    {
        mouseEnabled = false;
        animator = GetComponent<Animator>();
        ready = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && mouseEnabled)
        {
            OnMouseClick();
        }
    }

    public void OnMouseClick()
    {
        if (textDisplay.text != text)
        {
            if (writePart != null)
            {
                StopCoroutine(writePart);
                ready = true;
            }
            textDisplay.text = text;
        }
        else
        {
            if (nextLine != "END THE STORY")
            {
                text = nextLine;
                textDisplay.text = "";
                Type(text);
            }
            else
            {
                EndStory();
            }
        }
    }

    public void BeginStory(string s)
    {
        ready = false;
        text = s;
        StartCoroutine(BringUpTextBox());
    }
    public void Type(string s)
    {
        text = s;
        ready = false;
        writePart = WritePart(s);
        StartCoroutine(writePart);
    }
    public void EndStory()
    {
        StartCoroutine(BringDownTextBox());
    }
    public void LoadLine(string s)
    {
        ready = false;
        nextLine = s;
        mouseEnabled = true;
    }

    IEnumerator BringUpTextBox()
    {
        animator.SetTrigger("PopIn");//0.4f
        yield return new WaitForSeconds(0.6f);
        mouseEnabled = true;
        Type(text);
    }
    IEnumerator BringDownTextBox()
    {
        animator.SetTrigger("PopOut");
        yield return new WaitForSeconds(0.2f);//0.15f
        textDisplay.text = "";
        ready = true;
    }
    IEnumerator WritePart(string s)
    {
        foreach (char symbol in s.ToCharArray())
        {
            textDisplay.text += symbol;
            yield return new WaitForSeconds(0.05f);
        }
        ready = true;
    }
}