using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PhotonChat : MonoBehaviour
{
    public TMPro.TMP_InputField chatInput;
    public TMPro.TextMeshProUGUI chatDisplay;
    public PhotonView view;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(chatInput.text))
        {
            SendMessageToChat(chatInput.text);
            chatInput.text = "";
        }
    }

    public void SendMessageToChat(string message)
    {
        view.RPC("ReceiveChatMessage", RpcTarget.AllBuffered, PhotonNetwork.NickName, message);
    }

    [PunRPC]
    void ReceiveChatMessage(string sender, string message)
    {
        chatDisplay.text += $"\n<b>{sender}:</b> {message}";
    }
}
