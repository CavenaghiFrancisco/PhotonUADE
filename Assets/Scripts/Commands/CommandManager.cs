using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CommandManager : MonoBehaviourPunCallbacks
{
    public static CommandManager Instance;
    public PhotonView view;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ExecuteCommand(string command, string targetPlayerName, int value = 0)
    {
        Player targetPlayer = FindPlayerByName(targetPlayerName);
        if (targetPlayer == null)
        {
            Debug.LogError("Jugador no encontrado");
            return;
        }

        switch (command.ToLower())
        {
            case "heal":
                view.RPC("HealPlayer", targetPlayer, value);
                break;
            case "ban":
                Debug.LogError(targetPlayerName + " ha sido baneado");
                view.RPC("BanPlayer", targetPlayer);
                break;
            default:
                Debug.LogError("Comando desconocido");
                break;
        }
    }

    private Player FindPlayerByName(string name)
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.NickName == name)
                return player;
        }
        return null;
    }

}




