using UnityEngine;
using System.Collections;

public class BigHumanSkill_Attract : MonoBehaviour {

    public static bool Ene_target_flg = false;    //敵のターゲットがプレイヤーならtrue 違うならfalse
    //↑EnemyControllerから受け取り


    private GameObject Player;  //Playerオブジェクト

    private float distance; //この敵とプレイヤーの距離
    private float att_distance; //引っ張ってる時の敵とプレイヤーの距離

    private float Attract_distance = 15f; //敵がプレイヤーを引っ張り始める距離
    private float after_att_distance = 3f;   //敵がプレイヤーをどこまでひっぱるか

    private bool Attract_flg = false;
    private bool Attract_flg2 = false;
    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if(Ene_target_flg == true)
        {
            distance = Vector3.Distance(Player.gameObject.transform.position, gameObject.transform.position);

            if (distance >= Attract_distance && distance <= Attract_distance + 5f)
            {
                Attract_flg = true;
            }


            if(Attract_flg == true)
            {
                //引っ張る処理
                RaycastHit hit;
                gameObject.transform.LookAt(Player.gameObject.transform.position);
                Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);

                if (Physics.Raycast(ray, out hit, 20.0f))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        Attract_flg2 = true;
                        Attract_flg = false;
                    }
                }
            }

            if(Attract_flg2 == true)
            {
                att_distance = Vector3.Distance(Player.gameObject.transform.position, gameObject.transform.position);
                if (att_distance >= after_att_distance)
                {
                    Player.gameObject.transform.position = Player.gameObject.transform.position - gameObject.transform.forward;
                }
                else
                {
                    Attract_flg2 = false;
                }
            }

        }

    }
}
