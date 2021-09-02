using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonstuff : MonoBehaviour
{
   public void OnCreateRoom()
    {
        photonmanager.instance.CreateRoom();
    }
    public void OnRandomRoom()
    {
        photonmanager.instance.JoinRoom();
    }
}
