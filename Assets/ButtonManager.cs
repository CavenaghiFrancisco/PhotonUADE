using UnityEngine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviourPunCallbacks
{
    public TMPro.TextMeshProUGUI statusText; 
    public Button actionButton;
    private PhotonView view;
    private RoundManager roundManager;

    private void Start()
    {
        roundManager = FindObjectOfType<RoundManager>();
        view = GetComponent<PhotonView>();
        if(view.IsMine)
        {
            actionButton = FindObjectOfType<Button>();
            statusText = FindObjectOfType<TMPro.TextMeshProUGUI>();
            actionButton.onClick.AddListener(OnButtonPressed);
        }
    }

    public void OnButtonPressed()
    {
        double pressTime = PhotonNetwork.Time;
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        if (actorNumber == 2)
        {
            StartCoroutine(SendDelayed(pressTime, actorNumber));
        }
        else
        {
            roundManager.View.RPC("NotifyPress", RpcTarget.MasterClient, actorNumber, pressTime);
        }

        
    }

    IEnumerator SendDelayed(double pressTime, int actorNumber)
    {
        yield return new WaitForSeconds(3f);
        roundManager.View.RPC("NotifyPress", RpcTarget.MasterClient, actorNumber, pressTime);
    }

    
}
