using UnityEngine;
using System.Collections;

public class CauseSkilldamage1 : MonoBehaviour {
    private bool damage_flgg = true;
    public float skilldamage_amount;

    public static bool gainstartflg;    //スキルのことをsubhpbarの判定に飛ばす
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnParticleCollision(GameObject collisionObject)
    {
        if (collisionObject.gameObject.tag == "Enemy" && damage_flgg == true)
        {
            collisionObject.SendMessage("DamageProcess", skilldamage_amount);
        }
        damage_flgg = false;
    }


}
