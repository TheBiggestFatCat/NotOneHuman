using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using Unity.VisualScripting;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 direction;
    private PlayerInput playerInput;
    private int playerIndex;
    public float speed = 5f;
    private Actor actor;
    private bool isAttacking = false;

    public UnityEvent<Vector2> PlayerMove;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerIndex = playerInput.playerIndex;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log($"Player {playerIndex} On Awake");
        MoveToStartPoint();
        InitGameObject();
    }

    public void MoveToStartPoint()
    {
        var playerData = GameManager.Instance.GetPlayerData(playerIndex);
        transform.position = playerData.StartPosition;
    }

    public void InitGameObject()
    {
        var newActor = GameManager.Instance.CreateActor(playerIndex, transform);
        if(newActor != null)
        {
            this.actor = newActor;
            
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameOver.AddListener(OnGameOver);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameOver.RemoveListener(OnGameOver);
    }

    private void OnGameOver(int arg0)
    {
        this.enabled = false;
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
        if(direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }        
    }

    public void OnAttack()
    {
        Debug.Log($"Player {playerIndex} attacked!");
        if(actor != null)
        {
            actor.Attack();
        }
    }
    public void AttackStart()
    {
        isAttacking = true;
    }
    public void Attackover()
    {
        isAttacking = false;
    }

    private void Move()
    {
        PlayerMove?.Invoke(direction);
        if (isAttacking) return;
        
        if(actor != null && actor.CanMove)
        {
            Vector2 movement = direction * Time.deltaTime * speed;
            rb.MovePosition(rb.position + movement);
        }
        //Debug.Log($"Player {playerIndex} is moving: {direction}");
    }


    void Update()
    {
        Move();
    }    
}
