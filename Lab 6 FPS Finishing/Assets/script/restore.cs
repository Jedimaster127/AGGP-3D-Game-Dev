using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class restore : MonoBehaviour
{
    public bool isHP;

    private void Update()
    {
        gameObject.transform.Rotate(0, .3f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FPSPlayerManager>().curentHP < 100 && isHP == true)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        else if (other.gameObject.GetComponent<FPSPlayerManager>().gun.surplusAmmo[other.gameObject.GetComponent<FPSPlayerManager>().shotType] <
            other.gameObject.GetComponent<FPSPlayerManager>().gun.bullets[other.gameObject.GetComponent<FPSPlayerManager>().shotType].GetComponent<bullet>().surplusAmmo && isHP == false)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
