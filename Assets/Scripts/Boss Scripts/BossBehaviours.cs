using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviours : MonoBehaviour {

    public enum State{
        IDLE,
        MOVE,
        SPIN,
        BOMB,
        CHARGE,
        COMBINED
    }

    public State state;

	// Use this for initialization
	void Start () 
    {
        state = State.IDLE;
        StartCoroutine("FSM");
	}

    IEnumerator FSM()
    {
        while (true)
        {
            print(state);
            switch (state)
            {
                case State.IDLE:
                    Idle();
                    break;
                case State.MOVE:
                    Move();
                    break;
                case State.SPIN:
                    Spin();
                    break;
                case State.BOMB:
                    Bomb();
                    break;
                case State.CHARGE:
                    Charge();
                    break;
                case State.COMBINED:
                    Combined();
                    break;
            }
            yield return null;
        }
    }

    private void Idle()
    {

    }

    private void Move()
    {

        state = State.IDLE;
    }

    private void Spin()
    {
        state = State.IDLE;
    }

    private void Bomb()
    {
        state = State.IDLE;
    }

    private void Charge()
    {
        state = State.IDLE;
    }

    private void Combined()
    {
        state = State.IDLE;
    }
}
