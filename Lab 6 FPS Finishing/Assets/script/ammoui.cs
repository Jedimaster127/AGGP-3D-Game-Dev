using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ammoui : MonoBehaviour
{
    public Gun playerGun;

    public FPSPlayerManager player;

    public Text curentAmmo;

    public Text surplus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // displays the ammo info
    void Update()
    {
        curentAmmo.text = playerGun.currentAmmo[player.shotType].ToString();
        surplus.text = playerGun.surplusAmmo[player.shotType].ToString();
    }
}
