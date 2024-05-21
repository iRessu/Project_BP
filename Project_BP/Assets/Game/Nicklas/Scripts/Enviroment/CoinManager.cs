using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public TextMeshProUGUI coinText;
    public GameObject toOpen;
    public float coinsNeeded;
    private bool isDestroyed;
 


    // Update is called once per frame
    void Update()
    {
        coinText.text = ":" + coinCount.ToString();

        if(coinCount == coinsNeeded && !isDestroyed)
        {
            isDestroyed = true;
            Destroy(toOpen);
        }
    }
}
