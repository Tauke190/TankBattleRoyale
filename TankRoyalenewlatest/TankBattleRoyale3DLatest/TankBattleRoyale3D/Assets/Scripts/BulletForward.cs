using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForward : MonoBehaviour {

	public float BulletSpeed;

	void Update () {
		transform.position += transform.forward * BulletSpeed * Time.deltaTime;
	}
}
