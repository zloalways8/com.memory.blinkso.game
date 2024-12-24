using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AchievmentManager : MonoBehaviour
{
    [SerializeField] List<Achievment> achievments = new List<Achievment>();

    public int test;

    private void Start()
    {
        test = PlayerPrefs.GetInt("test");
        InitializeAchiements();   
    }

    private void InitializeAchiements()
    {
        achievments[0].requirment = (object o) => test >= 50;
        achievments[1].requirment = (object o) => test >= 100;
        achievments[2].requirment = (object o) => test >= 150;
        achievments[3].requirment = (object o) => test >= 200;
        achievments[4].requirment = (object o) => test >= 350;
        achievments[5].requirment = (object o) => test >= 400;
        achievments[6].requirment = (object o) => test >= 450;
    }
    private void Update()
    {
        CheckAchievmentCompletion();
        
    }

    private void CheckAchievmentCompletion()
    {
        if(achievments == null)
        {
            return;
        }

        foreach(Achievment achievment in achievments)
        {
            achievment.UpdateCompletion();
        }
    }
}
