using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System.Threading;

public class TypingDetection : MonoBehaviour
{
    public Sprite blank_phone;
    public Sprite phone_messages;

    public TMP_Text incoming_text;
    public TMP_Text outgoing_text;
    public TMP_Text sent_text;

    private (string, string) current_match_string;
    private List<(string, string)> all_texts = new List<(string, string)>();
    private string char_to_match;
    private int match_index;
    private bool done_message = true;

    public AudioClip typing_sfx;
    public AudioClip send_sfx;
    public AudioClip receive_sfx;

    private void Reset()
    {
        done_message = true;
        GetComponent<SpriteRenderer>().sprite = blank_phone;
        CancelInvoke();

        Start();
    }

    private void Start()
    {
        AddTexts();
        NewTextArrives();
    }

    private void NewTextArrives()
    {
        NewIncomingString(GetNewText());
    }

    private void NewIncomingString((string, string) new_message)
    {
        if (new_message.Item1 != null)
        {
            done_message = false;
            match_index = 0;
            current_match_string = new_message;

            GetComponent<SpriteRenderer>().sprite = phone_messages;
            incoming_text.text = new_message.Item1;
            incoming_text.transform.parent.gameObject.SetActive(true);
            outgoing_text.gameObject.SetActive(true);
            sent_text.transform.parent.gameObject.SetActive(false);

            HighlightUpToInd(match_index);

            // Receive message sfx
            GetComponent<AudioSource>().clip = receive_sfx;
            GetComponent<AudioSource>().volume = 1f;
            GetComponent<AudioSource>().Play();
        }
    }

    private void HighlightUpToInd(int ind)
    {
        if (ind < current_match_string.Item2.Length + 1)
        {
            string building_string = "";
            for (int i = 0; i < ind; i++)
            {
                building_string += current_match_string.Item2[i];
            }
            building_string += "<color=#968482>";
            for (int i = ind; i < current_match_string.Item2.Length; i++)
            {
                building_string += current_match_string.Item2[i];
            }
            building_string += "</color>";
            outgoing_text.text = building_string;

            if (ind < current_match_string.Item2.Length)
            {
                char_to_match = current_match_string.Item2[ind].ToString();
            }
            else
            {
                char_to_match = "return";
            }
        }
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && !Input.GetMouseButton(0) && !done_message)
        {
            // Some processing
            string code = e.keyCode.ToString().ToLower();
            if (code == "quote")
            {
                code = "'";
            }
            else if (code == "space")
            {
                code = " ";
            }
            else if (code == "comma")
            {
                code = ",";
            }
            code = code.Replace("alpha", "");
            code = code.Replace("keypad", "");

            if (code == char_to_match.ToLower())
            {
                if (char_to_match == "return")
                {
                    outgoing_text.gameObject.SetActive(false);
                    sent_text.transform.parent.gameObject.SetActive(true);
                    sent_text.text = current_match_string.Item2;

                    // Send message sfx
                    GetComponent<AudioSource>().clip = send_sfx;
                    GetComponent<AudioSource>().volume = 1f;
                    GetComponent<AudioSource>().Play();

                    done_message = true;
                    Invoke("NewTextArrives", 3);
                }
                else
                {
                    match_index++;
                    HighlightUpToInd(match_index);

                    // Typing sfx
                    GetComponent<AudioSource>().clip = typing_sfx;
                    GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.99f, 1.01f);
                    GetComponent<AudioSource>().volume = 0.5f;
                    GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    private void AddTexts()
    {
        all_texts.Clear();

        all_texts.Add(("Hey man, what's your address again?", "It's 2314 pineapple lane"));
        all_texts.Add(("You coming to the party tonight?", "Idk, guess so, lol"));
        all_texts.Add(("Dude, what's the wifi password?", "fr33kyd33ky"));

        all_texts.Shuffle();
    }

    private (string, string) GetNewText()
    {
        if (all_texts.Count > 0)
        {
            (string, string) new_text = all_texts[0];
            all_texts.RemoveAt(0);
            return new_text;
        }
        else
        {
            return (null, null);
        }
    }
}

public static class ThreadSafeRandom
{
    [ThreadStatic] private static System.Random Local;

    public static System.Random ThisThreadsRandom
    {
        get { return Local ?? (Local = new System.Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
    }
}

static class MyExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}