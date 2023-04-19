using UnityEngine;

public interface IEnemyState
{
    void EnterState();
    void UpdateState();
    void ExitState();
    void OnTriggerEnter2D(Collider2D other);
}
