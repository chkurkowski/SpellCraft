using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonCoverLaser : MonoBehaviour
{
    private LineRenderer lineRender;
    public float laserDamage = .1f;
    private Transform boss;
    // Use this for initialization

    void Awake()
    {
        lineRender = GetComponent<LineRenderer>();
        lineRender.useWorldSpace = true;
       
    }
    void Start ()
    {
        boss = GameObject.Find("ProtoNovus").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        lineRender.SetPosition(0, transform.position);
        lineRender.SetPosition(1, boss.position);

    }
}
