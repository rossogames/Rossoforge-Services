using UnityEngine;
using RossoForge.Service.Locator;

public class Enemy : MonoBehaviour
{
    private IPlayerHealthService playerHealthService;

    public int damage = 25;

    private void Start()
    {
        playerHealthService = ServiceLocator.Get<IPlayerHealthService>();
    }

    public void ApplyDamage()
    {
        playerHealthService.TakeDamage(damage);
    }
}
