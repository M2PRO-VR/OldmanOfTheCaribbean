using UnityEngine;
using System.Collections;

public class RideOnBuggy : MonoBehaviour {

    public static bool buggy_on_ride;  //乗っているかいないか
    private GameObject Player;  //Playerオブジェクト
    private GameObject CarSeet; //座る場所

    public bool can_ride; //車に乗れるフラグ

    private Animator animator;

    private float BeforeDestroy_Player_yAngle;

    // Use this for initialization
    void Start () {
	    Player = GameObject.FindWithTag("Player");
        CarSeet = gameObject.transform.FindChild("CarSeet").gameObject;
        animator = Player.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(buggy_on_ride == true)
        {
            Player.gameObject.transform.position = CarSeet.gameObject.transform.position;
            Player.gameObject.transform.rotation = CarSeet.gameObject.transform.rotation;
        }
	}

    public void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //motor = maxMotorTorque * -Input.GetAxis("Vertical") * 1.1f;
        //steering = maxSteeringAngle * Input.GetAxis("Horizontal") * 1.1f;
        if (Input.GetKeyDown(KeyCode.F) && buggy_on_ride == true && can_ride == false)
        {
            UnityChanController.isBuggyRide = false;
            buggy_on_ride = false;
            animator.SetBool("sitdown", false);
            BeforeDestroy_Player_yAngle = Player.gameObject.transform.eulerAngles.y;
            Player.gameObject.transform.eulerAngles = new Vector3(0, BeforeDestroy_Player_yAngle, 0);
        }
    }

    public void OnTriggerStay(Collider colInfo)
    {
        if (colInfo.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.R) && buggy_on_ride == false && can_ride == true)
            {
                Debug.Log("Ride on Buggy!!");
                UnityChanController.isBuggyRide = true;
                buggy_on_ride = true;
                animator.SetBool("sitdown", true);
            }
        }
    }

}
