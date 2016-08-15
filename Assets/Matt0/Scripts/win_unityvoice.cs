using UnityEngine;
using System.Collections;

public class win_unityvoice : MonoBehaviour {
	AudioSource audiosource;
	public AudioClip win_voice;
	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource> ();
		audiosource.PlayOneShot (win_voice);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
