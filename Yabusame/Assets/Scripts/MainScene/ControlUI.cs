using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlUI : MonoBehaviour
{
	/********************************************************************************/
	/*内部変数																		*/
	/********************************************************************************/
	private GameObject CanvasMain;//メインキャンバス
	private GameObject CanvasResult;//リザルトキャンバス
	private GameObject CanvasPose;//ポーズキャンバス
	private GameObject ButtonContinue;//コンティニューボタン
	private Text TextScore;//メインキャンバスに表示するスコア
	private Text TextArrow;//メインキャンバスに表示する矢の残り本数
	private Text TextDistance;//メインキャンバスに表示する到達距離
	private Text TextResultScore;//リザルトキャンバスに表示するスコア
	private Text TextResultDistance;//リザルトキャンバスに表示する到達距離
	private Text TextResultHighScore;//リザルトキャンバスに表示するハイスコア
	private GameObject TextDamage;//ダメージ表示テキスト
	private GameObject TextInc;//得点表示テキスト
	private GameObject BlockComing;//ブロック警告
	private bool FlgDamage;//ダメージ表示オンオフ
	private bool FlgInc;//得点表示オンオフ

	/********************************************************************************/
	/*内部定数																		*/
	/********************************************************************************/
	private const float CONVERT_POS_BlockComing = 1;//ブロック警告表示位置変換用定数

	// Start is called before the first frame update
	void Start()
    {
		CanvasMain = GameObject.Find("MainCanvas");
		CanvasResult = GameObject.Find("ResultCanvas");
		CanvasPose = GameObject.Find("PoseCanvas");
		ButtonContinue = GameObject.Find("ButtonContinue");
		TextScore = GameObject.Find("ScoreText").GetComponent<Text>();
		TextArrow = GameObject.Find("ArrowText").GetComponent<Text>();
		TextDistance = GameObject.Find("DistanceText").GetComponent<Text>();
		TextResultScore = GameObject.Find("TextResultScore").GetComponent<Text>();
		TextResultDistance = GameObject.Find("TextResultDistance").GetComponent<Text>();
		TextResultHighScore = GameObject.Find("TextResultHiScore").GetComponent<Text>();
		TextDamage = GameObject.Find("TextDamage");
		TextInc = GameObject.Find("TextInc");
		BlockComing = GameObject.Find("BlockComing");

		UndispResultCanvas();//アタッチしたとは結果表示時まで消えててもらう
		UndispPoseCanvas();
		BlockComing.SetActive(false);//上に同じ
		TextDamage.SetActive(false);
		FlgDamage = false;
		TextInc.SetActive(false);
		FlgInc = false;
	}

    // Update is called once per frame
    void Update()
    {
		if (FlgDamage)
		{
			undispDamage();
		}
		if (FlgInc)
		{
			undispInc();
		}
    }

	/********************************************************************************/
	/* 関数名	: ポーズキャンバス表示												*/
	/* 備考		: から呼ばれる。													*/
	/********************************************************************************/
	public void DispPoseCanvas()
	{
		CanvasPose.SetActive(true);
	}

	/********************************************************************************/
	/* 関数名	: ポーズキャンバス非表示											*/
	/* 備考		: から呼ばれる。								*/
	/********************************************************************************/
	public void UndispPoseCanvas()
	{
		CanvasPose.SetActive(false);
	}

	/********************************************************************************/
	/* 関数名	: リザルトキャンバス表示											*/
	/* 備考		: ControlGame/Game_over()から呼ばれる。								*/
	/********************************************************************************/
	public void DispResultCanvas()
	{
		CanvasResult.SetActive(true);
	}

	/********************************************************************************/
	/* 関数名	: リザルトキャンバス非表示											*/
	/* 備考		: ControlGame/Game_over()から呼ばれる。								*/
	/********************************************************************************/
	public void UndispResultCanvas()
	{
		CanvasResult.SetActive(false);
	}

	/********************************************************************************/
	/* 関数名	: メインキャンバス表示												*/
	/* 備考		: 動画リワードから呼ばれる。										*/
	/********************************************************************************/
	public void DispMainCanvas()
	{
		CanvasMain.SetActive(true);
	}

	/********************************************************************************/
	/* 関数名	: メインキャンバス非表示											*/
	/* 備考		: ControlGame/Game_over()から呼ばれる。								*/
	/********************************************************************************/
	public void UndispMainCanvas()
	{
		CanvasMain.SetActive(false);
	}

	/********************************************************************************/
	/* 関数名	: スコアテキスト更新												*/
	/* 備考		: ManageScoreから呼ばれる。											*/
	/********************************************************************************/
	public void UpdateTextScore(uint scoreNow)
	{
		TextScore.text = scoreNow.ToString();
	}

	/********************************************************************************/
	/* 関数名	: 矢の残り本数テキスト更新											*/
	/* 備考		: ManageArrowから呼ばれる。											*/
	/********************************************************************************/
	public void UpdateTextArrow(int arrow)
	{
		TextArrow.text = arrow.ToString();
	}

	/********************************************************************************/
	/* 関数名	: 距離テキスト更新													*/
	/* 備考		: Managedistanceから呼ばれる。										*/
	/********************************************************************************/
	public void UpdateTextDistance(int distace)
	{
		TextDistance.text = "距離：" + distace;
	}

	/********************************************************************************/
	/* 関数名	: (リザルト)スコアテキスト更新										*/
	/* 備考		: ControlGame.cs/set_result()から呼ばれる。							*/
	/********************************************************************************/
	public void UpdateTextResultScore(uint scoreNow)
	{
		TextResultScore.text = scoreNow.ToString();
	}

	/********************************************************************************/
	/* 関数名	: (リザルト)距離テキスト更新										*/
	/* 備考		: ControlGame.cs/set_result()から呼ばれる。							*/
	/********************************************************************************/
	public void UpdateTextResultDistance(int distance)
	{
		TextResultDistance.text = distance.ToString();
	}

	/********************************************************************************/
	/* 関数名	: (リザルト)ハイスコアテキスト更新									*/
	/* 備考		: ControlGame.cs/set_result()から呼ばれる。							*/
	/********************************************************************************/
	public void UpdateTextResultHighScore(uint highScore)
	{
		TextResultHighScore.text = highScore.ToString();
	}

	/********************************************************************************/
	/* 関数名	: ブロック警告表示メイン											*/
	/* 備考		: SenceBlockComing.cs/request_disp_UI()から呼ばれる。				*/
	/********************************************************************************/
	public void BlockDispMain(float disPlayerBlock)
	{
		bool resultJudged = judgeDispBlockComingUI(disPlayerBlock);//ブロック警告表示判定
		if (resultJudged)
		{   //表示するとき
			BlockComing.SetActive(true);
			float posBlockComing = calcPosBlockComing(disPlayerBlock);//ブロック警告表示位置を計算
			updatePosBlockComing(posBlockComing);//ブロック警告表示位置の更新
		}
		else
		{   //表示したくないとき
			BlockComing.SetActive(false);
		}
	}

	/********************************************************************************/
	/* 関数名	: ブロック警告UI表示判定											*/
	/* 備考		: UIの表示判定のみを行う。											*/
	/*			: 表示したい時だけtrueを返す。										*/
	/********************************************************************************/
	private bool judgeDispBlockComingUI(float disPlayerBlock)
	{
		bool reslutJudgedDispUI = false;//戻り値を定義

		if (disPlayerBlock >= 10)//blockが画面に見えない時だけ表示したいから(10より小さいと見えてしまう)
		{
			reslutJudgedDispUI = true;
		}

		return reslutJudgedDispUI;
	}

	/********************************************************************************/
	/* 関数名	: ブロック警告表示位置計算											*/
	/* 備考		: 適当な定数をかけて表示位置を調整し、表示位置を返す。				*/
	/********************************************************************************/
	private float calcPosBlockComing(float disPlayerBlock)
	{
		float posBlockComing = 0;

		posBlockComing = disPlayerBlock * CONVERT_POS_BlockComing;//変換用定数により位置を調整

		return posBlockComing;
	}

	/********************************************************************************/
	/* 関数名	: ブロック警告表示位置更新											*/
	/* 備考		:																	*/
	/********************************************************************************/
	private void updatePosBlockComing(float posBlockComing)
	{
		BlockComing.transform.position = new Vector3(BlockComing.transform.position.x, 300 + posBlockComing, 0);//ブロック警告表示位置を更新
	}

	/********************************************************************************/
	/* 関数名	: コンティニューボタン非表示										*/
	/* 備考		: ControlGame.csから呼ばれる。										*/
	/********************************************************************************/
	public void UndispButtonContinue()
	{
		ButtonContinue.SetActive(false);
	}

	/********************************************************************************/
	/* 関数名	: ダメージ表示														*/
	/* 備考		: から呼ばれる。													*/
	/********************************************************************************/
	public void DispDamage(int damage)
	{
		TextDamage.SetActive(true);
		TextDamage.GetComponent<Text>().text = "-" + damage;//ダメージをテキストに設定
		FlgDamage = true;
	}

	/********************************************************************************/
	/* 関数名	: ダメージ非表示													*/
	/* 備考		: から呼ばれる。													*/
	/********************************************************************************/
	private void undispDamage()
	{
		AnimatorStateInfo anim_info = TextDamage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
		if (anim_info.normalizedTime >= 1.0f)//normalizedTimeはアニメーションの再生時間。開始が0,終了が1
		{
			TextDamage.SetActive(false);
			FlgDamage = false;
		}
	}

	/********************************************************************************/
	/* 関数名	: ダメージ表示														*/
	/* 備考		: から呼ばれる。													*/
	/********************************************************************************/
	public void DispInc(int inc)
	{
		if (FlgInc)
		{   //既に表示されてる⇒素早く表示要求が来たら
			TextInc.SetActive(false);//1回消す
		}
		TextInc.SetActive(true);
		TextInc.GetComponent<Text>().text = "+" + inc;//ダメージをテキストに設定
		FlgInc = true;
	}

	/********************************************************************************/
	/* 関数名	: 得点テキスト非表示												*/
	/* 備考		: から呼ばれる。													*/
	/********************************************************************************/
	private void undispInc()
	{
		AnimatorStateInfo anim_info = TextInc.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
		if (anim_info.normalizedTime >= 1.0f)//normalizedTimeはアニメーションの再生時間。開始が0,終了が1
		{
			TextInc.SetActive(false);
			FlgInc = false;
		}
	}
}
