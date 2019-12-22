using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenceBlockComing : MonoBehaviour
{
	/********************************************************************************/
	/* 内部変数																		*/
	/********************************************************************************/
	private ControlUI ControlUI;
	private PlayerIF InterfacePlayer;



	// Start is called before the first frame update
	void Start()
    {
		ControlUI = GameObject.Find("GameMaster").GetComponent<ControlUI>();
		InterfacePlayer = GameObject.Find("InfoInterface").GetComponent<PlayerIF>();//オブジェクトからアタッチ
	}

	// Update is called once per frame
	void Update()
	{
		if (GameObject.Find("Block"))
		{   //Blockがヒエラルキーに存在するとき
			float dis_player_block = calcDistancePlayerBlock();//次に来るブロックとプレイヤの位置の差を計算
			requestDispUI(dis_player_block);//表示位置をControlUIに渡す。(表示位置の制御はControlUIで行う)
		}
	}

	/********************************************************************************/
	/* 関数名	: プレイヤ-ブロック間の距離計算										*/
	/* 備考		: ブロック引くプレイヤ、の値を返す。								*/
	/********************************************************************************/
	private float calcDistancePlayerBlock()
	{
		float disPlayerBlock = 0f;//戻り値を定義
		float posXPlayer = InterfacePlayer.GetPlayerPos().x;//Playerの位置を取得
		float posXBlock = GameObject.Find("Block").transform.position.x;//Blockの位置を取得(InterfacePlayerみたいにしてないことに意図はない)

		disPlayerBlock = posXBlock - posXPlayer;//差を計算(b-pなのは、b-p>0のときに使いたい機能だから)

		return disPlayerBlock;
	}

	/********************************************************************************/
	/* 関数名	: ブロック警告UI表示要求											*/
	/* 備考		:　UIの表示要求のみを行う。											*/
	/********************************************************************************/
	private void requestDispUI(float disPlayerBlock)
	{
		ControlUI.BlockDispMain(disPlayerBlock);//ControlUIに差を渡す
	}

}
