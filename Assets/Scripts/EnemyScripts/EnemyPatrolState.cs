using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    public float rangedDistance = 10f;
    // Start is called before the first frame update
    public override void Enter(EnemyStateManager enemy)
    {
        
    }

    public override void Update(EnemyStateManager enemy)
    {
        if (enemy.player == null) { return; }

        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distance < rangedDistance)
        {
            enemy.ChangeState(enemy.RangedState);
        }
    }

    public override void Exit(EnemyStateManager enemy)
    {

    }
}
