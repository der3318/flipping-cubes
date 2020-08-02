using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour
{
    private GameSettings settings;
    private bool isActive;
    private GameObject[] cubeList = new GameObject[4];

    void Start()
    {
        settings = GameObject.Find("GameSettings").GetComponent<GameSettings>();
        isActive = true;

        /* check child objects */
        if (transform.childCount != cubeList.Length)
        {
            Debug.Log("INVALID_STATE");
            return;
        }

        /* copy child objects */
        for (int cubeIdx = 0; cubeIdx < transform.childCount; cubeIdx++)
        {
            cubeList[cubeIdx] = transform.GetChild(cubeIdx).gameObject;
        }
    }

    void Update()
    {
        /* return if not active */
        if (!isActive)
        {
            return;
        }

        /* backup while getting the floor and ceiling coordinates of the cubes */
        List<Vector3> backupPositionList = new List<Vector3>();
        int xFloor = Int32.MaxValue, zFloor = Int32.MaxValue, xCeiling = Int32.MinValue, zCeiling = Int32.MinValue;
        for (int cubeIdx = 0; cubeIdx < transform.childCount; cubeIdx++)
        {
            backupPositionList.Add(new Vector3(cubeList[cubeIdx].transform.position.x, cubeList[cubeIdx].transform.position.y, cubeList[cubeIdx].transform.position.z));
            xFloor = Math.Min((int) Math.Floor(cubeList[cubeIdx].transform.position.x), xFloor);
            zFloor = Math.Min((int) Math.Floor(cubeList[cubeIdx].transform.position.z), zFloor);
            xCeiling = Math.Max((int) Math.Ceiling(cubeList[cubeIdx].transform.position.x), xCeiling);
            zCeiling = Math.Max((int) Math.Ceiling(cubeList[cubeIdx].transform.position.z), zCeiling);
        }

        /* rotate the cubes */
        if (Input.GetKeyUp("right"))
        {
            for (int cubeIdx = 0; cubeIdx < gameObject.transform.childCount; cubeIdx++)
            {
                float x = xCeiling + cubeList[cubeIdx].transform.position.y;
                float y = xCeiling - cubeList[cubeIdx].transform.position.x;
                float z = cubeList[cubeIdx].transform.position.z;
                cubeList[cubeIdx].transform.position = new Vector3(x, y, z);
            }
        }
        else if (Input.GetKeyUp("left"))
        {
            for (int cubeIdx = 0; cubeIdx < gameObject.transform.childCount; cubeIdx++)
            {
                float x = xFloor - cubeList[cubeIdx].transform.position.y;
                float y = cubeList[cubeIdx].transform.position.x - xFloor;
                float z = cubeList[cubeIdx].transform.position.z;
                cubeList[cubeIdx].transform.position = new Vector3(x, y, z);
            }
        }
        else if (Input.GetKeyUp("up"))
        {
            for (int cubeIdx = 0; cubeIdx < gameObject.transform.childCount; cubeIdx++)
            {
                float x = cubeList[cubeIdx].transform.position.x;
                float y = zCeiling - cubeList[cubeIdx].transform.position.z;
                float z = zCeiling + cubeList[cubeIdx].transform.position.y;
                cubeList[cubeIdx].transform.position = new Vector3(x, y, z);
            }
        }
        else if (Input.GetKeyUp("down"))
        {
            for (int cubeIdx = 0; cubeIdx < gameObject.transform.childCount; cubeIdx++)
            {
                float x = cubeList[cubeIdx].transform.position.x;
                float y = cubeList[cubeIdx].transform.position.z - zFloor;
                float z = zFloor - cubeList[cubeIdx].transform.position.y;
                cubeList[cubeIdx].transform.position = new Vector3(x, y, z);
            }
        }
        else
        {
            return;
        }

        /* validate and increase count */
        bool isActionValid = true;
        for (int cubeIdx = 0; cubeIdx < gameObject.transform.childCount; cubeIdx++)
        {
            int number = 1 + (int)Math.Floor(cubeList[cubeIdx].transform.position.x) + settings.squareSize * (int)Math.Floor(cubeList[cubeIdx].transform.position.z);
            isActionValid &= cubeList[cubeIdx].transform.position.x >= 0;
            isActionValid &= cubeList[cubeIdx].transform.position.x < settings.squareSize;
            isActionValid &= cubeList[cubeIdx].transform.position.z >= 0;
            isActionValid &= cubeList[cubeIdx].transform.position.z < settings.squareSize;
            isActionValid &= !(settings.blockNumberSet.Contains(number));
        }
        if (isActionValid)
        {
            settings.stepCount += 1;
        }
        else
        {
            for (int cubeIdx = 0; cubeIdx < gameObject.transform.childCount; cubeIdx++)
            {
                cubeList[cubeIdx].transform.position = backupPositionList[cubeIdx];
            }
        }

        /* check stage goal */
        bool areCubesMatched = true;
        for (int cubeIdx = 0; cubeIdx < transform.childCount; cubeIdx++)
        {
            int number = 1 + (int)Math.Floor(cubeList[cubeIdx].transform.position.x) + settings.squareSize * (int)Math.Floor(cubeList[cubeIdx].transform.position.z);
            if (number != settings.targetNumberList[cubeIdx] || cubeList[cubeIdx].transform.position.y > 1f)
            {
                areCubesMatched = false;
            }
        }
        if (areCubesMatched)
        {
            isActive = false;
            for (int cubeIdx = 0; cubeIdx < transform.childCount; cubeIdx++)
            {
                cubeList[cubeIdx].GetComponent<Renderer>().material.color /= 2;
            }
            if (settings.NextStage())
            {
                settings.SetupNewCubes();
            }
            else
            {
                settings.ShowGameOverWindow();
                settings.AppendNewScore();
                settings.UpdateTopScoreList();
            }
        }
    }
}
