using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
	/********************************************************************************/
	/*内部変数																		*/
	/********************************************************************************/
	private PlayerIF InterfacePlayer;
	private Rigidbody RbPlayer;
	private bool FlgOnRoad;//接地判定フラグ
	private bool PermitAddForce;//プレイヤへの力の印加の許可、ポーズ時に禁止する。

	/********************************************************************************/
	/* 関数名	: Start																*/
	/********************************************************************************/
	void Start()
	{
		InterfacePlayer = GameObject.Find("InfoInterface").GetComponent<PlayerIF>();//オブジェクトからアタッチ
		RbPlayer = InterfacePlayer.GetPlayerRb();//IFから取得

		FlgOnRoad = false;
		PermitAddForce = true;
	}

	/********************************************************************************/
	/* 関数名	: Update															*/
	/********************************************************************************/
	void Update()
	{
		movePlayer();//プレイヤをx軸性方向に移動
	}

	/********************************************************************************/
	/* 関数名	: プレイヤ移動関数													*/
	/********************************************************************************/
	private void movePlayer()
	{
		if(PermitAddForce && RbPlayer.velocity.magnitude < 80)
		{
			RbPlayer.AddForce(7f, 0, 0, ForceMode.Force);
		}
		else
		{
			Debug.Log("addForce停止");
		}
		
	}

	/********************************************************************************/
	/* 関数名	: プレイヤジャンプ													*/
	/* 概要		: ボタンタップで呼ばれる。											*/
	/********************************************************************************/
	public void JumpPlayer()
	{
		if (FlgOnRoad)
		{   //地面と接触しているときだけ
			RbPlayer.constraints = RigidbodyConstraints.None;//Freezeを解除(全部解除してるから空中でBlockにぶつかると大変なことになるかも)
			RbPlayer.constraints = RigidbodyConstraints.FreezeRotation;
			RbPlayer.AddForce(-7, 500, 0);//ジャンプ中加速し続けないようにx軸負方向に力を印加

			GameObject.Find("GameMaster").GetComponent<ManageAnimation>().TransAnimJumpMain();//ジャンプアニメーションへ遷移
		}
	}

	/********************************************************************************/
	/* 関数名	: 着地判定															*/
	/* 概要		: Roadと接触したらtrue												*/
	/********************************************************************************/
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "tag_ROAD")
		{
			RbPlayer.constraints = RigidbodyConstraints.FreezePositionY;
			RbPlayer.constraints = RigidbodyConstraints.FreezePositionZ;
			RbPlayer.constraints = RigidbodyConstraints.FreezeRotation;
			FlgOnRoad = true;
		}
	}

	/********************************************************************************/
	/* 関数名	: 接地判定															*/
	/* 概要		: Roadと接触していたらtrue											*/
	/********************************************************************************/
	private void OnCollisionStay(Collision collision)
	{
		if(collision.gameObject.tag == "tag_ROAD")
		{
			FlgOnRoad = true;
		}
	}

	/********************************************************************************/
	/* 関数名	: 離地判定															*/
	/* 概要		: Roadと離れたときfalse												*/
	/********************************************************************************/
	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "tag_ROAD")
		{
			FlgOnRoad = false;
		}
	}

	/********************************************************************************/
	/* 関数名	: AddForce解除														*/
	/* 概要		: ControlGame.csから呼ばれる。プレイヤへの力の印加を解除する。		*/
	/********************************************************************************/
	public void ResetForcePlayer()
	{
		RbPlayer.velocity = Vector3.zero;
	}

	/********************************************************************************/
	/* 関数名	: AddForce許可														*/
	/* 概要		: ControlGame.csから呼ばれる。プレイヤへの力の印加を許可する。		*/
	/********************************************************************************/
	public void DoAddForce()
	{
		PermitAddForce = true;
	}

	/********************************************************************************/
	/* 関数名	: AddForce禁止														*/
	/* 概要		: ControlGame.csから呼ばれる。プレイヤへの力の印加を禁止する。		*/
	/********************************************************************************/
	public void StopAddForce()
	{
		PermitAddForce = false;
	}
}
