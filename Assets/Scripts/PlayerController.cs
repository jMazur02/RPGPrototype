using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float turnSpeed = 50f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f; // Wysokość skoku w metrach (możesz zmieniać w Inspektorze)
    public InputAction jumpAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private CharacterController controller;
    private Animator anim;
    public InputAction moveAction;
    private Vector2 moveInput;
    private float verticalVelocity;
    public GameObject inventoryPanel;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        moveAction.Enable();
        anim = GetComponentInChildren<Animator>();
        jumpAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryPanel != null && inventoryPanel.activeSelf)
        {
            if (anim != null)
            {
                anim.SetBool("isWalking", false);
            }
            return;
        }
        moveInput = moveAction.ReadValue<Vector2>();

        // 1. OBRACANIE (to zostaje bez zmian, obrót nie potrzebuje fizyki)
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * moveInput.x);

        // 2. CHODZENIE DO PRZODU/TYŁU (używamy Character Controllera)
        // Obliczamy kierunek na podstawie tego, gdzie postać "patrzy"
        Vector3 horizontalMove = transform.forward * moveInput.y * speed;

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        if (jumpAction.triggered && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

            // === NOWA LINIJKA DO ANIMACJI ===
            if (anim != null)
            {
                anim.SetTrigger("jumpTrigger"); // Aktywujemy jednorazowy impuls skoku
            }
        }


        // Ciągnij postać w dół z siłą grawitacji z każdą klatką
        verticalVelocity += gravity * Time.deltaTime;

        // Aplikuj to spadanie w dół do postaci
        Vector3 finalMovement = horizontalMove;
        finalMovement.y = verticalVelocity;

        controller.Move(finalMovement * Time.deltaTime);

        if (anim != null)
        {
            // Jeśli moveInput.y nie jest równe 0 (czyli idziemy w przód lub w tył), 
            // zmienna 'idzie' przyjmie wartość true. W przeciwnym wypadku false.
            bool idzie = moveInput.y != 0;

            // Wysyłamy tę informację do naszego Animator Controller
            anim.SetBool("isWalking", idzie);
        }
    }
}
