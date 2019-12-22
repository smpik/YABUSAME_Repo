using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAnimation : MonoBehaviour
{
	/********************************************************************************/
	/*内部変数																		*/
	/********************************************************************************/
	private Animator AnimatorUma;
	private Animator AnimatorPlayer;
	private bool FlgUmaJump;
	private bool FlgPlayerShoot;
	private bool FlgHumanJump;

	// Start is called before the first frame update
	void Start()
    {
		AnimatorUma = GameObject.Find("uma").GetComponent<Animator>();//umaのアニメーション取得
		AnimatorPlayer = GameObject.Find("hito&bow").GetComponent<Animator>();//hito&bowのアニメーションを取得
		FlgUmaJump = false;
		FlgPlayerShoot = false;
		FlgHumanJump = false;
	}

    // Update is called once per frame
    void Update()
    {
		if (FlgPlayerShoot)
		{
			senseEndAnimShoot();
		}
		if (FlgUmaJump)
		{
			senseEndAnimUmaJump();
		}
		if (FlgHumanJump)
		{
			senseEndAnimHitoJump();
		}
	}

	/********************************************************************************/
	/* 関数名	: hitoアニメーションフラグリセット									*/
	/* 備考		: hitoのアニメーターおよび内部のアニメーションフラグをfalseにする。	*/
	/********************************************************************************/
	private void resetHitoFlgAnimation()
	{
		AnimatorPlayer.SetBool("anim_flg_shoot", false);
		AnimatorPlayer.SetBool("anim_FlgHumanJump", false);

		FlgPlayerShoot = false;
		FlgHumanJump = false;
	}

	/********************************************************************************/
	/* 関数名	: shootアニメーション終了検知。										*/
	/* 備考		: shootアニメーションが終わってるか。終わったら遷移許可解除。		*/
	/********************************************************************************/
	private void senseEndAnimShoot()
	{
		AnimatorStateInfo animInfo = AnimatorPlayer.GetCurrentAnimatorStateInfo(0);
		if (animInfo.normalizedTime >= 1.0f)//normalizedTimeはアニメーションの再生時間。開始が0,終了が1
		{
			AnimatorPlayer.SetBool("anim_flg_shoot", false);
			FlgPlayerShoot = false;
		}
	}

	/********************************************************************************/
	/* 関数名	: uma_jumpアニメーション終了検知。									*/
	/* 備考		: uma_jumpアニメーションが終わってるか。終わったら遷移許可解除。	*/
	/********************************************************************************/
	private void senseEndAnimUmaJump()
	{
		AnimatorStateInfo animInfo = AnimatorUma.GetCurrentAnimatorStateInfo(0);
		if (animInfo.normalizedTime >= 1.0f)//normalizedTimeはアニメーションの再生時間。開始が0,終了が1
		{
			AnimatorUma.SetBool("anim_FlgUmaJump", false);
			FlgUmaJump = false;
		}
	}

	/********************************************************************************/
	/* 関数名	: hito_jumpアニメーション終了検知。									*/
	/* 備考		: hito_jumpアニメーションが終わってるか。終わったら遷移許可解除。	*/
	/********************************************************************************/
	private void senseEndAnimHitoJump()
	{
		AnimatorStateInfo animInfo = AnimatorPlayer.GetCurrentAnimatorStateInfo(0);
		if (animInfo.normalizedTime >= 1.0f)//normalizedTimeはアニメーションの再生時間。開始が0,終了が1
		{
			AnimatorPlayer.SetBool("anim_FlgHumanJump", false);
			FlgHumanJump = false;
			Debug.Log("falseにしたアニメーション名" + AnimatorPlayer.GetCurrentAnimatorClipInfo(0)[0].clip.name);
		}
	}

	/********************************************************************************/
	/* 関数名	: shootアニメーション遷移許可。										*/
	/* 備考		: ShootArrow.csから呼ばれる。										*/
	/********************************************************************************/
	public void TransAnimShoot()
	{
		resetHitoFlgAnimation();//アニメーションをリセット(瞬時に遷移させるため)
		AnimatorPlayer.SetBool("anim_flg_shoot", true);//shootアニメーションへ遷移許可
		FlgPlayerShoot = true;
	}

	/********************************************************************************/
	/* 関数名	: jumpアニメーション遷移許可。										*/
	/* 備考		: ControlPlayer.csから呼ばれる。									*/
	/********************************************************************************/
	public void TransAnimJumpMain()
	{
		transAnimUmaJump();//umaのアニメーション遷移処理
		transAnimHitoJump();//hito_bowのアニメーション遷移処理
	}

	/********************************************************************************/
	/* 関数名	: uma_jumpアニメーション遷移許可。									*/
	/********************************************************************************/
	private void transAnimUmaJump()
	{
		AnimatorUma.SetBool("anim_FlgUmaJump", true);
		FlgUmaJump = true;
	}

	/********************************************************************************/
	/* 関数名	: hito_jumpアニメーション遷移許可。									*/
	/********************************************************************************/
	private void transAnimHitoJump()
	{
		resetHitoFlgAnimation();//アニメーションをリセット(瞬時に遷移させるため)
		AnimatorPlayer.SetBool("anim_FlgHumanJump", true);
		FlgHumanJump = true;
		Debug.Log("再生中アニメーション" + AnimatorPlayer.GetCurrentAnimatorClipInfo(0)[0].clip.name);
	}
}
