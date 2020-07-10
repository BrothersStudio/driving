using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPage : MonoBehaviour
{
    public bool forward;

    private void OnMouseDown()
    {
        if (forward)
        {
            GetComponentInParent<Manual>().PageForward();
        }
        else
        {
            GetComponentInParent<Manual>().PageBack();
        }
    }
}
