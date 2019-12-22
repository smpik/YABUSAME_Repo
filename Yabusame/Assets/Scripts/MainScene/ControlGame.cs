using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGame : MonoBehaviour
{
	/********************************************************************************/
	/* 内部変数																		*/
	/********************************************************************************/
	private ControlUI ControlUI;
	private ManageScore ManageScore;
	private ManageDistance ManageDistance;
	private ManageData ManageData;

	// Start is called before the first frame update
	void Start()
    {
		ControlUI = GameObject.Find("GameMaster").GetComponent<ControlUI>();//オブジェクトからアタッチ
		ManageScore = GameObject.Find("GameMaster").GetComponent<ManageScore>();//オブジェクトからアタッチ
		ManageDistance = GameObject.Find("GameMaster").GetComponent<ManageDistance>();//オブジェクトからアタッチ
		ManageData = GameObject.Find("GameMaster").GetComponent<ManageData>();//オブジェクトからアタッチ
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	/********************************************************************************/
	/* 関数名	: ポーズ処理														*/
	/* 備考		: ポーズボタンから呼ばれる。										*/
	/********************************************************************************/
	public void Pose()
	{
		Time.timeScale = 0;//ポーズ
		GameObject.Find("Player").GetComponent<ControlPlayer>().StopAddForce();//プレイヤへの力の印加を禁止
		ControlUI.UndispMainCanvas();//メインキャンバスを非表示
		ControlUI.DispPoseCanvas();//ポーズキャンバスを表示
	}

	/********************************************************************************/
	/* 関数名	: 再開処理															*/
	/* 備考		: リプレイボタンから呼ばれる。										*/
	/********************************************************************************/
	public void Replay()
	{
		Time.timeScale = 1;//ポーズ解除
		GameObject.Find("Player").GetComponent<ControlPlayer>().DoAddForce();//プレイヤへの力の印加を許可
		ControlUI.DispMainCanvas();//メインキャンバスを表示
		ControlUI.UndispPoseCanvas();//ポーズキャンバスを非表示
	}

	/********************************************************************************/
	/* 関数名	: ゲームオーバー処理												*/
	/* 備考		: ManageArrow.cs/SubstractArrows()から呼ばれる。					*/
	/********************************************************************************/
	public void GameOver()
	{
		Time.timeScale = 0;//ポーズ
		GameObject.Find("AudioPlayer").GetComponent<PlaySounds>().PlaySoundGameOver();//ゲームオーバー音再生
		ControlUI.UndispMainCanvas();//メインキャンバスを非表示
		ControlUI.UndispPoseCanvas();//ポーズキャンバスを非表示
		ControlUI.DispResultCanvas();//リザルトキャンバスを表示
		ManageData.UpdateHighScore();//ハイスコアの更新
		setResult();//リザルトテキスト設定

		/* コンティニュー時に初期化が必要な変数の初期化 */
		GameObject.Find("GameMaster").GetComponent<Timer>().ResetTimer();//ゲームオーバまでの待ち時間カウント用タイマのリセット
		GameObject.Find("GameMaster").GetComponent<ShootArrow>().SetFlgPermitShoot();//矢の射出許可
	}

	/********************************************************************************/
	/* 関数名	: リザルトテキスト設定												*/
	/* 備考		: リザルトキャンバスのテキストに値を設定する。						*/
	/********************************************************************************/
	private void setResult()
	{
		uint tempScore = ManageScore.GetScore();//スコアの取得
		int tempDistance = ManageDistance.GetDistance();//到達距離の取得
		uint tempHighScore = ManageData.ReadHighScore();//ハイスコアの読み出し
		ControlUI.UpdateTextResultScore(tempScore);//リザルトスコアテキストの設定
		ControlUI.UpdateTextResultDistance(tempDistance);//リザルト到達距離の設定
		ControlUI.UpdateTextResultHighScore(tempHighScore);//ハイスコアテキストの設定
	}

	/********************************************************************************/
	/* 関数名	: ポーズ解除														*/
	/* 備考		: ContorlScene.cs/Trans_title()、動画リワードから呼ばれる。			*/
	/********************************************************************************/
	public void ResetPose()
	{
		Time.timeScale = 1;//ポーズの解除
	}

	/********************************************************************************/
	/* 関数名	: 動画視聴後処理													*/
	/* 備考		: ManageAd.csから呼ばれる。											*/
	/********************************************************************************/
	public void WatchedReward()
	{
		GameObject.Find("GameMaster").GetComponent<ControlUI>().UndispButtonContinue();//動画を見てコンティニューボタンを非表示にする。
		GameObject.Find("Player").GetComponent<ControlPlayer>().ResetForcePlayer();//プレイヤに引火している力をリセットする(1回だけ力の印加を解除する。
		GameObject.Find("GameMaster").GetComponent<ManageArrow>().AddArrows(10);//矢の本数を回復
		GameObject.Find("GameMaster").GetComponent<ControlUI>().UndispResultCanvas();//リザルトキャンバスを非表示にする
		GameObject.Find("GameMaster").GetComponent<ControlUI>().DispMainCanvas();//メインキャンバスを表示する
		GameObject.Find("GameMaster").GetComponent<ControlGame>().ResetPose();//一時停止を解除
	}
}
