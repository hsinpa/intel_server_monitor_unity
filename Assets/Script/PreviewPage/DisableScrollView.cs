using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisableScrollView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private UnityEngine.UI.ScrollRect target;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (target != null) target.enabled = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (target != null) target.enabled = true;
    }
}
