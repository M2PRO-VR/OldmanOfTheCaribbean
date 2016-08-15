using UnityEngine;
using System.Collections;

public class HP_Restoration : MonoBehaviour {
    private int counter = 0;
    private float plus = 3f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 p = new Vector3(0, 0, plus);
        transform.Rotate(p);
        counter++;
    }

    void OnTriggerEnter(Collider hit)
    {
        //接触対象はPlayerタグですか？
        if (hit.CompareTag("Player"))
        {
            HP_Restore_Counter.Restore_preserve_flg = true;
            Destroy(gameObject);
        }
    }
}
