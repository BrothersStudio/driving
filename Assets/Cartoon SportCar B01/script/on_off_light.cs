using UnityEngine;
using System.Collections;

public class on_off_light : MonoBehaviour
{

	public Light[] lights;
	public KeyCode keyboard;

    private void Awake()
    {
        foreach (Light light in lights)
        {
            light.enabled = true;
        }
    }

    void Update ()
	{
        /*
		foreach (Light light in lights)
		{
			if (Input.GetKeyDown(keyboard))
			{
				light .enabled = !light .enabled;
			}
		}*/
	}
}

