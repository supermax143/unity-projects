using UnityEngine;
using System.Collections;

public class GameManager {

	private static GameManager _instanse;


	protected GameManager(){}


	public static GameManager Instanse {
		get {
			if(_instanse!=null)
				return _instanse;
			else
			{
				_instanse = new GameManager();
				return _instanse;
			}
		}
	}

	public void showLog(){

		Debug.Log("Hi Max!!!!!");
	}

}
