using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class FirstSceneController : MonoBehaviour, ISceneController, IUserAction{

	public IActionManager actionManager;
    private ObjectFactory objectFactory;
	public ScoreRecorder scoreRecorder;

    private float timer;
    public bool pause; // 游戏暂停，不再接收动作
	public bool over;
	public bool waitForNext;  // 等待下一轮开启

	private bool[] isShot;
	public bool enableFire;
	private float power;
	private bool powerPositive;
	public int arrowNum;

	// 游戏资源加载
    private GameObject player, terrain;
	private GameObject gameOverPrefabs, nextRoundPrefabs, arrow, staticObject, dynamicObject;
	private GameObject tmpArrow, arrowInCrossbow;
	public Texture restart;
	public GUISkin skin;
	public AudioSource backgroundSource, effectSource1, playerSource;
	public AudioClip gameOverAudio, mainAudio1, clickAudio, flyAudio, holdAudio, shootAudio, hitAudio, footAudio, accessAudio, moveAudio;

	public Animator crossbowAnimator;
	private Animator playerAnimator;

	void Awake () {
		SSDirector director = SSDirector.getInstance ();
		director.setFPS (60);
		director.currentSceneController = this;
		// 加载资源
		director.currentSceneController.LoadResources ();
		// 初始化
		FirstInit ();
	}
	 
	// loading resources for first scence
	public void LoadResources () {
		terrain = Resources.Load("Prefabs/TerrainPrefab") as GameObject;
		player = Resources.Load("Prefabs/PlayerPrefab") as GameObject;
		arrow = Resources.Load("Prefabs/arrow") as GameObject;

		restart = Resources.Load("Arts/Textures/restart") as Texture;
		skin = Resources.Load("Arts/GUISkin") as GUISkin;

		gameOverAudio = Resources.Load("Arts/Audios/gameOver") as AudioClip;
		mainAudio1 = Resources.Load("Arts/Audios/main1") as AudioClip;
		clickAudio = Resources.Load("Arts/Audios/click") as AudioClip;
		flyAudio = Resources.Load("Arts/Audios/fly") as AudioClip;
        holdAudio = Resources.Load("Arts/Audios/hold") as AudioClip;
        shootAudio = Resources.Load("Arts/Audios/shoot") as AudioClip;
		hitAudio = Resources.Load("Arts/Audios/hit") as AudioClip;
		footAudio = Resources.Load("Arts/Audios/foot") as AudioClip;
		accessAudio = Resources.Load("Arts/Audios/access") as AudioClip;


    }
    // 各项资源的初始化（在重启游戏时不再初始化）
    private void FirstInit()  
	{
        // 参数配置
        new StaticData();
        pause = true;
		// 预制实例化
        // 背景设置
        terrain = Instantiate(terrain);
        terrain.transform.position = StaticData.bgPosition;
		// 玩家配置
		player = Instantiate(player);
		crossbowAnimator = GameObject.Find("Crossbow").GetComponent<Animator>();
        playerAnimator = GameObject.Find("player").GetComponent <Animator>();
		arrowInCrossbow = GameObject.Find("箭");
		
        // 附加新组件
        gameObject.AddComponent<StartGUI>();
		gameObject.AddComponent<UserInteraction>();
		//gameObject.AddComponent<StartGUI>();
		backgroundSource = player.AddComponent<AudioSource>();
		effectSource1 = player.AddComponent<AudioSource>();
		playerSource = player.AddComponent<AudioSource>();

		// 音乐
		backgroundSource.loop = true;
		effectSource1.loop = false;
		backgroundSource.clip = mainAudio1;
		backgroundSource.Play();

		// 工厂
		objectFactory = ObjectFactory.GetInstance();
        objectFactory.SetDiskObject(arrow);
		//// 记分员
		scoreRecorder = new ScoreRecorder();

        Initialize();
    }
	// 各项数据初始化（重启游戏时重置数据）
    public void Initialize()
    {
		scoreRecorder.score = 0;

		// 动作管理器的适配器设置
		actionManager = PhysicsActionManager.GetInstance();
		objectFactory.actionManager = actionManager;

		// 音乐
		backgroundSource.clip = mainAudio1;
		backgroundSource.Play();

        player.transform.position = StaticData.playerPosition;
        arrowInCrossbow.SetActive(false);

        enableFire = false;
		isShot = new bool[3] {false, false, false};
		power = 1;
		powerPositive = false;
		arrowNum = 0;
	}

	#region IUserAction implementation
	public void StartGame()
	{
		// 正式开始
		StartGUI start = gameObject.GetComponent<StartGUI>();
		if (start != null)
		{
			Destroy(start);
		}
		gameObject.AddComponent<MainView>();
        // 播放音效
        effectSource1.clip = clickAudio;
        effectSource1.Play();
        // 初始化游戏数据
        Initialize();
        Cursor.lockState = CursorLockMode.Locked;
    }
	public void GameOver()
	{
		backgroundSource.clip = gameOverAudio;
		backgroundSource.loop = false;
		backgroundSource.Play();
		pause = true;
		over = true;
	}
    public void Move()
    {
		if (!playerSource.isPlaying)
		{
			playerSource.PlayOneShot(footAudio);
		}
    }
    public void AccessFirePoint(int order)
	{
		effectSource1.PlayOneShot(accessAudio);
		if (!isShot[order])
		{
			isShot[order] = true;
            arrowNum = 10;
        }
		if (arrowNum > 0)
		{
			enableFire = true;
		}
		
	}
	public void ExitFirePoint()
	{
        effectSource1.PlayOneShot(accessAudio);
        enableFire = false;
	}
	public void ArrowShoot()
	{
        // 使上一个箭消失，激活箭
        arrowInCrossbow.SetActive(false);
        tmpArrow.SetActive(true);
        // 使物体运行
        actionManager.PlayObject(tmpArrow, tmpArrow.transform.forward.normalized * 300.0f * (1.2f-power));

		if(arrowNum == 0)
		{
			enableFire = false;
			bool flag = false;
			for (int i = 0; i < 3; i++)
			{
				if (isShot[i])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				GameOver();
			}
		}

    }
	public void Hold()
	{
        crossbowAnimator.SetTrigger("Hold");
        arrowInCrossbow.SetActive(true);
    }
	public void Fill()
	{
		if (!effectSource1.isPlaying)
		{
			effectSource1.PlayOneShot(holdAudio);
		}
		power += powerPositive? 0.02f : -0.02f;
		if(Mathf.Abs(power)<0.0002f)
		{
			powerPositive = true;
		}
		if(Mathf.Abs(power-0.66f)<0.0002f)
		{
			powerPositive = false;
		}
		crossbowAnimator.SetLayerWeight(1, power);
	}
    public void Shoot()
	{
        effectSource1.PlayOneShot(shootAudio);
        tmpArrow = objectFactory.GetObject("arrow");
        
        
        crossbowAnimator.SetTrigger("Fire");

		power = 1f;
		powerPositive = false;

		arrowNum--;
    }
	public void Hit(GameObject obj)  
	{
		// 播放音效
		scoreRecorder.RecordScore(obj);

	}
	public void Restart () {
        // 播放音效
        effectSource1.clip = clickAudio;
		effectSource1.Play();
		pause = true;
        Initialize();
	}

    #endregion
}
