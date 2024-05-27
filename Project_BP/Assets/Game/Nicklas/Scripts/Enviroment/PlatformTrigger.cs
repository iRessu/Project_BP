using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            var playerMovement = FindObjectOfType<PlayerController>();
            if (playerMovement != null)
            {
                playerMovement.SetParent(transform);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var playerMovement = FindObjectOfType<PlayerController>();
            if (playerMovement != null)
            {
                playerMovement.ResetParent();
            }

        }
    }

}
