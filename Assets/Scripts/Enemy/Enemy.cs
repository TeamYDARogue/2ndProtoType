using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private GameObject m_target;  //最も近いオブジェクト
	private float m_time = 0;     //経過時間

	void Start(){
		//最も近いオブジェクトを取得
		this.m_target = this.serchTag (gameObject, "Player");
	}

	void Update(){

		this.m_time += Time.deltaTime;   //経過時間を取得

		if (this.m_time >= 1.0f) {
			//最も近いオブジェクトを取得
			this.m_target = this.serchTag (gameObject, "Player");
			//経過時間を初期化
			this.m_time = 0;

			//移動のスピード
			this.changeDirection (this.m_target);  //向かう方向に向く
			this.transform.Translate (Vector3.forward * 2.00f);    //向かう方向に向かう
		}
	}

	//Playerの座標に応じて向きを変える
	void changeDirection(GameObject player){

		//PlayerとEnemyの座標差分を取得
		int m_xDistance = Mathf.RoundToInt (this.transform.position.x - player.transform.position.x);
		int m_zDistance = Mathf.RoundToInt (this.transform.position.z - player.transform.position.z);

		//向きたい角度
		int m_rotateDir = 0;

		//x座標とy座標の差分から向きたい角度を取得
		//PlayerとEnemyに距離がある場合
		if (m_xDistance == 0) {
			//x座標が同じ場合,z座標のみ向きを取得
			m_rotateDir = this.getDirection (m_zDistance, "z");
		} else if (m_zDistance == 0) {
			//z座標が同じ場合,x座標のみ向きを取得
			m_rotateDir = this.getDirection (m_xDistance, "x");
		} else {
			//どちらも差がある場合,ランダムで進む向きを取得
			int m_rand = UnityEngine.Random.Range (0, 2);
			if (m_rand == 0) {
				//z座標
				m_rotateDir = this.getDirection (m_zDistance, "z");
			} else {
				//x座標
				m_rotateDir = this.getDirection (m_xDistance, "x");
			}
		}

		//取得した方向にオブジェクトの向きを変える
		this.transform.rotation = Quaternion.Euler (0, m_rotateDir, 0);

	}

	//向きの角度を取得
	int getDirection(int distance,string axis){
		//距離がプラスかマイナスを取得
		int flag = distance > 0 ? 1 : 0;

		//角度を返す
		if (axis == "x") {
			return flag == 1 ? 270 : 90;
		} else {
			return flag == 1 ? 180 : 0;
		}
	}


	GameObject serchTag(GameObject nowObj,string tagName){

		float tmpDis = 0;     //距離用の一時変数
		float nearDis = 0;    //最も近いオブジェクトの距離
		GameObject targetObj = null;     //オブジェクト

		//タグ指定されたオブジェクトを配列で取得
		foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName)) {
			//Enemyと取得したオブジェクトの距離を取得
			tmpDis = Vector3.Distance (obs.transform.position, nowObj.transform.position);

			//オブジェクトの距離が近いか,距離0であればオブジェクト名を取得
			//一時変数に距離を格納
			if (nearDis == 0 || nearDis > tmpDis) {
				nearDis = tmpDis;
				targetObj = obs;
			}
		}

		//最も近いオブジェクトを返す
		return targetObj;
	}

	//当たり判定
	void OnTriggerEnter(Collider col){
		//Playerに当たった時
		if (col.gameObject.tag == "Player") {
			Debug.Log ("攻撃");
		}
	}

}
