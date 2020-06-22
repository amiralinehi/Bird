using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject prefab;
    public float speed = 50f;

    void Start()
    {
        prefab = Resources.Load("Bullet") as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(prefab) as GameObject;
            projectile.transform.position = transform.position + gameObject.transform.right;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = gameObject.transform.right * speed;

            Physics.IgnoreCollision(projectile.GetComponent<Collider>(),gameObject.GetComponent<Collider>());

        }
    }
}
