using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HasteScript : MonoBehaviour
{
    public float speed = 2f;
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
      if (!GameManager.Instance.CanClick(gameObject)) return;

      GameManager.Instance.HandleHasteClick();
      GameManager gm = GameManager.Instance;
      gm.isGameActive = true;
      Destroy(gameObject);
  }

}
