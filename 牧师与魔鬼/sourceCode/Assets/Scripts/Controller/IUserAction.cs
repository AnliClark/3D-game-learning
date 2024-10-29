using System;
using UnityEngine;


public interface IUserAction
{
	void StartGame();
	void MoveObject(GameObject gameobject);
	void GameWin();
	void GameLose();
	void Restart();
}


