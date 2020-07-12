using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboBar : MonoBehaviour
{
    private GameObject bar;
    private float decrease_speed = 0.025f;
    private bool animating = false;

    public void Restart()
    {
        animating = false;

        transform.parent.Find("Multiplier").GetComponent<TMP_Text>().text = "";
        transform.parent.Find("Multiplier").GetComponent<TMP_Text>().enabled = false;

        bar.GetComponent<RectTransform>().anchoredPosition = new Vector3(-2, 0);
    }

    private void Start()
    {
        bar = transform.Find("Combo Bar").gameObject;
    }

    public void StartCombo(int combo)
    {
        animating = true;
        bar.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        transform.parent.Find("Multiplier").GetComponent<TMP_Text>().text = combo.ToString() + "x";
        transform.parent.Find("Multiplier").GetComponent<TMP_Text>().enabled = true;
    }

    private void Update()
    {
        if (animating)
        {
            Vector3 current_pos = bar.GetComponent<RectTransform>().anchoredPosition;
            current_pos.x -= Time.deltaTime * decrease_speed;
            bar.GetComponent<RectTransform>().anchoredPosition = current_pos;
            if (current_pos.x < -1.15)
            {
                animating = false;

                FindObjectOfType<Score>().ComboOver();
                transform.parent.parent.GetComponent<AudioSource>().Play();

                transform.parent.Find("Multiplier").GetComponent<TMP_Text>().text = "";
                transform.parent.Find("Multiplier").GetComponent<TMP_Text>().enabled = false;
            }
        }
    }
}
