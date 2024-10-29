using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneController : MonoBehaviour, ISceneController, IUserAction, IEndListener {

	public CCActionManager actionManager { get; set;}
	public GameObject priest, devil, boatObject, background;
	public Characters characters;
	public Boat boat;
	private GameObject clouds, winPrefabs, losePrefabs;
	public Texture restart, victory, lose;
	public AudioSource audioSource;
	public AudioClip winAudio, loseAudio, mainAudio, clickAudio;
	private float debugTimer = 0.0f;
    public bool over; // 游戏结束，不再接收动作

    // the first scripts
    void Awake () {
		SSDirector director = SSDirector.getInstance ();
		director.setFPS (60);
		director.currentSceneController = this;
		director.currentSceneController.LoadResources ();
		// 初始化
		initialize();
	}
	 
	// loading resources for first scence
	public void LoadResources () {
		priest = Resources.Load("Prefabs/Priest") as GameObject;
        devil = Resources.Load("Prefabs/devil") as GameObject;
        boatObject = Resources.Load("Prefabs/boat") as GameObject;
		background = Resources.Load("Prefabs/backgroundPrefabs") as GameObject;
		clouds = Resources.Load("Prefabs/Clouds") as GameObject;
        winPrefabs = Resources.Load("Prefabs/winPrefabs") as GameObject;
        losePrefabs = Resources.Load("Prefabs/losePrefabs") as GameObject;

        restart = Resources.Load("Textures/return") as Texture;
        victory = Resources.Load("Textures/return") as Texture;
        lose = Resources.Load("Textures/return") as Texture;

		winAudio = Resources.Load("Arts/winSound") as AudioClip;
        loseAudio = Resources.Load("Arts/loseSound") as AudioClip;
        mainAudio = Resources.Load("Arts/mainSound") as AudioClip;
		clickAudio = Resources.Load("Arts/click") as AudioClip;
    }

    public void initialize()
    {
		over = true;

		new Position();
		new Referee();

		characters = new Characters(priest, devil);
		boat = new Boat(boatObject); 
		background = Instantiate(background);
		background.transform.position = Position.bgPosition;

		clouds = Instantiate(clouds);

		Camera mainCamera = Camera.main;
		mainCamera.transform.position = Position.cameraPosition;
		mainCamera.transform.eulerAngles = Position.cameraRotation;

		CCActionManager.isActionActive = true;  // 允许移动

        audioSource = background.AddComponent<AudioSource>();
		PlaySound();

		// 给出引导的建议
		gameObject.AddComponent<StartGUI>();

		
    }

    public void Pause ()
	{
        over = true; // 禁止用户操作
	}

	public void Resume ()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public void OnEnd(bool WinLose)
	{
		if(WinLose)
		{
			GameWin();
		}
		else
		{
			GameLose();
		}
	}

	#region IUserAction implementation
	public void StartGame()
	{
		// 切换音乐
		audioSource.clip = clickAudio;
		audioSource.Play();
		audioSource.loop = false;

        // 正式开始
		StartGUI start = gameObject.GetComponent<StartGUI>();
		if(start != null)
		{
			Destroy(start);
		}
        over = false;
        gameObject.AddComponent<UserGUI>();


		Invoke("PlaySound", 0.3f);
    }
	public void GameWin ()
	{
        audioSource.clip = winAudio;
        audioSource.Play();
		audioSource.loop = true;
        winPrefabs = Instantiate(winPrefabs);
		SSDirector.getInstance ().NextScene ();
	}
	public void GameLose()
	{
		losePrefabs = Instantiate(losePrefabs);
        audioSource.clip = loseAudio;
		audioSource.Play();
		audioSource.loop= true;
		Pause();
	}
	public void Restart () {
        // 切换音乐
        audioSource.clip = clickAudio;
        audioSource.Play();
        audioSource.loop = false;


		Invoke("Resume", 0.3f);

    }
	public void MoveObject(GameObject gameobject)
	{
		if (over)  // 不接受新动作
		{
			return;
		}
		actionManager.MoveObject(gameobject);
		
    }
	#endregion


	// Use this for initialization
	void Start () {
		//give advice first
		
	}
	
	// Update is called once per frame
	void Update () {
		//give advice first
		if(Time.deltaTime - debugTimer> 1) {
			debugTimer = 0.0f;
			// Debug.Log()
		}
	}

	void PlaySound() {
        audioSource.clip = mainAudio;
        audioSource.Play();
        audioSource.loop = true;
    }

}
