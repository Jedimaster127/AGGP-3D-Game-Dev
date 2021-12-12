using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class photonmanager : MonoBehaviourPunCallbacks
{
    public string username;

    public float red;

    public float green;

    public float blue;

    public Material playerColor;

    string gameVersion = "1";
    RoomOptions roomOptions = new RoomOptions();
    public GameObject gameManagerPref;

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

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void Update()
    {
        username = buttonstuff.button.user.text;
        playerColor.SetColor("_Color", new Color(red, green, blue));
    }

    void OnSceneLoad(Scene scene, LoadSceneMode loadMode)
    {
        //Spawns in FPSGameManager if the local player is the master client
        if (scene.name == "KingofHill" && PhotonNetwork.IsMasterClient)
        { 
            if(!FPSGameManager.instance && PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(gameManagerPref.name, Vector3.zero, Quaternion.identity);
            }
                
        }
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

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
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

        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("waiting room");
        }

    }

    public override void OnLeftRoom()
    {
        Debug.Log("[PhotonManager][OnLeftRoom]");

        PhotonNetwork.LoadLevel("SampleScene");
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
