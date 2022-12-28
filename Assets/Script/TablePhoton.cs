using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class TablePhoton : MonoBehaviourPunCallbacks
{
    GameObject panel;
    [SerializeField] GameObject koyukare;
    [SerializeField] GameObject açıkkare;
    char[] harf = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
    [SerializeField] GameObject bPiyon, sPiyon, bKale, sKale, bAt, sAt, bFil, sFil, bVezir, sVezir, bSah, sSah;
    public static char tasrenk;
    public static char tasrenkrakip;
    private void Awake()
    {

        //YT();

    }
    void Start()
    {
        PhotonView pw = GetComponent<PhotonView>();
        tasrengiayarla();
        square();
        piyonyerlestir();
        kaleyerlestir();
        atyerlestir();
        filyerlestir();
        veziryerlestir();
        sahyerlestir();
        Invoke("rakipraycast",.5f);
    }

    void tasrengiayarla()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            tasrenk = Sunucu.player1;
            tasrenkrakip = Sunucu.player2;
        }
        else
        {
            tasrenk = Sunucu.player2;
            tasrenkrakip = Sunucu.player1;
        }
    }
    void square()
    {
        panel = GameObject.FindGameObjectWithTag("Table");
        float val = GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width / 8;
        for (int i = 0; i < 8; i++)
        {
            for (int k = 0; k < 8; k++)
            {
                Vector3 vect = new Vector3(k * val, -i * val, 1);
                GameObject kare;
                if ((i + 1 * k + 1) % 2 == 1)
                {
                    //kare = PhotonNetwork.Instantiate("Acik", Vector2.zero, Quaternion.identity, 0, null);
                    //kare.transform.SetParent(panel.transform);
                    //kare.transform.localScale = Vector3.one;
                    kare = Instantiate(açıkkare, panel.transform);
                    kare.transform.localPosition = vect;
                    kare.name = harf[i].ToString() + (k + 1).ToString();
                    kare.name = (i + 1).ToString() + harf[7 - k].ToString();

                }
                else
                {
                    //kare = PhotonNetwork.Instantiate("Koyu", Vector3.zero, Quaternion.identity, 0, null);
                    //kare.transform.SetParent(panel.transform);
                    //kare.transform.localScale = Vector3.one;
                    kare = Instantiate(koyukare, panel.transform);
                    kare.transform.localPosition = vect;
                    kare.name = harf[i].ToString() + (k + 1).ToString();
                    kare.name = (i + 1).ToString() + harf[7 - k].ToString();

                }
                if (i == 0 || i == 7)
                {
                    kare.tag = "UstKenar";
                }
                else if (k == 0 || k == 7)
                {
                    kare.tag = "YanKenar";

                }
            }
        }
    }
    void piyonyerlestir()
    {
        int bsayi = 0;
        int ssayi = 0;
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            GameObject kare = panel.transform.GetChild(i).gameObject;
            if (kare.name.Contains("2"))
            {
                //GameObject piyon =Instantiate("bPiyon", Vector3.zero, Quaternion.identity, 0, null);
                GameObject piyon = Instantiate(bPiyon, kare.transform);
                piyon.transform.SetParent(kare.transform);
                //piyon.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                piyon.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                piyon.transform.tag = piyon.name.Substring(0,2) + (bsayi+1).ToString();
                bsayi++;

            }
            else if (kare.name.Contains("7"))
            {
                //Instantiate(sPiyon, kare.transform);
                //GameObject piyon = PhotonNetwork.Instantiate("sPiyon", Vector3.zero, Quaternion.identity, 0, null);
                GameObject piyon = Instantiate(sPiyon, kare.transform);
                piyon.transform.SetParent(kare.transform);
               // piyon.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                piyon.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                piyon.transform.tag = piyon.name.Substring(0, 2) + (ssayi+1).ToString();
                ssayi++;

            }
        }
    }
    void kaleyerlestir()
    {
        GameObject bK1=  Instantiate(bKale, GameObject.Find("1A").transform);
        bK1.tag = "bK1";
        GameObject bK2 = Instantiate(bKale, GameObject.Find("1H").transform);
        bK2.tag = "bK2";
        GameObject sK1 = Instantiate(sKale, GameObject.Find("8A").transform);
        sK1.tag = "sK1";
        GameObject sK2 = Instantiate(sKale, GameObject.Find("8H").transform);
        sK2.tag = "sK2";
    }
    void atyerlestir()
    {
        GameObject bA1 = Instantiate(bAt, GameObject.Find("1B").transform);
        bA1.tag = "bA1";
        GameObject bA2 = Instantiate(bAt, GameObject.Find("1G").transform);
        bA2.tag = "bA2";
        GameObject sA1 = Instantiate(sAt, GameObject.Find("8B").transform);
        sA1.tag = "sA1";
        GameObject sA2 = Instantiate(sAt, GameObject.Find("8G").transform);
        sA2.tag = "sA2";
    }
    void filyerlestir()
    {
        GameObject bF1= Instantiate(bFil, GameObject.Find("1C").transform);
        bF1.tag = "bF1";
        GameObject bF2 = Instantiate(bFil, GameObject.Find("1F").transform);
        bF2.tag = "bF2";
        GameObject sF1 = Instantiate(sFil, GameObject.Find("8C").transform);
        sF1.tag = "sF1";
        GameObject sF2 = Instantiate(sFil, GameObject.Find("8F").transform);
        sF2.tag = "sF2";
    }
    void veziryerlestir()
    {
        GameObject bV1=Instantiate(bVezir, GameObject.Find("1D").transform);
        bV1.tag = "bV";
        GameObject sV1=Instantiate(sVezir, GameObject.Find("8D").transform);
        sV1.tag = "sV";
    }
    void sahyerlestir()
    {
        GameObject bS=Instantiate(bSah, GameObject.Find("1E").transform);
        bS.tag = "bS";
        GameObject sS = Instantiate(sSah, GameObject.Find("8E").transform);
        sS.tag = "sS";
    }
   public void rakipraycast()
    {
        if(tasrenk=='s')
        {
            for (int k = 0; k < 16; k++)
            {
                panel.transform.GetChild(k).GetChild(0).GetComponent<Image>().raycastTarget = false;
               
            }
        }
        else
        {
            for (int i = 63; i >47; i--)
            {
                panel.transform.GetChild(i).GetChild(0).GetComponent<Image>().raycastTarget = false;

            }
        }
    }
    
}
