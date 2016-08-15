using UnityEngine;
using System.Collections;

public class BigHuman_hyoutan_explosion : MonoBehaviour {

    private float DamageAmount = 50.0f;
    private GameObject CauseSkillEffect;

	// Use this for initialization
	void Start () {
        CauseSkillEffect = gameObject.transform.FindChild("ExplosionEffect").gameObject;
        CauseSkillEffect.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider hit)
    {
        //接触対象はPlayerタグですか？
        if (hit.CompareTag("Player") && (HpBarCtrl.escapenow == false))
        {
            HpBarCtrl.Damage(DamageAmount, true);
            CauseSkillEffect.SetActive(true);
            Invoke("explosionFire", 1.0f);
        }
    }

    private void explosionFire()
    {
        CauseSkillEffect.SetActive(false);
        gameObject.SetActive(false);
    }

}
