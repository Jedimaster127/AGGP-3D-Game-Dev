using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class buttonstuff : MonoBehaviour
{
    public TMP_InputField user;

    public static buttonstuff button;

    private void Awake()
    {
        button = this;
    }

    public void OnCreateRoom()
    {
        if (user.text != "")
        {
            photonmanager.instance.CreateRoom();
        }

    }
    public void OnRandomRoom()
    {
        if (user.text != "")
        {
            photonmanager.instance.JoinRoom();
        }
        
    }

    public void OnJoinLobby()
    {
        if(user.text != "")
        {
            
        }
    }
}
