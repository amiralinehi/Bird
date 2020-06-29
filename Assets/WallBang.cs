using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBang : MonoBehaviour
{

    [SerializeField] GameObject death;
 



    public float hp = 3;



    public GameObject  z;

    void Start()
    {
        z = (GameObject)Instantiate(death, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp == 0)
        {
            
            
            Destroy(gameObject);
            z.GetComponent<ParticleSystem>().Stop();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            hp--;
            z.GetComponent<ParticleSystem>().Play();


        }
    }
}
