using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    private float latest_player_score;
    private float highest_player_score;

    private string player_name = "You";

    private List<(string,float)> saved_scores = new List<(string, float)>();

    private void Start() 
    {
        InitializeFakeScores();    
    }

    void UpdateScores(float new_score)
    {
        latest_player_score = new_score;
        if(new_score > highest_player_score)
        {
            highest_player_score = new_score;
        }
        saved_scores.Add((player_name, new_score));
    }

    void InitializeFakeScores()
    {
        saved_scores.Add(("Patrick", 1));
        saved_scores.Add(("Joshua", 2));
        saved_scores.Add(("Kevin", 3));
        saved_scores.Add(("Christopher", 4));
        saved_scores.Add(("Stacey", 5));
        saved_scores.Add(("Elizabeth", 6));
        saved_scores.Add(("Barry", 7));
        saved_scores.Add(("Emily", 8));
    }

    
}
