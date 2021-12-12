using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Photon.Pun;
using Photon.Realtime;
public class FPSPlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public Slider hpSlider;

    public static FPSPlayerManager instance;

    public float speed = 5f;
    public CharacterController charControl;
    Vector3 move;
    public TextMeshPro username;

    public float maxHP = 100;

    public float curentHP;

    public Gun gun;

    public List<GameObject> spawnPoints = new List<GameObject>();

    public AudioSource gunshot;

    public GameObject playerCam;

    public GameObject ui;

    public GameObject baroll;

    public int points = 0;

    public int shotType = 0;

    int amount;

    int playerNum;

    float savedspeed;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //sets up the hp slider, saves the speed, displays the players name, and activates the cam
        if (gameObject.GetPhotonView().IsMine)
        {
            gameObject.GetPhotonView().RPC("PlayerUser", RpcTarget.AllBuffered, photonmanager.instance.username);

            curentHP = maxHP;

            hpSlider.value = maxHP;
            hpSlider.maxValue = maxHP;
            hpSlider.minValue = 0;

            savedspeed = speed;

            playerCam.SetActive(true);

            ui.SetActive(true);

            if(FPSPointManager.instance)
            {
                FPSPointManager.instance.gameObject.GetPhotonView().RPC("NameAdder", RpcTarget.AllBuffered, photonmanager.instance.username);

                playerNum = FPSPointManager.instance.playercount;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.GetPhotonView().IsMine)
        {
            //moving
            if (charControl.isGrounded)
            {
                float x = Input.GetAxis("Horizontal");

                float z = Input.GetAxis("Vertical");

                move = gameObject.transform.right * x + gameObject.transform.forward * z;

                move *= speed;
            }

            move.y -= 9.81f * Time.deltaTime;

            charControl.Move(move * Time.deltaTime);

            //swaps bullet type
            if (Input.GetKeyDown(KeyCode.E) && shotType != 2)
            {
                if (shotType != 2)
                {
                    shotType++;
                }
                else if(shotType >= 2)
                {
                    shotType = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.Q) && shotType != 0)
            {
                if (shotType != 0)
                {
                    shotType--;
                }
                else if(shotType <= 0)
                {
                    shotType = 2;
                }
            }

            //shoots while there is ammo and allows animation to play
            if (Input.GetKeyDown(KeyCode.Mouse0) && chatroom_manager.instance.input.IsInteractable() == false)
            {
                if(gun.currentAmmo[shotType] != 0)
                {
                    gun.Shoot(shotType);
                    gun.currentAmmo[shotType] -= 1;
                    gunshot.Play();

                    baroll.GetComponent<Animator>().SetBool("fired", true);
                }
            }

            //stops animation
            if(Input.GetKeyUp(KeyCode.Mouse0) && chatroom_manager.instance.input.IsInteractable() == false)
            {
                baroll.GetComponent<Animator>().SetBool("fired", false);
            }

            //reloading
            if(Input.GetKeyDown(KeyCode.R) && gun.currentAmmo[shotType] < gun.bullets[shotType].GetComponent<bullet>().avalableAmmo && gun.surplusAmmo[shotType] != 0 && chatroom_manager.instance.input.IsInteractable() == false)
            {
                while(gun.currentAmmo[shotType] < gun.bullets[shotType].GetComponent<bullet>().avalableAmmo)
                {
                    gun.currentAmmo[shotType]++;

                    gun.surplusAmmo[shotType]--;
                }
            }

            gameObject.GetPhotonView().RPC("PlayerHPLoss", RpcTarget.AllBuffered);

            if(FPSPointManager.instance)
            {
                FPSPointManager.instance.gameObject.GetPhotonView().RPC("PointDetermin", RpcTarget.AllBuffered, playerNum, points);
            }

        }

        //stops movement while typeing in chat
        if (chatroom_manager.instance.input.IsInteractable() == true)
        {
            speed = 0;
        }
        else
        {
            speed = savedspeed;
        }

        if (curentHP == 0)
        {
            gameObject.GetPhotonView().RPC("OnDeath", RpcTarget.AllBuffered);
        }
    }
    
    //determins how harmfull each bullet type is

    private void OnTriggerEnter(Collider other)
    {
        if (curentHP < maxHP || gun.surplusAmmo[shotType] < gun.bullets[shotType].GetComponent<bullet>().surplusAmmo)
        {
            if (other.gameObject.tag == "LargHP")
            {
                curentHP = maxHP;
            }

            if (other.gameObject.tag == "SmallHP")
            {
                if(curentHP <= maxHP - 50)
                {
                    curentHP += 50;
                }
                else
                {
                    curentHP = maxHP;
                }

                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "SmallAmmo")
            {
                if (gun.surplusAmmo[shotType] <= gun.bullets[shotType].GetComponent<bullet>().surplusAmmo - 50)
                {
                    gun.surplusAmmo[shotType] += 50;
                }
                else
                {
                    gun.surplusAmmo[shotType] = gun.bullets[shotType].GetComponent<bullet>().surplusAmmo;
                }

                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "LargAmmo")
            {
                if (gun.surplusAmmo[shotType] <= gun.bullets[shotType].GetComponent<bullet>().surplusAmmo - 100)
                {
                    gun.surplusAmmo[shotType] += 100;
                }
                else
                {
                    gun.surplusAmmo[shotType] = gun.bullets[shotType].GetComponent<bullet>().surplusAmmo;
                }

                Destroy(other.gameObject);
            }       

        }
    }

    //death
    [PunRPC]
    void OnDeath()
    {
        int point = Random.Range(0, spawnPoints.Count);
        gameObject.transform.position = spawnPoints[point].transform.position;
        gameObject.transform.rotation = spawnPoints[point].transform.rotation;

        hpSlider.value = maxHP;
        curentHP = maxHP;
        
    }
    
    //display user
    [PunRPC]
    void PlayerUser(string name )
    {
        username.text = name;
    }

    [PunRPC]
    void PlayerHPLoss()
    {
        hpSlider.value = curentHP;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(curentHP);
        }
        else
        {
            curentHP = (int)stream.ReceiveNext();
        }
    }
}
