using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockback = 2;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        if(rb)
        {
            Vector3 direction = collision.transform.position - transform.position;

            rb.AddForce(direction.normalized * knockback, ForceMode.Impulse);
        }
    }
}
