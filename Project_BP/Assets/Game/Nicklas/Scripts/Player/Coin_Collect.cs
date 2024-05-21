using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Collect : MonoBehaviour
{
    private CoinManager cm;

    private void Start()
    {
        cm = FindObjectOfType<CoinManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
            if (other.gameObject.CompareTag("Coin"))
            {
               if (other.GetComponent<BoxCollider2D>() != null)
               {
                Destroy(other.gameObject);
                cm.coinCount++;
               }
            }
        
    }
}
