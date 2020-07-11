using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    private float speed = 20;

    public List<Material> paint_colors;

    private void Start()
    {
        Material paint_chosen = paint_colors[Random.Range(0, paint_colors.Count)];

        List<Material> materials = transform.Find("carrosserie").GetComponent<MeshRenderer>().materials.ToList();
        materials[0] = paint_chosen;
        transform.Find("carrosserie").GetComponent<MeshRenderer>().materials = materials.ToArray();

        materials = transform.Find("Animatable/door_front_L").GetComponent<MeshRenderer>().materials.ToList();
        materials[0] = paint_chosen;
        transform.Find("Animatable/door_front_L").GetComponent<MeshRenderer>().materials = materials.ToArray();

        materials = transform.Find("Animatable/door_front_R").GetComponent<MeshRenderer>().materials.ToList();
        materials[0] = paint_chosen;
        transform.Find("Animatable/door_front_R").GetComponent<MeshRenderer>().materials = materials.ToArray();

        materials = transform.Find("Animatable/hood").GetComponent<MeshRenderer>().materials.ToList();
        materials[0] = paint_chosen;
        transform.Find("Animatable/hood").GetComponent<MeshRenderer>().materials = materials.ToArray();

        materials = transform.Find("Animatable/Trunk").GetComponent<MeshRenderer>().materials.ToList();
        materials[0] = paint_chosen;
        transform.Find("Animatable/Trunk").GetComponent<MeshRenderer>().materials = materials.ToArray();

        materials = transform.Find("Animatable/Trunk/spoiler").GetComponent<MeshRenderer>().materials.ToList();
        materials[0] = paint_chosen;
        transform.Find("Animatable/Trunk/spoiler").GetComponent<MeshRenderer>().materials = materials.ToArray();
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime), Space.World);
        if (transform.position.z < -100)
        {
            Destroy(gameObject);
        }
    }
}
