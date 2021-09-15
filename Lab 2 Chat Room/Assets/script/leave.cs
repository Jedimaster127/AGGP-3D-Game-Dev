using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
