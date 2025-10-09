using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public abstract void Enter(EnemyStateManager enemy);

    public abstract void Update(EnemyStateManager enemy);
    public abstract void Exit(EnemyStateManager enemy);
}
