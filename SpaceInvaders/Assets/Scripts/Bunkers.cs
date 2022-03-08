using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunkers : MonoBehaviour
{

    private int bunkerHealth = 100;

    void OnCollisionEnter()
    {
        bunkerHealth -= 10;
        if (bunkerHealth <= 10)
        {
            Destroy(GetComponent<Rigidbody>());
        }
    }
}
