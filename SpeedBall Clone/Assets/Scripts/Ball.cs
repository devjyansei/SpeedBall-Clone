using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Ball : MonoBehaviour
{   
    Vector3 targetPosition;
    Vector3 startPosition;
    public float Speed {get; private set;}
    float sideChangeDuration;
    float leftDirection = -1;
    float rightDirection = 1;
    float currentDirection;
    bool isChangingSide;
    public bool IsLiving { get; private set; }

    [SerializeField] float acceleration = 1f;
    [Range(1, 4)] [SerializeField] float horizontalMoveValue = 2f;
    [SerializeField] float sideChangeSpeedMultiplier = 2f;


    private void OnEnable()
    {
        GameManager.OnGameOver += StopTheBall;
        GameManager.OnWinTheGame += StopTheBall;
    }
    private void OnDisable()
    {
        GameManager.OnGameOver -= StopTheBall;
        GameManager.OnWinTheGame -= StopTheBall;
    }

    private void Start()
    {
        Speed = 5f;
        IsLiving = true;
        currentDirection = leftDirection;
    }
    private void Update()
    {
        Move();
    }

    void Move()
    {
        if (IsLiving)
        {
            Speed += acceleration * Time.deltaTime;
            sideChangeDuration += Time.deltaTime;

            if (!isChangingSide)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) && currentDirection != leftDirection)
                {
                    SetTargetPosition(leftDirection);
                    MoveToTargetPosition();
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) && currentDirection != rightDirection)
                {
                    SetTargetPosition(rightDirection);
                    MoveToTargetPosition();
                }
            }

            if (isChangingSide)
            {
                ChangeSide();
            }
            else
            {
                transform.position += Vector3.forward * Speed * Time.deltaTime;
            }
        }
        
    }
    void ChangeSide()
    {
        transform.position = Vector3.Lerp(startPosition, targetPosition, sideChangeDuration * sideChangeSpeedMultiplier);

        if (sideChangeDuration >= 1 / sideChangeSpeedMultiplier)
        {
            isChangingSide = false;
            sideChangeDuration = 0f;
            transform.position = targetPosition;
        }
    }
    void SetTargetPosition(float direction)
    {
        targetPosition = new Vector3(direction * horizontalMoveValue, transform.position.y, transform.position.z + Speed * (1 / sideChangeSpeedMultiplier));//z yi deðiþtir. velocitye göre
        currentDirection = direction;
    }
    void MoveToTargetPosition()
    {
        isChangingSide = true;
        startPosition = transform.position;
        sideChangeDuration = 0f;
    }
    void StopTheBall()
    {
        acceleration = 0;
        Speed = 0;
        IsLiving = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GameManager.OnGameOver?.Invoke();
        }
        if (other.gameObject.CompareTag("Finisher"))
        {
            GameManager.OnWinTheGame?.Invoke();
        }
    }
    
}