using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene : MonoBehaviour
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
	/* 概要		: メインシーンへの遷移処理											*/
	/* 備考		: ボタンクリックで呼ばれる。										*/
	/********************************************************************************/
	public void TransMainScene()
	{
		GameObject.Find("AudioPlayer").GetComponent<PlaySounds>().PlaySoundTapButton();//ボタンタップ音再生
		SceneManager.LoadScene("Main");
	}

	/********************************************************************************/
	/* 概要		: タイトルシーンへの遷移処理										*/
	/* 備考		: ボタンクリックで呼ばれる。										*/
	/********************************************************************************/
	public void TransTitleScene()
	{
		GameObject.Find("AudioPlayer").GetComponent<PlaySounds>().PlaySoundTapButton();//ボタンタップ音再生
		GameObject.Find("GameMaster").GetComponent<ControlGame>().ResetPose();//ポーズの解除
		SceneManager.LoadScene("Title");
	}
}
