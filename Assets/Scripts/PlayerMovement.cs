using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    [Header("Movement")]
    public float kecepatan = 5f;

    private Vector2 arahGerak;

    private Rigidbody2D rb;
    private InputAction moveAction;


    [Header("Player Stats")]
    [SerializeField] private int hp = 100;


    [Header("Coin")]
    public int skor = 0;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        moveAction = InputSystem.actions.FindAction("Move");
    }




    void Update()
    {
        Move();
    }



    void Move()
    {
        if (moveAction != null)
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();

            arahGerak = moveValue.normalized;

            rb.linearVelocity = arahGerak * kecepatan;
        }
    }


    // ABSTRACTION:
    // Player menerapkan fungsi KenaDamage()
    // dari interface IDamageable.
    public void KenaDamage(int jumlah)
    {
        hp -= jumlah;

        Debug.Log(
            name + " kena " +
            jumlah +
            " damage. HP sekarang: " +
            hp
        );

        if (hp <= 0)
        {
            Mati();
        }
    }




    void Mati()
    {
        Debug.Log("PLAYER MATI!");

        // Menghentikan gerakan Player
        rb.linearVelocity = Vector2.zero;

        // Menonaktifkan script PlayerMovement
        enabled = false;
    }




    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);

            skor++;

            Debug.Log("Skor Coin Player: " + skor);

            GameManager gameManager =
                FindFirstObjectByType<GameManager>();

            if (gameManager != null)
            {
                gameManager.AmbilKoin();
            }
        }
    }
}