using UnityEngine;
public interface IEnemyState
{
    void EnterState();
    void UpdateState();
    void OnTriggerEnter2D(Collider2D other);
}
