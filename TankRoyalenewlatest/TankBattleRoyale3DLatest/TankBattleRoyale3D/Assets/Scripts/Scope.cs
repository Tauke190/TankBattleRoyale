using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour {

	LineRenderer lr;
	[Range(5,50)]
	public float scopeRange = 10;

	Ray shootRay;

	void Awake(){
		lr = GetComponent<LineRenderer> ();
	}

	void Update () {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit,scopeRange)) {
			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;
			lr.SetPosition (0, transform.position);
			lr.SetPosition (1, shootRay.origin + shootRay.direction * hit.distance);
		} else {
			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;
			lr.SetPosition (0, transform.position);
			lr.SetPosition (1, shootRay.origin + shootRay.direction * scopeRange);
		}
	}
}
