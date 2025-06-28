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


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerIndex = playerInput.playerIndex;
        Debug.Log($"Player {playerIndex} On Awake");
        if(GameManager.Instance.gameStats.AttackerPlayerIndex == playerIndex)
        {
            Debug.Log($"Player {playerIndex} is the Attacker");
            var attacker = gameObject.AddComponent<Attacker>();
            attacker.InitObject(playerIndex);
        }
        else
        {
            Debug.Log($"Player {playerIndex} is not the Attacker");
            var defender = gameObject.AddComponent<Defender>();
            defender.InitObject(playerIndex);
        }
    }

    private void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
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
