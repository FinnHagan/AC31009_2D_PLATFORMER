using UnityEngine;
public interface IBossState
{
    void EnterState();
    void UpdateState();
    void OnTriggerEnter2D(Collider2D other);
}
