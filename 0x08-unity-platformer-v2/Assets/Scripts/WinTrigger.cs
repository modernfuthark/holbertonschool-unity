using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    Timer script;
    public Text timer_text;
    public Canvas winCanvas;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            script = other.GetComponent<Timer>();
            //script.CancelInvoke();
            script.Invoke("Win", 0);

            //timer_text.color = Color.green;
            //timer_text.fontSize = 60;
            timer_text.text = "";
            winCanvas.gameObject.SetActive(true);
        }
    }
}
