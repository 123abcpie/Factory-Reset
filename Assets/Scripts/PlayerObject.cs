using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public int health = 3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health < 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            Destroy(collider.gameObject);
            health -= 1;
        }
    }
}
