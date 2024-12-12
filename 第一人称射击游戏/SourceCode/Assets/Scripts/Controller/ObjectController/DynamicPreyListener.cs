using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class DynamicPreyListener : MonoBehaviour
{
    private IActionManager actionManager;
    private Rigidbody rb;
    private Animator animator;
    private PreyData preyData;
    public bool hasCollide;
    public float timer;
    public bool waitRun;
    private bool isRun;
    public Vector3 vel;   // debug
    public Vector3 transfo;

    private AudioSource audioSource;
    private IUserAction action;

    void Start()
    {
        
        actionManager = PhysicsActionManager.GetInstance();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        preyData = GetComponent<PreyData>();
        preyData.velocity = gameObject.transform.forward;
        preyData.velocity.z = 0f;
        preyData.score = 3;
        hasCollide = false;
        timer = 0f;
        waitRun = true;
        isRun = false;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ((FirstSceneController)SSDirector.getInstance().currentSceneController).moveAudio;
        audioSource.loop = true;
        audioSource.Play();

        action = SSDirector.getInstance().currentSceneController as IUserAction;
    }
    public void Run()
    {
        transform.Rotate(0f, 180f, 0f);
        actionManager.PlayObject(gameObject, preyData.speed * gameObject.transform.forward.normalized);
        
        waitRun = false;   // 跑步
        
    }

    void Update()
    {
        if (!hasCollide && !waitRun)    // 跑步阶段
        {
            if (timer > 3f)
            {
                waitRun = true;  // 进入等待阶段
                timer = 0f;
                animator.SetTrigger("Stop");
            }
            timer += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollide && collision.gameObject.CompareTag("Arrow"))
        {
            audioSource.PlayOneShot(((FirstSceneController)SSDirector.getInstance().currentSceneController).hitAudio);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            animator.SetTrigger("Die");
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            hasCollide = true;

            action.Hit(gameObject);
        }
    }
}
