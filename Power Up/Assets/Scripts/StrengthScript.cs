using UnityEngine;
using UnityEngine.UI;

public class StrengthScript : MonoBehaviour
{
    public Text strengthText; // Should be assigned to StrengthObject's Text
    public float speed = 2f;
    public float rightBound = 10f;

    private void Start()
    {
        rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    }

    private void OnMouseDown()
    {
        if (!GameManager.Instance.CanClick(gameObject)) return;

        int strength = strengthText != null ? int.Parse(strengthText.text) : 1;
        strength++;
        if (strengthText != null)
        {
            strengthText.text = strength.ToString();
        }

        GameManager.Instance.isGameActive = true;
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        GameManager gm = GameManager.Instance;
        if (gm.score <= 0)
        {
            gm.score = 1;
        }

    }

    public void SetTextReference(Text textRef)
    {
        strengthText = textRef;
    }
}