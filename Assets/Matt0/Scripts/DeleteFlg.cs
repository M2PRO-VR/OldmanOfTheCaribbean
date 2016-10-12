using UnityEngine;
using System.Collections;

public class DeleteFlg : MonoBehaviour{

    public static bool deleteflg = false;

    void OnTriggerEnter(Collider material) {
        if(material.gameObject.tag == ("Enemy"))
            deleteflg = true;
    }
}
