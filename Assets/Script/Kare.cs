using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Kare : MonoBehaviour, IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject controller = GameObject.Find("GController");
        if (transform.childCount>0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                string isim = transform.GetChild(i).name.Substring(1, transform.GetChild(i).name.Length - 1);
                if (transform.GetChild(i).name[0] != TablePhoton.tasrenkrakip)
                {

                    Debug.LogError("kare");
                    switch (isim)
                    {
                        case "Piyon(Clone)": controller.GetComponent<Move>().piyonhareket(transform.GetChild(i)); break;
                        case "At(Clone)": controller.GetComponent<Move>().athareket(transform.GetChild(i)); break;
                        case "Fil(Clone)": controller.GetComponent<Move>().filhareket(transform.GetChild(i)); break;
                        case "Kale(Clone)": controller.GetComponent<Move>().kalehareket(transform.GetChild(i)); break;
                        case "Vezir(Clone)": controller.GetComponent<Move>().vezirhareket(transform.GetChild(i)); break;
                        case "Sah(Clone)": controller.GetComponent<Move>().sahhareket(transform.GetChild(i)); break;
                        case "aire(Clone)": controller.GetComponent<Move>().kare(transform); break;

                        default:
                           // controller.GetComponent<Move>().kare(transform);
                            break;
                    }
                }
            }
        }
         transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Move.secilenvarmi())
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                char isim = transform.GetChild(i).name[0];

                if (isim != 's' && isim != 'b')
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                    transform.GetComponent<Image>().color = new Color32(220, 220, 220, 255);
                }
            }
        }       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Move.secilenvarmi())
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                char isim = transform.GetChild(i).name[0];

                if (isim != 's' && isim != 'b')
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                    transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                }
            }
        }
    }
}
