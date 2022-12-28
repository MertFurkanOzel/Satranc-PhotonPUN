using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Kale : MonoBehaviour, IPointerDownHandler
{
    public bool hareket = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (name[0] == TablePhoton.tasrenk)
            GameObject.Find("GController").GetComponent<Move>().kalehareket(transform);
    }
}