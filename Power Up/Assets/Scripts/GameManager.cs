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
    public int score = 0;
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
    public Spawner spawner;

    // Cached references to spawned instances
    [HideInInspector] public Monster monsterInstance;
    [HideInInspector] public StrengthScript strengthInstance;
    [HideInInspector] public HasteScript hasteInstance;
    [HideInInspector] public List<Monster> monsters = new List<Monster>();


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
        spawner.StopSpawning();
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
    }

    public void Reset()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            speedUp();
        }

        int scoreUpdate = int.Parse(ScoreObject.GetComponent<Text>().text);
        if (scoreUpdate >= nextSpeedMilestone)
        {
            speedUp();
            nextSpeedMilestone += 10;
        }


    }

    public void speedUp()
    {
        try
        {
            foreach (var monster in monsters)
            {
                if (monster != null) monster.speed++;
            }

            if (strengthInstance != null) strengthInstance.speed++;
            if (hasteInstance != null) hasteInstance.speed++;

            int currSpeed = int.Parse(Speed.GetComponent<Text>().text);
            Speed.GetComponent<Text>().text = (currSpeed + 1).ToString();
        }
        catch (Exception e)
        {
            Debug.LogError("SpeedUp error: " + e.Message);
        }
    }

}