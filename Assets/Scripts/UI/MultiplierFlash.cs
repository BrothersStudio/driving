using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultiplierFlash : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<TMP_Text>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        GetComponent<TMP_Text>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<TMP_Text>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        GetComponent<TMP_Text>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
