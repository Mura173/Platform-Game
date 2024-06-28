using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float speed;
    public float distance;

    bool isRight = true;

    // Pegar o transform de um game object "GroundCheck"
    public Transform groundCheck;

    // Vida
    [SerializeField]
    private int vidas;

    // Animator
    public Animator animSkeleton;

    void Start()
    {
       animSkeleton = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimentacao do inimigo
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Passar posições nesse método
                                                // Posição de origem  // Direção    // Distância
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);

        // Verificacao para ver se o inimigo esta no chao
        if (ground.collider == false)
        {
            if (isRight == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isRight = true;
            }
        }

        if(speed > 0)
        {
            animSkeleton.SetBool("idle", false);
            animSkeleton.SetBool("andar", true);
        }
    }

    public void ReceberDano()
    {
        this.vidas--;

        if (this.vidas == 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
