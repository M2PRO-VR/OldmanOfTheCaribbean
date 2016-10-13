using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{

    public GameObject Enemy_target;
    private GameObject after_Enemy_target;  //近づくとターゲット変更する変数
    private float movedistance = 50; //感知距離
    private float returndistance = 30;  //回帰距離
    public GameObject PoP_position; //POP位置保存

    private bool re_turnflg = false; //回帰したらRePOPする。

    public Transform HumanTypeEnemyPosition;
    NavMeshAgent agent;
    Animator animator;
    //攻撃力関係
    public float EnemyDamage;

    private bool attack = false;
    //距離関係
    private float distance;
    private float mindistance = 2;
    //攻撃関係
    private float nextAttack;
    private float AttackRate = 3;
    //攻撃モーション関係
    private static bool AtkStartflg = true;
    //never used: EnemyAttackTrigger enemytrigger;
    public static bool Actionflg = true;
    //吹っ飛び攻撃(3回目のパンチ)関係
    private int goawayID; //stateのID
    public static bool Threeflg = false;

    //敵の死んだフラグ
    public bool DIEDFLG = false;

    //敵のスキルフラグ
    private bool encount_on = false;    //敵と会った時タイマーオン
    private bool skill_Flg = false;     //30秒(初期値)クールでスキル発生
    private float intervalTime = 0.0f;
    private float setSkillTime = 20.0f;
    public bool UseSkill = false;   //EnemyPOPから値受け取る(スキルを使う敵か使わない敵か判断)
    private GameObject CauseSkillObj;   //スキル発生装置（空のオブジェクト）

    private bool enemycureflg =false;  //WeaponAttackに回復フラグを送る

    // Use this for initialization
    void Start()
    {
        after_Enemy_target = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        HumanTypeEnemyPosition = transform;
        //never used: enemytrigger = GetComponent<EnemyAttackTrigger>();
        goawayID = Animator.StringToHash("Base Layer.Attack3");

        if (UseSkill == true)
        {
            CauseSkillObj = gameObject.transform.FindChild("EnemyCauseSkill").gameObject;
            CauseSkillObj.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(DIEDFLG);
        distance = Vector3.Distance(after_Enemy_target.transform.position, HumanTypeEnemyPosition.position);


        if (distance < movedistance)
        {
            Enemy_target = after_Enemy_target;  //ターゲットをプレイヤーに変更
            BigHumanSkill_Attract.Ene_target_flg = true;

            encount_on = true;
        }

        if (distance > returndistance && Enemy_target == after_Enemy_target)
        {
            Enemy_target = PoP_position;    //回帰する
            BigHumanSkill_Attract.Ene_target_flg = false;

            encount_on = false;
            intervalTime = 0.0f;

            this.tag = "RETURN_REPOP";
            Invoke("return_move", 5.0f);    //5秒後、POP位置に瞬間移動
            re_turnflg = true;
        }

        if (re_turnflg == true)
        {
            Invoke("r_ePOP", 5.0f); //同じく5秒後、敵状態初期化
            re_turnflg = false;
        }

        /*-------------------------------------------------------------------------------------------------------------------------*/
        if (encount_on == true)
        {
            intervalTime += Time.deltaTime; //スキルクール時間
        }

        if (intervalTime >= setSkillTime)
        {
            skill_Flg = true;
            enemycureflg = true;
            //Debug.Log("skill");
        }
        /*-------------------------------------------------------------------------------------------------------------------------*/
        //ターゲットとの距離が指定距離以内になった時攻撃開始
        if ((distance < mindistance) && (Time.time > nextAttack) && (AtkStartflg == true) && (this.tag == "Enemy"))
        {
            transform.LookAt(Enemy_target.transform.position);
            nextAttack = Time.time + AttackRate; //3秒足す
            //Attack開始
            attack = true;


            if (DIEDFLG == true)    //死んでいるならば攻撃やめる
            {
                attack = false;
                animator.SetBool("Attack", attack);
            }


            if (skill_Flg == false || UseSkill == false)    //スキル持たない敵の場合orスキルフラグじゃない場合
            {
                animator.SetBool("Attack", attack);
                EnemyAttackTrigger.AnimationNow(Actionflg);
                //3回目パンチは吹っ飛ばすフラグONします
                Invoke("Threeway", 1.0f);
                //Attack終了
                Invoke("Attack", 1.5f);
            }
            else if (skill_Flg == true && UseSkill == true)
            {
                animator.SetBool("Skill", skill_Flg);
                Invoke("EnemyHPRestore", 1.0f);
                agent.speed = 0;
                Invoke("EnemySkill", 2.5f);

                intervalTime = 0.0f;
            }
        }
        else if ((distance < mindistance + 15) && (Time.time > nextAttack) && (AtkStartflg == true) && (this.tag == "Enemy"))
        {
            //至近距離じゃない場合のスキル発動条件(2 + 15m)
            if (skill_Flg == true && UseSkill == true)  
            {
                animator.SetBool("Skill", skill_Flg);
                Invoke("EnemyHPRestore", 1.0f);
                agent.speed = 0;
                Invoke("EnemySkill", 2.5f);

                intervalTime = 0.0f;
            }
        }


            //ターゲットに向かって移動する。
            if (this.tag == "Enemy")
        {
            agent.destination = Enemy_target.transform.position;
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        else if (this.tag == "RETURN_REPOP")
        {
            agent.destination = Enemy_target.transform.position;
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        else if (DIEDFLG == true)
        {
            agent.destination = Enemy_target.transform.position;
            agent.speed = 0;
            animator.SetFloat("Speed", 0);
        }
        else if (this.tag == "DIED")
        {
            agent.destination = Enemy_target.transform.position;
            agent.speed = 0;
            animator.SetFloat("Speed", 0);
        }
    }

    public static void Atkflg(bool Atkflg)
    {
        AtkStartflg = Atkflg;
        //Debug.Log(AtkStartflg);
    }

    public void FallingAnimation(bool fallingflg)
    {
        if (this.tag == "Enemy" && DIEDFLG == false &&UseSkill == false)
        {
            agent.destination = Enemy_target.transform.position;
            agent.speed = 0;
            animator.SetBool("Falling", true);
            animator.SetFloat("Speed", 0);
            Invoke("speedup", 3.0f);
        }
    }

    private void speedup()
    {
        agent.speed = 3;
        animator.SetBool("Falling", false);
    }

    private void Attack()
    {
        if (attack == true && this.tag != "DIED")
        {
            attack = false;
            animator.SetBool("Attack", attack);
        }
    }

    private void EnemySkill()   //スキルのアニメーション終了
    {
        if (skill_Flg == true)
        {
            CauseSkillObj.SetActive(true);
            BigHumanSkillAttack.EnemySkill_firstflg = true;

            skill_Flg = false;
            animator.SetBool("Skill", skill_Flg);
            agent.speed = 5;
        }
    }

    private void EnemyHPRestore()
    {
        if (enemycureflg == true)
        {
            WeaponAttack tr = gameObject.GetComponent<WeaponAttack>();  //回復フラグ送る
            tr.EnemyRestoreflg = true;
            enemycureflg = false;
        }
    }

    private void Threeway()
    {
        AnimatorStateInfo Goawayanim = animator.GetCurrentAnimatorStateInfo(0);
        if (Goawayanim.fullPathHash == goawayID)
        {
            Threeflg = true;
            //Debug.Log(Threeflg);
            EnemyAttackTrigger.Goaway(Threeflg);
            Invoke("StopThreeflg", 1.0f);
        }
    }

    private void StopThreeflg()
    {
        Threeflg = false;
        EnemyAttackTrigger.Goaway(Threeflg);
        //Debug.Log(Threeflg);
    }

    private void return_move()
    {
        gameObject.transform.position = PoP_position.transform.position;
    }

    private void r_ePOP()
    {
        gameObject.SetActive(false);
        PoP_position.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
