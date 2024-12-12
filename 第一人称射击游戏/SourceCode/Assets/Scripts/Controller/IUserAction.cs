using System;
using UnityEngine;


public interface IUserAction
{
	void StartGame();
	void Move();
	void AccessFirePoint(int order);
	void ExitFirePoint();
	void Fill();
	void Hold();
	void Shoot();
	void Hit(GameObject gameobject);
	//void NextRound();
	void GameOver();
	void Restart();
}


