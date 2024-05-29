using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent enemyAgent;
    private Animator animator;
    private FirstPersonController playerMovement;

    [SerializeField]
    private float distance = 30;

    [SerializeField]
    private string blendParameterName = "Blend";

    [SerializeField]
    private float blendDuration = 1f;

    private Coroutine blendCoroutine;

    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        playerMovement = FindObjectOfType<FirstPersonController>();
        animator = GetComponent<Animator>();
        if(playerMovement == null)
        {
            Debug.LogError("PlayerMovement is null");
        }
        if (enemyAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing on " + gameObject.name);
        }
        else if (!enemyAgent.isOnNavMesh)
        {
            Debug.LogError(gameObject.name + " is not on a NavMesh at position " + transform.position);
        }


    }

    private void Update()
    {
        if (!enemyAgent.isOnNavMesh)
        {
            enemyAgent = GetComponent<NavMeshAgent>();

        }
        if (enemyAgent != null && playerMovement != null)
        {
            Vector3 posEnemy = enemyAgent.transform.position;
            Vector3 posPlayer = playerMovement.transform.position;
            if (Vector3.Distance(posEnemy, posPlayer) < distance)
            {
                Vector3 direction = (playerMovement.transform.position - transform.position).normalized;
                Vector3 moveVector = direction * enemyAgent.speed * Time.deltaTime;
                enemyAgent.Move(moveVector);
                StartBlend();
            }
            else
            {
                if (blendCoroutine != null)
                {
                    StopCoroutine(blendCoroutine);
                }
                blendCoroutine = StartCoroutine(BlendParameter(0));
            }
        }
    }

    private void StartBlend()
    {
        if (blendCoroutine != null)
        {
            StopCoroutine(blendCoroutine);
        }
        blendCoroutine = StartCoroutine(BlendParameter(1));
    }

    private IEnumerator BlendParameter(float targetBlendValue)
    {
        float currentBlendValue = animator.GetFloat(blendParameterName);
        float blendTimer = 0f;

        while (blendTimer < blendDuration)
        {
            blendTimer += Time.deltaTime;
            float blendValue = Mathf.Lerp(currentBlendValue, targetBlendValue, blendTimer / blendDuration);
            animator.SetFloat(blendParameterName, blendValue);
            yield return null;
        }

        // Ensure that the blend value is exactly the target value
        animator.SetFloat(blendParameterName, targetBlendValue);
    }
}
