using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // RigidBody e Spawner
    [SerializeField] private Rigidbody2D mj;
    [SerializeField] private Transform startPos;

    // Variáveis de controle de velocidade e força
    public float speed;
    float horizontalInput;
    float jumpPower = 5f;

    // Variáveis booleanas
    public bool isGrounded = false;
    bool isFacingRight = true;

    // Animator
    Animator anim;

    // Corpo
    void Start()
    {
        mj = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        posicionaPersonagem();
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown ("Jump") && isGrounded)
        {
            // Aplica uma força de pulo no personagem
            mj.velocity = new Vector2(mj.velocity.x, jumpPower);

            isGrounded = false;
            anim.SetBool("isJumping", !isGrounded);
        }

        FlipSprite();
    }

    private void FixedUpdate()
    {
        // Atualiza a velocidade do personagem com base na entrada horizontal e na velocidade configurada
        mj.velocity = new Vector2(horizontalInput * speed, mj.velocity.y);


        anim.SetFloat("xVelocity", Mathf.Abs(mj.velocity.x));
        anim.SetFloat("yVelocity", mj.velocity.y);
    }

    void FlipSprite()
    {
        // Verifica se deve inverter a sprite
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            // Inverte a direção do personagem
            isFacingRight = !isFacingRight;

            // Inverte a escala no eixo X
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }

    // Verificação de colisão com o chão
    private void OnCollisionEnter2D (Collision2D outro)
    {
        if (outro.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", !isGrounded);
        }
    }

    void posicionaPersonagem()
    {
        this.gameObject.transform.position = startPos.position;
    }
}
