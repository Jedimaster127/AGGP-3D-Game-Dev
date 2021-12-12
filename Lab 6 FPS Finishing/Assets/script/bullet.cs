using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
public class bullet : MonoBehaviour
{
    public int bulletSpeed = 25;

    public int damage = 25;

    //preset ammo it can shoot at once
    public int avalableAmmo = 15;

    //preset how much ammo is there in surplus
    public int surplusAmmo = 300;

    public static bullet instance;
    private void Awake()
    {
        instance = this;
    }
    // destroy shot when it exists for too long and moves it forward
    void Update()
    {
        Destroy(gameObject, 1);

        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
    }

    //destroy the shot when it hit's an object
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" || collision.gameObject.tag != "Bullet2" || collision.gameObject.tag != "Bullet3")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<FPSPlayerManager>().curentHP -= damage;
        }
    }

}
