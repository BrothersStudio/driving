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
        saved_scores.Add(("Patrick", 235000000));
        saved_scores.Add(("Joshua", 220500000));
        saved_scores.Add(("Kevin", 195000000));
        saved_scores.Add(("Christopher", 104000000));
        
    }
}
