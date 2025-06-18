using UnityEngine;
using UnityEngine.UI;

public class StrengthScript : MonoBehaviour
{
    public Text strengthText;
    private int strength = 1;
    public float speed = 2f;
    public float rightBound = 10f;

    private void Start()
    {
        rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
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
       if (!GameManager.Instance.CanClick(gameObject)) return;
       strength = strengthText != null ? int.Parse(strengthText.text) : 1;
       strength++;
       UpdateText();
       GameManager gm = GameManager.Instance;
       gm.isGameActive = true;
       Destroy(gameObject);
   }


    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

    }

    public void SetTextReference(Text textRef)
    {
        strengthText = textRef;
    }
}