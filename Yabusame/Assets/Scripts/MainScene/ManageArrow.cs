using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageArrow : MonoBehaviour
{
	/********************************************************************************/
	/* 内部変数																		*/
	/********************************************************************************/
	private int arrows;//矢の本数

	// Start is called before the first frame update
	void Start()
    {
		arrows = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	/********************************************************************************/
	/* 関数名	: 矢本数減算処理													*/
	/* 備考		: ShootArrowから呼ばれる。											*/
	/********************************************************************************/
	public void SubstractArrows(int numSubstract)
	{
		arrows -= numSubstract;//デクリメント

		if (arrows <= 0)//矢の本数が0になったら
		{
			arrows = 0;//矢の本数を0で固定(ブロック衝突時にマイナスの値になってしまうから)
			GameObject.Find("GameMaster").GetComponent<ShootArrow>().ClearFlgPermitShoot();//矢の射出禁止
			GameObject.Find("GameMaster").GetComponent<Timer>().SetFlgTimerOn();//ゲームオーバーまでのカウントダウン開始
		}

		GameObject.Find("GameMaster").GetComponent<ControlUI>().UpdateTextArrow(arrows); //矢の残り本数テキストの更新
	}

	/********************************************************************************/
	/* 関数名	: 矢本数加算処理													*/
	/* 備考		: JudgeHit.csから呼ばれる。											*/
	/********************************************************************************/
	public void AddArrows(int numAdd)
	{
		arrows+=numAdd;//デクリメント
		GameObject.Find("GameMaster").GetComponent<ControlUI>().UpdateTextArrow(arrows); //矢の残り本数テキストの更新
	}
}
