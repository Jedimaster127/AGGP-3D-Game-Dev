using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class chatroom_manager : MonoBehaviour
{
    public TMP_InputField input;
    public TextMeshProUGUI field;

    public static chatroom_manager instance;

    bool isSelected;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (lobbymanager.instance)
        {
            lobbymanager.instance.gameObject.GetPhotonView().RPC("PlayerJoined", RpcTarget.AllBuffered);
            lobbymanager.instance.gameObject.GetPhotonView().RPC("PlayerUser", RpcTarget.AllBuffered, photonmanager.instance.username);     
        }

        isSelected = false;
    }

    private void Update()
    {

        if(Input.GetKey(KeyCode.T))
        {
           if(isSelected == false)
           {
               input.interactable = true;
               input.Select();
           }
           else
           {
               input.interactable = false;
           }
            
        }

        if (Input.GetKey(KeyCode.Return) && !string.IsNullOrEmpty(input.text))
        {
            gameObject.GetPhotonView().RPC("UpdateChatroom", RpcTarget.AllBuffered, photonmanager.instance.username, input.text);
            input.text = "";
            input.interactable = false;
        }
    }

    [PunRPC]
    void UpdateChatroom(string _user, string _chat)
    {
        field.text += _user + ":  " + _chat + "\n";
    }
    
}
