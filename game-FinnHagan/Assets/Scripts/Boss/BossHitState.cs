using UnityEngine;
public class BossHitState : IBossState
{
    private BossStateManager stateManager;

    public BossHitState(BossStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public void EnterState()
    {
        stateManager.GetComponent<Animator>().SetTrigger("bossHit");
        SoundEffects.instance.Play(stateManager.bossHitSound);
        stateManager.lastAttackTime = Time.time;
    }

    public void UpdateState()
    {
        if(stateManager.hitCount < 3)
        {
            stateManager.TransitionToState(typeof(BossMovingState));
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

    }

}
