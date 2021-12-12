using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class leave : MonoBehaviour
{
    public static leave instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnLeaveRoom()
    {
        photonmanager.instance.LeaveRoom();

        if (lobbymanager.instance)
        {
            lobbymanager.instance.gameObject.GetPhotonView().RPC("Playerleft", RpcTarget.AllBuffered);
        }
    }
}
