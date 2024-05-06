
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value;
    private bool hasTriggered;

  private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            //give score to the player
            Destroy(gameObject);
        }
    }
}
