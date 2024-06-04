using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    Vector3 spinner = new Vector3(45, 0, 0);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(spinner * Time.deltaTime);
	}
}
