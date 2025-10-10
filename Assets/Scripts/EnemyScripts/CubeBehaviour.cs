using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CubeBehaviour : MonoBehaviour
{
    // Basic Enum
    public enum EnemyState
    {
        Chase,
        Idle
    }
    public EnemyState currentState = EnemyState.Idle;
    public GameObject projectilePrefab;
    public GameObject burstPrefab;
    public float targetDistance = 10f;
    public Transform player;
    public float attackRate = 2f;
    public float cooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //detect the player if left unassigned
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogWarning("Player not found in scene!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}