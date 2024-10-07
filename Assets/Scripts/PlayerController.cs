using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    private Rigidbody2D rb; // Componente Rigidbody2D
    public Vector2 movement; // Vector de movimiento
    private Animator animator; // Componente Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Captura la entrada del jugador
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Verifica si las teclas opuestas están presionadas
        bool isOppositeX = (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow));
        bool isOppositeY = (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow));

        if (isOppositeX)
        {
            // Detener movimiento en el eje X si las teclas opuestas están presionadas
            moveX = 0;
        }

        if (isOppositeY)
        {
            // Detener movimiento en el eje Y si las teclas opuestas están presionadas
            moveY = 0;
        }

        // Bloquear movimiento diagonal
        if (Mathf.Abs(moveX) > Mathf.Abs(moveY))
        {
            movement.x = moveX;
            movement.y = 0; // Si el movimiento horizontal es dominante, bloquear el vertical
        }
        else
        {
            movement.x = 0; // Si el movimiento vertical es dominante, bloquear el horizontal
            movement.y = moveY;
        }

        // Actualiza las animaciones
        UpdateAnimation();
    }


    void FixedUpdate()
    {
        // Mueve al personaje
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void UpdateAnimation()
    {
        // Configura las animaciones según la dirección del movimiento
        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
