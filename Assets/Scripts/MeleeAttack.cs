using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackcooldown;

    public float range;

    public int damage;

    public float colliderdistance;

    public BoxCollider2D boxcollider;

    private float cooldowntimer = Mathf.Infinity;

    public LayerMask Playerlayer;

    private Animator anim;

    private Health playerhealth;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxcollider.bounds.center + transform.right* range * transform.localScale.x *colliderdistance, 
            new  Vector3(boxcollider.bounds.size.x * range, boxcollider.bounds.size.y,boxcollider.bounds.size.z) ,0, Vector2.left,0,Playerlayer);
        if (hit.collider != null)
        {
            playerhealth = hit.transform.GetComponent<Health>();
            // Check if the player is dead
            if (playerhealth != null && playerhealth.IsDead())
            {
                return false;
            }
        }
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxcollider.bounds.center + transform.right * range * transform.localScale.x *colliderdistance,
            new Vector3(boxcollider.bounds.size.x * range, boxcollider.bounds.size.y, boxcollider.bounds.size.z));
    }
    private void DamagePlayer()
    {
        if (playerhealth != null)
        {
            playerhealth.TakeDamage(damage);
            if (playerhealth.IsDead())
            {
                FindAnyObjectByType<UIManager>().GameOver();
            }
        }
    }
}
