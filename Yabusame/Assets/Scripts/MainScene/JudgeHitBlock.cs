using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeHitBlock: MonoBehaviour
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
	/* 関数名	: 衝突検知処理														*/
	/* 備考		: なにかに当たったら呼ばれる。										*/
	/********************************************************************************/
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "tag_BLOCK")
		{
			HitBlock();
			GameObject.Find("GameMaster").GetComponent<Timer>().SetFlgTimerHittingBlock();//ブロック接触タイマをセット
		}
	}

	/********************************************************************************/
	/* 関数名	: 離地判定															*/
	/* 概要		: なにかから離れたときに呼ばれる。									*/
	/********************************************************************************/
	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "tag_BLOCK")
		{
			GameObject.Find("GameMaster").GetComponent<Timer>().StopTimerHittingBlock();//ブロック接触タイマを解除(強制的に0)
		}
	}

	/********************************************************************************/
	/* 関数名	: ブロック衝突処理													*/
	/* 備考		: ブロックに当たったら呼ばれる。									*/
	/********************************************************************************/
	public void HitBlock()
	{
		int num_block = GameObject.Find("GameMaster").GetComponent<CreateField>().GetIdBlock();//これまで生成したBlock数を取得
		int num_arrow_pattern = GameObject.Find("GameMaster").GetComponent<ShootArrow>().GetNumArrowPattern();//射出パターン取得
		GameObject.Find("GameMaster").GetComponent<ManageArrow>().SubstractArrows(5 * num_block * num_arrow_pattern);
		GameObject.Find("GameMaster").GetComponent<ControlUI>().DispDamage(5 * num_block * num_arrow_pattern);//ダメージ表示
	}
}
