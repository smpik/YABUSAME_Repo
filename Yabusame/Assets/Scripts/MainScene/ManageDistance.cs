using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageDistance : MonoBehaviour
{
	/********************************************************************************/
	/*内部変数																		*/
	/********************************************************************************/
	private PlayerIF InterfacePlayer;
	private int Distance;//到達距離

	// Start is called before the first frame update
	void Start()
    {
		InterfacePlayer = GameObject.Find("InfoInterface").GetComponent<PlayerIF>();//オブジェクトからアタッチ
		Distance = 0;
	}

    // Update is called once per frame
    void Update()
    {
		calculateDistance();//距離計算＋更新
    }

	/********************************************************************************/
	/* 関数名	: 距離設定															*/
	/* 備考		: プレイヤのスタート地点からの距離を取得し、テキストを更新する。	*/
	/********************************************************************************/
	private void calculateDistance()
	{
		Vector3 tempDis = InterfacePlayer.GetPlayerPos();//プレイヤの現在地を取得
		//スタート地点からの差を取得(今はx=0をスタート地点としているため不要
		Distance = (int)tempDis.x;//型変換
		GameObject.Find("GameMaster").GetComponent<ControlUI>().UpdateTextDistance(Distance);//テキストの更新
	}

	/********************************************************************************/
	/* 関数名	: 距離取得															*/
	/* 備考		: プレイヤのスタート地点からの距離を取得し、テキストを更新する。	*/
	/********************************************************************************/
	public int GetDistance()
	{
		return Distance;
	}
}
