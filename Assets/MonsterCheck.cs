using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCheck : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider;

    [SerializeField] private GameObject Player;

    [SerializeField] private GameObject Spawner;

    [SerializeField] private int monsterInZoneCount;
    private void Start()
    {
        //circleCollider.enabled = false;
    }

    private void Update()
    {
        transform.position = Player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (monsterInZoneCount > 50)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                monsterInZoneCount += 1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            monsterInZoneCount -= 1;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (monsterInZoneCount > 50)
        {
            Destroy(collision.gameObject);
        }
    }

    public void DecreaseMonsterInZone()
    {
        monsterInZoneCount -= 1;
    }
}