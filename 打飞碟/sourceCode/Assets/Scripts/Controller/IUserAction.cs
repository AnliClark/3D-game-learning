using System;
using UnityEngine;


public interface IUserAction
{
	void StartGame();
	void Hit(GameObject gameobject);
	void NextRound();
	// void GameWin();
	// void GameLose();
	void GameOver();
	void Restart();
	void ChangeMode(bool isPhysics);
}


