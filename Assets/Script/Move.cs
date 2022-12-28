using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Move : MonoBehaviour
{
    List<GameObject> oynanabilirkareler = new List<GameObject>();
    GameObject[] yesildaireler;
    static GameObject secilentas;
    [SerializeField] GameObject daire;
    bool sifirlanmismi;
    bool hamlesirasibeyaz = true;
    PhotonView pw;

    private void Start()
    {
        pw = GetComponent<PhotonView>();
    }
    public void piyonhareket(Transform tas)
    {
        if (!hamlesirasi())
            return;
        Vector2[] Piyonvektor = { new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, -1) };
        int max = 1;
        bool hareket = tas.GetComponent<Piyon>().hareket;
        if (!hareket)
        {
            max = 2;

        }
        genelhareket(max, tas, Piyonvektor);
    }
    public void athareket(Transform tas)
    {
        if (!hamlesirasi())
            return;
        Vector2[] Atvektor = {new Vector2(1,2),new Vector2(2,1), new Vector2(2,-1), new Vector2(1,-2), new Vector2(-1,-2), new Vector2(-2,-1)
         ,new Vector2(-2,1),new Vector2(-1,2)};
            genelhareket(1, tas, Atvektor);
    }
    public void filhareket(Transform tas)
    {
        if (!hamlesirasi())
            return;
        Vector2[] Filvektor = { new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1) };
            genelhareket(8, tas, Filvektor);
    }
    public void kalehareket(Transform tas)
    {
        if (!hamlesirasi())
            return;
        Vector2[] Kalevektor = { new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1) };
            genelhareket(8, tas, Kalevektor);

    }
    public void vezirhareket(Transform tas)
    {
        if (!hamlesirasi())
            return;
        Vector2[] Vezirvektor = { new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1),
        new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1)};
            genelhareket(8, tas, Vezirvektor);
    }
    public void sahhareket(Transform tas)
    {
        if (!hamlesirasi())
            return;
        Vector2[] Sahvektor = { new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1),
        new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1)};
        genelhareket(1, tas, Sahvektor);
    }

    void yesildaire(bool goster)
    {

        if (goster)
        {
            yesildaireler = new GameObject[oynanabilirkareler.Count];
            for (int k = 0; k < yesildaireler.Length; k++)
            {
                yesildaireler[k] = Instantiate(daire, oynanabilirkareler[k].transform);
                //yesildaireler[k] = Instantiate(daire, oynanabilirkareler[k].transform.position,Quaternion.identity);
                sifirlanmismi = false;

            }
        }
        else if (yesildaireler != null && yesildaireler.Length > 0)
        {
            for (int k = 0; k < yesildaireler.Length; k++)
            {
                sifirlanmismi = true;
                yesildaireler[k].transform.SetParent(null, true);
                Destroy(yesildaireler[k]);

            }
        }

    }
    void sifirla()
    {
        yesildaire(false);
        oynanabilirkareler.Clear();

    }
    void genelhareket(int max, Transform tr, params Vector2[] vektorler)
    {
        if (!sifirlanmismi)
            sifirla();

        int sayi = int.Parse(tr.parent.name[0].ToString());
        char harf = tr.parent.name[1];
        secilentas = tr.gameObject;
        for (int i = 0; i < vektorler.Length; i++)
        {
            int x = (int)vektorler[i].x;
            int y = (int)vektorler[i].y;

            for (int j = 0; j < max; j++)
            {
                string kareismi;
                if (TablePhoton.tasrenk=='b')
                {
                    kareismi = (sayi + (x * (j + 1))).ToString() + (char)(harf + (y * (j + 1)));

                }
                else
                {

                    kareismi = (sayi - (x * (j + 1))).ToString() + (char)(harf - (y * (j + 1)));

                }

                //Debug.Log(kareismi);
                if (GameObject.Find(kareismi))
                {
                    GameObject kare = GameObject.Find(kareismi);
                    if (kare.transform.childCount == 0)
                    {
                        if (!(tr.GetComponent<Piyon>() && i != 0))
                            oynanabilirkareler.Add(kare);


                    }
                    //else if (kare.transform.GetChild(0).tag == "Rakip")
                    else if (kare.transform.GetChild(0).name[0]==TablePhoton.tasrenkrakip)
                    {
                        if (!(tr.GetComponent<Piyon>() && i == 0))
                        {
                            oynanabilirkareler.Add(kare);
                            break;
                        }

                    }
                    else
                        break;


                }
                else
                    break;
            }
        }
        yesildaire(true);


    }
    [PunRPC]
    public void yoket(string tastag)
    {
        Destroy(GameObject.FindWithTag(tastag));
    }
    [PunRPC]
    public void sira()
    {
        hamlesirasibeyaz = !hamlesirasibeyaz;

    }
    [PunRPC]
    public void hareket(string tastag,string kareisim)
    {
        Debug.Log("taş tag=" + tastag + " kare tag=" + kareisim);
        GameObject.FindWithTag(tastag).transform.SetParent(GameObject.Find(kareisim).transform);
        GameObject.FindWithTag(tastag).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

    }
    public void kare(Transform kare)
    {
        if (oynanabilirkareler.Contains(kare.gameObject))
        {
            pw.RPC("sira", RpcTarget.All);
            sifirla();
            if (kare.childCount >= 1)
            {

                if (kare.GetChild(0).name[0]==TablePhoton.tasrenkrakip)
                {
                    Debug.Log(kare.GetChild(0).name);
                    //Destroy(kare.GetChild(0).gameObject);
                    pw.RPC("yoket", RpcTarget.All,kare.GetChild(0).tag);

                }
            }

            if (secilentas.GetComponent<Piyon>())
            {
                secilentas.GetComponent<Piyon>().hareket = true;
            }
            /*secilentas.transform.SetParent(kare);
            secilentas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);*/
            pw.RPC("hareket", RpcTarget.All,secilentas.tag,kare.name);
            //sahkontrol(kare);
        }

    }

    public static bool secilenvarmi()
    {
        if (secilentas)
            return true;
        else
            return false;
    }
   /* void rok(Transform t)
    {
        if (t.GetComponent<Sah>().hareket || !(t.GetComponent<Sah>().rokatildimi))
            return;
        int sayac = 0;
        char tasrengi = t.name[0];
        switch (tasrengi)
        {
            case 's':
                if (GameObject.Find("8A"))
                {
                    GameObject sagkale = GameObject.Find("8A");
                    if (!(sagkale.GetComponent<Kale>().hareket))
                    {
                        for (char k = 'd'; k > 'a'; k--)
                        {
                            string kareisim = "8" + k;
                            if (GameObject.Find(kareisim).transform.childCount == 0)
                            {
                                sayac++;
                            }
                            if (sayac == 3)
                            {
                                oynanabilirkarelerrok.Add(GameObject.Find("8C"));
                            }

                        }
                    }
                }
                if (GameObject.Find("8H"))
                {
                    GameObject solkale = GameObject.Find("8H");
                    if (!(solkale.GetComponent<Kale>().hareket))
                    {
                        for (char k = 'f'; k < 'h'; k++)
                        {
                            string kareisim = "8" + k;
                            if (GameObject.Find(kareisim).transform.childCount == 0)
                            {
                                sayac++;
                            }
                            if (sayac == 3)
                            {
                                oynanabilirkarelerrok.Add(GameObject.Find("8G"));
                            }
                        }
                    }
                }
                yesildairerok(true);
                break;

            case 'b': break;
            default:
                break;
        }

    }*/
   /*void roka(Transform sah)
    {
        char ikinci = sah.parent.name[1];
        char e = 'E';
        GameObject kale;
        for (char k = e += (char)1; k <= 'H'; k++)
        {
            if (k != 'H')
            {

            }
            else
            {
                if(GameObject.Find((ikinci + k).ToString()))
                kale= GameObject.Find((ikinci + k).ToString());

            }

        }
        for (char k = e -= (char)1; k >= 'A'; k--)
        {


        }
    }*/
   /*void yesildairerok(bool goster)
    {

        if (goster)
        {
            yesildairelerrok = new GameObject[oynanabilirkarelerrok.Count];
            for (int k = 0; k < yesildairelerrok.Length; k++)
            {
                yesildairelerrok[k] = Instantiate(daire, oynanabilirkarelerrok[k].transform);
                //yesildaireler[k] = Instantiate(daire, oynanabilirkareler[k].transform.position,Quaternion.identity);
                sifirlanmismirok = false;

            }
        }
        else if (yesildairelerrok != null && yesildairelerrok.Length > 0)
        {
            for (int k = 0; k < yesildairelerrok.Length; k++)
            {
                sifirlanmismirok = true;
                yesildairelerrok[k].transform.SetParent(null, true);
                Destroy(yesildairelerrok[k]);

            }
        }

    }*/
   bool hamlesirasi()
    {
        if(TablePhoton.tasrenk=='b')
        {
            return hamlesirasibeyaz;
            
        }
        else
        {
            return !hamlesirasibeyaz;
        }
        
        
    }
    /*void sahkontrol(Transform karetr)
    {
        int sayi = int.Parse(karetr.name[0].ToString());
        char harf = karetr.name[1];
        GameObject rakipsahkare = GameObject.Find(TablePhoton.tasrenkrakip + "Sah").transform.parent.gameObject;
        for (int i = 0; i < secilentasvektorler.Length; i++)
        {
            int x = (int)secilentasvektorler[i].x;
            int y = (int)secilentasvektorler[i].y;

            for (int j = 0; j < secilentasmax; j++)
            {
                string kareismi;
                if (TablePhoton.tasrenk == 'b')
                {
                    kareismi = (sayi + (x * (j + 1))).ToString() + (char)(harf + (y * (j + 1)));

                }
                else
                {

                    kareismi = (sayi - (x * (j + 1))).ToString() + (char)(harf - (y * (j + 1)));

                }

                //Debug.Log(kareismi);
                if (GameObject.Find(kareismi))
                {
                    GameObject kare = GameObject.Find(kareismi);
                    if (kare.transform.childCount == 0)
                    {
                        if (!(karetr.GetComponent<Piyon>() && i != 0))
                            oynanabilirkarelersah.Add(kare);


                    }
                    //else if (kare.transform.GetChild(0).tag == "Rakip")
                    else if (kare.transform.GetChild(0).name[0] == TablePhoton.tasrenkrakip)
                    {
                        if (!(karetr.GetComponent<Piyon>() && i == 0))
                        {
                            oynanabilirkarelersah.Add(kare);
                            break;
                        }

                    }
                    else
                        break;


                }
                else
                    break;
            }
        }
        if(oynanabilirkarelersah.Contains(rakipsahkare))
        {
            check = true;
        }


    }*/
}




