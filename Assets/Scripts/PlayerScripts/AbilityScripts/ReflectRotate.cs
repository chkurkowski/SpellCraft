using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectRotate : MonoBehaviour {

	private PlayerMovement playerMovement;

	// Use this for initialization
	void Start () 
	{
		playerMovement = GetComponentInParent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerMovement.Rotate(transform);
	}
}
