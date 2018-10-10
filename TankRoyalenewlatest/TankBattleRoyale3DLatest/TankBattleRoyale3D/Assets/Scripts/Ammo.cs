using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

	public float bulletForce;
	Rigidbody rb;

	void Awake(){
		rb = GetComponent<Rigidbody> ();
	}

	void OnEnable(){
		Transform noz = GameObject.FindObjectOfType<Scope> ().transform;
		rb.AddForce (noz.forward * bulletForce);
	}
		
	void OnTriggerEnter(Collider col){
		gameObject.SetActive (false);
	}
}
