using UnityEngine;
using System.Collections;

public class car_ride : MonoBehaviour
{
    public GameObject effect;
    private GameObject Player;
    private Animator animator;

    private float intervalTime;
    private float setTime = 2.0f;

    private float BeforeDestroy_Player_yAngle;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        animator = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        intervalTime += Time.deltaTime;
        if (intervalTime >= setTime)
        {
            if (gameObject.transform.localEulerAngles.z >= 125 && gameObject.transform.localEulerAngles.z <= 235)
            {
                //never used: GameObject effect_start = (GameObject)Instantiate(effect, gameObject.transform.position, gameObject.transform.rotation);
                animator.SetBool("sitdown", false);
                RideOnBuggy.buggy_on_ride = false;
                BeforeDestroy_Player_yAngle = Player.gameObject.transform.eulerAngles.y;
                Player.gameObject.transform.eulerAngles = new Vector3(0, BeforeDestroy_Player_yAngle, 0);
                Destroy(gameObject);
            }
        intervalTime = 0;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //never used: GameObject effect_start = (GameObject)Instantiate(effect, gameObject.transform.position, gameObject.transform.rotation);
            other.gameObject.SendMessage("DamageProcess", 100f);
            animator.SetBool("sitdown", false);
            RideOnBuggy.buggy_on_ride = false;
            BeforeDestroy_Player_yAngle = Player.gameObject.transform.eulerAngles.y;
            Player.gameObject.transform.eulerAngles = new Vector3(0, BeforeDestroy_Player_yAngle, 0);
            Destroy(gameObject);
        }
    }
}
