using UnityEngine;
using System.Collections;

public class CauseSkillDamage : MonoBehaviour {
    private bool damage_flgg = true;
    public float skilldamage_amount;
    public bool fallingflg;

    public static bool gainstartflg;    //スキルのことをsubhpbarの判定に飛ばす
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {


	}

    void OnParticleCollision(GameObject collisionObject) {
        if (collisionObject.gameObject.tag == "Enemy" && damage_flgg == true) {
            collisionObject.SendMessage("DamageProcess",skilldamage_amount);

            if(fallingflg == true)
            {
                collisionObject.SendMessage("FallingAnimation", fallingflg);
            }
        }
        damage_flgg = false;
    }

}

