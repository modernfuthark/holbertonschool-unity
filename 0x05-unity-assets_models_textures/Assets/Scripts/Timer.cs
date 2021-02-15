using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text Timer_Text;
    private int ms = 0;
    private int s = 0;
    private int m = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CountUp", 0, .01f);
    }

    // Update is called once per frame
    void CountUp()
    {
        ms++;
        if (ms == 99)
        {
            ms = 0;
            s++;
        }
        if (s == 60)
        {
            s = 0;
            m++;
        }

        Timer_Text.text = $"{m}:{s:D2}.{ms:D2}";
    }
}
