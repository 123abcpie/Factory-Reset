using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{

    EnemyState currentState;
    public EnemyPatrolState PatrolState = new EnemyPatrolState();
    public EnemyRangedAttack RangedState = new EnemyRangedAttack();

    public Transform player;
    public GameObject projectilePrefab;

    private void Start()
    {
        // Replace with patrol state once implemented
        currentState = PatrolState;
        currentState.Enter(this);
    }

    private void Update()
    {
        currentState.Update(this);
    }

    public void ChangeState(EnemyState state)
    {
        currentState = state;
        state.Enter(this);
    }
}
