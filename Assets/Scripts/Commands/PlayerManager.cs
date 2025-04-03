using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public int health = 100;
    public PhotonView view;
    public TMPro.TextMeshProUGUI name;
    public bool banned = false;

    private void Start()
    {
        name.text = PhotonNetwork.NickName;
    }

    private void Update()
    {
        name.text = PhotonNetwork.NickName + " - " + health + "\n" + (!banned ? "<color=green>ONLINE" : "<color=red>BANNED");
    }

    [PunRPC]
    public void HealPlayer(int amount)
    {
        health += amount;
        Debug.Log($"{view.Owner.NickName} ha sido curado en {amount} puntos de vida. Vida actual: {health}");
    }

    [PunRPC]
    public void BanPlayer()
    {
        Debug.Log("Has sido baneado");
        banned = true;
        PhotonNetwork.Disconnect();
    }
}