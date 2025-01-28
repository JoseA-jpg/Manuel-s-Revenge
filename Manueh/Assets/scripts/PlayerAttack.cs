using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ajustes de Ataque")]
    public float attackRange = 2f; // Rango del ataque
    public int attackDamage = 10; // Daño del ataque
    public LayerMask enemyLayer; // Capa de los enemigos

    [Header("Cooldown del Ataque")]
    public float attackCooldown = 1f; // Tiempo entre ataques
    private bool canAttack = true;

    // Update se llama una vez por frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Iniciar el ataque
        Debug.Log("Atacando!");
        canAttack = false;

        // Detectar enemigos dentro del rango
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        // Aplicar daño a cada enemigo detectado
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("Golpeó a " + enemy.name);
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);
        }

        // Iniciar cooldown
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    // Dibujar el rango de ataque en la vista del editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}