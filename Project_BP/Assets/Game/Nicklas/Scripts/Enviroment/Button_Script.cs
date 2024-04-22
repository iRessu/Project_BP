using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button_Script : MonoBehaviour
{
    public UnityEvent happen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            happen.Invoke();
        }
    }
}
