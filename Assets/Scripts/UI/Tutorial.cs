using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    bool seen = false;

    public void TextingExplanation()
    {
        GetComponent<TMP_Text>().text = "release mouse, type, press enter";
    }

    public void RemoveTutorial()
    {
        gameObject.SetActive(false);
    }
}
