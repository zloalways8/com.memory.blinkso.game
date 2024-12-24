using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;
using TMPro;

public class RankedMode : MonoBehaviour
{
    [SerializeField] GameObject textObjects;
    [SerializeField] TMP_Text textPrefab;

    public static RankedMode Instance;
    public static bool isInRankedMode = false;
    public static int currentRankedSessionNumber = 1;
    public static int currentSessionLevelsPassed = 0;

    public static bool statsDisplayed = false;

    private void Start()
    {
        currentRankedSessionNumber = PlayerPrefs.GetInt("RankedSessionsPassed", 1);

        Instance = this;
    }

    public static void EnableRankedMode(bool enable)
    {
        isInRankedMode = enable;
    }

    public static void SaveStats()
    {
        PlayerPrefs.SetInt("RankedSessionsPassed", currentRankedSessionNumber + 1);
        PlayerPrefs.SetInt("RankedSession" + currentRankedSessionNumber + "LevelsPassed", currentSessionLevelsPassed);
        PlayerPrefs.SetString("RankedSession" + currentRankedSessionNumber + "Data", DateTime.Now.Date.ToString("dd/MM/yyyy"));

        currentSessionLevelsPassed = 0;


        statsDisplayed = false;

    }

    public void DisplayStats()
    {


        if (!statsDisplayed && PlayerPrefs.GetInt("RankedSessionsPassed") >= 1)
        {
            statsDisplayed = true;
            for (int i = 1; i < PlayerPrefs.GetInt("RankedSessionsPassed", 1); i++)
            {
                TMP_Text statsText = Instantiate(textPrefab, textObjects.transform);
                statsText.text = i.ToString() + ". " + PlayerPrefs.GetString("RankedSession" + i + "Data", DateTime.Now.Date.ToString("dd/MM/yyyy")) + "\t" + PlayerPrefs.GetInt("RankedSession" + i + "LevelsPassed", currentSessionLevelsPassed).ToString() + " levels";

            }
        }
        
        
    }


}
