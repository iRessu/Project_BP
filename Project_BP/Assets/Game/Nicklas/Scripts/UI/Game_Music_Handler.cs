using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Music_Handler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlaySound("Bana1");
    }


}
