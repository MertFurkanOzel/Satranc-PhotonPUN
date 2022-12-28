using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurnTab : MonoBehaviour
{
 
    private void Start()
    {
        Invoke("turntable",.3f);
    }
    public void turntable()
    {
        if (TablePhoton.tasrenk == 'b')
            turnB();
         else
            turnS();
    }

    public void turnB()
    {
        float val = GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width / 8;
        GameObject table = GameObject.Find("Table");
        for (int i = 0; i < 64; i++)
        {
            Transform kare = table.transform.GetChild(i);
            int num = int.Parse(table.transform.GetChild(i).name[0].ToString());
            kare.localPosition = new Vector3(-kare.localPosition.x, (num - 1) * val, kare.localPosition.z);
            //table.transform.Translate(new Vector3(7 * val, 0, 0));
        }
        //table.GetComponent<RectTransform>().anchoredPosition = new Vector2(900 - val, -900 + val);
        table.GetComponent<RectTransform>().anchoredPosition = new Vector2(7 * val, -7*val);
    }
    public void turnS()
    {
        GameObject table = GameObject.Find("Table");
        float val = GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width / 8;

        for (int i = 0; i < 64; i++)
        {
            Transform kare = table.transform.GetChild(i);
            int num = int.Parse(table.transform.GetChild(i).name[0].ToString());
            kare.localPosition = new Vector3(i%8 * val, -i/8 * val, 1);
        }
        table.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

    }

}
