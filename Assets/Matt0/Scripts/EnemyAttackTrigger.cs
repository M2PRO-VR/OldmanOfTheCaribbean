using UnityEngine;
using System.Collections;

public class EnemyAttackTrigger : MonoBehaviour
{
    //Unityちゃんにくっつけるスクリプトです
    //public static float DamageAmount
    private GameObject _enemyparent;

    private bool Damageflg = true;
    //never used: HpBarCtrl hpbarctrl;
    public static bool actionnow = false;
    public static bool goawayflg = false;
	private float goawayTime = 0f; //吹っ飛び時間

    public GameObject CameraObj;

    // Use this for initialization
    void Start()
    {
        //never used: hpbarctrl = GetComponent<HpBarCtrl>();
    }

    void FixedUpdate()
    {
        //transform.localEulerAngles = new Vector3(0, CameraObj.transform.localEulerAngles.y, 0);
        //Debug.Log(CameraObj.transform.localEulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider EnemyAttackHit)
    {
        //接触対象はこのタグですか？
        if (EnemyAttackHit.gameObject.tag == ("EnemyAttack") && (actionnow == true) && (HpBarCtrl.escapenow == false))
        {
            _enemyparent = EnemyAttackHit.gameObject.transform.root.gameObject;
            HpBarCtrl.Damage(_enemyparent.gameObject.GetComponent<EnemyController>().EnemyDamage, Damageflg);
            // Debug.Log("ok");
            Invoke("Stopflg", 1.5f); 
        }

        if (EnemyAttackHit.gameObject.tag == ("EnemyAttack") && (actionnow == true) && (goawayflg == true) && (HpBarCtrl.escapenow == false)) {
            // Debug.Log("goaway");    
            
            // 転倒フラグ送る場所
			//UnityChanController.DamageAnimationUnitychan(goawayflg);
		
            // trueしか送らないから、goawayflgでfalseの処理を使いたかったらキャラクタコントローラで切り替える
            EnemyController.Atkflg(false);
            Invoke("StopAtk", goawayTime);
        }

    }


    private void StopAtk() {
        EnemyController.Atkflg(true);
    }

    private void Stopflg()
    {
        actionnow = false;
    }

    public static void AnimationNow(bool anmnow)
    {
        actionnow = anmnow;
    }

    public static void Goaway(bool Threeflg)
    {
        goawayflg = Threeflg;
    }

    /*public static void EnemyDamageAmount(float EnemyDamageAmount) {
        GiftDamageAmount = EnemyDamageAmount;
    }*/
}
