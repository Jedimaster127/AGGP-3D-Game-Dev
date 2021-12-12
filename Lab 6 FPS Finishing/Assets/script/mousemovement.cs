using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class mousemovement : MonoBehaviour
{
    public float mouseSensitive = 200f;

    public GameObject playerBody;

    public GameObject head;

    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (gameObject.GetPhotonView().IsMine)
        {
            //playerBody = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetPhotonView().IsMine)
        {

            float mosueX = Input.GetAxis("Mouse X") * mouseSensitive * Time.deltaTime;

            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitive * Time.deltaTime;

            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 60f);

            head.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.transform.Rotate(Vector3.up * mosueX);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(Cursor.lockState == CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }
}
