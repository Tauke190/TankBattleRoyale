using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ammo : MonoBehaviour {

	[SerializeField]
	[Range(3000,10000)]
	float bulletForce = 5000f;
	Rigidbody rb;

	void Awake(){
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (transform.forward * bulletForce);
	}
		
	void OnTriggerEnter(Collider col){
		Destroy (gameObject);
	}
}
