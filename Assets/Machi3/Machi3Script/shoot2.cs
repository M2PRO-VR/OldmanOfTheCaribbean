using UnityEngine;
using System.Collections;
using Leap;

public class shoot2 : MonoBehaviour
{

    //投げる前フラグ
    private bool flg = false;
    //投げますフラグ
    private bool Throwflg = false;
    private float Speed = 10f;
    private bool Rayflg = false;
    private Vector3 Raypoint;

    //時間でナイフ消えますフラグ
    private bool DestroyTimeflg = false;
    private float intervalTime = 0;
    //敵オブジェクト自動探索
    //public GameObject ReachPoint;

    //ナイフを持っている時の手オブジェクト
    //private GameObject hand_palm;

    //爆発エフェクトゲームオブジェクト
    public GameObject bakuhatu;
    private GameObject bakuhatuCopy;

    // Use this for initialization
    void Start()
    {        
        //hand_palm = GameObject.Find("RigidRoundHand_R").gameObject.transform.FindChild("palm").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (Throwflg == false)
        {
            flg = true;
        }

        //Debug.Log(hand_palm.transform.localEulerAngles);

        /*if (Throwflg == false && DestroyTimeflg == false)
        {
            transform.position = hand_palm.transform.position;
            transform.position = transform.localPosition + new Vector3(-0.1f, 0, 0);
            transform.eulerAngles = hand_palm.transform.eulerAngles + new Vector3(0, 0, 180);
        }*/

        //投げるタイミング
        if (flg == true)
        {
            flg = false;
            Throwflg = true;
            DestroyTimeflg = true;
        }

        if (Throwflg == true)
        {
            if (Rayflg == false)
            {
                //投げ部分（書き換え検討場所）
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //カメラ座標から前方向にレイを飛ばす
                Raypoint = ray.GetPoint(100);
                Rayflg = true;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().velocity = (Raypoint - transform.position).normalized * Speed; //getpointで１００ｍ地点からカメラ座標を引いて，距離（方向）を出して正規化した後に任意の速度をかける
            }
            //transform.LookAt(ray.GetPoint(100));

            //Vector3 newRotation = Quaternion.LookRotation(transform.position - Raypoint).eulerAngles;
            //transform.rotation = Quaternion.Euler(newRotation);

            //transform.Translate(transform.forward * KnifeSpeed * Time.deltaTime);*/
            //Rigidbody rigidbody = GetComponent<Rigidbody>();
            //rigidbody.AddForce(Mathf.Cos(30), Mathf.Sin(60)*5f, 1f,ForceMode.Acceleration); 

        }

        if (DestroyTimeflg == true)
        {
            intervalTime += Time.deltaTime;

            //1秒後ナイフ装填可能にする
            if (intervalTime >= 1.0f)
            {
                Throwflg = false;
            }

            //3.0秒後object消す
            if (intervalTime >= 3.0f)
            {
                bakuhatuCopy = (GameObject)Instantiate(bakuhatu, transform.position, transform.rotation);
                Destroy(gameObject);
                DestroyTimeflg = false;
                intervalTime = 0;
            }
        }

    }
}
