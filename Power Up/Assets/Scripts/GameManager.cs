using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int nextSpeedMilestone = 10;
    public Text scoreText;
    public int strength = 1;
    public int score = 1;
    public int speed = 2;
    public List<Monster> monsters = new List<Monster>();
    public bool isClickCooldownEnabled = true;
    private bool hasteClickedOnce = false;
    public bool isGameActive = false;
    // UI elements
    public Text mainMenuText;
    public GameObject playButton;
    public GameObject QuitButton;
    public GameObject NewGameButton;
    public GameObject ResumeButton;
    public GameObject MonsterImage;
    public GameObject SpeedText;
    public GameObject Speed;
    public Text strengthTextUI;  // renamed for clarity
    public Text scoreTextUI;     // renamed for clarity
    public GameObject ScoreObject;
    public GameObject StrengthObject;
    public Text RespawnText;
    public Spawner spawner;

    // Cached references to spawned instances
    [HideInInspector] public Monster monsterInstance;
    [HideInInspector] public StrengthScript strengthInstance;
    [HideInInspector] public HasteScript hasteInstance;



    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Application.targetFrameRate = 60;
        MainMenu();
    }

    public void MainMenu()
    {
        mainMenuText.enabled = true;
        playButton.SetActive(true);
        strengthTextUI.enabled = false;
        scoreTextUI.enabled = false;
        ScoreObject.SetActive(false);
        StrengthObject.SetActive(false);
        QuitButton.SetActive(true);
        MonsterImage.SetActive(true);
        ResumeButton.SetActive(false);
        NewGameButton.SetActive(false);
        SpeedText.SetActive(false);
        Speed.SetActive(false);
        RespawnText.gameObject.SetActive(false);
        // You can disable other UI or objects as needed here
    }

    public void Play()
    {
        mainMenuText.enabled = false;
        playButton.SetActive(false);
        strengthTextUI.enabled = true;
        scoreTextUI.enabled = true;
        ScoreObject.SetActive(true);
        StrengthObject.SetActive(true);
        QuitButton.SetActive(false);
        MonsterImage.SetActive(false);
        ResumeButton.SetActive(false);
        NewGameButton.SetActive(false);
        SpeedText.SetActive(true);
        Speed.SetActive(true);
        spawner.StartSpawning();
        RespawnText.gameObject.SetActive(false);

        // Enable or reset other gameplay objects if needed
    }

    public void Pause()
    {
        mainMenuText.enabled = true;
        playButton.SetActive(false);
        strengthTextUI.enabled = false;
        scoreTextUI.enabled = false;
        ScoreObject.SetActive(false);
        StrengthObject.SetActive(false);
        QuitButton.SetActive(false);
        MonsterImage.SetActive(true);
        NewGameButton.SetActive(true);
        ResumeButton.SetActive(true);
        SpeedText.SetActive(false);
        Speed.SetActive(false);
        isGameActive = false;
        spawner.StopSpawning();
        RespawnText.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        mainMenuText.enabled = false;
        playButton.SetActive(false);
        strengthTextUI.enabled = true;
        scoreTextUI.enabled = true;
        ScoreObject.SetActive(true);
        StrengthObject.SetActive(true);
        QuitButton.SetActive(false);
        MonsterImage.SetActive(false);
        ResumeButton.SetActive(false);
        NewGameButton.SetActive(false);
        SpeedText.SetActive(true);
        Speed.SetActive(true);
        spawner.StartSpawning();
        RespawnText.gameObject.SetActive(false);
    }

    public void Reset()
    {
       strength = 1;
       score = 1;
       speed = 2;
       isClickCooldownEnabled = true;
       hasteClickedOnce = false;
       mainMenuText.enabled = false;
       playButton.SetActive(false);
       strengthTextUI.enabled = true;
       scoreTextUI.enabled = true;
       ScoreObject.SetActive(true);
       StrengthObject.SetActive(true);
       QuitButton.SetActive(false);
       MonsterImage.SetActive(false);
       ResumeButton.SetActive(false);
       NewGameButton.SetActive(false);
       SpeedText.SetActive(true);
       Speed.SetActive(true);
       spawner.StartSpawning();
       isGameActive = true;
       ScoreObject.GetComponent<Text>().text = score.ToString();
       StrengthObject.GetComponent<Text>().text = strength.ToString();
       Speed.GetComponent<Text>().text = "1";
       RespawnText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        int currScoreValue;
        if (int.TryParse(ScoreObject.GetComponent<Text>().text, out currScoreValue))
        {
            if (isGameActive && currScoreValue <= 0)
            {
                GameOver();
                return;
            }

            if (currScoreValue >= nextSpeedMilestone)
            {
                speedUp();
                nextSpeedMilestone += 10;
            }
        }
        else
        {
            // Optionally log or handle this if you expect score to always be valid
            Debug.LogWarning("Score text could not be parsed: " + scoreTextUI.text);
        }
    }

    public void speedUp()
    {
        speed++;

        foreach (var m in monsters)
        {
            if (m != null)
                m.speed = speed;
        }

        if (monsterInstance != null) monsterInstance.speed = speed;
        if (strengthInstance != null) strengthInstance.speed = speed;
        if (hasteInstance != null) hasteInstance.speed = speed;

        Speed.GetComponent<Text>().text = speed.ToString();

        Debug.Log("Speed increased to: " + speed);
    }
    public bool CanClick(GameObject obj)
    {
        ClickableTracker tracker = obj.GetComponent<ClickableTracker>();
        if (tracker == null) return true;

        if (!isClickCooldownEnabled) return true;

        if (Time.time - tracker.lastClickTime < 1f)
            return false;

        tracker.lastClickTime = Time.time;
        return true;
    }
    public void HandleHasteClick()
    {
        if (!hasteClickedOnce)
        {
            isClickCooldownEnabled = false;
            hasteClickedOnce = true;
            Debug.Log("Click cooldown disabled!");
        }
        else
        {
            isClickCooldownEnabled = true;
            Debug.Log("Click cooldown permanently re-enabled!");
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        spawner.StopSpawning();
        Debug.Log("Game Over!");
        RespawnText.gameObject.SetActive(true);
        mainMenuText.enabled = true;
        NewGameButton.SetActive(true);
        QuitButton.SetActive(false);
        Speed.SetActive(false);
        SpeedText.SetActive(false);
        ScoreObject.SetActive(false);
        StrengthObject.SetActive(false);
        scoreText.enabled = false;
        strengthTextUI.enabled = false;

    }
}