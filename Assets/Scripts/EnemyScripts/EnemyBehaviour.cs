using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Basic Enum
    public enum EnemyState
    {
        Melee,
        Ranged,
        Idle
    }
    public EnemyState currentState = EnemyState.Idle;
    public float targetDistance = 10f;
    public float meleeDistance = 2f;
    public Transform player;
    public float fireRate = 1f;
    public float projectileSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                // The case switch for the Idle State
                if (Vector3.Distance(transform.position, player.position) < meleeDistance)
                {
                    currentState = EnemyState.Melee;
                }
                else if (Vector3.Distance(transform.position, player.position) < targetDistance)
                {
                    currentState = EnemyState.Ranged;
                }
                else
                {
                    Idle();
                }
                break;
            case EnemyState.Ranged:
                // The case switch for the Ranged State
                if (Vector3.Distance(transform.position, player.position) > targetDistance)
                {
                    currentState = EnemyState.Idle;
                }
                else if (Vector3.Distance(transform.position, player.position) < meleeDistance)
                {
                    currentState = EnemyState.Melee;
                }
                else
                {
                    Ranged();
                }
                break;
            case EnemyState.Melee:
                // The case switch for the Melee State
                if (Vector3.Distance(transform.position, player.position) > targetDistance)
                {
                    currentState = EnemyState.Idle;
                }
                if (Vector3.Distance(transform.position, player.position) > meleeDistance)
                {
                    currentState = EnemyState.Ranged;
                }
                else
                {
                    Melee();
                }
                break;
        }
    }
    
    public void Idle()
    {
        
    }
    public void Ranged()
    {
        
    }
    public void Melee()
    {
        
    }
}
