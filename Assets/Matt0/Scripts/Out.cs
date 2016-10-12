using UnityEngine;
using System.Collections;

public class Out : MonoBehaviour{

    void OnTriggerEnter(Collider col){
        
        if(col.gameObject.tag == "Player")
            col.gameObject.transform.position = new Vector3(0, 0, 0);
    }
}
