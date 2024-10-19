using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    Unit target;
    int dmg;
    float speed;
    int team;

    public void init(Unit target, int team ,int dmg, float speed)
    {
        this.target = target;
        this.team = team;
        this.dmg = dmg;
        this.speed = speed;
        this.speed /= 100;
    }

    private void Update()
    {
        if(target != null)
        {
            Vector3 newPos = new Vector3();
            transform.LookAt(target.transform);
            newPos = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            rb.MovePosition(newPos);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Unit tmp = other.GetComponent<Unit>();
        if(target == tmp)
        {
            target.GetDamage(dmg);
            Destroy(this.gameObject);
        }
    }
}
