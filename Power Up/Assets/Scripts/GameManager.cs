using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    public int strength = 1;
    public int score = 0;

    // Launching the Main Menu
    public Text mainMenuText;
    public GameObject playButton;
    public Text StrenghText;
    public Text ScoreText;
    public GameObject ScoreObject;
    public GameObject StrengthObject;

    public void Awake()
    {
        Application.targetFrameRate = 60;
        MainMenu();
    }

    public void MainMenu()
    {
        mainMenuText.enabled = true;
        playButton.SetActive(true);
        StrenghText.enabled = false;
        ScoreText.enabled = false;
        ScoreObject.SetActive(false);
        StrengthObject.SetActive(false);
    }

    public void Play()
    {
        mainMenuText.enabled = false;
        playButton.SetActive(false);
        StrenghText.enabled = true;
        ScoreText.enabled = true;
        ScoreObject.SetActive(true);
        StrengthObject.SetActive(true);
    }

    public void Pause()
    {
        mainMenuText.enabled = true;
        playButton.SetActive(true);
        StrenghText.enabled = false;
        ScoreText.enabled = false;
        ScoreObject.SetActive(false);
        StrengthObject.SetActive(false);

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
}
