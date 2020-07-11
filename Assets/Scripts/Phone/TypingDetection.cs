﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingDetection : MonoBehaviour
{
    public TMP_Text incoming_text;
    public TMP_Text outgoing_text;

    private (string, string) incoming_match_string = ("Hey man, what's your address again?", "It's 2314 pineapple lane");
    private string char_to_match;
    private int match_index;
    private bool done_message = false;

    private void Start()
    {
        NewIncomingString();
    }

    private void NewIncomingString()
    {
        done_message = false;
        match_index = 0;
        HighlightUpToInd(match_index);
    }

    private void HighlightUpToInd(int ind)
    {
        if (ind < incoming_match_string.Item2.Length + 1)
        {
            string building_string = "";
            for (int i = 0; i < ind; i++)
            {
                building_string += incoming_match_string.Item2[i];
            }
            building_string += "<color=#968482>";
            for (int i = ind; i < incoming_match_string.Item2.Length; i++)
            {
                building_string += incoming_match_string.Item2[i];
            }
            building_string += "</color>";
            outgoing_text.text = building_string;

            if (ind < incoming_match_string.Item2.Length)
            {
                char_to_match = incoming_match_string.Item2[ind].ToString();
            }
            else
            {
                char_to_match = "return";
                done_message = true;
            }
        }
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && !Input.GetMouseButton(0))
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
            code = code.Replace("alpha", "");
            Debug.Log(code);

            if (code == char_to_match.ToLower())
            {
                match_index++;
                HighlightUpToInd(match_index);
            }
        }
    }
}