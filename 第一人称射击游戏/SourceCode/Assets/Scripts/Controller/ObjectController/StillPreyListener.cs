using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyListener : MonoBehaviour
{
    private bool hasCollide;
    private AudioSource audioSource;
    private IUserAction action;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<PreyData>().score = 2;
        hasCollide = false;
        action = SSDirector.getInstance().currentSceneController as IUserAction;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollide && collision.gameObject.CompareTag("Arrow"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            hasCollide = true;
            audioSource.PlayOneShot(((FirstSceneController)SSDirector.getInstance().currentSceneController).hitAudio);
            action.Hit(gameObject);
        }
        
    }
}
