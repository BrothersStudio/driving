using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoModeSwitch : MonoBehaviour
{
    bool flipped = false;
    public List<Sprite> sprites;

    private void OnMouseDown()
    {
        flipped = !flipped;
        GetComponent<AudioSource>().Play();

        if (flipped)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
    }
}
