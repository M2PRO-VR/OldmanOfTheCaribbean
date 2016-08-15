using UnityEngine;
using System.Collections;

public class FootSE : MonoBehaviour {
	private AudioSource audiosource;
	public AudioClip walk;
	private int walkvoiceStart=0;
	CharacterController unity;
	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource> ();
		unity = GameObject.Find ("unitychan").GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		walkvoiceStart = Random.Range (1,20);
		if(Input.GetKey(KeyCode.W) && (unity.isGrounded)){
		if (!audiosource.isPlaying) {
				if (walkvoiceStart == 1) {
					audiosource.PlayOneShot (walk);
				} 

			}
		}else {
			audiosource.Stop ();
		}
	}
}
