using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        Invoke("rakiptagayarla", 1f);
    }
    void rakiptagayarla()
    {
        GameObject table = GameObject.Find("Table");
        if(!(TablePhoton.tasrenk=='b'))
        {
            for(int k=0;k<16;k++)
            {
                GameObject rakip = table.transform.GetChild(k).GetChild(0).gameObject;
                rakip.tag = "Rakip";
                rakipraycast(rakip);
            }
        }
        else
        {
            for (int k = table.transform.childCount-1; k > 47; k--)
            {
                GameObject rakip = table.transform.GetChild(k).GetChild(0).gameObject;
                rakip.tag = "Rakip";
                rakipraycast(rakip);


            }
        }
    }
    void rakipraycast(GameObject rakiptas)
    {
        rakiptas.GetComponent<Image>().raycastTarget = false;
    }
}
