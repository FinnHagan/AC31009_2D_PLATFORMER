using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyStateManager : MonoBehaviour
{

    public IEnemyState currentState { get; private set; }

    private Dictionary<Type, IEnemyState> availableStates = new Dictionary<Type, IEnemyState>();

    private void Awake()
    {
        availableStates.Add(typeof(EnemyMovingState), new EnemyMovingState(this));
        availableStates.Add(typeof(EnemyDeathState), new EnemyDeathState(this));

    }

    private void Start()
    {
        TransitionToState(typeof(EnemyMovingState));
    }

    public void TransitionToState(Type stateType)
    {
        if (!availableStates.ContainsKey(stateType))
        {
            Debug.LogError($"State type {stateType} is not valid");
            return;
        }

        if (currentState != null && currentState.GetType() == stateType)
        {
            Debug.LogWarning($"Already in state {stateType}");
            return;
        }

        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = availableStates[stateType];
        currentState.EnterState();
    }


    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

}
