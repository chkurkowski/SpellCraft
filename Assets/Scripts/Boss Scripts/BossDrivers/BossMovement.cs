using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    private BossHealth bossHealthInfo;

    public enum State
    {
        IDLE,
        MOVE,
        STUN,
    }

    public State state;

    // Use this for initialization
    void Start ()
    {
        state = State.IDLE;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
