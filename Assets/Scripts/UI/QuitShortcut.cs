using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitShortcut : MonoBehaviour
{
    private GameObject quit_text;
    private GameObject quit_bar;

    private float animation_t = 0;
    private GameObject bar_fill;
    private Vector3 empty_pos = new Vector3(-900, 0, 0);

    private void Awake()
    {
        quit_text = transform.Find("Quit Text").gameObject;
        quit_bar = transform.Find("Quit Bar").gameObject;

        bar_fill = transform.Find("Quit Bar/Quit Bar Fill").gameObject;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            animation_t += Time.deltaTime;
            SetUIActive(true);
            
            if (animation_t < 1)
            {
                bar_fill.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(empty_pos, Vector3.zero, SmoothStep(animation_t));
            }
            else
            {
                Debug.Log("QUIT");
                Application.Quit();
            }
        }
        else
        {
            animation_t = 0;
            SetUIActive(false);
        }
    }

    private void SetUIActive(bool active)
    {
        quit_text.SetActive(active);
        quit_bar.SetActive(active);
    }

    private float SmoothStep(float t)
    {
        return 3 * t * t - 2 * t * t * t;
    }
}
