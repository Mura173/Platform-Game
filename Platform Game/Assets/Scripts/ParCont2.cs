using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParCont2 : MonoBehaviour
{
    // Refer�ncia � transforma��o da c�mera principal
    Transform cam;
    // Posi��o inicial da c�mera
    Vector3 camStartPos;
    // Posi��o anterior da c�mera
    Vector3 prevCamPos;
    // Vetor de dist�ncia entre a posi��o anterior e atual da c�mera
    Vector3 distance;

    // Array de GameObjects que representam os backgrounds
    GameObject[] backgrounds;
    // Array de materiais dos backgrounds
    Material[] mat;
    // Array de velocidades dos backgrounds
    float[] backSpeed;

    // Vari�vel para armazenar a maior dist�ncia de um background em rela��o � c�mera
    float farthestBack;

    // Controle da velocidade do parallax, ajust�vel no editor entre 0.01 e 0.05
    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;

    // M�todo chamado ao iniciar o script
    void Start()
    {
        // Inicializa a refer�ncia da c�mera principal
        cam = Camera.main.transform;
        // Armazena a posi��o inicial da c�mera
        camStartPos = cam.position;
        // Armazena a posi��o anterior da c�mera como a inicial
        prevCamPos = camStartPos;

        // Obt�m a contagem de filhos (backgrounds) do transform atual
        int backCount = transform.childCount;
        // Inicializa os arrays com o tamanho correspondente ao n�mero de backgrounds
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        // Preenche os arrays de backgrounds e materiais
        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;

            // Configura o modo de repeti��o da textura para lidar com a repeti��o da textura
            mat[i].mainTexture.wrapMode = TextureWrapMode.Repeat;
        }
        // Calcula a velocidade de cada background
        BackSpeedCalculate(backCount);
    }

    // M�todo para calcular a velocidade do parallax de cada background
    void BackSpeedCalculate(int backCount)
    {
        // Encontra o background mais distante
        for (int i = 0; i < backCount; i++)
        {
            float distanceToCam = backgrounds[i].transform.position.z - cam.position.z;
            if (distanceToCam > farthestBack)
            {
                farthestBack = distanceToCam;
            }
        }

        // Calcula a velocidade de parallax para cada background
        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    // M�todo chamado ap�s todas as atualiza��es do frame terem sido processadas
    private void LateUpdate()
    {
        // Calcula a dist�ncia que a c�mera se moveu em rela��o � posi��o anterior
        distance = cam.position - prevCamPos;

        // Atualiza a posi��o do transform atual para seguir a c�mera, mantendo a posi��o Z original
        transform.position = new Vector3(cam.position.x, cam.position.y, transform.position.z);

        // Atualiza o offset da textura dos materiais dos backgrounds para criar o efeito de parallax
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            Vector2 offset = new Vector2(distance.x, distance.y) * speed;
            mat[i].SetTextureOffset("_MainTex", mat[i].GetTextureOffset("_MainTex") + offset);
        }

        // Atualiza a posi��o anterior da c�mera
        prevCamPos = cam.position;
    }
}
