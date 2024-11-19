using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
//using UnityEngine.UIElements;


public class Player : Character
{
    [SerializeField] Slider hpSlider;
    [SerializeField] ParticleSystem bleeding;
    [SerializeField] GameObject GameOverWindow;
    [SerializeField] ParticleSystem healingEffect; // Adicione esta linha
    static Player instance;
    float attackSpeed;
    float expAdditional;
    int luck;
    bool isColliding;
    public bool morto = false;
    public ControladorJogo CJ;
    public List<Sprite> Roupinhas;
    public Animator Anim;
    int roupinha;

    public int vidasExtras = 0; // Vidas extras que o jogador possui

    private Player() { }

    void Awake()
    {
        string RoupaAtual = PlayerPrefs.GetString("RoupaEscolhido");
        CJ = GameObject.FindGameObjectWithTag("GameController").
                GetComponent<ControladorJogo>();
        Anim = GetComponent<Animator>();
        Initialize();
    }

    private void Start()
    {
        roupinha = PlayerPrefs.GetInt("Roupinha");
        Anim.SetInteger("Roupinha", roupinha);
    }

    private void Update()
    {
        hpSlider.value = GetHealthPoint();
    }

    protected override void Initialize()
    {
        base.Initialize();
        GameOverWindow.SetActive(false);
        instance = this;
        attackSpeed = 100f;
        expAdditional = 100f;
        luck = 0;
        //hpSlider.va
        hpSlider.maxValue = GetMaxHealthPoint();
        hpSlider.value = GetHealthPoint();
        isColliding = false;

        GetFirstWeapon();
    }

    public static Player GetInstance()
    {
        return instance;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public float GetExpAdditional()
    {
        return expAdditional;
    }

    public int GetLuck()
    {
        return luck;
    }

    public void DecreaseAttackSpeed(float value)
    {
        attackSpeed -= value;
    }

    public void IncreaseExpAdditional(float value)
    {
        expAdditional += value;
    }

    public void IncreaseLuck(int value)
    {
        luck += value;
    }

    public override void Die()
    {
        if (vidasExtras > 0)
        {
            vidasExtras--;
            Respawn();
        }
        else
        {
            morto = true;
            PlayerMove.GetInstance().isDead = true;
            StartCoroutine(DieAnimation());
        }
    }

    void Respawn()
    {
        morto = false;
        PlayerMove.GetInstance().isDead = false;
        RecoverHealthPoint(GetMaxHealthPoint()); // Restaurar a saúde do jogador
        hpSlider.value = GetHealthPoint(); // Atualizar o Slider de HP
        transform.position = Vector3.zero; // Exemplo de redefinir a posição do jogador
    }

    protected override IEnumerator DieAnimation()
    {
        GetAnimator().SetBool("Death", true);
        yield return new WaitForSeconds(1.6f);
        GameOverWindow.SetActive(true);
        Time.timeScale = 0f;
    }

    void GetFirstWeapon()
    {
        switch (GetComponentInParent<Player>().GetCharacterType())
        {
            case CharacterData.CharacterType.Knight:
                Inventory.GetInstance().AddWeapon(WeaponData.WeaponType.Whip);
                break;
            case CharacterData.CharacterType.Bandit:
                Inventory.GetInstance().AddWeapon(WeaponData.WeaponType.Axe);
                break;
        }
    }

    public override void ReduceHealthPoint(int damage)
    {
        if (!PlayerMove.GetInstance().isDead)
        {
            base.ReduceHealthPoint(damage);
            hpSlider.value = GetHealthPoint();
            bleeding.Play();
            isColliding = true;

            if (hitCoroutine == null)
                hitCoroutine = StartCoroutine(UnderAttack());
        }
    }

   

    protected override IEnumerator UnderAttack()
    {
        spriteRenderer.color = Color.red;

        do
        {
            isColliding = false;
            yield return new WaitForSeconds(0.2f);
        }
        while (isColliding);

        spriteRenderer.color = Color.white;
        hitCoroutine = null;
    }

    public void GanharVidaExtra()
    {
        vidasExtras++;
        Debug.Log("Vida extra adquirida! Total de vidas extras: " + vidasExtras);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "moeda")
        {
            CJ.GanhaMoedas(1);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D colidiu)
    {

    }
    public void UpdateHealthBar()
    {
        if (hpSlider != null)
        {
            //Debug.Log(hpSlider.value);
            hpSlider.value = GetHealthPoint() / GetMaxHealthPoint();
        }
    }

    public void PlayHealingEffect() // Adicione este método
    {
        if (healingEffect != null)
        {
            healingEffect.Play();
        }
    }
}
