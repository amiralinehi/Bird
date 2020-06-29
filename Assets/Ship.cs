using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{

    Rigidbody rigidBody;
    
    AudioSource audioSource;
    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;

    public float ammo = 0;



    public GameObject deathParticles;
    [SerializeField] ParticleSystem mainEngineParticles;


    [SerializeField] float LevelLoadDelay = 2f;

    GameObject prefab;
    public float speed = 50f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();
        prefab = Resources.Load("Bullet") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {

            RespondToThrust();
            RespondToRotate();

            if (ammo > 0 )
            {
                    Shooting();
                    
            }
            
            
        }
    }

    private void Shooting()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(prefab) as GameObject;
            projectile.transform.position = transform.position + gameObject.transform.forward;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = gameObject.transform.forward * speed;

            Physics.IgnoreCollision(projectile.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            ammo--;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;

        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;

            case "Bullet":

                break;

            case "Ammo":

                ammo = 3;
                break;


            case "Finish":
                //load next scene
                StartSuccessSequencess();
                break;

            default:
                //dead
                StartDeathSequencess();
                break;
        }

    }

    void RespondToRotate()
    {

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        rigidBody.freezeRotation = true;// take control manually 

        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(-Vector3.right * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(Vector3.right * rotationThisFrame);
        }

        rigidBody.freezeRotation = true; //resume physics control of rotation

    }


    void RespondToThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            ApplyThrust();

        }
        else

        {

            audioSource.Stop();
            mainEngineParticles.Stop();

        }

    }


    void LoadNextLevel()
    {

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);

    }
    void LoadFirstLevel()
    {

        SceneManager.LoadScene(0);

    }

    void ApplyThrust()
    {

        rigidBody.AddRelativeForce(Vector3.up * mainThrust);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
            mainEngineParticles.Play();

        }

    }
    void StartSuccessSequencess()
    {
        state = State.Transcending;
        audioSource.Stop();
      //  successParticles.Play();
      //  audioSource.PlayOneShot(success);
        Invoke("LoadNextLevel", LevelLoadDelay);


    }


    void StartDeathSequencess()
    {

        state = State.Dying;
        audioSource.Stop();
        //audioSource.PlayOneShot(death);

        GameObject x= (GameObject)Instantiate(deathParticles, transform.position, Quaternion.identity);
        x.GetComponent<ParticleSystem>().Play();
        x.GetComponent<AudioSource>().Play();

        Invoke("LoadFirstLevel", LevelLoadDelay);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ammo")
        {
            ammo = 3;
            Destroy(other.gameObject, 1f);
        }
    }




}
