using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayHealth : MonoBehaviour
{
    public TextMeshProUGUI dispHealth;
    public TextMeshProUGUI dispGameOver;
    private PlayerObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerObject = player.GetComponent<PlayerObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject.health < 1)
        {
            dispGameOver.enabled = true;
        }
        if (playerObject != null)
        {
            dispHealth.text = $"Health: {playerObject.health}";
        }
    }
}
