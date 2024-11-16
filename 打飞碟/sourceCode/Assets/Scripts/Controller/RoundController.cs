using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundController : MonoBehaviour, ISceneController, IUserAction{

	public IActionManager actionManager;
	private Ruler ruler;
    private DiskFactory diskFactory;
	public ScoreRecorder scoreRecorder;

	public int round;
	private int diskNum;
    private float timer;
    public bool pause; // 游戏暂停，不再接收动作
	public bool over;
	public bool waitForNext;  // 等待下一轮开启

    private GameObject disk1, disk2, disk3, background;
    private GameObject gameOverPrefabs, nextRoundPrefabs;
	public Texture restart;
	public GUISkin skin;
	public AudioSource backgroundSource, effectSource;  
	public AudioClip gameOverAudio, mainAudio1, mainAudio2, mainAudio3, nextRoundAudio, clickAudio, flyAudio, shootAudio, hitAudio;
	
	

    // the first scripts
    void Awake () {
		SSDirector director = SSDirector.getInstance ();
		director.setFPS (60);
		director.currentSceneController = this;
		director.currentSceneController.LoadResources ();
		// 初始化
		FirstInit ();
	}
	 
	// loading resources for first scence
	public void LoadResources () {
		background = Resources.Load("Prefabs/background") as GameObject;
        disk1 = Resources.Load("Prefabs/disk1") as GameObject;
        disk2 = Resources.Load("Prefabs/disk2") as GameObject;
        disk3 = Resources.Load("Prefabs/disk3") as GameObject;
        //clouds = Resources.Load("Prefabs/Clouds") as GameObject;
        //      winPrefabs = Resources.Load("Prefabs/winPrefabs") as GameObject;
        //      losePrefabs = Resources.Load("Prefabs/losePrefabs") as GameObject;

        restart = Resources.Load("Arts/Textures/restart") as Texture;
        skin = Resources.Load("Arts/GUISkin") as GUISkin;
        //      victory = Resources.Load("Textures/return") as Texture;
        //      lose = Resources.Load("Textures/return") as Texture;

        gameOverAudio = Resources.Load("Arts/Audios/gameOver") as AudioClip;
        mainAudio1 = Resources.Load("Arts/Audios/main1") as AudioClip;
        mainAudio2 = Resources.Load("Arts/Audios/main2") as AudioClip;
        mainAudio3 = Resources.Load("Arts/Audios/main3") as AudioClip;
        nextRoundAudio = Resources.Load("Arts/Audios/nextRound") as AudioClip;
        clickAudio = Resources.Load("Arts/Audios/click") as AudioClip;
        flyAudio = Resources.Load("Arts/Audios/fly") as AudioClip;
        shootAudio = Resources.Load("Arts/Audios/shoot") as AudioClip;
        hitAudio = Resources.Load("Arts/Audios/hit") as AudioClip;
        
	}
    // 各项资源的初始化（在重启游戏时不再初始化）
    private void FirstInit()  
	{
        // 参数配置
        new StaticData();
        pause = true;
        // 背景设置
        background = Instantiate(background);
        background.transform.position = StaticData.bgPosition;
        // 附加新组件
        gameObject.AddComponent<CCActionManager>();
        gameObject.AddComponent<StartGUI>();
        backgroundSource = background.AddComponent<AudioSource>();
        effectSource = backgroundSource.AddComponent<AudioSource>();

        // 音乐
        backgroundSource.loop = true;
        effectSource.loop = false;
        backgroundSource.clip = mainAudio1;
        backgroundSource.Play();

        ruler = Ruler.GetInstance();
        // 飞碟工厂
        diskFactory = DiskFactory.GetInstance();
        diskFactory.SetDiskObject(disk1, disk2, disk3);
		// 记分员
        scoreRecorder = new ScoreRecorder();

        

    }
	// 各项数据初始化（重启游戏时重置数据）
    public void Initialize()
    {
		Debug.Log("Init");
        round = 0;    
		ruler.round = round;
		scoreRecorder.score = 0;
        for (int i = DiskFactory.usedDisk.Count - 1; i >= 0; i--)
        {
            GameObject obj = DiskFactory.usedDisk[i];
            diskFactory.FreeDisk(obj);
        }

		// 动作管理器的适配器设置（默认为物理模式）
        actionManager = PhysicsActionManager.GetInstance();
        diskFactory.actionManager = actionManager;
		timer = 0.0f;

		// 音乐
        backgroundSource.clip = mainAudio1;
		backgroundSource.Play();

		pause = false;  // 运行执行操作
		over = false;
        NextRound();  // 开启第一轮
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
        effectSource.clip = clickAudio;
        effectSource.Play();
        // 初始化游戏数据
        Initialize();
    }
	public void GameOver()
	{
		backgroundSource.clip = gameOverAudio;
		backgroundSource.loop = false;
		backgroundSource.Play();
		pause = true;
		over = true;
	}
	public void NextRound()
	{
		// 10轮后游戏结束
        if (round == 10)
        {
            GameOver();
        }
        round++;
        ruler.round = round;
        diskNum = 0;
		waitForNext = false;  // 下一轮开启
		// 不同的阶段，播放不同的音乐
		if(round == 2)
		{
			backgroundSource.clip = mainAudio2;
			backgroundSource.Play();
		}
		if(round == 5)
		{
			backgroundSource.clip = mainAudio3;
			backgroundSource.Play();
		}
	}
	public void Hit(GameObject obj)  
	{
		// 播放音效
		effectSource.clip = hitAudio;
		effectSource.Play();
		scoreRecorder.RecordScore(obj);
		diskFactory.FreeDisk(obj);

	}
	public void Restart () {
        // 播放音效
        effectSource.clip = clickAudio;
		effectSource.Play();
		pause = true;
        Initialize();
	}
	public void ChangeMode(bool isPhysics)
	{
        // 播放音效
        effectSource.clip = clickAudio;
        effectSource.Play();
        actionManager = isPhysics ? PhysicsActionManager.GetInstance() : CCActionManager.GetInstance();
		diskFactory.actionManager = actionManager;
    }

    #endregion

	void Update () {
		if (pause)  // 停止更新
		{
			return;
		}
        timer += Time.deltaTime;
		// 当飞碟数小于10个时，定期生成飞碟
        if (diskNum < 10 && timer> 1) { 
			timer = 0.0f;
            diskFactory.GetDisk(round);
			diskNum++;
		}
		// 当飞碟数等于10，且存在的飞碟都已释放时，2s后开启下一轮
		if (diskNum == 10 && DiskFactory.usedDisk.Count == 0)  
		{
            if (!waitForNext)
            {
                waitForNext = true;
                effectSource.clip = nextRoundAudio;
                effectSource.Play();
				timer = 0.0f;
            }
            if (timer > 3)
			{
				timer = 0.0f;
				NextRound();
			}
		}
    }
}
