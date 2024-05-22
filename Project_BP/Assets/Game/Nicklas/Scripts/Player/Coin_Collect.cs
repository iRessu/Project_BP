using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Collect : MonoBehaviour
{
    private CoinManager cm;
    private bool isCollected;

    private void Start()
    {
        cm = FindObjectOfType<CoinManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!isCollected && other.gameObject.CompareTag("Player"))
        {
            isCollected = true;
            FindObjectOfType<AudioManager>().PlaySound("Quack");
            cm.coinCount++;
            Destroy(gameObject);
        }   
    }
}
