using Photon.Pun;
using UnityEngine;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public int sceneToLoad;

    void Start()
    {
        ConnectToPhoton();
    }

    void ConnectToPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado al Master Server de Photon.");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        int playerNumber = PhotonNetwork.PlayerList.Length;
        PhotonNetwork.NickName = "Player" + playerNumber;
        PhotonNetwork.LoadLevel(sceneToLoad);
    }
}
