using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private float score = 0;
    private int combo = 1;

    public void Restart()
    {
        score = 0;
        combo = 1;
        
        transform.parent.Find("Combo Bar Mask").GetComponent<ComboBar>().Restart();
    }

    private void Update()
    {
        score += Time.deltaTime * combo;
        GetComponent<TMP_Text>().text = $"{score:000000}";
    }

    public void Text()
    {
        combo++;
        transform.parent.Find("Combo Bar Mask").GetComponent<ComboBar>().StartCombo(combo);
    }

    public void ComboOver()
    {
        combo = 1;
    }

    public float GetScore()
    {
        return score;
    }
}
