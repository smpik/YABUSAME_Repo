using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
	/********************************************************************************/
	/*内部変数																		*/
	/********************************************************************************/
	private PlayerIF InterfacePlayer;
	private bool FlgPermitShoot;//許可=セット。禁止=クリア。
	private int NumArrowPattern;//射出する矢の本数

	/********************************************************************************/
	/* 関数名	: Start																*/
	/********************************************************************************/
	void Start()
    {
		InterfacePlayer = GameObject.Find("InfoInterface").GetComponent<PlayerIF>();//オブジェクトからアタッチ
		SetFlgPermitShoot();//許可
		NumArrowPattern = 1;
	}

	/********************************************************************************/
	/* 関数名	: Update															*/
	/********************************************************************************/
	void Update()
    {
	
    }

	/********************************************************************************/
	/* 関数名	: Shoot																*/
	/********************************************************************************/
	public void Shoot()
	{
		if (FlgPermitShoot)
		{
			shootArrowByPattern();//射出パターンを決定し、射出を行う
			GameObject.Find("GameMaster").GetComponent<ManageArrow>().SubstractArrows(1);//矢の本数を減らす処理をコール
			GameObject.Find("AudioPlayer").GetComponent<PlaySounds>().PlaySoundShoot();//効果音の再生

			GameObject.Find("GameMaster").GetComponent<ManageAnimation>().TransAnimShoot();//shootアニメーションへ遷移許可
		}
	}

	/********************************************************************************/
	/* 関数名	: 矢射出許可														*/
	/********************************************************************************/
	public void SetFlgPermitShoot()
	{
		FlgPermitShoot = true;
	}

	/********************************************************************************/
	/* 関数名	: 矢射出禁止														*/
	/********************************************************************************/
	public void ClearFlgPermitShoot()
	{
		FlgPermitShoot = false;
	}

	/********************************************************************************/
	/* 関数名	: 射出パターン決定													*/
	/********************************************************************************/
	private void shootArrowByPattern()
	{
		Vector3 posFirstArrowCreate;//矢の生成位置
		float posXFirstArrow = 0;//基準となる一本目の矢の生成位置のx座標
		Vector3 posPlayer = InterfacePlayer.GetPlayerPos();//プレイヤの位置を取得

		posXFirstArrow += (NumArrowPattern - 1);//射出する矢の本数により一本目の生成位置をずらす。(-1は1本だけのときにずらさないため)
		posFirstArrowCreate = new Vector3(posPlayer.x + posXFirstArrow, posPlayer.y + 3, posPlayer.z);//+3をしていないとプレイ他のコライダと矢が接触してしまいプレイヤが移動する。
		createShootArrow(posFirstArrowCreate);//一本目の矢を射出

		if (NumArrowPattern > 1)//複数本の矢を射出するとき
		{
			for (int i = 2; i <= NumArrowPattern; i++)
			{
				Vector3 posArrowCreate;
				float disBeforeArrow = (-1) * (i+i-2);//直前の矢からずらす量を計算
				posArrowCreate = new Vector3(posPlayer.x+posXFirstArrow+disBeforeArrow, posPlayer.y + 3, posPlayer.z);
				createShootArrow(posArrowCreate);//i本目の矢を射出
			}
		}
	}

	/********************************************************************************/
	/* 関数名	: 矢生成&射出														*/
	/********************************************************************************/
	private void createShootArrow(Vector3 posCreate)
	{
		GameObject prefab = (GameObject)Resources.Load("Prefabs/Arrow");//prefabを取得
		GameObject arrow = Instantiate(prefab, posCreate, Quaternion.identity);//プレイヤの座標にarrowオブジェクトを生成
		arrow.GetComponent<Rigidbody>().AddForce(0, 0, 100, ForceMode.Impulse);//z軸正方向にarrowを飛ばす
	}

	/********************************************************************************/
	/* 関数名	: 射出パターン加算													*/
	/********************************************************************************/
	public void AddNumArrowPattern()
	{
		NumArrowPattern++;
	}

	/********************************************************************************/
	/* 関数名	: 射出パターン取得													*/
	/********************************************************************************/
	public int GetNumArrowPattern()
	{
		return NumArrowPattern;
	}
}
