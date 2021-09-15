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

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return) && !string.IsNullOrEmpty(input.text))
        {
            gameObject.GetPhotonView().RPC("UpdateChatroom", RpcTarget.AllBuffered, photonmanager.instance.username, input.text);
            input.text = "";
        }
    }

    [PunRPC]
    void UpdateChatroom(string _user, string _chat)
    {
        field.text += _user + ":  " + _chat + "\n";
    }
}
