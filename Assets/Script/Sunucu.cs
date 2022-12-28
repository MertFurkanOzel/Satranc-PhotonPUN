using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class Sunucu : MonoBehaviourPunCallbacks
{
    public static char player1, player2;
    public string nick;
    public string rakipnick;
    PhotonView pw;
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        pw = GetComponent<PhotonView>();
        DontDestroyOnLoad(this);
        PhotonNetwork.ConnectUsingSettings();
        if (PlayerPrefs.HasKey("nick"))
        {
            GameObject.FindGameObjectWithTag("inputf").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("nick");
        }

    }

    public override void OnConnectedToMaster()
    {

        Debug.Log("Sunucuya bağlandı");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye bağlandı");


    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya bağlandı");
        //kontrol();
        PhotonNetwork.LoadLevel(1);
        InvokeRepeating("arasahne", 0, .5f);
    }


    public void OdaOlustur()
    {
        nick = GameObject.FindGameObjectWithTag("inputf").GetComponent<TextMeshProUGUI>().text;
        PhotonNetwork.JoinOrCreateRoom("Mert", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);

    }
    public void Odayakatil()
    {
        nick = GameObject.FindGameObjectWithTag("inputf").GetComponent<TextMeshProUGUI>().text;
        PhotonNetwork.JoinRandomRoom();

    }
    void nickyazdir()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.PlayerList[0].NickName = nick;
                GameObject.FindGameObjectWithTag("rakipnick").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[1].NickName;
            }
            else
            {
                PhotonNetwork.PlayerList[1].NickName = nick;
                GameObject.FindGameObjectWithTag("rakipnick").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;

            }
        }
        GameObject.FindGameObjectWithTag("nick").GetComponent<TextMeshProUGUI>().text = nick;
        PlayerPrefs.SetString("nick", nick);
    }
    [PunRPC]
    public void tasrengi(int sayi)
    {
        Debug.LogError("rpc");
        if (sayi % 2 == 0)
        {
            player1 = 'b';
            player2 = 's';

        }
        else
        {
            player1 = 's';
            player2 = 'b';

        }
        PhotonNetwork.LoadLevel(2);
    }
    void arasahne()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                pw.RPC("tasrengi", RpcTarget.All, Random.Range(0, 2));
            }
      
            InvokeRepeating("nickyazdir", 1f, 1f);
            CancelInvoke("arasahne");
        }
        else
        {
            Debug.LogError("1 kişi var");
        }
    }
}
