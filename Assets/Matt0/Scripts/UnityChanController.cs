using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class UnityChanController : MonoBehaviour 
{
	//private float forwardSpeed = 5f;
	private float runSpeed=6f;
	private float backwalkSpeed = 6f;
	private float Yrotation;
	private int doWalkId;
	private int doRunId;
	private int doBackWalkId;
	private float jumpPower = 6f;
    private Vector3 velocity;
	private Vector3 tempvelocity;
	private float speed = 0.0f;
	private float timer=0;
	private Transform player;
	static public bool player_attacknow;
	CharacterController controller;
	Animator animator;
	Animation anim;
	AnimatorStateInfo stateinfo;
	AnimatorStateInfo jumpstateinfo;
	AnimatorStateInfo skilstateinfo;
	private Transform mcamera;
	private Vector3 mcamerajumpmae;
	private Rigidbody rb;
	public static bool Unitychandamaged = false;
	private bool damagedNow = false;
	private ParticleSystem ps;
	public GameObject beam;
	public GameObject beam2;
	public static float damagetimer=0;
	private bool skillNow = false;
	private bool normalattacEndflag = false;
	private bool normalattackNoskill = false;
	private int jumpNoAttack;
	Ray ray;
	RaycastHit hit;
	GameObject hitgameobject;
	Camera mcamera_enabled;
	//Unityちゃんの声関係
	public AudioSource audiosource;
	public AudioClip attackVoice;
	public AudioClip skillVoice;
	private int attackvoiceStart=0;

    GameObject buggy;
    public static bool isBuggyRide = false;
    Vector3 buggyrotate;

    // Use this for initialization

    void Start () 
	{
		Unitychandamaged = false;
        animator = GetComponent<Animator> ();
		animator.Rebind ();
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animation> ();
		player = GetComponent<Transform> ();
		mcamera = GameObject.Find ("Main Camera").GetComponent<Transform> ();
		audiosource = GetComponent<AudioSource> ();
		mcamera_enabled = GameObject.Find ("Main Camera").GetComponent<Camera> ();
        buggy = GameObject.Find("Buggi (Ride)");

    }

    void Update()
    {
        /* if (!isBuggyRide)
         {
             Move();
             CameraMove();
         }
         else if (isBuggyRide)
         {
             controller.transform.position = buggy.transform.position;
             buggyrotate = buggy.transform.eulerAngles;
             buggyrotate.x = 0;
             buggyrotate.y += 180;
             buggyrotate.z = 0;
             controller.transform.eulerAngles = buggyrotate;
         }*/
        if (RideOnBuggy.buggy_on_ride == false)
        {
            Move();
            CameraMove();
            Attack();
        }
	}

/*****************************************************************************************/
	float GRAVITY=9.8f;
	float jump = 0f;
	bool jumpflag=false;
	//移動
	void Move(){

		velocity = new Vector3(0f, 0f,Input.GetAxis("Vertical"));
		velocity = transform.TransformDirection(velocity); //global から local座標に


	
		if (controller.isGrounded) {
			timer += Time.deltaTime;


			//走る
			if ((Input.GetKey (KeyCode.W))) {
				

				animator.SetBool ("Do Run", true);
				//左に曲がる
				if (Input.GetKey (KeyCode.A)) {
					animator.SetBool ("lrun", true);

					Debug.Log ("左");
					animator.SetBool ("rotation_foot", false);
				}
				animator.SetBool ("lrun", false);
				//右に曲がる
				if (Input.GetKey (KeyCode.D)) {
					animator.SetBool ("rrun", true);
					animator.SetBool ("rotation_foot", false);
				} 
				animator.SetBool ("rrun", false);

                //回避
                if (Input.GetKeyDown(KeyCode.R) && timer > 0.7f && normalattacEndflag == false)
                {
                    animator.SetBool("EscapeAction", true);
                    HpBarCtrl.escapenow = true;
                    Invoke("Escapenow", 0.35f);
                    Invoke("EscapeAction", 0.7f);
                    timer = 0;
                }

                //動く速度を設定
                speed = runSpeed;
			}else {
				animator.SetBool ("Do Run", false);
			}

            //回避後にキックがでないようにする
            if (HpBarCtrl.escapenow == true)
            {
                animator.SetBool("NormalAttack", false);
            }


            //後ろ歩き
            if (Input.GetKey (KeyCode.S)) {
				animator.SetBool ("Do BackWalk", true);
				speed = backwalkSpeed;

			} else {
				animator.SetBool ("Do BackWalk", false);
			}

			//ジャンプ

			jumpstateinfo = animator.GetCurrentAnimatorStateInfo (0);
			jumpNoAttack = Animator.StringToHash ("Base Layer.jump");

			if(damagedNow == false ){
                if (Input.GetKeyDown(KeyCode.Space) && timer > 0.2f && normalattacEndflag == false)
                {

                    timer = 0f;
                    animator.SetBool("Jumpping", true);
                    jump = jumpPower;

                }else {
                    animator.SetBool("Jumpping",false);
                }
                

				if(jumpstateinfo.normalizedTime > 0.8f){
			//		animator.SetBool ("NormalAttack", false);
				}

			}
            else
            {
                animator.SetBool("Jumpping", false);

            }
            //jumpflag = false;

            //ダメージフラグを受けたら倒れるアニメーション再生
            if (skillNow == false){
			if (Unitychandamaged && timer > 3.5f) {
				timer = 0f;
				animator.SetBool ("damagetrigger", true);

				Unitychandamaged = false;
				StartCoroutine ("wait", 3.5f);
			} 
			}

		} else if(controller.isGrounded == false){
			jump -= GRAVITY * Time.deltaTime;
			jumpflag = true;

		}

		//速度設定
		velocity *= speed;
		velocity.y = jump;
		//ダメージ入ってないとき
		if(damagedNow == false){
			//スキル発動中ではないとき
			if(skillNow == false){
				//通常攻撃中ではないとき
			//	if(normalattackNow == false){
			controller.Move (velocity*Time.deltaTime);
			//	}
			}
		}

	}

	//指定した秒数待つ
	private	IEnumerator wait(float waittime) {

		damagedNow = true;
		animator.SetBool ("rotation_foot",false);

		yield return new WaitForSeconds (waittime);
		animator.SetBool ("damagetrigger",false);
		damagedNow = false;


	}
/*****************************************************************************************/
	//カメラ
	void CameraMove(){
		//ダメージ受けて倒れているときにカメラを回せないように
		if(damagedNow == false){
			if(skillNow == false){
			//前進していないときにカメラの回転をしたらその場で足踏みしてカメラを回す
			if (Input.GetKey (KeyCode.A)) {
				if (Input.GetKey (KeyCode.W)== false && damagedNow==false) {
					animator.SetBool ("rotation_foot", true);
				}
				Yrotation = (-170) * Time.deltaTime;
				transform.Rotate (0, Yrotation, 0);
			} else if (Input.GetKey (KeyCode.D)) {
				if (Input.GetKey (KeyCode.W)== false && damagedNow == false) {
					animator.SetBool ("rotation_foot", true);
				}

				Yrotation = 170 * Time.deltaTime;
				transform.Rotate (0, Yrotation, 0);
			} else {
				animator.SetBool ("rotation_foot",false);
			}
			}
		}

	}
/*****************************************************************************************/
	//Enemyの三回目の攻撃のフラグを受け取る
	public static void DamageAnimationUnitychan(bool damage){
		Unitychandamaged = damage;
		}

/*****************************************************************************************/
	private int halberdattack= Animator.StringToHash ("Layer_Arms.HalberdAttack");
	private int normalattack= Animator.StringToHash ("Base Layer.normalAttack");
	private int swordattack = Animator.StringToHash("Layer_Arms.SwordAttack");
	private int skill1;
	private int skill2;

    private GameObject dele;

    void Attack(){
		jumpstateinfo = animator.GetCurrentAnimatorStateInfo (0);

		if (buttonScript.aaa == false && damagedNow == false) {
			//現在のアニメーションをセット
			stateinfo = animator.GetCurrentAnimatorStateInfo (1);

		
			attackvoiceStart = Random.Range (1,3);
			//連打したときに連続して攻撃しないようにifで判定
			if (stateinfo.fullPathHash == halberdattack) {
				animator.SetBool ("HalberdAttack", false);
			} else {
				if((ChangeWeaponsKey.have_weapon1 == true) ){
					if (Input.GetMouseButtonDown (0)  && normalattackNoskill == false &&  !(jumpstateinfo.fullPathHash == jumpNoAttack)) {
					if(attackvoiceStart == 1){
					audiosource.PlayOneShot(attackVoice);
					}
					animator.SetBool ("HalberdAttack", true);
					StartCoroutine ("weapon1wait",0.5f);
				}
				}
			}

			if (stateinfo.fullPathHash == swordattack) {
				animator.SetBool ("SwordAttack",false);
			}else if(ChangeWeaponsKey.have_weapon2 == true){
				if (Input.GetMouseButtonDown (0) && normalattackNoskill == false  && !(jumpstateinfo.fullPathHash == jumpNoAttack)){
						if(attackvoiceStart == 1){
							audiosource.PlayOneShot(attackVoice);
						}
						animator.SetBool ("SwordAttack", true);
						StartCoroutine ("weapon1wait",0.5f);
					}
				}
			

			//右クリックの通常攻撃
			if (jumpstateinfo.fullPathHash == normalattack) {
				normalattackNoskill = true;
				if ((jumpstateinfo.normalizedTime > 0.3f) || (jumpstateinfo.normalizedTime < 0.99f)) {
					normalattacEndflag = true;
				} 
				if(jumpstateinfo.normalizedTime <= 0.3f || jumpstateinfo.normalizedTime >= 0.99f){
					normalattacEndflag = false;
				}
				animator.SetBool ("NormalAttack", false);
			} else {
				normalattackNoskill = false;
				normalattacEndflag = false;
				if(Input.GetKeyDown(KeyCode.Mouse1) && !(jumpstateinfo.fullPathHash == jumpNoAttack)){
					animator.SetBool ("NormalAttack",true);
					StartCoroutine ("normalattackwait",2f);

				}
			}
				

			//スキル1　黒い球
			{
			skilstateinfo = animator.GetCurrentAnimatorStateInfo (0);
			skill1 = Animator.StringToHash ("Base Layer.Skill1");
			Vector3 beamPosition = transform.forward * 1f + transform.position + Vector3.up * 1.2f;
			float localY = transform.localEulerAngles.y;
			Quaternion beam_rotation = Quaternion.AngleAxis (localY, Vector3.up);
			//スキル発動中に連打しても連続してアニメーションが再生されないように
			if (skilstateinfo.fullPathHash == skill1) {
				animator.SetBool ("skill1", false);
			} else {
                //1を押したらスキル発動
					if (Input.GetKeyDown (KeyCode.Alpha1) && cooltimebar.cooltimeNow == false && !(jumpstateinfo.fullPathHash == jumpNoAttack) && normalattackNoskill == false ) {
					GameObject beam_shot = Instantiate (beam, beamPosition, beam_rotation) as GameObject;
                    WeaponAttack.WeaponAnimationNow(player_attacknow);
					cooltimebar.cooltime_start (true);
					ps = beam_shot.GetComponent<ParticleSystem> ();
					var em = ps.emission;
					em.enabled = true;
					ps.Play ();
					animator.SetBool ("skill1", true);
						audiosource.PlayOneShot (skillVoice);
						skillNow = true;
						Invoke ("skillNoMove",1.5f);
						Destroy(beam_shot,5f);

				}
			}


	        }

			//スキル2　黄色い球
			{
				skilstateinfo = animator.GetCurrentAnimatorStateInfo (0);
				skill2 = Animator.StringToHash ("Base Layer.Skill1");
				Vector3 beam2Position = transform.forward * 1f + transform.position + Vector3.up * 1.2f;
				float localY = transform.localEulerAngles.y;
				Quaternion beam2_rotation = Quaternion.AngleAxis (localY, Vector3.up);
				//スキル発動中に連打しても連続してアニメーションが再生されないように
				if (skilstateinfo.fullPathHash == skill2) {
					animator.SetBool ("skill2", false);
				} else {
					//2を押したらスキル発動
					if (Input.GetKeyDown (KeyCode.Alpha2) && cooltimebar2.cooltime2Now == false && !(jumpstateinfo.fullPathHash == jumpNoAttack) && normalattackNoskill == false) {
						GameObject beam2_shot = Instantiate (beam2, beam2Position, beam2_rotation) as GameObject;
						WeaponAttack.WeaponAnimationNow(player_attacknow);
						cooltimebar2.cooltime2_start (true);
						ps = beam2_shot.GetComponent<ParticleSystem> ();
						var em = ps.emission;
						em.enabled = true;
						ps.Play ();
						animator.SetBool ("skill2", true);
						audiosource.PlayOneShot (skillVoice);
						//skillNow = true;
						Destroy(beam2_shot,5f);

					}
				}


			}
		}

		}
	private	IEnumerator weapon1wait(float waittime) {

		player_attacknow = true;
		WeaponAttack.WeaponAnimationNow (player_attacknow);
		yield return new WaitForSeconds (waittime);
		player_attacknow = false;


	}
	private	IEnumerator normalattackwait(float waittime) {
		player_attacknow = true;
		WeaponAttack.KickAnimationNow (player_attacknow);
		yield return new WaitForSeconds (waittime);
		animator.SetBool ("NormalAttack",false);
        player_attacknow = false;

    }

    void skillNoMove(){
		skillNow = false;
	}

    private void EscapeAction()
    {
        speed = runSpeed;
        HpBarCtrl.escapenow = false;
        animator.SetBool("EscapeAction", false);
    }

    private void Escapenow()
    {
        speed = runSpeed + 5f;
        //速度設定
        velocity *= speed;
        velocity.y = jump;
        //ダメージ入ってないとき
        if (damagedNow == false)
        {
            //スキル発動中ではないとき
            if (skillNow == false)
            {
                //通常攻撃中ではないとき
                //	if(normalattackNow == false){
                controller.Move(velocity * Time.deltaTime);
                //	}
            }
        }
    }

}




