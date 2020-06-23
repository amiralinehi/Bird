using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    

    [SerializeField] float mainThrust = 1f;
    [SerializeField] float rcsThrust = 100f;

    void Start()
    {
        
    }

   
    void Update()
    {
        VerticalMoving();

        HorizontalMoving();

    }

    private void HorizontalMoving()
    {
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        GetComponent<Rigidbody>().freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(-Vector3.right * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(Vector3.right * rotationThisFrame);
        }

        GetComponent<Rigidbody>().freezeRotation = true;
    }




    private void VerticalMoving()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * mainThrust);

        }
    }
}
