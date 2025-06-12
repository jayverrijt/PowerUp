using UnityEngine;
using UnityEngine.UI;

public class StrengthScript : MonoBehaviour
{

    public Text StrenthText;
    private int strength = 1;
    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StrenthText.text = strength.ToString();
            strength++;
            StrenthText.text = strength.ToString();
        }
    }
}
