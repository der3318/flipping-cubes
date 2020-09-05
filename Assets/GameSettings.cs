using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    /* linked objects */
    public GameObject gameOverWindow;
    public GameObject cubes;
    public GameMap gameMap;
    public Image backgroundImage;

    /* config */
    public int squareSize;

    /* game status */
    public HashSet<int> blockNumberSet = new HashSet<int>();
    public int[] targetNumberList = new int[4];
    public int[] topScoreList = new int[5];
    public int stepCount;
    public int stage;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowGameOverWindow()
    {
        gameOverWindow.SetActive(true);
    }

    public void SetupNewCubes()
    {
        var clone = Instantiate(cubes) as GameObject;
        clone.transform.localPosition = new Vector3(0.5f, 0.5f, 0.5f);
        clone.SetActive(true);
    }

    public bool NextStage()
    {
        bool hasNextStage = true;
        try
        {
            StreamReader sr = new StreamReader(String.Format("GameSettings/Stage{0,1:D3}.txt", stage + 1));
            for (int lineIdx = 0; !sr.EndOfStream; lineIdx++)
            {
                string line = sr.ReadLine();
                if (line.Length > 0 && lineIdx < targetNumberList.Length)
                {
                    int targetNumber = int.Parse(line);
                    if (stage > 0)
                    {
                        blockNumberSet.Add(targetNumberList[lineIdx]);
                    }
                    targetNumberList[lineIdx] = targetNumber;
                }
            }
            sr.Close();
            stage += 1;
        }
        catch (Exception)
        {
            Debug.Log("NO_MORE_STAGES");
            hasNextStage = false;
        }
        gameMap.ResetAndUpdateMap();
        return hasNextStage;
    }

    public void AppendNewScore()
    {
        try
        {
            File.AppendAllText("GameSettings/Scoreboard.txt", System.String.Format("{0}\n", stepCount.ToString()));
        }
        catch (Exception)
        {
            Debug.Log("FILE_WRITE_ERROR");
        }
    }

    public void UpdateTopScoreList()
    {
        List<int> scoreList = new List<int>();
        try
        {
            StreamReader sr = new StreamReader("GameSettings/Scoreboard.txt");
            for (int lineIdx = 0; !sr.EndOfStream; lineIdx++)
            {
                string line = sr.ReadLine();
                if (line.Length > 0 && int.Parse(line) > 0)
                {
                    scoreList.Add(int.Parse(line));
                }
            }
            sr.Close();
        }
        catch (Exception)
        {
            Debug.Log("SCOREBOARD_ERROR");
        }
        scoreList.Sort();
        for (int rankIdx = 0; rankIdx < scoreList.Count && rankIdx < topScoreList.Length; rankIdx++)
        {
            topScoreList[rankIdx] = scoreList[rankIdx];
        }
    }

    private void LoadBackgroundImage()
    {
        try
        {
            byte[] data = File.ReadAllBytes("GameSettings/Background.jpg");
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(data))
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), 100.0f, 0, SpriteMeshType.Tight);
                backgroundImage.GetComponent<Image>().sprite = sprite;
            }
        }
        catch (Exception)
        {
            Debug.Log("USE_DEFAULT_BACKGROUND");
        }
    }

    void Start()
    {
        LoadBackgroundImage();
        blockNumberSet.Clear();
        stepCount = 0;
        stage = 0;
        NextStage();
        SetupNewCubes();
        UpdateTopScoreList();
    }

    void Update()
    {
        
    }
}
