using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;

            case "Finish":

                Invoke("LoadNextScene", .25f);
                break;

            default:
                //dead
                Invoke("LoadFirtScene", .25f);
                break;
        }
    }

    void LoadFirtScene()
    {
        SceneManager.LoadScene(0);
    }

    void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        /*if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }*/

        SceneManager.LoadScene(nextScene);
    }



}
