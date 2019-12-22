using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;//これを入れないとEventArgsあたりにエラーが出る
using GoogleMobileAds.Api;//これは必須

public class ManageAd : MonoBehaviour
{
	/********************************************************************************/
	/* 内部変数																		*/
	/********************************************************************************/
	private const string adUnitId = "ca-app-pub-3940256099942544/5224354917";//テスト用id:ca-app-pub-3940256099942544/5224354917
	/********************************************************************************/
	/* 内部変数																		*/
	/********************************************************************************/
	private RewardedAd rewardedAd;//リワードを読み込むためのインスタンスを生成用

	// Start is called before the first frame update
	void Start()
    {
		this.rewardedAd = new RewardedAd(adUnitId);//インスタンス生成
		
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded ad with the request.
		this.rewardedAd.LoadAd(request);

		// Called when an ad request has successfully loaded.
		this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
		// Called when an ad request failed to load.
		this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		// Called when an ad is shown.
		this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
		// Called when an ad request failed to show.
		this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		// Called when the user should be rewarded for interacting with the ad.
		this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		// Called when the ad is closed.
		this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	/********************************************************************************/
	/* 関数名	: 動画再生															*/
	/* 備考		: 動画を見るボタンタップで呼ばれる。								*/
	/********************************************************************************/
	public void ShowAdMovie()
	{
		if (this.rewardedAd.IsLoaded())//広告の読み込みが完了していれば
		{
			this.rewardedAd.Show();//広告を表示
		}
	}

	/********************************************************************************/
	/* AdLoaded (広告の読み込みが完了すると呼ばれる。)								*/
	/********************************************************************************/
	public void HandleRewardedAdLoaded(object sender, EventArgs args)
	{
		Debug.Log("HandleRewardedAdLoaded event received");
	}

	/********************************************************************************/
	/* FailedToLoad (広告の読み込みが失敗すると呼ばれる。)							*/
	/********************************************************************************/
	public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
	{
		Debug.Log("HandleRewardedAdFailedToLoad event received with message: " + args.Message);
	}

	/********************************************************************************/
	/* AdOpening (広告が画面いっぱいに表示されると呼ばれる。)						*/
	/* 必要に応じて、アプリの音声出力やゲームループを一時停止することができる。		*/
	/********************************************************************************/
	public void HandleRewardedAdOpening(object sender, EventArgs args)
	{
		Debug.Log("HandleRewardedAdOpening event received");
	}

	/********************************************************************************/
	/* FailedToShow (広告の表示に失敗すると呼ばれる。)								*/
	/********************************************************************************/
	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		Debug.Log("HandleRewardedAdFailedToShow event received with message: " + args.Message);
	}

	/********************************************************************************/
	/* AdClosed (ユーザが「閉じる」アイコンまたは「戻る」ボタンをタップして、		*/
	/* 動画リワード広告を閉じると呼ばれる。											*/
	/* ゲームの一時停止を再開するのに適したタイミング。								*/
	/********************************************************************************/
	public void HandleRewardedAdClosed(object sender, EventArgs args)
	{
		Debug.Log("HandleRewardedAdClosed event received");
	}

	/********************************************************************************/
	/* EarnedReward (動画を視聴したユーザに報酬を付与するときに呼ばれる。)			*/
	/********************************************************************************/
	public void HandleUserEarnedReward(object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		Debug.Log("HandleRewardedAdRewarded event received for "+ amount.ToString() + " " + type);

		GameObject.Find("GameMaster").GetComponent<ControlGame>().WatchedReward();//動画視聴後の処理をコール
	}
}
