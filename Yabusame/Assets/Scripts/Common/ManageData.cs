using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	/********************************************************************************/
	/* 概要		: ハイスコア更新処理												*/
	/* 備考		: ControlGame.cs/Game_over()から呼ばれる。							*/
	/********************************************************************************/
	public void UpdateHighScore()
	{
		uint scoreNow = GameObject.Find("GameMaster").GetComponent<ManageScore>().GetScore();//今回のスコアを取得
		uint highScore = ReadHighScore();//今までのハイスコアと比較
		if (scoreNow > highScore)
		{	//上回っていれば更新
			PlayerPrefs.SetInt("HiScore", (int)scoreNow);//書き込み
		}
		PlayerPrefs.Save();//保存
	}

	/********************************************************************************/
	/* 関数名	: ハイスコア読み出し処理											*/
	/* 備考		: タイトルシーンで呼ばれる。										*/
	/********************************************************************************/
	public uint ReadHighScore()
	{
		int highScore;
		highScore = PlayerPrefs.GetInt("HiScore", 0);//ハイスコア読み出し、デフォルト値は0
		return (uint)highScore;
	}
}
