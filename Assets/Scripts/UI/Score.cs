using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private float score = 0;

    public void Restart()
    {
        score = 0;
    }

    private void Update()
    {
        score += Time.deltaTime;
        GetComponent<TMP_Text>().text = $"{score:000000}";
    }

    public void Text()
    {
        score *= 2;
        transform.parent.Find("Multiplier").gameObject.SetActive(true);
    }

    public float GetScore()
    {
        return score;
    }
}
