using UnityEngine;
using System.Collections;

public class EnemyBowerPoP : MonoBehaviour {

    private GameObject enemy_initiate;
    public GameObject OldmanEnemy;  //出現させるEnemy
    public GameObject EnemyTargetPlayer;    //初期ターゲット
    public float EnemyPower;  //敵の攻撃力
    public float enemy_max_hp;  //MAXhp
    public float enemy_current_hp;  //CurrentHP
    public GameObject POPPlace;  //敵の出現位置
    public bool this_UseSkill;  //スキル使う敵ならtrue, 使わないならfalse

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        enemy_initiate = (GameObject)Instantiate(OldmanEnemy, POPPlace.transform.position, OldmanEnemy.transform.rotation);
        EnemyBowerController em = enemy_initiate.GetComponent<EnemyBowerController>();
        em.EnemyDamage = EnemyPower;
        em.Enemy_target = EnemyTargetPlayer;
        em.PoP_position = POPPlace;
        em.UseSkill = this_UseSkill;

        WeaponAttack em2 = enemy_initiate.GetComponent<WeaponAttack>();
        em2.rePoP = POPPlace;
        enemy_initiate.SendMessage("EnemyInfo_MaxHP", enemy_max_hp);
        enemy_initiate.SendMessage("EnemyInfo_CurrentHP", enemy_current_hp);


        enemyhpbarlabel em3 = enemy_initiate.GetComponent<enemyhpbarlabel>();
        em3.GUIWatchPoint = enemy_initiate;
        em3.NameObject = OldmanEnemy;

        e_hpbarSlider em4 = enemy_initiate.GetComponent<e_hpbarSlider>();
        em4.GUIWatchPoint2 = enemy_initiate;

        enemy_initiate.SendMessage("EnemyInfo_SubMaxHP", enemy_max_hp);
        enemy_initiate.SendMessage("EnemyInfo_SubCurrentHP", enemy_current_hp);

        gameObject.SetActive(false);
    }



}
