using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public Camera mainCam;
    public GameObject player;
    public Canvas timerCanvas;
    public void BeginPlay()
    {
        mainCam.gameObject.SetActive(true);
        player.GetComponent<PlayerController>().enabled = true;
        mainCam.gameObject.SetActive(true);
        gameObject.GetComponent<CutsceneController>().enabled = false;
    }
}
