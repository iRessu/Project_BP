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
        dialScript = dialogueObject.GetComponent<Dialogue>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            dialogueObject.SetActive(true);
            dialScript.StartDialogue();
            FindObjectOfType<AudioManager>().PlaySound("EggBen");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if(other.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().ShutUpEggBen();
            dialogueObject.SetActive(false);
        }
    }


}
