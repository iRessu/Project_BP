using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public GameObject dialogueObject;
    private Dialogue dialScript;
    // Start is called before the first frame update
    void Start()
    {
        dialScript = FindObjectOfType<Dialogue>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            dialogueObject.SetActive(true);
            dialScript.StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            dialogueObject.SetActive(false);
        }
    }


}
