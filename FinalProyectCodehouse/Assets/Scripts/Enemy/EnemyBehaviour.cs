using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private bool isIdle;
    private Vector3 PreviousPosition;
    enum EnemyTypes { Malee, Range };
    [SerializeField] EnemyTypes enemyType;
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator enemyAnimator;

    void Start()
    {

    }
    void Update()
    {
        switch (enemyType)
        {
            case EnemyTypes.Malee:
                ChasePlayer();
                break;
            case EnemyTypes.Range:
                RotateAroundPlayer();
                break;
        }
    }

    private void RotateAroundPlayer()
    {
        LookPlayer();
        transform.RotateAround(playerTransform.position, Vector3.up, 5f * Time.deltaTime);
        Animate();
        Invoke("resetPosition", 2f);  
    }

    private void ChasePlayer()
    {
        LookPlayer();
        Vector3 direction = (playerTransform.position - transform.position);
        if (direction.magnitude > 1.5f)
        {
            transform.position += direction.normalized * speed * Time.deltaTime;
            Animate();
            Invoke("resetPosition", 2f);
        }
    }

    private void LookPlayer()
    {
        Quaternion newRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 1.5f * Time.deltaTime);
    }

    private void Animate()
    {
        isIdle= (PreviousPosition == transform.position) ? true : false;
        switch (isIdle)
        {
            case false:
                enemyAnimator.SetBool("Idle", false);
                enemyAnimator.SetBool("Run", true);
                break;
            case true:
                enemyAnimator.SetBool("Idle", true);
                enemyAnimator.SetBool("Run", false);
                break;
        }

    }
    private void resetPosition()
    {
        PreviousPosition = transform.position;
    }

}
