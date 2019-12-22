using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateField : MonoBehaviour
{
	/********************************************************************************/
	/*内部変数																		*/
	/********************************************************************************/
	private PlayerIF InterfacePlayer;

	private uint IdLatestRoad;//最新RoadのID
	private uint IdOldestRoad;//最古RoadのID、DeleterによりRoadが消されるたびに更新(本当はヒエラルキーに存在するRoadのidを比較して最初のものを格納したほうがいい)
	private int IdBlock;//BlockのID、矢の減算に使う。
	private uint CounterBlock;//Block生成タイミングを数えるためのカウンタ
	private uint CounterItem;//Item生成タイミングのカウンタ
	private bool PermitCreateBlock;//Block生成許可
	private bool PermitCreateItem;//Item生成許可

	// Start is called before the first frame update
	void Start()
    {
		InterfacePlayer = GameObject.Find("InfoInterface").GetComponent<PlayerIF>();//オブジェクトからアタッチ

		IdLatestRoad = 1;//最初から配置してあるRoadのidは1だから
		IdOldestRoad = 1;
		IdBlock = 0;
		CounterBlock = 3;//3回に1回
		CounterItem = 5;
		PermitCreateBlock = true;//最初は許可しておかないと1個目のブロックが作れない。(Blockを削除したときにしか許可しないため,続かなくなる)
		PermitCreateItem = true;
	}

    // Update is called once per frame
    void Update()
    {
		float posX_player = InterfacePlayer.GetPlayerPos().x;//現在地を取得
		GameObject latest_road = GameObject.Find("Road" + IdLatestRoad);
		
		if (posX_player > (latest_road.transform.position.x - latest_road.transform.localScale.x))//Roadの長さの半分まできたら
		{
			createFieldMain();//フィールド生成
		}
		
	}

	/********************************************************************************/
	/* 関数名	: フィールド生成メイン												*/
	/* 備考		: Road生成とTarget生成をコールする。								*/
	/********************************************************************************/
	private void createFieldMain()
	{
		GameObject temp = createRoad();// Road生成をコール
		createWall(temp);//Wall生成をコール
		createTarget(temp);// Target生成をコール

		if (CounterBlock != 0)	//0でなければデクリメント
		{						//そうしないとuintは0-1=4???????になってしまうから次のif文がうまく回らない
			CounterBlock--;
		}
		if (PermitCreateBlock && (CounterBlock <= 0))
		{
			CounterBlock = 3;//カウンタをリセット
			createBlock(temp);//Block生成をコール
			IdBlock++;//ID更新
		}

		if (CounterItem != 0) //0でなければデクリメント
		{                       //そうしないとuintは0-1=4???????になってしまうから次のif文がうまく回らない
			CounterItem--;
		}
		if (PermitCreateItem && (CounterItem <= 0))
		{
			CounterItem = 3;//カウンタをリセット
			createItem(temp);//Item生成をコール
		}
	}


	/********************************************************************************/
	/* 関数名	: Road生成															*/
	/* 備考		: 最新のRoadのPos+長さの座標に新しくRoadを生成。					*/
	/********************************************************************************/
	private GameObject createRoad()
	{
		float posXLatestRoad = 0;//最新Roadの位置
		Vector3 posCreateRoad;//生成位置
		float scaleBeforeRoad = 0;//Roadの長さを取得
		IdLatestRoad++;

		// ヒエラルキーから最新Roadを取得
		uint i = IdOldestRoad;//do-while文用カウンタ(一番古いRoadから探索)
		do
		{
			posXLatestRoad = GameObject.Find("Road" + i).transform.position.x;//最新Roadの座標を取得
			scaleBeforeRoad = GameObject.Find("Road" + i).transform.localScale.x;//最新Roadの長さを取得
			i++;
		} while (GameObject.Find("Road" + i));//戻り値がnull=存在しない、なら探索終了
		posCreateRoad = new Vector3(posXLatestRoad + scaleBeforeRoad,-0.5f,0);//生成位置 = 最新Road.pos + 長さ

		GameObject prefab = (GameObject)Resources.Load("Prefabs/Road");
		GameObject objCreate = Instantiate(prefab, posCreateRoad, Quaternion.identity);//生成オブジェクト = instanciate("Roads")
		objCreate.name = "Road" + IdLatestRoad;//生成オブジェクト名 = "Road" + id

		return objCreate;//Target生成の際に座標を使わせてあげる
	}

	/********************************************************************************/
	/* 関数名	: 最古id更新														*/
	/* 備考		: DeleteOldObjects.csから呼ばれる。									*/
	/********************************************************************************/
	public void UpdateIdOldestRoad()
	{
		IdOldestRoad++;
	}

	/********************************************************************************/
	/* 関数名	: Wall生成															*/
	/* 備考		: Roadに１こ生成。													*/
	/********************************************************************************/
	private void createWall(GameObject objRef)
	{
		Vector3 posCreateTarget = new Vector3(0, 0, 0);//生成位置(Wallは３つの子をまとめて使ってるから注意

		posCreateTarget.x = objRef.transform.position.x - 30;//Roadと同じx座標で生成するために調整(空オブジェクトでまとめてるから座標がずれてる)
		GameObject prefab = (GameObject)Resources.Load("Prefabs/Wall");
		GameObject objCreate = Instantiate(prefab, posCreateTarget, Quaternion.identity);// 生成オブジェクト = instanciate("Wall")
		objCreate.name = "Wall";// 生成オブジェクト名 = "Wall"
	}

	/********************************************************************************/
	/* 関数名	: Target生成														*/
	/* 備考		: Roadに１こ生成。													*/
	/********************************************************************************/
	private void createTarget(GameObject objRef)
	{
		Vector3 posCreateTarget=new Vector3(0,5,30);//生成位置
		Quaternion rotCreateTarget = Quaternion.Euler(0, 0, 45);//生成時の回転指定

		posCreateTarget.x = (objRef.transform.position.x - (objRef.transform.localScale.x / 2)) + Random.Range(0, objRef.transform.localScale.x);//生成位置=Roadの原点寄り端座標+Roadの長さ内でのランダム値
		GameObject prefab = (GameObject)Resources.Load("Prefabs/Target");
		GameObject objCreate = Instantiate(prefab, posCreateTarget, rotCreateTarget);// 生成オブジェクト = instanciate("Target")
		objCreate.name = "Target";// 生成オブジェクト名 = "Target"
	}

	/********************************************************************************/
	/* 関数名	: Block生成															*/
	/* 備考		: 3回に1回呼ばれる。もしtargetに近かったら削除(したい)。			*/
	/********************************************************************************/
	private void createBlock(GameObject objRef)
	{
		Vector3 posCreateBlock = new Vector3(0, 0, 0);//生成位置

		float randomRangeMin = objRef.transform.position.x + (objRef.transform.localScale.x / 2);//ランダム値下限=最新Roadのx軸正方向側端座標(じゃないと生成位置が近すぎる)
		float randomRangeMax = randomRangeMin + (objRef.transform.localScale.x * 4);//適当な長さ
		posCreateBlock.x = Random.Range(randomRangeMin, randomRangeMax);//ランダム生成座標生成
		GameObject prefab = (GameObject)Resources.Load("Prefabs/Block");
		GameObject objCreate = Instantiate(prefab, posCreateBlock, Quaternion.identity);// 生成オブジェクト = instanciate("Block")
		objCreate.name = "Block";//(clone)をつけられたくないから

		resetPermitCreateBlock();//ブロック生成許可を解除
	}

	/********************************************************************************/
	/* 関数名	: Block生成許可設定													*/
	/* 備考		: DeleteBlock.csから呼ばれる。ブロックを削除したときに呼ばれる。	*/
	/********************************************************************************/
	public void SetPermitCreateBlock()
	{
		PermitCreateBlock = true;
	}

	/********************************************************************************/
	/* 関数名	: Block生成許可解除													*/
	/* 備考		: ブロックを生成したときに呼ばれる。								*/
	/********************************************************************************/
	private void resetPermitCreateBlock()
	{
		PermitCreateBlock = false;
	}

	/********************************************************************************/
	/* 関数名	: Item生成															*/
	/* 備考		: 5回に1回呼ばれる。												*/
	/********************************************************************************/
	private void createItem(GameObject objRef)
	{
		Vector3 posCreateItem = new Vector3(0, 5, 30);//生成位置

		float randomRangeMin = objRef.transform.position.x + (objRef.transform.localScale.x / 2);//ランダム値下限=最新Roadのx軸正方向側端座標(じゃないと生成位置が近すぎる)
		float randomRangeMax = randomRangeMin + (objRef.transform.localScale.x * 4);//適当な長さ
		posCreateItem.x = Random.Range(randomRangeMin, randomRangeMax);//ランダム生成座標生成
		GameObject prefab = (GameObject)Resources.Load("Prefabs/Item");
		GameObject objCreate = Instantiate(prefab, posCreateItem, Quaternion.identity);// 生成オブジェクト = instanciate("Block")
		objCreate.name = "Item";//(clone)をつけられたくないから

		resetPermitCreateItem();//Item生成許可を解除
	}

	/********************************************************************************/
	/* 関数名	: Item生成許可設定													*/
	/* 備考		: DeleteBlock.csから呼ばれる。itemを削除したときに呼ばれる。		*/
	/********************************************************************************/
	public void SetPermitCreateItem()
	{
		PermitCreateItem = true;
	}

	/********************************************************************************/
	/* 関数名	: Item生成許可解除													*/
	/* 備考		: ブロックを生成したときに呼ばれる。								*/
	/********************************************************************************/
	private void resetPermitCreateItem()
	{
		PermitCreateItem = false;
	}

	/********************************************************************************/
	/* 関数名	: BlockID取得														*/
	/********************************************************************************/
	public int GetIdBlock()
	{
		return IdBlock;
	}
}
