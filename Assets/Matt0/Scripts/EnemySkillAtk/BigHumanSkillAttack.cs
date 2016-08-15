using UnityEngine;
using System.Collections;

public class BigHumanSkillAttack : MonoBehaviour {
    private float plus = 0.5f;

    private GameObject hyoutan1;
    private GameObject hyoutan2;
    private GameObject hyoutan3;
    private GameObject hyoutan4;

    public static bool EnemySkill_firstflg = true;

    // Use this for initialization
    void Start () {
        hyoutan1 = gameObject.transform.FindChild("hyoutan").gameObject;
        hyoutan2 = gameObject.transform.FindChild("hyoutan (2)").gameObject;
        hyoutan3 = gameObject.transform.FindChild("hyoutan (3)").gameObject;
        hyoutan4 = gameObject.transform.FindChild("hyoutan (4)").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 p = new Vector3(0, plus, 0);
        transform.Rotate(p);

        if(EnemySkill_firstflg == true)
        {
            Invoke("Re_Skill", 15f);
            EnemySkill_firstflg = false;
        }

    }

    private void Re_Skill()
    {
        hyoutan1.SetActive(true);
        hyoutan2.SetActive(true);
        hyoutan3.SetActive(true);
        hyoutan4.SetActive(true);
        gameObject.SetActive(false);
    }
}
