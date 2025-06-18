using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public GameObject hastePrefab;
    public GameObject strengthPrefab;
    public Transform spawnPoint;

    public Text strengthTextUI;
    public Text scoreTextUI;

    public float monsterInterval = 2f;
    public float strengthInterval = 0.4f;
    public float hasteInterval = 0.2f;

    private bool isSpawning = false;

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            InvokeRepeating(nameof(Spawn), 0f, monsterInterval);
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke(nameof(Spawn));
    }

    public void Spawn()
    {
        Vector3 spawnPos = spawnPoint ? spawnPoint.position : transform.position;
        float roll = Random.value;
        GameObject obj;

        if (roll < hasteInterval)
        {
            obj = Instantiate(hastePrefab, spawnPos + Vector3.right * 2, Quaternion.identity);
            HasteScript haste = obj.GetComponent<HasteScript>();
            if (haste != null)
            {
                haste.speed = GameManager.Instance.speed;
                GameManager.Instance.hasteInstance = haste;
            }
        }
        else if (roll < strengthInterval)
        {
            obj = Instantiate(strengthPrefab, spawnPos + Vector3.right * 2, Quaternion.identity);
            StrengthScript strength = obj.GetComponent<StrengthScript>();
            if (strength != null)
            {
                strength.SetTextReference(strengthTextUI);
                strength.speed = GameManager.Instance.speed;
                GameManager.Instance.strengthInstance = strength;
            }
        }
        else
        {
            obj = Instantiate(monsterPrefab, spawnPos + Vector3.right * 2, Quaternion.identity);
            Monster monsterScript = obj.GetComponent<Monster>();
            if (monsterScript != null)
            {
                monsterScript.SetUITextReferences(strengthTextUI, scoreTextUI);
                monsterScript.speed = GameManager.Instance.speed;
                GameManager.Instance.monsterInstance = monsterScript;
                GameManager.Instance.monsters.Add(monsterScript);
            }
        }
    }
}
