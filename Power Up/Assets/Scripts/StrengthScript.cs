using UnityEngine;
using UnityEngine.UI;

public class StrengthScript : MonoBehaviour
{
    public Text strengthText; // (✔ fixed typo from "StrenthText")
    private int strength = 1;
    public float speed = 2f;
    public float rightBound = 10f;

    private void Start()
    {
        rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        UpdateText();
    }

    private void UpdateText()
    {
        if (strengthText != null)
        {
            strengthText.text = strength.ToString();
        }
        else
        {
            Debug.LogWarning("StrengthText is not assigned!");
        }
    }

    private void OnMouseDown()
    {
        strength++;
        UpdateText();
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x > rightBound)
        {
            // Destroy(gameObject); // optional
        }
    }

    public void SetTextReference(Text textRef)
    {
        strengthText = textRef;
        UpdateText();
    }
}