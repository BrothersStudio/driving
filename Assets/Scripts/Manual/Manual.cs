using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual : MonoBehaviour
{
    public List<Sprite> pages;
    private int current_page = 0;
    private GameObject forward_page_button;
    private GameObject back_page_button;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = pages[0];

        forward_page_button = transform.Find("FlipPageForward").gameObject;
        forward_page_button.SetActive(true);

        back_page_button = transform.Find("FlipPageBack").gameObject;
        back_page_button.SetActive(false);
    }

    public void PageForward()
    {
        current_page++;
        GetComponent<SpriteRenderer>().sprite = pages[current_page];

        if (current_page == pages.Count - 1)
        {
            forward_page_button.SetActive(false);
        }
        back_page_button.SetActive(true);
    }

    public void PageBack()
    {
        current_page--;
        GetComponent<SpriteRenderer>().sprite = pages[current_page];

        if (current_page == 0)
        {
            back_page_button.SetActive(false);
        }
        forward_page_button.SetActive(true);
    }
}
