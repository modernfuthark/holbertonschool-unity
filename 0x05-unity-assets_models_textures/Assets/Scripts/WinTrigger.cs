using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    Timer script;
    public Text timer_text;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            script = other.GetComponent<Timer>();
            script.CancelInvoke();

            timer_text.color = Color.green;
            timer_text.fontSize = 60;
        }
    }
}
