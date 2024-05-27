using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [TextArea(3, 10)]
    public string line;

    public float textSpeed;

    private Coroutine typingCoroutine;
    private bool isTyping;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    public void ResetText()
    {
        textComponent.text = string.Empty;
    }

    public void StartDialogue()
    {
        ResetText();

        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in line)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
