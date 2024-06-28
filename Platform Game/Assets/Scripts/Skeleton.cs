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

    void Update()
    {
        // Movimentacao do inimigo
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Passar posi��es nesse m�todo
                                                // Posi��o de origem  // Dire��o    // Dist�ncia
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
    }

    public void ReceberDano()
    {
        this.vidas--;
        Debug.Log(this.name + " foi atacado. Vida: " + this.vidas);
        if(this.vidas == 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
