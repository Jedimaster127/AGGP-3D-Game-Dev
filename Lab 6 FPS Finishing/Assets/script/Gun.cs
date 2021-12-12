using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Gun : MonoBehaviour
{
    public GameObject shotPoint;

    public List<GameObject> bullets = new List<GameObject>();

    public List<int> currentAmmo = new List<int>();
    public List<int> surplusAmmo = new List<int>();

    // what the name implies
    public void Shoot(int s)
    {
        PhotonNetwork.Instantiate(bullets[s].name, shotPoint.transform.position, shotPoint.transform.rotation);
    }

    // all bullet's curently loaded ammo and surplus ammo are saved to lists to be used to determin amount
    private void Start()
    {
        currentAmmo.Add(bullets[0].GetComponent<bullet>().avalableAmmo);
        currentAmmo.Add(bullets[1].GetComponent<bullet>().avalableAmmo);
        currentAmmo.Add(bullets[2].GetComponent<bullet>().avalableAmmo);

        surplusAmmo.Add(bullets[0].GetComponent<bullet>().surplusAmmo);
        surplusAmmo.Add(bullets[1].GetComponent<bullet>().surplusAmmo);
        surplusAmmo.Add(bullets[2].GetComponent<bullet>().surplusAmmo);
    }

    private void Update()
    {
        
    }
}
