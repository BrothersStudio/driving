using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private float score = 0;

    private void Reset()
    {
        score = 0;
    }

    private void Update()
    {
        score += Time.deltaTime;
        GetComponent<TMP_Text>().text = $"{score:000000}";
    }

    public void Text()
    {
        score *= 2;
    }
}
