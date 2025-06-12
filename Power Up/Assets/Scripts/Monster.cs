using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public Text StrenthText;
    public Text ScoreText;
    private int score;
    public float speed = 5f;
    public float rightBound = 10f;

    private void Start()
    {
        rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

    }

    public void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x > rightBound)
        {
            Destroy(gameObject);
        }
    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            string strengthConv = StrenthText.text;
            int strength = int.Parse(strengthConv);
            string scoreConv = ScoreText.text;
            int score = int.Parse(scoreConv);
            int multiplier = 1 * strength;
            score += multiplier;
            ScoreText.text = score.ToString();
        }

    }

}
