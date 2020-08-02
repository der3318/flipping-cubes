using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepCount : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        int stepCount = GameObject.Find("GameSettings").GetComponent<GameSettings>().stepCount;
        GetComponent<Text>().text = (String.Format("STEPS = {0,1:D4}", stepCount));
    }
}
