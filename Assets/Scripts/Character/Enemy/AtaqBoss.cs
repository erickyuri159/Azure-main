using UnityEngine;
using System.Collections;

public class AtaqBoss : MonoBehaviour
{
    Enemy character;
    Coroutine coroutine;
    Animator animator;
    public float attackRange = 2.0f; // Distância para iniciar o ataque

    void Awake()
    {
        character = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsPlayerInRange())
        {
            if (coroutine == null && gameObject.activeSelf)
            {
                coroutine = StartCoroutine(Attack());
            }
        }
        else
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }

    bool IsPlayerInRange()
    {
        GameObject player = Player.GetInstance().gameObject;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance <= attackRange;
    }

    IEnumerator Attack()
    {
        while (true)
        {
            GiveDamage();
            animator.SetTrigger("Attack"); // Chama a animação de ataque
            yield return new WaitForSeconds(0.2f);
        }
    }

    void GiveDamage()
    {
        int damage = character.GetAttackPower() - (int)(character.GetAttackPower() * Player.GetInstance().GetDefencePower() / 100f);
        Player.GetInstance().ReduceHealthPoint(damage);
    }
}