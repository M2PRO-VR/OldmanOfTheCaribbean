using UnityEngine;
using System.Collections;

public class ThrowKnife : MonoBehaviour{

    public GameObject prefab;
    public float power;

	void Update(){
        
        if(Input.GetMouseButtonDown(0)){

            GameObject knife = LoadKnife();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 dir = ray.direction.normalized;
            knife.GetComponent<Rigidbody>().velocity = dir * power;
        }
    }

    GameObject LoadKnife(){

        GameObject bullet = GameObject.Instantiate(prefab);
        bullet.transform.parent = transform;
        bullet.transform.localPosition = Vector3.zero;

        return bullet;
    }
}
