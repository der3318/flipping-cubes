using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    private GameSettings settings;

    void Start()
    {
        settings = GameObject.Find("GameSettings").GetComponent<GameSettings>();
    }

    void Update()
    {
        string text = "";
        for (int rankIdx = 0; rankIdx < settings.topScoreList.Length; rankIdx++)
        {
            text += String.Format("#{0}\t{1,1:D4}\n", rankIdx + 1, settings.topScoreList[rankIdx]);
        }
        GetComponent<Text>().text = text;
    }
}
