using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using Random = System.Random;

public class snake : MonoBehaviour
{
    private int score;
    private int[,] pos;  // 0��ʾ�գ�1��ʾ����2��ʾ����
    private LinkedList<Tuple<int, int>> snakeBody;  // �洢���������
    private int direction;
    private float timer = 0.0f;
    private Random rd = new Random();
    private bool lose;
    // ���峤�������
    private int windowWidth;
    private int windowHeight;
    private int squareWidth;
    private const int squareCount = 25;
    private int buttonWidth;
    // ��Դ
    public Texture boxTexture;
    public Texture snakeTexture;
    public Texture appleTexture;
    public Texture arrowUp;
    public Texture arrowDown;
    public Texture arrowleft;
    public Texture arrowRight;
    public Texture restart;
    private GUIStyle style;



    void Start()
    {
        init();
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, windowWidth, windowHeight), "");
        
        for(int i = 0; i < squareCount; i++)
        {
            for(int j = 0; j < squareCount; j++)
            {
                if (pos[i,j] == 0)
                {
                    GUI.Box(new Rect(20 + i * squareWidth, 20 + j * squareWidth, squareWidth, squareWidth), boxTexture);
                }else if (pos[i,j] == 1)
                {
                    GUI.Box(new Rect(20 + i * squareWidth, 20 + j * squareWidth, squareWidth, squareWidth), snakeTexture);
                }
                else
                {
                    GUI.Box(new Rect(20 + i * squareWidth, 20 + j * squareWidth, squareWidth, squareWidth), appleTexture);
                }
                
            }
        }

        
        if (GUI.Button(new Rect(140 + squareCount * squareWidth, windowWidth / 4, buttonWidth, buttonWidth), arrowleft))
        {
            direction = 2;
        }// ��
        else if(GUI.Button(new Rect(160 + squareCount * squareWidth + buttonWidth, windowWidth / 4, buttonWidth, buttonWidth), arrowDown))
        {
            direction = 1;
        } // ��
        else if(GUI.Button(new Rect(160 + squareCount * squareWidth + buttonWidth, windowWidth / 4 - buttonWidth - 20, buttonWidth, buttonWidth), arrowUp))
        {
            direction = 0;
        } // ��
        else if(GUI.Button(new Rect(180 + squareCount * squareWidth + buttonWidth * 2, windowWidth / 4, buttonWidth, buttonWidth), arrowRight))
        {
             direction = 3;
        } // ��
        else if(GUI.Button(new Rect(windowWidth - 20 - buttonWidth, windowHeight - 20 - buttonWidth, buttonWidth, buttonWidth), restart))
        {
            init();
            return;
        }// ����

        if (lose)
        {
            GUI.Box(new Rect(windowWidth / 4, windowHeight / 4, buttonWidth, buttonWidth), "You Lose", style);
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

    void init()
    {
        score = 0;
        direction = 0;
        lose = false;
        pos = new int[squareCount, squareCount];
           

        snakeBody = new LinkedList<Tuple<int, int>>();
        for (int i=0; i<squareCount; i++)
        {
            for(int j = 0; j<squareCount; j++)
            {
                pos[i,j] = 0;
            }
        }
        pos[10, 10] = 1;
        pos[10, 11] = 1;

        snakeBody.AddFirst(Tuple.Create(10, 10));
        snakeBody.AddLast(Tuple.Create(10, 11));

        windowWidth = Screen.width - 10;
        windowHeight = Screen.height - 10;
        squareWidth = (windowHeight - 40) / squareCount;
        buttonWidth = windowHeight / 8;
        style = new GUIStyle();
        style.fontSize = buttonWidth;
        style.normal.textColor = Color.blue;

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
            if (pos[x,y] == 0)
            {
                pos[x,y] = 2;
                break;
            }
        }
    }

    bool move(int towords)  // 0-3��ʾ�ĸ������������ҡ�����true��ʾ����������false��ʾ����
    {
        // ����ͷ��
        int x = -1;
        int y = -1;
        switch (towords)
        {
            case 0:
                x = snakeBody.First.Value.Item1;
                y = snakeBody.First.Value.Item2 - 1;            
                break;
            case 1:
                x = snakeBody.First.Value.Item1;
                y = snakeBody.First.Value.Item2 + 1;               
                break;
            case 2:
                x = snakeBody.First.Value.Item1 - 1;
                y = snakeBody.First.Value.Item2;
                break;
            case 3:
                x = snakeBody.First.Value.Item1 + 1;
                y = snakeBody.First.Value.Item2;
                break;
        }
        if(collision(x, y) == 0)
        {
            // ɾȥβ��
            pos[snakeBody.Last.Value.Item1, snakeBody.Last.Value.Item2] = 0;
            snakeBody.RemoveLast();
        }else if(collision(x, y) == 1)
        {
            return false;
        }
        else
        {
            score++;
            generateApple();
        }
        snakeBody.AddFirst(Tuple.Create(x, y));
        pos[snakeBody.First.Value.Item1, snakeBody.First.Value.Item2] = 1;
        return true;
    }

    // ��ײ���
    int collision(int x, int y)  // 0��ʾδ��ײ��1��ʾ������2��ʾ��ȡ��Apple
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
        else if (pos[x, y] == 1)
        {
            return 1;
        }
        else { return 2; }
    }
}
