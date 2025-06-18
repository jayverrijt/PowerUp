using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public Text strengthText;
    public Text scoreText;
    private int score;
    public float speed = 2f;
    public float rightBound = 10f;

    private void Start()
    {
        rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x > rightBound)
        {
            DecreaseScore();
            Destroy(gameObject);
        }

    }

     public void OnMouseDown()
     {
       if (!GameManager.Instance.CanClick(gameObject)) return;

       if (strengthText != null && scoreText != null)
       {
           int strength = int.Parse(strengthText.text);
           int currentScore = int.Parse(scoreText.text);
           int multiplier = 1 * strength;
           currentScore += multiplier;
           scoreText.text = currentScore.ToString();
           GameManager gm = GameManager.Instance;
           gm.isGameActive = true;
           Destroy(gameObject);
       }
     }

    public void DecreaseScore()
    {
        int strength = int.Parse(strengthText.text);
        int currentScore = int.Parse(scoreText.text);
        int multiplier = 1 * strength;
        currentScore -= multiplier;
        scoreText.text = currentScore.ToString();
    }

    public void SetUITextReferences(Text strengthUI, Text scoreUI)
    {
        strengthText = strengthUI;
        scoreText = scoreUI;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.monsters.Remove(this);
        }
    }

}