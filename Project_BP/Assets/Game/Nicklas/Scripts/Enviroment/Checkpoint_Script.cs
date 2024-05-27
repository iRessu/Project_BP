using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint_Script : MonoBehaviour
{

    private Animator animator;
    private RespawnScript respawn;
    private BoxCollider2D coll;

    private string currantState;
    private string CP_OPEN = "CheckPoint_Open";


    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnScript>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().PlaySound("Checkpoint");
            respawn.respawnPoint = this.gameObject; 
            DirectionalAnimationState();
            coll.enabled = false;
            
        }
    }

    private void ChangeAnimationState(string newState)
    {
        if(currantState == newState) return;
        animator.Play(newState);
        currantState = newState;
    }

    private void DirectionalAnimationState()
    {
        if (respawn.respawnPoint == this.gameObject)
        {
            ChangeAnimationState(CP_OPEN);
        }     
    }

}
