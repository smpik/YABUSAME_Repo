using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUI : MonoBehaviour
{
	/********************************************************************************/
	/* 内部変数																		*/
	/********************************************************************************/
	private Text Text_hi_score;

	// Start is called before the first frame update
	void Start()
    {
		Text_hi_score = GameObject.Find("HiScoreText").GetComponent<Text>();
		uint hi_score = GameObject.Find("Main Camera").GetComponent<ManageData>().ReadHighScore();//ハイスコアテキストの読み出し
		Text_hi_score.text = hi_score.ToString();//テキストの設定
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
