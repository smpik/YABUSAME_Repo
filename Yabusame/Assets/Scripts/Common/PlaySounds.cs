using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
	/********************************************************************************/
	/* 外部定数(要アタッチ)															*/
	/********************************************************************************/
	public AudioClip Sound_tap_button;//ボタンタップ音
	public AudioClip Sound_shoot;//矢の射出音
	public AudioClip Sound_hit_target;//矢が的に当たった時の音
	public AudioClip Sound_hit_wall;//矢が壁に当たった時の音
	public AudioClip Sound_game_over;//リザルト画面を表示するときの音
	public AudioClip Sound_hit_item;//矢がitemに当たった時の音

	/********************************************************************************/
	/* 内部変数																		*/
	/********************************************************************************/
	private AudioSource AudioPlayer;//音を再生するオブジェクト

	// Start is called before the first frame update
	void Start()
    {
		DontDestroyOnLoad(GameObject.Find("AudioPlayer"));//AudioPlayerをシーン遷移しても削除されないようにする。
		AudioPlayer = GameObject.Find("AudioPlayer").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	/********************************************************************************/
	/* 関数名	: ボタンタップ音再生												*/
	/* 備考		: ボタンタップ時に呼ばれる。										*/
	/********************************************************************************/
	public void PlaySoundTapButton()
	{
		AudioPlayer.PlayOneShot(Sound_tap_button, 0.5f);//第一引数が音源、第二引数が音量
	}

	/********************************************************************************/
	/* 関数名	: 射出音再生														*/
	/* 備考		: ShootArrow.csから呼ばれる。										*/
	/********************************************************************************/
	public void PlaySoundShoot()
	{
		AudioPlayer.PlayOneShot(Sound_shoot, 0.5f);//第一引数が音源、第二引数が音量
	}

	/********************************************************************************/
	/* 関数名	: 矢-的衝突音再生													*/
	/* 備考		: Judgehit.csから呼ばれる。											*/
	/********************************************************************************/
	public void PlaySoundHitTarget()
	{
		AudioPlayer.PlayOneShot(Sound_hit_target, 0.5f);//第一引数が音源、第二引数が音量
	}

	/********************************************************************************/
	/* 関数名	: 矢-壁衝突音再生													*/
	/* 備考		: Judgehit.csから呼ばれる。											*/
	/********************************************************************************/
	public void PlaySoundHitWall()
	{
		AudioPlayer.PlayOneShot(Sound_hit_wall, 0.1f);//第一引数が音源、第二引数が音量
	}

	/********************************************************************************/
	/* 関数名	: ゲームオーバー音再生												*/
	/* 備考		: GameOver.csから呼ばれる。											*/
	/********************************************************************************/
	public void PlaySoundGameOver()
	{
		AudioPlayer.PlayOneShot(Sound_game_over, 0.3f);//第一引数が音源、第二引数が音量
	}

	/********************************************************************************/
	/* 関数名	: 矢-item衝突音再生													*/
	/* 備考		: Judgehit.csから呼ばれる。											*/
	/********************************************************************************/
	public void PlaySoundHitItem()
	{
		AudioPlayer.PlayOneShot(Sound_hit_item, 0.5f);//第一引数が音源、第二引数が音量
	}
}
