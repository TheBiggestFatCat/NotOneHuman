using UnityEngine.InputSystem;
using UnityEngine;
using UnityEditor.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 direction;
    private PlayerInput playerInput;
    private int playerIndex;
    public float speed = 5f;
    private Actor actor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerIndex = playerInput.playerIndex;
        InitGameObject();
    }

    public void InitGameObject()
    {
        Debug.Log($"Player {playerIndex} On Awake");
        PlayerData playerData = GameManager.Instance.GetPlayerData(playerIndex);
        if (playerData != null)
        {
            var obj = Instantiate(playerData.prefab, transform);
            actor = obj.GetComponent<Actor>();
        }
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

    private void Move()
    {
        Vector2 movement = direction * Time.deltaTime * speed;
        rb.MovePosition(rb.position + movement);
    }

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }
}
