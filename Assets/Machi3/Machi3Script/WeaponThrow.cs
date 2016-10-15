using UnityEngine;
using System.Collections;

public class WeaponThrow : MonoBehaviour {

    //掴んだフラグ
    private bool flg = false;
    private bool flg1 = false;
    private bool flg2 = false;
    private bool flg3 = false;
    private bool flg4 = false;
    //離したフラグ
    private bool Releaseflg1 = false;
    private bool Releaseflg2 = false;
    private bool Releaseflg3 = false;
    private bool Releaseflg4 = false;
    //投げますフラグ
    private bool Throwflg = false;
    private float KnifeSpeed = 1f;
    //時間でナイフ消えますフラグ
    private bool DestroyTimeflg = false;
    private float intervalTime = 0;
    //敵オブジェクト自動探索
    //public GameObject ReachPoint;

    //ナイフを持っている時の手オブジェクト
    private GameObject hand_palm;

    // Use this for initialization
    void Start () {
        //ReachPoint = GameObject.FindGameObjectWithTag("Enemy");
        hand_palm = GameObject.Find("RigidRoundHand_R").gameObject.transform.FindChild("palm").gameObject;
        transform.eulerAngles = hand_palm.transform.eulerAngles + new Vector3(180, 0, 0); ;
    }
	
	// Update is called once per frame
	void Update () {

        if (Throwflg == false && WeaponHold.Trackflg == true && DestroyTimeflg == false)
        {
            transform.position = hand_palm.transform.position;
            transform.position = transform.localPosition + new Vector3(0, -0.05f, 0);
            transform.eulerAngles = hand_palm.transform.eulerAngles + new Vector3(180, 0, 0); ;
        }

        if (flg1 == true && flg2 == true && flg3 == true && flg4 == true)
        {
            flg = true;
        } 

        if(flg == true && Releaseflg1 == true && Releaseflg2 == true && Releaseflg3 == true && Releaseflg4 == true)
        {
            flg = false;
            WeaponHold.Trackflg = false;
            Throwflg = true;
            DestroyTimeflg = true;
        }

        if(Throwflg == true)
        {
            //投げ部分（書き換え検討場所）
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //カメラ座標から前方向にレイを飛ばす
            GetComponent<Rigidbody>().velocity = (ray.GetPoint(100) - transform.position).normalized * KnifeSpeed; //getpointで１００ｍ地点からカメラ座標を引いて，距離（方向）を出して正規化した後に任意の速度をかける
            transform.LookAt(ray.GetPoint(100)); 
            //transform.Translate(transform.forward * KnifeSpeed * Time.deltaTime);
            KnifeSpeed += 0.35f;
        }

        if(DestroyTimeflg == true)
        {
            intervalTime += Time.deltaTime;
            
            //1秒後ナイフ装填可能にする
            if (intervalTime >= 1.0f)
            {
                Throwflg = false;
            }

            //3.0秒後ナイフ消す
            if (intervalTime >= 3.0f)
            {
                Destroy(gameObject);
                DestroyTimeflg = false;
                intervalTime = 0;
            }
        }

	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "bone3" && col.gameObject.transform.parent.gameObject.name == "index" && col.gameObject.transform.parent.gameObject.transform.parent.gameObject.name == "RigidRoundHand_R")
        {
            Releaseflg1 = false;
            //Debug.Log("index");
        }

        if (col.gameObject.name == "bone3" && col.gameObject.transform.parent.gameObject.name == "middle" && col.gameObject.transform.parent.gameObject.transform.parent.gameObject.name == "RigidRoundHand_R")
        {
            Releaseflg2 = false;
            //Debug.Log("middle");
        }

        if (col.gameObject.name == "bone3" && col.gameObject.transform.parent.gameObject.name == "pinky" && col.gameObject.transform.parent.gameObject.transform.parent.gameObject.name == "RigidRoundHand_R")
        {
            Releaseflg3 = false;
            //Debug.Log("pinky");
        }

        if (col.gameObject.name == "bone3" && col.gameObject.transform.parent.gameObject.name == "ring" && col.gameObject.transform.parent.gameObject.transform.parent.gameObject.name == "RigidRoundHand_R")
        {
            Releaseflg4 = false;
            //Debug.Log("ring");
        }

        if(col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "bone3" && col.gameObject.transform.parent.gameObject.name == "index" && col.gameObject.transform.parent.gameObject.transform.parent.gameObject.name == "RigidRoundHand_R")
        {
            flg1 = true;
            Debug.Log("index");
        }

        if (col.gameObject.name == "bone3" && col.gameObject.transform.parent.gameObject.name == "middle" && col.gameObject.transform.parent.gameObject.transform.parent.gameObject.name == "RigidRoundHand_R")
        {
            flg2 = true;
            Debug.Log("middle");
        }

        if (col.gameObject.name == "bone3" && col.gameObject.transform.parent.gameObject.name == "pinky" && col.gameObject.transform.parent.gameObject.transform.parent.gameObject.name == "RigidRoundHand_R")
        {
            flg3 = true;
            Debug.Log("pinky");
        }

        if (col.gameObject.name == "bone3" && col.gameObject.transform.parent.gameObject.name == "ring" && col.gameObject.transform.parent.gameObject.transform.parent.gameObject.name == "RigidRoundHand_R")
        {
            flg4 = true;
            Debug.Log("ring");
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "bone3" && col.gameObject.transform.parent.gameObject.name == "index" && col.gameObject.transform.parent.gameObject.transform.parent.gameObject.name == "RigidRoundHand_R")
        {
            flg1 = false;
            Releaseflg1 = true;
            //Debug.Log("index");
        }

        if (col.gameObject.name == "bone3" && col.gameObject.transform.parent.gameObject.name == "middle" && col.gameObject.transform.parent.gameObject.transform.parent.gameObject.name == "RigidRoundHand_R")
        {
            flg2 = false;
            Releaseflg2 = true;
            //Debug.Log("middle");
        }

        if (col.gameObject.name == "bone3" && col.gameObject.transform.parent.gameObject.name == "pinky" && col.gameObject.transform.parent.gameObject.transform.parent.gameObject.name == "RigidRoundHand_R")
        {
            flg3 = false;
            Releaseflg3 = true;
            //Debug.Log("pinky");
        }

        if (col.gameObject.name == "bone3" && col.gameObject.transform.parent.gameObject.name == "ring" && col.gameObject.transform.parent.gameObject.transform.parent.gameObject.name == "RigidRoundHand_R")
        {
            flg4 = false;
            Releaseflg4 = true;
            //Debug.Log("ring");
        }

    }

}
