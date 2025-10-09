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
    public float rotationSpeed = 0.1f;
    public float fireRate = 1f;
    public float projectileSpeed = 10f;
    public Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        targetRotation = transform.localRotation;
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
        if (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
        }
        else
        {
            if (UnityEngine.Random.Range(0, 50) < 1)
            {
                float randomYAngle = UnityEngine.Random.Range(0f, 360f);
                targetRotation = Quaternion.Euler(0f, randomYAngle, 0f);
            }
        }
    }
    public void Ranged()
    {
        
    }
    public void Melee()
    {
        
    }
}
