using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulacrumAbilities : MonoBehaviour
{

    public GameObject fireball;

    public enum State
    {
        IDLE,
        LONGATK,
        ABSORBDMG
    }
    public State state;

    public int atkSpeed = 80;
    public bool alive = true;
    public int rotSpeed = 25;
    public string type = "Attack";

    private const float ATTACKCOOLDOWN = .7f;
    private float attackTimer;
    private float damageTaken = 0;
    private Transform boss;
    private Vector2 cursorInWorldPos;

    // Use this for initialization
    void Start()
    {
        boss = GameObject.Find("Boss").transform;
        Invoke("Destroy", 8f);
        StartCoroutine("FSM");
    }

    IEnumerator FSM()
    {
        while (alive)
        {
            cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (type == "Absorb")
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            switch (state)
            {
                case State.IDLE:
                    Idle();
                    break;
                case State.LONGATK:
                    LongAttack();
                    break;
            }
            yield return null;
        }
    }

    private void Idle()
    {
        //print(type);
        Rotate();

        if (type == "Attack")
        {
            TimerHandler();

            if (Input.GetKey(KeyCode.Mouse0) && attackTimer >= ATTACKCOOLDOWN)
            {
                attackTimer = 0;
                state = State.LONGATK;
            }
        }

    }

    public void LongAttack()
    {
        //print("hit");
        Vector2 direction = cursorInWorldPos - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        GameObject fb = Instantiate(fireball, transform.position, Quaternion.identity);
        fb.GetComponent<Rigidbody2D>().velocity = direction * atkSpeed;
        //print("hit2");
        state = State.IDLE;
    }

    public void AbsorbDamage(float amt)
    {
        if(type == "Absorb")
        {
            damageTaken += amt;
            print(damageTaken);
        }
    }

    public void Explode()
    {
        int explodeAmount = Mathf.CeilToInt(damageTaken/4);

        LookAtBoss();

        for (int i = 0; i < explodeAmount; i++)
        {
            Vector2 direction = new Vector3(Random.Range(boss.position.x - 20, boss.position.x + 20), Random.Range(boss.position.y - 10, boss.position.y + 10), boss.position.x) - transform.position;
            direction.Normalize();
            GameObject fb = Instantiate(fireball, transform.position, Quaternion.identity);
            fb.GetComponent<Rigidbody2D>().velocity = direction * Random.Range(atkSpeed - 20, atkSpeed + 20);
        }
    }

    public void Rotate()
    {
        Vector3 vectorToTarget = cursorInWorldPos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion rotAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotAngle, rotSpeed);
    }

    public void LookAtBoss()
    {
        Vector3 vectorToTarget = GameObject.Find("Boss").transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion rotAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotAngle, rotSpeed);
    }

    public void TimerHandler()
    {
        attackTimer += Time.deltaTime;
    }

    private void Destroy()
    {
        alive = false;

        if (type == "Attack")
        {
            Destroy(gameObject);
        }
        else
        {
            Explode();
            Destroy(gameObject);
        }
    }
}
