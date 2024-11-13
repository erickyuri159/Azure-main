using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D Playerbody2D;
    static PlayerMove instance;
    public Animator animator;
    SpriteRenderer spriteRenderer;
    Player character;
    float horizontal;
    float vertical;
    bool lookingLeft;
    public bool isDead;
    private Vector2 direcaoPlayer;
    private Vector3 direcaoMause;
    public float moveSpeed; // Velocidade de movimento do personagem

    private void Start()
    {
        Playerbody2D = GetComponent<Rigidbody2D>();
    }

    void Awake()
    {
        character = GetComponent<Player>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lookingLeft = false;
        instance = this;
        isDead = false;
    }

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (!Level.GetIsLevelUpTime())
            {
                // Verifica se o botão do mouse está pressionado
                if (Input.GetMouseButton(0))
                {
                    // Pega a posição do mouse
                    Vector3 destino = Input.mousePosition;
                    Vector3 PosTela = Camera.main.ScreenToWorldPoint(destino);
                    Vector3 PosTelaCorrigida = new Vector3(PosTela.x, PosTela.y + 2.5f, 0);

                    direcaoMause = PosTelaCorrigida - transform.position;

                    animator.SetFloat("horizontal", Mathf.Clamp(direcaoMause.x,-1,1));
                    animator.SetFloat("vertical", Mathf.Clamp(direcaoMause.y, -1, 1));
                    animator.SetFloat("velocidade", Mathf.Clamp(direcaoMause.sqrMagnitude,-2,2));

                    // Mover o jogador em direção ao mouse
                    transform.position = Vector3.MoveTowards(transform.position, PosTelaCorrigida, moveSpeed * Time.deltaTime);
                }
                else
                {
                    // Pega a entrada do teclado
                    direcaoPlayer = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                    animator.SetFloat("horizontal", direcaoPlayer.x);
                    animator.SetFloat("vertical", direcaoPlayer.y);
                    animator.SetFloat("velocidade", direcaoPlayer.sqrMagnitude);

                    if (direcaoPlayer != Vector2.zero)
                    {
                        animator.SetFloat("horizontal", direcaoPlayer.x);
                        animator.SetFloat("vertical", direcaoPlayer.y);
                        animator.SetFloat("velocidade", direcaoPlayer.sqrMagnitude);
                    }

                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!character.morto)
        {
            Playerbody2D.MovePosition(Playerbody2D.position + direcaoPlayer * moveSpeed * Time.fixedDeltaTime);
        }           
    }

    public static PlayerMove GetInstance()
    {
        return instance;
    }

    public bool GetLookingLeft()
    {
        return lookingLeft;
    }

    public float GetHorizontal()
    {
        return horizontal;
    }

    public float GetVertical()
    {
        return vertical;
    }
   
}