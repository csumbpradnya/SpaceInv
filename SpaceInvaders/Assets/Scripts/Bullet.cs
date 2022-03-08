using System;
using UnityEngine;

// Technique for making sure there isn't a null reference during runtime if you are going to use get component
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D body;

    //-----------------------------------------------------------------------------
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Fire();
    }

    //-----------------------------------------------------------------------------
    private void Fire()
    {
        if (body.gameObject.tag == "friendly")
        {
            body.velocity = Vector2.up * speed;
        }
        else if (body.gameObject.tag == "enemy")
        {
            body.velocity = Vector2.down * 8f;
        }

        Debug.Log("Wwweeeeee");
    }

   
}
