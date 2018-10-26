using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ammo : MonoBehaviour {

	[SerializeField]
	float bulletForce;
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
