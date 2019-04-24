using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

   
    public float duration = 4f;

    public bool rate;

    public GameObject player;

    void Start()
    { 
    }

    void Update()
    {
        PickUp();
        
    }

    void PickUp()
    {
        if (rate == true)
            {
            player.GetComponent<PlayerController>().fireRate = 0.25f;   
        }

    }  
}
