using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievment : MonoBehaviour
{

    private Button achievmentButton;
    private TMP_Text achievmentText;

    public int number;
    

    public string title;
    public Predicate<object> requirment;

    private bool achieved;

    public Achievment(string title, Predicate<object> requirment)
    {
        this.title = title;
        this.requirment = requirment;
    }

    private void Awake()
    {
        achievmentButton = GetComponent<Button>();
        achievmentText = GetComponentInChildren<TMP_Text>();

        achievmentText.text = title;

        achieved = PlayerPrefs.GetInt("Achievment" +  number, 0) == 1;

    }

    public void UpdateCompletion()
    {
        if(achieved && achievmentButton.interactable == false)
        {
            achievmentButton.interactable = true;
            return;
        }

        if(RequirmentsMet()&&achievmentButton!=null)
        {
            PlayerPrefs.SetInt("Achievment" + number, 1);
            achievmentButton.interactable = true;
            achieved = true;
        }
    }

    private bool RequirmentsMet()
    {
        return requirment.Invoke(null);
    }
}
