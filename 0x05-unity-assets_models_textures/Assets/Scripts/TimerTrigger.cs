using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    Timer script;
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
            script = other.GetComponent<Timer>();
            script.enabled = true;
    }
}
