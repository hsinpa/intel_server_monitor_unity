using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSlotVIew : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI slote_text;

    public void SetText(string text)
    {
        if(slote_text != null) {
            slote_text.text = text;
        }
    }
}
