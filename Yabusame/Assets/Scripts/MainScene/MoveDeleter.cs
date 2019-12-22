using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDeleter : MonoBehaviour
{
	/********************************************************************************/
	/*内部変数																		*/
	/********************************************************************************/
	private PlayerIF InterfacePlayer;
	private GameObject Deleter;

	// Start is called before the first frame update
	void Start()
    {
		InterfacePlayer = GameObject.Find("InfoInterface").GetComponent<PlayerIF>();//オブジェクトからアタッチ
		Deleter = GameObject.Find("Deleter");
	}

    // Update is called once per frame
    void Update()
    {
		float posXPlayer = InterfacePlayer.GetPlayerPos().x;//Playerの位置を取得
		Deleter.transform.position = new Vector3(posXPlayer - 100,Deleter.transform.position.y,Deleter.transform.position.z);//BlockDeleterの位置を更新
	}
}