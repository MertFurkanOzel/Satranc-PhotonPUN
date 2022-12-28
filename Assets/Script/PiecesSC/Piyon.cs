using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Piyon : MonoBehaviour,IPointerDownHandler//,IPointerUpHandler
{
    public bool hareket;
    GraphicRaycaster graycaster;
    PointerEventData peventdata;
    EventSystem eventsystem;

    private void Awake()
    {
        hareket = false;
        //graycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        //eventsystem= GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (name[0] == TablePhoton.tasrenk)
        {
            Debug.LogError("piyon");
            GameObject.Find("GController").GetComponent<Move>().piyonhareket(transform);
            //StartCoroutine(drag());
        }
    }

    /* public void OnPointerUp(PointerEventData eventData)
     {
         StopAllCoroutines();
         Debug.Log(raycastgameobject().name);
     }
     IEnumerator drag()
     {
         while(true)
         {
             transform.position = Input.mousePosition;
             yield return 0;
         }

     }
     GameObject raycastgameobject()
     {
         peventdata = new PointerEventData(eventsystem);
         peventdata.position = Input.mousePosition;
         List<RaycastResult> results = new List<RaycastResult>();
         graycaster.Raycast(peventdata, results);

         return results[1].gameObject;
     }*/
}
