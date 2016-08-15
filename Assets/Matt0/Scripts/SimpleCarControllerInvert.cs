using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleCarControllerInvert : MonoBehaviour {
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    private float motor;
    private float steering;
    private Rigidbody rb;

    private bool Control_flg = false;

    public void Start()
    {
        motor = 0.0f;
        steering = 0.0f;
        rb = GetComponent<Rigidbody>();
    }
    public void FixedUpdate()
    {

        if (RideOnBuggy.buggy_on_ride == false)
        {
            //コントローラ軸用　とりあえず今は使わない
            //float motor = maxMotorTorque * Input.GetAxis("Vertical"); 
            //float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.K) && Control_flg == true)
            {
                motor += 1.0f;
                if (motor > 1.0f) { motor = 1.0f; }
            }
            if (Input.GetKey(KeyCode.I) && Control_flg == true)
            {
                motor -= 1.0f;
                if (motor < -1.0f) { motor = -1.0f; }
            }
            if (Input.GetKey(KeyCode.L) && Control_flg == true)
            {
                steering += 1.0f;
                if (steering > 1.0f) { steering = 1.0f; }
            }
            if (Input.GetKey(KeyCode.J) && Control_flg == true)
            {
                steering -= 1.0f;
                if (steering < 1.0f) { steering = -1.0f; }
            }

            motor = maxMotorTorque * motor;
            steering = maxSteeringAngle * steering;

            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }

                //WheelColliderからrpmを取得してなんとなくホイールがキレイに動いているように見せてる
                axleInfo.leftTrans.Rotate(axleInfo.leftWheel.rpm * 0.1f, 0, 0);
                axleInfo.rightTrans.Rotate(axleInfo.rightWheel.rpm * 0.1f, 0, 0);
                //スティーリングの動きは検討中
            }

            if (!Input.GetKeyDown(KeyCode.I) && !Input.GetKeyDown(KeyCode.K) && Control_flg == true)
            {
                motor = 0.0f;
            }
            if (!Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.L) && Control_flg == true)
            {
                steering = 0.0f;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.S) && Control_flg == true)
            {
                motor += 1.0f;
                if (motor > 1.0f) { motor = 1.0f; }
            }
            if (Input.GetKey(KeyCode.W) && Control_flg == true)
            {
                motor -= 1.0f;
                if (motor < -1.0f) { motor = -1.0f; }
            }
            if (Input.GetKey(KeyCode.D) && Control_flg == true)
            {
                steering += 1.0f;
                if (steering > 1.0f) { steering = 1.0f; }
            }
            if (Input.GetKey(KeyCode.A) && Control_flg == true)
            {
                steering -= 1.0f;
                if (steering < 1.0f) { steering = -1.0f; }
            }

            motor = maxMotorTorque * motor;
            steering = maxSteeringAngle * steering;

            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }

                //WheelColliderからrpmを取得してなんとなくホイールがキレイに動いているように見せてる
                axleInfo.leftTrans.Rotate(axleInfo.leftWheel.rpm * 0.1f, 0, 0);
                axleInfo.rightTrans.Rotate(axleInfo.rightWheel.rpm * 0.1f, 0, 0);
                //スティーリングの動きは検討中
            }

            if (!Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.S) && Control_flg == true)
            {
                motor = 0.0f;
            }
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && Control_flg == true)
            {
                steering = 0.0f;
            }
        }
    }

    public void OnCollisionEnter(Collision colInfo)
    {
        if (colInfo.gameObject.CompareTag("Player") && gameObject.tag == "WeaponAttack")
        {
            RideOnBuggy aaa = gameObject.GetComponent<RideOnBuggy>();
            aaa.can_ride = true;
            //rb.useGravity = false;
            //rb.freezeRotation = true;
            Debug.Log("true");
        }
    }
    public void OnCollisionExit(Collision colInfo)
    {
        if (colInfo.gameObject.CompareTag("Player") && gameObject.tag == "WeaponAttack")
        {
            //rb.useGravity = true;
            //rb.freezeRotation = false;
            RideOnBuggy aaa = gameObject.GetComponent<RideOnBuggy>();
            aaa.can_ride = false;
            Debug.Log("false");
        }

    }

    public void OnTriggerStay(Collider colInfo)
    {
        if (colInfo.gameObject.CompareTag("Player"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.name == "enable ride")
                    {
                        Control_flg = true;
                        //ユニティちゃんを乗せる処理
                        //プレイヤー操作で動くように　bool?
                        Debug.Log("Click on Buggy!!");
                    }
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
        }
    }
}


[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public Transform leftTrans;
    public Transform rightTrans;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}



//乗るやつのは以下の通りでした    (/*//でコメントアウトしておく)
/*//
 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleCarControllerInvert : MonoBehaviour {
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    private float motor;
    private float steering;
    private Rigidbody rb;
    private CharacterController unitychan;
    private bool ride;
    private Vector3 buggyrotate;

    public void Start()
    {
        motor = 0.0f;
        steering = 0.0f;
        rb = GetComponent<Rigidbody>();
        unitychan = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
        ride = false;
    }
    public void FixedUpdate()
    {
        //軸用
        if (ride)
        {
            Move();

            //ボタン用
            /*
            if (Input.GetKey(KeyCode.K))
            {
                motor += 1.0f;
                if (motor > 1.0f) { motor = 1.0f; }
            }
            if (Input.GetKey(KeyCode.I))
            {
                motor -= 1.0f;
                if (motor < -1.0f) { motor = -1.0f; }
            }
            if (Input.GetKey(KeyCode.L))
            {
                steering += 1.0f;
                if (steering > 1.0f) { steering = 1.0f; }
            }
            if (Input.GetKey(KeyCode.J))
            {
                steering -= 1.0f;
                if (steering < 1.0f) { steering = -1.0f; }
            }

            motor = maxMotorTorque * motor;
            steering = maxSteeringAngle * steering;
            */
/*//
            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }

                axleInfo.leftTrans.Rotate(axleInfo.leftWheel.rpm* 0.1f, 0, 0);
                axleInfo.rightTrans.Rotate(axleInfo.rightWheel.rpm* 0.1f, 0, 0);
                //スティーリングの動き要検討
            }

            /*
            if (!Input.GetKeyDown(KeyCode.I) && !Input.GetKeyDown(KeyCode.K))
            {
                motor = 0.0f;
            }
            if (!Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.L))
            {
                steering = 0.0f;
            }
            */

/*//
        }
    }

    public void OnTriggerStay(Collider colInfo)
{
    if (colInfo.gameObject.CompareTag("Player"))
    {
        if (Input.GetKey(KeyCode.R) && !ride)
        {
            Debug.Log("Ride on Buggy!!");
            UnityChanController.isBuggyRide = true;
            ride = true;
        }
    }
    /* マウスレイ用
    if (Input.GetMouseButtonDown(0))
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "enable ride")
            {
                //ユニティちゃんを乗せる処理
                Debug.Log("Click on Buggy!!");
                //UnityChanController.rideBuggy(true);
                ride = true;
            }
            Debug.Log(hit.collider.gameObject.name);
        }
    }
    */

/*//
}

void Move()
{
    motor = maxMotorTorque * -Input.GetAxis("Vertical") * 1.1f;
    steering = maxSteeringAngle * Input.GetAxis("Horizontal") * 1.1f;
    if (Input.GetKey(KeyCode.F))
    {
        UnityChanController.isBuggyRide = false;
        ride = false;
    }
    /*
    Vector3 trans = rb.transform.position;
    trans.y += 2; 
    unitychan.transform.position = trans;

    buggyrotate = rb.transform.eulerAngles;
    buggyrotate.x = 0;
    buggyrotate.y += 180;
    buggyrotate.z = 0;
    unitychan.transform.eulerAngles = buggyrotate;
    */


//  /*//    }
//  /*//    }

/*//
[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public Transform leftTrans;
    public Transform rightTrans;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}

*/     //    /*//

