using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponAttack : MonoBehaviour {
    public static float WeaponDamageAmount = 50;   //武器のダメージ量　ChangeWeaponsKeyから値受け取り
	private float kickdamage= 20f;
    private bool WeaponDamageflg = false;
	public static bool Kickflg=false;
    private float EnemyMaxHp;   //Infoから最大HP受け取る
    private float EnemyCurrentHp; //InfoからHP受け取る
    private float BeforeCurrentHp;
    public static bool WeaponActionFlg = false;
    private bool DestroyEnemyPOP = false;
    private float GiftEnemyMaxHp; //Info変数
    private float GiftEnemyCurrentHp; //Info変数
    

    //HPバー表示の変数に送る為の変数
    public static float damaGe;
    public static bool dmFlg;
    public static float maxEnemyhp;
    public static float enemyHp;
    public static bool KOROSU = false;

    //Tabで敵別々に表示する関係の変数
    private static GameObject[] enemys;
    private float Enemyhpp;
    private float maxEnemyhpp;
    private int i = 0;
    private float j = 0, k = 0;
    public static float numberof_Enemy;
    private bool dmaaflg = true;
    private float dmmma = 0;
    private GameObject enemytargett;
    private float targetDistance = 0;   //プレイヤーとの距離変数 
    private float appointDistance = 15; //指定範囲距離内のHP可視化変数
    private bool whilebol = true;
    private int storeflg;
    private bool noneEnemy=false;
    
    //死んだときのリポップ変数
    public GameObject rePoP;
    private float intervalTime = 0.0f;  //死ぬまでの時間（初期値3秒）
    private float set_DIETIME = 0.0f;

    //ターゲットカーソル表示用
    private GameObject targetCursor;
    //エフェクト用
	private GameObject effect;

    //敵の小さいHPバー用
    public Slider sub_hpbar; //enemyと一緒に映る名前下のSlider(e_hpbarSliderから中身受け取る)
    private float after_DAMEGE;
    private bool smallhp_flg;

    //色変更用
    public GameObject MaterialObj;  //マテリアルのついたオブジェクト
    private GameObject MaterialFinding; //そのオブジェクトのマテリアル格納

    //敵回復フラグ受け取り変数
    public bool EnemyRestoreflg = false;
    private bool Restoreflg = false;
    private float EnemyCureAmount = 250.0f;   //回復量　50
    public static float enecure;
    public static bool enecureFlg;
    private bool smallhp_restoreflg;
    private float Before_Restore;
    public GameObject EnemyDie; //敵死亡エフェクト

    // Use this for initialization
    void Start () {
        EnemyMaxHp = GiftEnemyMaxHp;
        EnemyCurrentHp = GiftEnemyCurrentHp;

        dmFlg = WeaponDamageflg;
        maxEnemyhp = EnemyMaxHp;
        enemyHp = EnemyCurrentHp;
        BeforeCurrentHp = EnemyCurrentHp;

        //Tabボタンで好きな敵のHP表示変数
        Enemyhpp = EnemyCurrentHp;
        maxEnemyhpp = EnemyMaxHp;
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        numberof_Enemy = enemys.Length;
        enemytargett = GameObject.FindGameObjectWithTag("Player");

        MaterialFinding = gameObject.transform.FindChild(MaterialObj.name).gameObject;
    }

    // Update is called once per frame
    void Update() {	
        //敵の死ぬ処理
        if (EnemyCurrentHp <= 0 && KOROSU == true)
        {
            intervalTime += Time.deltaTime;
            this.tag = ("DIED");

            DiedColor died_color = MaterialFinding.gameObject.GetComponent<DiedColor>();
            died_color.diedcolor_flg = true;

            if (intervalTime >= set_DIETIME)
            {
                //敵死亡エフェクト呼び出し場所
				//effect.SetActive(true);
                Invoke("EnemyDie_FadeOut", 0.0f);
                Invoke("re_POP", 600.0f);
                System.Array.Clear(enemys, 0, enemys.Length);
                enemys = GameObject.FindGameObjectsWithTag("Enemy");
                numberof_Enemy -= 1;
                intervalTime = 0.0f;
            }
        }


        //Tabボタンで好きな敵のHP表示
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            if (i >= numberof_Enemy)
            {
                i = 0;
            }

            //プレイヤーとの距離を検索
            targetDistance = Vector3.Distance(enemys[i].gameObject.transform.position, enemytargett.gameObject.transform.position);

            //指定範囲内であれば
            if (targetDistance <= appointDistance)
            {
                targetCursor = enemys[i].gameObject.transform.FindChild("enemy_point").gameObject;
                Target_Cursor.TARGET_CURSOR = targetCursor;
                EnemyHpBarCtrl.EnemyDamage(dmmma, dmaaflg, enemys[i].gameObject.GetComponent<WeaponAttack>().maxEnemyhpp, enemys[i].gameObject.GetComponent<WeaponAttack>().Enemyhpp);
                Debug.Log("距離:" + targetDistance + "\nhp:" + enemys[i].gameObject.GetComponent<WeaponAttack>().Enemyhpp);
                //Debug.Log(enemys[i].gameObject.GetComponent<WeaponAttack>().maxEnemyhpp);

                i += 1;
            }
            else
            {
                storeflg = i;
                while (whilebol)
                {

                    //プレイヤーとの距離を検索
                    targetDistance = Vector3.Distance(enemys[i].gameObject.transform.position, enemytargett.gameObject.transform.position);
                    if (targetDistance <= appointDistance)
                    {
                        targetCursor = enemys[i].gameObject.transform.FindChild("enemy_point").gameObject;
                        Target_Cursor.TARGET_CURSOR = targetCursor;
                        EnemyHpBarCtrl.EnemyDamage(dmmma, dmaaflg, enemys[i].gameObject.GetComponent<WeaponAttack>().maxEnemyhpp, enemys[i].gameObject.GetComponent<WeaponAttack>().Enemyhpp);
                        //Debug.Log("距離:" + targetDistance + "\nhp:" + enemys[i].gameObject.GetComponent<WeaponAttack>().Enemyhpp);
                        //Debug.Log(enemys[i].gameObject.GetComponent<WeaponAttack>().maxEnemyhpp);
                        whilebol = false;
                        break;
                    }
                    i += 1;
                    if (i >= numberof_Enemy)
                    {
                        i = 0;
                    }
                    if (storeflg == i)
                    {
                        //指定範囲以内に敵がいませんでした
                        whilebol = false;
                        noneEnemy = true;
                        Target_Cursor.TARGET_CURSOR = null;
                        EnemyHpBarCtrl.RangeEnemy(noneEnemy);
                        noneEnemy = false;
                        break;
                    }
                }

                whilebol = true;
            }

        }

        //hpバー処理
        if (smallhp_flg == true)
        {
                sub_hpbar.value = sub_hpbar.value - 1;
                j--;
                if (j == after_DAMEGE)
                {
                    sub_hpbar.value = after_DAMEGE;
                    smallhp_flg = false;
                    j = 0;
                }
        }

        //subhpバー回復処理
        if (smallhp_restoreflg == true)
        {
            sub_hpbar.value += sub_hpbar.value/100;
            k += sub_hpbar.value/100;
            if (k >= Before_Restore)
            {
                sub_hpbar.value = Before_Restore;
                smallhp_restoreflg = false;
                k = 0;
            }
        }

        if (EnemyCurrentHp >= EnemyMaxHp)
        {
            EnemyRestoreflg = false;
        }

        //回復処理
        if (EnemyRestoreflg == true)
        {
            RestoreProcess(EnemyCureAmount);
            EnemyRestoreflg = false;
        }
        


    }



    void OnTriggerEnter(Collider WeaponAttackHit)
    {
        //接触対象はこのタグですか？
        if (WeaponAttackHit.gameObject.tag == ("WeaponAttack") /*&& (WeaponActionFlg == true)*/)
        {
            DamageProcess(WeaponDamageAmount);
        }
		/*if(WeaponAttackHit.gameObject.tag == "kick" && Kickflg == true && WeaponActionFlg == false){
			DamageProcess (kickdamage);
            Kickflg = false;
		}*/
    }

    public void DamageProcess(float DAMAGE) {
        if (this.tag == "Enemy")
        {
            //物理・スキル攻撃　が当たった時カーソル表示
            targetCursor = gameObject.transform.FindChild("enemy_point").gameObject;
            Target_Cursor.TARGET_CURSOR = targetCursor;
            
            //ダメージ処理
            BeforeCurrentHp = EnemyCurrentHp;
            EnemyCurrentHp = EnemyCurrentHp - DAMAGE;
            WeaponActionFlg = false;
            WeaponDamageflg = true;
            damaGe = DAMAGE;
            dmFlg = WeaponDamageflg;
            enemyHp = BeforeCurrentHp;

            smallhp_flg = true;

            j = BeforeCurrentHp;
            after_DAMEGE = EnemyCurrentHp;

            EnemyHpBarCtrl.EnemyDamage(damaGe, dmFlg, EnemyMaxHp, enemyHp);
            //Debug.Log(maxEnemyhp);
            WeaponDamageflg = false;

            Enemyhpp = EnemyCurrentHp;
        }
    }

    public void RestoreProcess(float CURE)
    {
        if (this.tag == "Enemy")
        {
            //ダメージ処理
            BeforeCurrentHp = EnemyCurrentHp;
            EnemyCurrentHp = EnemyCurrentHp + CURE;
            if(EnemyCurrentHp >= EnemyMaxHp)
            {
                EnemyCurrentHp = EnemyMaxHp;
            }

            WeaponActionFlg = false;    //少し無敵
            Restoreflg = true;
            enecure = CURE;
            enecureFlg = Restoreflg;
            enemyHp = BeforeCurrentHp;

            smallhp_restoreflg = true;

            k = BeforeCurrentHp;
            Before_Restore = EnemyCurrentHp;

            EnemyHpBarCtrl.EnemyRestore(enecure, enecureFlg, EnemyMaxHp, enemyHp);
            //Debug.Log(maxEnemyhp);
            Restoreflg = false;

            Enemyhpp = EnemyCurrentHp;
        }
    }

    public static void WeaponAnimationNow(bool WeaponAnm)
    {
        WeaponActionFlg = WeaponAnm;    //キャラからアクション中のフラグtrueもらう
    }
	public static void KickAnimationNow(bool kick){
		Kickflg = kick;
	}
    public static void EnemyHP(bool EnemyHpflag)
    {
        //オペリアスクリプトから敵の更新されたhp受け取る
        KOROSU = EnemyHpflag;
    }

    public void EnemyInfo_MaxHP(float enemy_max) {
        GiftEnemyMaxHp = enemy_max;
        //Debug.Log(enemy_max);
    }

    public void EnemyInfo_CurrentHP(float enemy_current_hp) {
        GiftEnemyCurrentHp = enemy_current_hp;
    }

    private void re_POP() {
        rePoP.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    private void EnemyDie_FadeOut()
    {
        effect = (GameObject)Instantiate(EnemyDie, gameObject.transform.position, gameObject.transform.rotation);
        gameObject.SetActive(false);
        Invoke("die_effect", 1.0f);
    }

    private void die_effect()
    {
        Destroy(effect.gameObject);
    }

}
