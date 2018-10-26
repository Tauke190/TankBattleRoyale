using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour {

	public GameObject ammo;
	GameObject[] ammoArray;
	[SerializeField]
	int ammoCount = 100;
	public Queue<Transform> ammoQueue = new Queue<Transform> ();
	public static AmmoManager AmmoManagerSingleton = null;

	void Awake(){
		if(AmmoManagerSingleton != null)
		{
			Destroy(GetComponent<AmmoManager>());
			return;
		}
		AmmoManagerSingleton = this;
		ammoArray = new GameObject[ammoCount];
	}

	void Start () {
		for (int i = 0; i < ammoCount; i++) {
			ammoArray [i] = Instantiate (ammo, Vector3.zero, Quaternion.identity) as GameObject;
			Transform objTransform = ammoArray [i].GetComponent<Transform> ();
			objTransform.SetParent (transform);
			ammoQueue.Enqueue (objTransform);
			ammoArray [i].SetActive (false);
		}
	}

	public static void SpawnAmmo(Vector3 position,Quaternion rotation) {
		Transform spawnedAmmo = AmmoManagerSingleton.ammoQueue.Dequeue ();
		spawnedAmmo.gameObject.SetActive (true);
		spawnedAmmo.position = position;
		spawnedAmmo.rotation = rotation;
		AmmoManagerSingleton.ammoQueue.Enqueue (spawnedAmmo);
	}
}
