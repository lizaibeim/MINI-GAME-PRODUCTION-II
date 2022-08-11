using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private bool destroyBullet;
    private bool enterEnumerator;
    public int damage;

    public float startupTime;

    private void Start()
    {
        destroyBullet = false;
        enterEnumerator = false;
    }

    private void Update()
    {
        startupTime -= Time.deltaTime;

        if (enterEnumerator)
            StartCoroutine(numerator());
        if (destroyBullet)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (startupTime > 0) return;

        PlayerStatus playerStatus = collision.gameObject.GetComponent<PlayerStatus>();
        if (playerStatus != null) playerStatus.TakeDamage(damage);
        print(collision.gameObject);
        enterEnumerator = true;
    }

    IEnumerator numerator()
    {
        Debug.Log("Trigered");
        yield return new WaitForSeconds(0.1f);
        destroyBullet = true;
    }
    // Start is called before the first frame update

}
