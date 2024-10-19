using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int maxHealth = 100; //몬스터 최대 체력
    private int currentHealth;  //몬스터 현재 체력

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    //데미지 계산
    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) // 현재 체력이 0 이하면 제거
        {
            Invoke("DestroyMonster", 0.9f);
        }
    }

    //몬스터 제거
    public void DestroyMonster()
    {
        Destroy(gameObject);
    }
}
