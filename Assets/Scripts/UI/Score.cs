using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private float played_sound_for_score = 0;
    private float score = 0;
    private int combo = 1;

    private GameController game_con;

    private void Awake()
    {
        game_con = FindObjectOfType<GameController>();
    }

    public void Restart()
    {
        played_sound_for_score = 0;
        score = 0;
        combo = 1;
        
        transform.parent.Find("Combo Bar Mask").GetComponent<ComboBar>().Restart();
    }

    private void Update()
    {
        score += Time.deltaTime * combo;

        float display_score = Mathf.Floor(score);
        GetComponent<TMP_Text>().text = $"{display_score:000000}";
        
        if ((Mathf.Floor(display_score) % 10 == 0)  && 
            Mathf.Floor(display_score) != played_sound_for_score &&
            !game_con.IsGameOver())
        {
            played_sound_for_score = Mathf.Floor(score);

            GetComponent<AudioSource>().pitch = Random.Range(0.95f, 1.05f);
            GetComponent<AudioSource>().Play();
        }
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
