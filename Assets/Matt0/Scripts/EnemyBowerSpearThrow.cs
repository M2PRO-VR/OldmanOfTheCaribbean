using UnityEngine;
using System.Collections;

public class EnemyBowerSpearThrow : MonoBehaviour
{
    private bool Rayflg = false;
    private Vector3 Raypoint;
    private float Speed = 10f;

    private GameObject Player;

    private float intervalTime = 0;
    public GameObject blood;
    private GameObject bloodCopy;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Rayflg == false)
        {
            //投げ部分（書き換え検討場所）

            transform.LookAt(Player.transform.position);
            Ray ray = new Ray(transform.position, transform.forward);
            Raypoint = ray.GetPoint(100);
            GetComponent<Rigidbody>().velocity = (Raypoint + new Vector3(0,0.3f,0) - transform.position).normalized * Speed;
            transform.eulerAngles = transform.eulerAngles + new Vector3(90, 0, 0);
            Rayflg = true;
        }

        intervalTime += Time.deltaTime;

        //3.0秒後object消す
        if (intervalTime >= 3.0f)
        {
            Destroy(gameObject);
            intervalTime = 0;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            bloodCopy = (GameObject)Instantiate(blood, Player.transform.position, Player.transform.rotation);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            HpBarCtrl.Damage(20, true);
        }

        if(col.gameObject.tag == "WeaponAttack")
        {
            Destroy(gameObject);
        }
    }
}

