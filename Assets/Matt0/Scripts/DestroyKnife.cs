using UnityEngine;
using System.Collections;

public class DestroyKnife : MonoBehaviour {

	void Start(){
        Destroy(gameObject, 2.0f);
    }

    void Update(){
        if(DeleteFlg.deleteflg == true)
            Destroy(gameObject);

        DeleteFlg.deleteflg = false;
    }
}
