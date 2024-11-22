using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Player : Character
{
    [SerializeField] Slider hpSlider;
    [SerializeField] ParticleSystem bleeding;
    [SerializeField] GameObject GameOverWindow;
    [SerializeField] ParticleSystem healingEffect;
    [SerializeField] GameObject lightningPrefab; // Adicione esta linha
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

    public int vidasExtras = 0;

    private int lightningAbilityLevel = 0;
    private bool isInvulnerable = false;

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
        RecoverHealthPoint(GetMaxHealthPoint());
        hpSlider.value = GetHealthPoint();
        transform.position = Vector3.zero;
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
            if (!isInvulnerable)
            {
                base.ReduceHealthPoint(damage);
                hpSlider.value = GetHealthPoint();
                bleeding.Play();
                isColliding = true;

                if (hitCoroutine == null)
                    hitCoroutine = StartCoroutine(UnderAttack());
            }
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
            hpSlider.value = GetHealthPoint() / GetMaxHealthPoint();
        }
    }

    public void PlayHealingEffect()
    {
        if (healingEffect != null)
        {
            healingEffect.Play();
        }
    }

    public void UpgradeLightningAbility(int level)
    {
        lightningAbilityLevel = level;
    }

    public void ActivateLightningAbility()
    {
        StartCoroutine(LightningAbilityCoroutine());
    }

    private IEnumerator LightningAbilityCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            List<Enemy> enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
            if (enemies.Count > 0)
            {
                int targetsHit = 0;
                int maxTargets = 2 + lightningAbilityLevel;

                Enemy currentTarget = enemies[Random.Range(0, enemies.Count)];
                while (targetsHit < maxTargets && currentTarget != null)
                {
                    // Lógica para aplicar dano ao inimigo
                    currentTarget.TakeDamage(10 + (5 * lightningAbilityLevel));

                    // Instanciar o raio
                    GameObject lightning = Instantiate(lightningPrefab, transform.position, Quaternion.identity);
                    LineRenderer lineRenderer = lightning.GetComponent<LineRenderer>();
                    ConfigureLightning(lineRenderer, transform.position, currentTarget.transform.position);

                    // Encontrar próximo alvo
                    enemies.Remove(currentTarget);
                    if (enemies.Count > 0)
                    {
                        currentTarget = enemies[Random.Range(0, enemies.Count)];
                    }
                    else
                    {
                        currentTarget = null;
                    }

                    targetsHit++;
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }


    public IEnumerator ActivateInvulnerability()
    {
        while (true)
        {
            isInvulnerable = true;
            yield return new WaitForSeconds(3f);
            isInvulnerable = false;
            yield return new WaitForSeconds(117f);
        }
    }
    private void ConfigureLightning(LineRenderer lineRenderer, Vector3 start, Vector3 end)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        // Adicionar um pouco de aleatoriedade para o efeito de raio
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            positions[i] = Vector3.Lerp(start, end, (float)i / (lineRenderer.positionCount - 1));
            positions[i] += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
        }
        lineRenderer.SetPositions(positions);
    }

}
