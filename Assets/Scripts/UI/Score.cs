using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private int prev_score = 0;
    private float score = 0;
    private int combo = 1;

    private GameController game_con;

    private void Awake()
    {
        game_con = FindObjectOfType<GameController>();
    }

    public void Restart()
    {
        prev_score = 0;
        score = 0;
        combo = 1;
        
        transform.parent.Find("Combo Bar Mask").GetComponent<ComboBar>().Restart();
    }

    private void Update()
    {
        score += Time.deltaTime * combo;
        GetComponent<TMP_Text>().text = $"{score:000000}";
        
        if (prev_score != Mathf.Floor(score) && !game_con.IsGameOver())
        {
            GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
            GetComponent<AudioSource>().Play();
        }
        prev_score = (int)score;
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
