using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaBomb : MonoBehaviour {

    public float bombDamage = 25f;
    public float fireBallSpeed = 50;

    public GameObject bomb;
    

    private void Start()
    {

        transform.Rotate(new Vector3(0, 0, 90));
        Invoke("Explode", 2);
    }
    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * fireBallSpeed);
    }

    // transform.localScale += new Vector3(1,0);

    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealth -= bombDamage;
            GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount -= .25f;

        }
        else if (col.gameObject.tag == "Simulacrum")
        {
            col.gameObject.GetComponent<SimulacrumAbilities>().AbsorbDamage(bombDamage);
            Explode();
        }

        else if (col.gameObject.tag != "Boss" || gameObject.tag != "CameraTrigger")
        {
            Explode();
        }
    }

    public void Explode()
    {
        GameObject bomb1 = Instantiate(bomb, transform.position, transform.rotation);
        GameObject bomb2 = Instantiate(bomb, transform.position, transform.rotation);
        bomb2.transform.Rotate(0, 0, 180);
        GameObject bomb3 = Instantiate(bomb, transform.position, transform.rotation);
        bomb3.transform.Rotate(0, 0, 90);
        GameObject bomb4 = Instantiate(bomb, transform.position, transform.rotation);
        bomb4.transform.Rotate(0, 0, -90);

        Destroy(gameObject);
    }
}
