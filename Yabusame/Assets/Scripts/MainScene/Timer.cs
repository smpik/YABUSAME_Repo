using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
	/********************************************************************************/
	/*内部変数																		*/
	/********************************************************************************/
	private bool FlgTimerOn;//矢の本数がゼロになったらセットするフラグ。
	private float TimerUntilGameOver;//最後の矢を放ってからゲームオーバーするまで間をあけるため
	private bool FlgTimerHittingBlockOn;
	private float TimerHittingBlock;//ブロックと当たっている間のタイマ

	// Start is called before the first frame update
	void Start()
    {
		FlgTimerOn = false;
		FlgTimerHittingBlockOn = false;
		TimerUntilGameOver = 1;
		TimerHittingBlock = 1;
    }

    // Update is called once per frame
    void Update()
    {
		if (FlgTimerOn)
		{
			if (TimerUntilGameOver > 0)//少し待つ
			{
				TimerUntilGameOver -= Time.deltaTime;
			}
			else
			{
				GameObject.Find("GameMaster").GetComponent<ControlGame>().GameOver();//ゲームオーバー処理をコール
			}
		}

		if (FlgTimerHittingBlockOn)
		{
			if (TimerHittingBlock > 0)
			{
				TimerHittingBlock -= Time.deltaTime;
			}
			else
			{
				GameObject.Find("Player").GetComponent<JudgeHitBlock>().HitBlock();//ダメージを与える
				TimerHittingBlock = 1;//リセット
			}
		}
		
    }

	/********************************************************************************/
	/* 関数名	: タイマフラグセット												*/
	/* 備考		: ManageArrow.csから呼ばれる。										*/
	/********************************************************************************/
	public void SetFlgTimerOn()
	{
		FlgTimerOn = true;
	}

	/********************************************************************************/
	/* 関数名	: ゲームオーバ待ち時間タイマリセット								*/
	/* 備考		: ControlGame.csから呼ばれる。										*/
	/********************************************************************************/
	public void ResetTimer()
	{
		TimerUntilGameOver = 1;
		FlgTimerOn = false;//タイマフラグのクリア
	}

	/********************************************************************************/
	/* 関数名	: ブロック接触フラグセット											*/
	/* 備考		: ブロック接触時に呼ばれる。										*/
	/********************************************************************************/
	public void SetFlgTimerHittingBlock()
	{
		FlgTimerHittingBlockOn = true;
	}

	/********************************************************************************/
	/* 関数名	: ブロック接触タイマセット											*/
	/* 備考		: ブロック接触時に呼ばれる。										*/
	/********************************************************************************/
	public void StopTimerHittingBlock()
	{
		FlgTimerHittingBlockOn = false;
		TimerHittingBlock = 1;
	}
}
