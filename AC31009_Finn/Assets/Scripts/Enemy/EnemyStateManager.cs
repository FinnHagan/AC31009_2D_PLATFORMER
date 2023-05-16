using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyStateManager : MonoBehaviour
{
    public AudioClip enemyDeathSound;

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
            Debug.LogError($"The state {stateType} is invalid");
            return;
        }

        if (currentState != null && currentState.GetType() == stateType)
        {
            Debug.LogWarning($"{stateType} is already active");
            return;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState != null)
        {
            currentState.OnTriggerEnter2D(collision);
        }
    }
}
