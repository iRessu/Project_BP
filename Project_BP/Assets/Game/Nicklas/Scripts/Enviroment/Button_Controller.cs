using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Controller : MonoBehaviour
{
    public PlatformButtonHandler platformButtonHandler;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            platformButtonHandler.canMove = true;
        }
    }
}
