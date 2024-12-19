using UnityEngine;

public class DeathHandler : HealthHandler
{
    private void Awake()
    {
        CharacterDead += Die;
    }

    private void OnDestroy()
    {
        CharacterDead -= Die;
    }

    private void Die(bool isDead)
    {
        if (isDead)
        {
            gameObject.SetActive(false);
        }
    }
}
