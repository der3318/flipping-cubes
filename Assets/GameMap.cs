using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    public GameObject planeBlack;
    public GameObject planeWhite;
    public GameObject planeGray;
    public GameObject[] targetPlaneList = new GameObject[4];
    public TextMeshPro textBlack;
    public TextMeshPro textWhite;

    private GameSettings settings;

    public void ResetAndUpdateMap()
    {
        /* clean up */
        for (int childIdx = 0; childIdx < transform.childCount; childIdx++)
        {
            GameObject.Destroy(transform.GetChild(childIdx).gameObject);
        }

        /* check target list between map and settings */
        if (settings.targetNumberList.Length != targetPlaneList.Length)
        {
            Debug.Log("INVALID_STATE");
            return;
        }

        /* generate new map */
        for (int x = 0; x < settings.squareSize; x++)
        {
            for (int z = 0; z < settings.squareSize; z++)
            {
                GameObject planeToUse = (x + z) % 2 != 0 ? planeBlack : planeWhite;
                TextMeshPro textToUse = (x + z) % 2 != 0 ? textWhite : textBlack;
                int number = 1 + x + settings.squareSize * z;
                bool isMatchedToTarget = false;
                for (int targetIdx = 0; targetIdx < settings.targetNumberList.Length; targetIdx++)
                {
                    if (number == settings.targetNumberList[targetIdx])
                    {
                        var planeInstance = Instantiate(targetPlaneList[targetIdx]);
                        planeInstance.transform.SetParent(transform);
                        planeInstance.transform.localPosition = new Vector3(x + 0.5f, 0.01f, z + 0.5f);
                        planeToUse = planeGray;
                        isMatchedToTarget = true;
                    }
                }
                if (!isMatchedToTarget)
                {
                    var textInstance = Instantiate(textToUse);
                    textInstance.SetText(number.ToString());
                    textInstance.transform.SetParent(transform);
                    textInstance.transform.localPosition = new Vector3(x + 0.5f, 0.01f, z + 0.5f);
                }
                var newPlane = Instantiate(planeToUse);
                newPlane.transform.parent = transform;
                newPlane.transform.localPosition = new Vector3(x + 0.5f, 0, z + 0.5f);
            }
        }
    }

    void Start()
    {
        settings = GameObject.Find("GameSettings").GetComponent<GameSettings>();
    }

    void Update()
    {
        
    }
}
