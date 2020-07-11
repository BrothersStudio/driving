﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    private Color text_color = Color.white;

    public GameObject game_over_screen;
    public TMP_Text final_score;
    public TMP_Text space_to_restart;
    private bool waiting_for_restart = false;

    public GameObject player;

    public void GameOver()
    {
        if (!waiting_for_restart)
        {
            waiting_for_restart = true;

            player.GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().Play();

            float score = FindObjectOfType<Score>().GetScore();
            game_over_screen.transform.Find("Final Score").GetComponent<TMP_Text>().text = $"{score:000000}";

            game_over_screen.SetActive(true);

            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        for (int i = 0; i < 255; i++)
        {
            text_color.a = i / 255f;
            final_score.color = text_color;
            space_to_restart.color = text_color;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }
    }

    private void Restart()
    {
        waiting_for_restart = false;
        game_over_screen.SetActive(false);

        player.GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().Stop();

        FindObjectOfType<TypingDetection>().Restart();
        FindObjectOfType<Score>().Restart();
        FindObjectOfType<EnemyCarSpawner>().Restart();
        FindObjectOfType<Steering>().Restart();
        FindObjectOfType<CollisionDetection>().Restart();
    }
}