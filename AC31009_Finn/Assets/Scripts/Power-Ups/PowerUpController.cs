using UnityEngine;
using System.Collections;

public class PowerUpController : MonoBehaviour
{
    private PlayerStateManager playerState;
    private Health playerHealth;

    public enum PowerUpType
    {
        SpeedBoost,
        Invincibility,
        None
    }

    private void Awake()
    {
        playerState = GameObject.Find("Player").GetComponent<PlayerStateManager>();
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
    }

    public void ActivatePowerup()
    {
            // Randomly choose power-up type
            PowerUpType type = GetRandomPowerUpType();

            // Apply power-up effect based on power-up type
            switch (type)
            {
                case PowerUpType.SpeedBoost:
                    StartCoroutine(SuperJelly());
                    break;
                case PowerUpType.Invincibility:
                    StartCoroutine(InvincibilityEffect());
                    break;
                case PowerUpType.None:
                    break;
            }
    }

    private IEnumerator SuperJelly()
    {
        float originalSpeed = playerState.playerSpeed;
        playerState.playerSpeed *= 2f;
        float originalJumpForce = playerState.jumpForce;
        playerState.jumpForce *= 2f;
        yield return new WaitForSeconds(5f);
        playerState.playerSpeed = originalSpeed;
        playerState.jumpForce = originalJumpForce;
    }

    private IEnumerator InvincibilityEffect()
    {
        playerHealth.checkInvincibility = true;
        yield return new WaitForSeconds(5f);
        playerHealth.checkInvincibility = false;
    }

    // Returns a random power-up type
    private PowerUpType GetRandomPowerUpType()
    {
        int randomIndex = Random.Range(0, 2);
        switch (randomIndex)
        {
            case 0:
                return PowerUpType.SpeedBoost;
            case 1:
                return PowerUpType.Invincibility;
            default:
                return PowerUpType.None;
        }
    }
}
