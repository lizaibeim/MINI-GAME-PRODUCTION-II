using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public float forceThreshold;
    public delegate void StatusEvent();
    public StatusEvent takeDamageEvent;
    public StatusEvent deathEvent;
    public static bool isDead;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        health = maxHealth;
        isDead = false;
    }

    public int Health
    {
        get { return health; }
        set
        {

            health = value;
            if (value <= 0)
            {
                health = 0;
                if (!isDead)
                {
                    isDead = true;
                    deathEvent?.Invoke();
                    TransitionManager.Instance.Death();
                    LevelManager.Instance.Death();
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        float collisionImpact = collision.impulse.magnitude;
        if (collisionImpact > forceThreshold)
        {
            TakeDamage((int)collisionImpact);

        }
    }

    public void TakeDamage(int damage)
    {

        Health -= damage;
        CameraShake.Instance.ShakeCamera(10, 1);
        takeDamageEvent?.Invoke();
    }
}
