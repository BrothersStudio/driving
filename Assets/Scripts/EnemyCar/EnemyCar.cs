using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    private float speed;

    public List<Material> paint_colors;

    private void Start()
    {
        speed = Random.Range(15.0f, 30.0f);
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

    private void FixedUpdate() 
    {
        PreventCollision();    
    }

    public float getSpeed()
    {
        return speed;
    }

    private void PreventCollision()
    {
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z), new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z + 10f));
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 10f)
            && hit.collider.tag == "EnemyCar")
        {
            float colliderSpeed = hit.collider.GetComponent<EnemyCar>().getSpeed();
            if(colliderSpeed != this.speed)
            {
                this.speed = colliderSpeed;
            }
        }
    }
}
