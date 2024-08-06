using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateSlotVIew : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI slote_text;

    [SerializeField]
    private Image background_image;

    [SerializeField]
    private Color highlight_color;

    [HideInInspector]
    public Color origin_color;
    public void Start()
    {
        origin_color = background_image.color;
    }


    public void SetText(string text)
    {
        if(slote_text != null) {
            slote_text.text = text;
        }
    }

    public void SetHighlight(bool is_highlight)
    {
        background_image.color = (is_highlight) ? highlight_color : origin_color;
    }
}
