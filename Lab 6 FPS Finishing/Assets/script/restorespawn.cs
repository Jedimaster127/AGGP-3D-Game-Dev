using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class restorespawn : MonoBehaviour
{
    public GameObject restoreItems;

    private void Start()
    {
        PhotonNetwork.Instantiate(restoreItems.name, new Vector3(gameObject.transform.position.x, .03f, gameObject.transform.position.z), gameObject.transform.rotation);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "LargHP" || other.gameObject.tag == "SmallHP" || other.gameObject.tag == "LargAmmo" || other.gameObject.tag == "SmallAmmo")
        {
            PhotonNetwork.Instantiate(restoreItems.name, new Vector3(gameObject.transform.position.x, .03f, gameObject.transform.position.z), gameObject.transform.rotation);
        }
    }
}
