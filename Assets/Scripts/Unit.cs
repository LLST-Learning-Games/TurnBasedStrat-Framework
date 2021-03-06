using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] private Animator unitAnimator;
    private Vector3 targetPosition;
    private GridPosition currentGridPosition;


    private float moveSpeed = 4f;
    private float stopDist = 0.1f;
    private float rotateSpeed = 10f;



    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Start()
    {
        currentGridPosition = LevelGrid.Instance.GetGridPositionFromWorldPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(currentGridPosition, this);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) >= stopDist) {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
            unitAnimator.SetBool("isWalking", true);
        }
        else
            unitAnimator.SetBool("isWalking", false);

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPositionFromWorldPosition(transform.position);

        if(newGridPosition != currentGridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, currentGridPosition, newGridPosition);
            currentGridPosition = newGridPosition;
        }

    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
