using UnityEngine;
using System.Collections;

public class Target_Cursor : MonoBehaviour {

    public static GameObject TARGET_CURSOR; //表示・非表示用
    private GameObject enemy_root;  //矢印オブジェクト

    private float targetDistance;   //カーソルとプレイヤーの距離
    private GameObject enemytargett; //プレイヤーオブジェクト
    private float appointDistance = 25; //自動HP 可視化範囲距離
    private bool noneEnemy;

	// Use this for initialization
	void Start () {
        enemy_root = gameObject.transform.FindChild("enemy_point").gameObject;
        enemy_root.gameObject.SetActive(false);

        enemytargett = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (enemy_root.gameObject == TARGET_CURSOR)
        {
            TARGET_CURSOR.gameObject.SetActive(true);
            
            //プレイヤーとの距離を検索
            targetDistance = Vector3.Distance(TARGET_CURSOR.gameObject.transform.position, enemytargett.gameObject.transform.position);
            if(targetDistance > appointDistance) //一定距離離れた場合
            {
                noneEnemy = true;
                TARGET_CURSOR = null;   //カーソル非表示
                EnemyHpBarCtrl.RangeEnemy(noneEnemy);   //右上HP非表示
                noneEnemy = false;
            }
            
        }
        else {
            enemy_root.gameObject.SetActive(false);
        }
	}
}
