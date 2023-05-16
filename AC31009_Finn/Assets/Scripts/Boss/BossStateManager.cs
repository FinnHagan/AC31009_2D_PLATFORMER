using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class BossStateManager : MonoBehaviour
{
	public AudioClip bossDeathSound;
    public AudioClip bossHitSound;
    public WinScreen winScreen;
    public AudioClip gameWonSound;
    public float hit = 1f;
    public float attackCooldown = 1f;
    public float lastAttackTime = 0f;
    public int hitCount = 0;

    public IBossState currentState { get; private set; }

	private Dictionary<Type, IBossState> availableStates = new Dictionary<Type, IBossState>();

	private void Awake()
	{
		availableStates.Add(typeof(BossMovingState), new BossMovingState(this));
		availableStates.Add(typeof(BossHitState), new BossHitState(this));
        availableStates.Add(typeof(BossDeathState), new BossDeathState(this));
    }

    private void Start()
	{
		TransitionToState(typeof(BossMovingState));
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

    public void Defeated()
    {
        SoundEffects.instance.Play(bossDeathSound);
        GetComponent<Collider2D>().enabled = false;
        enabled = false; //Disables movement
		StartCoroutine(WinScreenTimer());
    }

    public void WinScreen()
    {
        winScreen.GameWon();
    }

    public IEnumerator WinScreenTimer()
    {
        yield return new WaitForSeconds(3f);
        WinScreen();
        SoundEffects.instance.Play(gameWonSound);

    }
}
