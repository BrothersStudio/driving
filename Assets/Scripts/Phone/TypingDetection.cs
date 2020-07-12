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

    public void Restart()
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //NewTextArrives();
        }
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
            else if (code == "slash")
            {
                code = "?";
            }
            code = code.Replace("alpha", "");
            code = code.Replace("keypad", "");
            //Debug.Log(code);

            if (code == char_to_match.ToLower())
            {
                if (char_to_match == "return")
                {
                    outgoing_text.gameObject.SetActive(false);
                    sent_text.transform.parent.gameObject.SetActive(true);
                    sent_text.text = current_match_string.Item2;

                    FindObjectOfType<Score>().Text();

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

        all_texts.Add(("hey man, what's your address again?", "it's 2814 north croskey lane"));
        all_texts.Add(("you coming to the party later?", "idk, any girls coming?"));
        all_texts.Add(("bro, you slept with my girlfriend??", "you weren't dating that long chill"));
        all_texts.Add(("dude, what's the wifi password?", "fr33k7d33k7"));
        all_texts.Add(("you good to drive after all those shots?", "haha yeah i drive better drunk"));
        all_texts.Add(("can you pick up some chips on the way?", "only if you pay me back this time"));
        all_texts.Add(("Your dad and I are worried about you", "whatever"));
        all_texts.Add(("I really had fun last night :)", "no offence but i didn't, sorry"));
        all_texts.Add(("are you really too busy to respond??", "nah just driving, what's up?"));
        all_texts.Add(("where's mike live?", "he's at 8301 scottfield rd now"));
        all_texts.Add(("what do you want from applebees?", "pick me up some wings?"));
        all_texts.Add(("what's the name of that show?", "tiger king, man you gotta watch"));
        all_texts.Add(("Charges remaining on your U-haul order...", "STOP"));
        all_texts.Add(("Whats your dogs name?","pippinpaddleopsicopolis"));
        all_texts.Add(("did you take the car?","don't worry about it man"));
        all_texts.Add(("whats the flavor of juice grandma likes?","mango watermelon kiwi banana"));
        all_texts.Add(("Whats the code for the shed lock","it's 9261"));

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
            AddTexts();
            return GetNewText();
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