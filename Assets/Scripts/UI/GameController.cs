using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    private Color text_color = Color.white;

    private int user_score;
    public Scoreboard score_screen;
    public GameObject game_over_screen;
    public TMP_Text final_score;
    public TMP_Text space_to_restart;

    private float death_time = 0;
    private float typing_buffer = 1;

    private bool waiting_for_restart = false;
    private bool viewing_scores = false;

    public GameObject player;

    public void StartGame()
    {
        FindObjectOfType<EnemyCarSpawner>().StartGame();
        FindObjectOfType<TypingDetection>().StartGame();
    }

    public bool IsGameOver()
    {
        return waiting_for_restart;
    }

    public void GameOver()
    {
        if (!waiting_for_restart)
        {
            death_time = Time.timeSinceLevelLoad;

            score_screen.gameObject.SetActive(false);
            final_score.gameObject.SetActive(true);
            space_to_restart.gameObject.SetActive(true);

            // Stop other sounds
            FindObjectOfType<MusicController>().Stop();
            player.GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().Play();

            user_score = FindObjectOfType<Score>().GetScore();
            game_over_screen.transform.Find("Final Score").GetComponent<TMP_Text>().text = $"{user_score:000000}";

            game_over_screen.SetActive(true);

            StartCoroutine(FadeIn());

            Invoke(nameof(WaitForRestart), 0.5f);
        }
    }

    public void WaitForRestart()
    {
        waiting_for_restart = true;
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
        if (Input.GetKeyDown(KeyCode.Space) && 
            waiting_for_restart && 
            !viewing_scores &&
            Time.timeSinceLevelLoad > death_time + typing_buffer)
        {
            DisplayScores(user_score);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && 
            waiting_for_restart && 
            viewing_scores)
        {
            Restart();
        }
    }

    private void DisplayScores(int user_score)
    {
        viewing_scores = true;

        final_score.gameObject.SetActive(false);
        space_to_restart.gameObject.SetActive(false);

        score_screen.Display(user_score);
    }

    private void Restart()
    {
        waiting_for_restart = false;
        viewing_scores = false;
        game_over_screen.SetActive(false);

        FindObjectOfType<MusicController>().Play();
        player.GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().Stop();

        FindObjectOfType<TypingDetection>().Restart();
        FindObjectOfType<Score>().Restart();
        FindObjectOfType<EnemyCarSpawner>().Restart();
        FindObjectOfType<ScenerySpawner>().Restart();
        FindObjectOfType<Steering>().Restart();
        FindObjectOfType<CollisionDetection>().Restart();
        FindObjectOfType<TitleDisappear>().Restart();
    }
}
