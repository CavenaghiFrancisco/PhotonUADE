using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject prefab;

    void Start()
    {
        PhotonNetwork.Instantiate(prefab.name, Vector3.zero, Quaternion.identity);
    }

    
}
