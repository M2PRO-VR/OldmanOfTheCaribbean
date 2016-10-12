using UnityEngine;

public class WeaponAttack2 : MonoBehaviour{

    public static bool hs = false;

    void OnTriggerEnter(Collider WeaponAttack2Hit) {
        if(WeaponAttack2Hit.gameObject.tag == ("WeaponAttack"))
            hs = true;
    }
}