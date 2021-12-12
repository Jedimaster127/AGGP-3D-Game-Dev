using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Billboard : MonoBehaviour
{
    public Transform cam;

    private void Awake()
    {
        if (!gameObject.GetPhotonView().IsMine)
        {
            cam = GameObject.FindWithTag("MainCamera").transform;
        }

    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
