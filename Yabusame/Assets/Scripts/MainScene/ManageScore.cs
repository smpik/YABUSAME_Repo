using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageScore : MonoBehaviour
{
	/********************************************************************************/
	/*内部変数																		*/
	/********************************************************************************/
	private uint ScoreNow;//現在の得点

	// Start is called before the first frame update
	void Start()
	{
		ScoreNow = 0;
	}

	// Update is called once per frame
	void Update()
	{

	}

	/********************************************************************************/
	/* 関数名	: スコア加算														*/
	/* 備考		: JudgeHitから呼ばれる。											*/
	/********************************************************************************/
	public void AddScore()
	{
		ScoreNow++;
		GameObject.Find("GameMaster").GetComponent<ControlUI>().UpdateTextScore(ScoreNow);//UIの更新
	}

	/********************************************************************************/
	/* 関数名	: スコア値取得														*/
	/* 備考		: 内部変数であるScoreを渡す。										*/
	/********************************************************************************/
	public uint GetScore()
	{
		return ScoreNow;
	}
}
