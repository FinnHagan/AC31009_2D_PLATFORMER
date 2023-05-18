using UnityEngine;
public class BossDeathState : IBossState
{
    private BossStateManager stateManager;
    public BossDeathState(BossStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public void EnterState()
    {
        stateManager.GetComponent<Animator>().SetTrigger("bossDie");
        stateManager.Defeated();
    }

    public void UpdateState()
    {
        //No update to be done as boss  is dead
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //No trigger events as boss is dead
    }
}
