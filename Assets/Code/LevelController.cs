using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int sequenceBlinkCount;
    public int minSequencePlace;
    public int maxSequencePlace;

    public float blinkTime;

    [SerializeField] GameObject sequenceMainObject;
    [SerializeField] Button startButton;
    private TMP_Text startButtonText;
    [SerializeField] TMP_Text levelStateText;
    [SerializeField] TMP_Text levelName;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject gameMenu;
    [SerializeField] GameObject loseMenu;

    [SerializeField] Sprite blinkedSequenceSprite;
     
    private Button[] sequencePlacesButtons;
    private Image[] sequencePlacesImages;

    private int[] blinkPlaces;

    private int buttonClicks = 0;
    private int correctClicks = 0;

    public AudioClip clip, win, lose;

    public bool isAchiv;
    private void Awake()
    {
        levelName.text = SceneManager.GetActiveScene().name.ToString();
        sequencePlacesButtons = sequenceMainObject.GetComponentsInChildren<Button>();
        sequencePlacesImages = sequenceMainObject.GetComponentsInChildren<Image>();
        startButtonText = startButton.GetComponentInChildren<TMP_Text>();
        startButton.onClick.AddListener(Blink);
        foreach (Button button in sequencePlacesButtons)
        {
            button.interactable = false;
        }
    }

    private IEnumerator BlinkCoroutine()
    {

        buttonClicks = 0;
        correctClicks = 0;
        startButton.interactable = false;

        int[] generatedBlinkPlaces = new int[sequenceBlinkCount];


        for (int i = 0; i < generatedBlinkPlaces.Length; i++)
        {
            generatedBlinkPlaces[i] = UnityEngine.Random.Range(minSequencePlace, maxSequencePlace);
        }

        blinkPlaces = generatedBlinkPlaces;

        for (int j = 0; j < generatedBlinkPlaces.Length; j++)
        {
            yield return new WaitForSeconds(blinkTime);
            for (int i = 0; i <= sequencePlacesButtons.Length; i++)
            {



                if (generatedBlinkPlaces[j] == i)
                {
                    Sprite lastSprite = sequencePlacesImages[i - 1].sprite;

                    sequencePlacesImages[i - 1].sprite = blinkedSequenceSprite;
                    AudioManager.Instance.PlaySound(clip);
                    yield return new WaitForSeconds(blinkTime);
                    sequencePlacesImages[i - 1].sprite = lastSprite;



                }

            }
        }
        

        

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(CheckSequence);
        
        
        startButtonText.text = "Done";
        
        

        levelStateText.text = "Repeat the Sequence";

        foreach (Button button in sequencePlacesButtons)
        {
            button.interactable = true;
        }
    }

    public void CheckChoice(int buttonId)
    {
        

        

        if (buttonId == blinkPlaces[buttonClicks])
        {
            correctClicks++;
            
        }


        buttonClicks++;

        if (buttonClicks == sequenceBlinkCount)
        {
            startButton.interactable = true;

            foreach (Button button in sequencePlacesButtons)
            {
                button.interactable = false;
            }
        }
    }

    

    public void CheckSequence()
    {
        if (correctClicks >= buttonClicks)
        {
            Win();
        }
        else
        {
            Lose();
        }
    }

    private void Win()
    {
        UnlockNextLevel();

        if(RankedMode.isInRankedMode)
        {
            RankedMode.currentSessionLevelsPassed++;
        }

        gameMenu.SetActive(false);
        winMenu.SetActive(true);
        if (isAchiv)
        {
            var achiv = PlayerPrefs.GetInt("test");
            achiv += 50;
            PlayerPrefs.SetInt("test", achiv);
            PlayerPrefs.Save();
        }

        AudioManager.Instance.PlaySound(win);
        
    }
    
    private void Lose()
    {
        RankedMode.statsDisplayed = false;
        if(RankedMode.isInRankedMode)
        {
            RankedMode.EnableRankedMode(false);
            RankedMode.SaveStats();
        }

        gameMenu.SetActive(false);
        loseMenu.SetActive(true);
        AudioManager.Instance.PlaySound(lose);
    }


    public void Blink()
    {
        StartCoroutine(BlinkCoroutine());
    }

    private void UnlockNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("MaxReachedLevel"))
        {
            PlayerPrefs.SetInt("MaxReachedLevel", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}

