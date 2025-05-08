using UnityEngine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;

public class RoundManager : MonoBehaviourPunCallbacks
{
    public TMPro.TextMeshProUGUI statusText;
    private List<(int actorNumber, double pressTime, double arrivalTime)> pressQueue = new();
    private bool waitingForInputs = false;
    private float inputWindowDuration = 4f;
    private PhotonView view;

    public PhotonView View { get => view; }

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void NotifyPress(int actorNumber, double pressTime)
    {
        double arrivalTime = PhotonNetwork.Time;
        pressQueue.Add((actorNumber, pressTime, arrivalTime));

        statusText.text = $"¡Presionaste! (t = {pressTime:F3})";
        Debug.Log($"Recibido de {actorNumber} con t={pressTime}, llegada a t={arrivalTime}");

        if (!waitingForInputs)
        {
            waitingForInputs = true;
            StartCoroutine(InputWindow());
        }
    }

    private IEnumerator InputWindow()
    {
        yield return new WaitForSeconds(inputWindowDuration);

        pressQueue.Sort((a, b) => a.pressTime.CompareTo(b.pressTime));

        string resultText = "Resultados:\n";
        for (int i = 0; i < pressQueue.Count; i++)
        {
            resultText += $"{i + 1}. Jugador {pressQueue[i].actorNumber} (t={pressQueue[i].pressTime:F3}), Llegada: {pressQueue[i].arrivalTime:F3}\n";
        }

        view.RPC("ReceiveResults", RpcTarget.All, resultText);

        pressQueue.Clear();
        waitingForInputs = false;
    }

    [PunRPC]
    void ReceiveResults(string resultText)
    {
        statusText.text = resultText;
    }
}
