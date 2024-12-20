using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using Random = System.Random;

public class snake : MonoBehaviour
{
    // **********************************//
    // Entities and their states / Model //
    // **********************************//
    private int score;
    private int[,] pos;  // 0表示空，1表示蛇身，2表示蛇头，3表示奖励
    private LinkedList<Tuple<int, int>> snakeBody;  // 存储蛇身的坐标
    private int direction;
    private float timer = 0.0f;
    private Random rd = new Random();
    private bool lose;
    // 定义长宽等数据
    private int windowWidth;
    private int windowHeight;
    private int squareWidth;
    private const int squareCount = 25;
    private int buttonWidth;
    // 资源
    public Texture boxTexture;
    public Texture snakeTexture;
    public Texture snakeHeadTexture;
    public Texture appleTexture;
    public Texture arrowUp;
    public Texture arrowDown;
    public Texture arrowleft;
    public Texture arrowRight;
    public Texture restart;
    private GUIStyle textStyle;
    private GUIStyle scoreStyle;



    void Start()
    {
        init();
    }
    // **********************************//
    // View render entities / models     //
    // **********************************//
    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, windowWidth, windowHeight), "");
        // 绘制背景图
        GUI.DrawTexture(new Rect(20, 20, squareWidth*squareCount, squareWidth*squareCount), boxTexture);
        for (int i = 0; i < squareCount; i++)
        {
            for (int j = 0; j < squareCount; j++)
            {
                if (pos[i, j] == 1)
                {
                    GUI.DrawTexture(new Rect(20 + i * squareWidth, 20 + j * squareWidth, squareWidth, squareWidth), snakeTexture);
                }
                else if (pos[i, j] == 2)
                {
                    GUI.DrawTexture(new Rect(20 + i * squareWidth, 20 + j * squareWidth, squareWidth, squareWidth), snakeHeadTexture);
                }else if (pos[i, j] == 3)
                {
                    GUI.DrawTexture(new Rect(20 + i * squareWidth, 20 + j * squareWidth, squareWidth, squareWidth), appleTexture);
                }

            }
        }
        GUI.Box(new Rect(windowWidth - 20 - buttonWidth*2, 20, buttonWidth*2, buttonWidth*2), "score:" + score, scoreStyle);

        if (GUI.Button(new Rect(140 + squareCount * squareWidth, windowWidth / 4, buttonWidth, buttonWidth), arrowleft))
        {
            changeDirection(2);
        }// 左
        else if (GUI.Button(new Rect(160 + squareCount * squareWidth + buttonWidth, windowWidth / 4, buttonWidth, buttonWidth), arrowDown))
        {
            changeDirection(1);
        } // 下
        else if (GUI.Button(new Rect(160 + squareCount * squareWidth + buttonWidth, windowWidth / 4 - buttonWidth - 20, buttonWidth, buttonWidth), arrowUp))
        {
            changeDirection(0);
        } // 上
        else if (GUI.Button(new Rect(180 + squareCount * squareWidth + buttonWidth * 2, windowWidth / 4, buttonWidth, buttonWidth), arrowRight))
        {
            changeDirection(3);
        } // 右
        else if (GUI.Button(new Rect(windowWidth - 20 - buttonWidth, windowHeight - 20 - buttonWidth, buttonWidth, buttonWidth), restart))
        {
            init();
            return;
        }// 重来

        if (lose)
        {
            GUI.Box(new Rect(windowWidth / 4, windowHeight / 4, buttonWidth, buttonWidth), "You Lose", textStyle);
            return;
        }
        timer += Time.deltaTime;
        if (timer > 0.5)
        {
            if (!move(direction))
            {
                lose = true;
            }
            timer = 0.0f;
        }
    }
    // **********************************//
    // Components /controls              //
    // **********************************//
    void init()
    {
        score = 0;
        direction = 0;
        lose = false;
        pos = new int[squareCount, squareCount];


        snakeBody = new LinkedList<Tuple<int, int>>();
        for (int i = 0; i < squareCount; i++)
        {
            for (int j = 0; j < squareCount; j++)
            {
                pos[i, j] = 0;
            }
        }
        pos[10, 10] = 2;
        pos[10, 11] = 1;
        pos[10, 12] = 1;

        snakeBody.AddFirst(Tuple.Create(10, 10));
        snakeBody.AddLast(Tuple.Create(10, 11));
        snakeBody.AddLast(Tuple.Create(10, 12));
        snakeHeadTexture = Resources.Load<Texture>("蛇头上");

        windowWidth = Screen.width - 10;
        windowHeight = Screen.height - 10;
        squareWidth = (windowHeight - 40) / squareCount;
        buttonWidth = windowHeight / 8;

        textStyle = new GUIStyle();
        textStyle.fontSize = buttonWidth;
        textStyle.normal.textColor = new Color(190f/255, 74f/255, 47f / 255);
        scoreStyle = new GUIStyle();
        scoreStyle.fontSize = buttonWidth/2;
        scoreStyle.normal.textColor = new Color(190f / 255,74f / 255,47f / 255 );

        generateApple();
    }

    void generateApple()
    {
        int x;
        int y;
        while (true)
        {
            x = rd.Next() % squareCount;
            y = rd.Next() % squareCount;
            if (pos[x, y] == 0)
            {
                pos[x, y] = 3;
                break;
            }
        }
    }

    bool move(int towords)  // 0-3表示四个方向，上下左右。返回true表示正常，返回false表示结束
    {
        // 增加头部
        int x = -1;
        int y = -1;
        switch (towords)
        {
            case 0:
                x = snakeBody.First.Value.Item1;
                y = snakeBody.First.Value.Item2 - 1;
                snakeHeadTexture = Resources.Load<Texture>("蛇头上");
                break;
            case 1:
                x = snakeBody.First.Value.Item1;
                y = snakeBody.First.Value.Item2 + 1;
                snakeHeadTexture = Resources.Load<Texture>("蛇头下");
                break;
            case 2:
                x = snakeBody.First.Value.Item1 - 1;
                y = snakeBody.First.Value.Item2;
                snakeHeadTexture = Resources.Load<Texture>("蛇头左");
                break;
            case 3:
                x = snakeBody.First.Value.Item1 + 1;
                y = snakeBody.First.Value.Item2;
                snakeHeadTexture = Resources.Load<Texture>("蛇头右");
                break;
        }
        if (collision(x, y) == 0)
        {
            // 删去尾部
            pos[snakeBody.Last.Value.Item1, snakeBody.Last.Value.Item2] = 0;
            snakeBody.RemoveLast();
        }
        else if (collision(x, y) == 1)
        {
            return false;
        }
        else
        {
            score++;
            generateApple();
        }
        pos[snakeBody.First.Value.Item1, snakeBody.First.Value.Item2] = 1;
        snakeBody.AddFirst(Tuple.Create(x, y));
        pos[x, y] = 2;
        return true;
    }

    // 碰撞检测
    int collision(int x, int y)  // 0表示未碰撞，1表示死亡，2表示获取到Apple
    {
        if (x >= squareCount || x < 0)
        {
            return 1;
        }
        else if (y >= squareCount || y < 0)
        {
            return 1;
        }
        else if (pos[x, y] == 0)
        {
            return 0;
        }
        else if (pos[x, y] == 1 || pos[x, y] == 2)
        {
            return 1;
        }
        else { return 2; }
    }
    void changeDirection(int x)
    {
        direction = x;
    }
}