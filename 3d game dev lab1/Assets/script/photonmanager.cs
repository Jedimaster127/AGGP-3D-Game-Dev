using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class photonmanager : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";
    RoomOptions roomOptions = new RoomOptions();

   public static photonmanager instance { get; private set; }

    void Awake()
    {
        if(instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        PhotonNetwork.AutomaticallySyncScene = true;

        roomOptions.MaxPlayers = 4;
    }

    void Start()
    {
        Connect();
    }

    //connect user to master server
    public void Connect()
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
        
    }

    #region Photon CallBack
    public void CreateRoom()
    {
        Debug.Log("[PhotonManager][OnConnectedToMaster] trying to make room....");
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public void JoinRoom()
    {
        Debug.Log("[PhotonManager][JoinRoom] trying to join room....");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("[PhotonManager][OnConnectedToMaster]");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("[PhotonManager][OnCreatedRoom]");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("[PhotonManager][OnJoinedRoom]");

        PhotonNetwork.LoadLevel("gameplay");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("[PhotonManager][OnDisconnected]" + cause);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("[PhotonManager][OnCreateRoomFailed]" + message);
        JoinRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("[PhotonManager][OnJoinRandomFailed]" + message);
        CreateRoom();
    }
    #endregion
}
