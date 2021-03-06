﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    private int highest_player_score = 0;

    private List<(string, int)> saved_scores = new List<(string, int)>();

    public GameObject score_line;
    public Transform scoreboard;
    public Color user_color;
    private List<GameObject> generated_lines = new List<GameObject>();

    public void Display(int user_score)
    {
        foreach (GameObject line in generated_lines)
        {
            Destroy(line);
        }
        generated_lines.Clear();

        if (user_score > highest_player_score)
        {
            highest_player_score = user_score;
        }

        InitializeFakeScores();
        DrawScores();
        gameObject.SetActive(true);
    }

    private void DrawScores()
    {
        bool player_score_drawn = false;
        foreach ((string, float) score in saved_scores)
        {
            if (highest_player_score > score.Item2 && !player_score_drawn)
            {
                player_score_drawn = true;

                GameObject user_line = Instantiate(score_line, scoreboard);
                string user_text = "you";
                for (int i = 0; i < 41 - 3 - highest_player_score.ToString().Length; i++)
                {
                    user_text += ".";
                }
                user_text += highest_player_score.ToString();
                user_line.GetComponent<TMP_Text>().text = user_text;
                user_line.GetComponent<TMP_Text>().color = user_color;
                generated_lines.Add(user_line);
            }

            GameObject new_line = Instantiate(score_line, scoreboard);
            string line_text = "";
            line_text += score.Item1;
            for (int i = 0; i < 41 - score.Item1.Length - score.Item2.ToString().Length; i++)
            {
                line_text += ".";
            }
            line_text += score.Item2.ToString();
            new_line.GetComponent<TMP_Text>().text = line_text;
            generated_lines.Add(new_line);
        }

        if (!player_score_drawn)
        {
            GameObject user_line = Instantiate(score_line, scoreboard);
            string user_text = "you";
            for (int i = 0; i < 41 - 3 - highest_player_score.ToString().Length; i++)
            {
                user_text += ".";
            }
            user_text += highest_player_score.ToString();
            user_line.GetComponent<TMP_Text>().text = user_text;
            user_line.GetComponent<TMP_Text>().color = user_color;
            generated_lines.Add(user_line);
        }
    }

    void InitializeFakeScores()
    {
        saved_scores.Clear();
        
        saved_scores.Add(("mike", 691));
        saved_scores.Add(("stacey", 590));
        saved_scores.Add(("will", 572));
        saved_scores.Add(("patrick", 453));
        saved_scores.Add(("elizabeth", 400));
        saved_scores.Add(("emily", 382));
        saved_scores.Add(("christopher", 220));
        saved_scores.Add(("nick", 110));
        saved_scores.Add(("barry", 60));
        saved_scores.Add(("kevin", 37));
        saved_scores.Add(("joshua", 20));
    }
}
