using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderAR : MonoBehaviour
{
    public GameObject text;
    
    public void Show()
    {
        text.gameObject.SetActive(true);
    }

    public void Hide()
    {
        text.gameObject.SetActive(false);
    }

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }
}
