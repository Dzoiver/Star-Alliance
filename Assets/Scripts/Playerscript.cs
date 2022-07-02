using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Powerup
{
    public float time = 0f;
    public string name = "";
    public Powerup(float t, string n)
    {
        time = t;
        name = n;
    }
}
public class Playerscript : MonoBehaviour
{
    public static Playerscript instance;
    float maxhealth = 5f;
    float currentHealth = 5f;
    float speed = 6f;
    float verticalSpeedMult = 0.8f;
    float currentSpeed = 6f;
    float horizontal;
    float vertical;
    List<Powerup> powerUps = new List<Powerup>();

    public float rocketDamage = 4f;

    public float gutlingSize = 1f;
    public float gutlingDamage = 1f;
    float gutlingLastFired = 0f;
    float gutlingInterval = 0.15f;
    Rigidbody _rb;
    Vector3 lookDirection = new Vector3(0, 1, 0);

    public GameObject attackSphere;
    SphereAttack sphereScript;
    float sphereTimeDelay = 2f;
    float currentSphereTime = 3f;

    public Healthbar healthBar;

    AudioSource audioSource;
    Transform sphereTrans;
    Quaternion startValue;

    private void Awake()
    {
        instance = this;
    }
    public AudioClip missiles;
    // Start is called before the first frame update
    public Vector3 GetPos()
    {
        return transform.position;
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        healthBar.SetMaxHealth(maxhealth);
        sphereScript = attackSphere.GetComponent<SphereAttack>();
        startValue = transform.rotation;
        sphereTrans = attackSphere.GetComponent<Transform>();
    }
    float timeElapsed = 0f;
    float lerpDuration = 0.6f;
    float currentDodgeTime = 2f;
    float dodgeCDTime = 0.6f;
    bool dodging = false;

    bool isSpinning = false;

    void Lerp()
    {
        if (timeElapsed < lerpDuration)
        {
            currentSpeed = Mathf.Lerp(12f, speed, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }
        else
        {
            dodging = false;
            currentSpeed = speed;
            timeElapsed = 0f;
        }
    }
    float time = 0;
    void LerpFunction(Quaternion endValue, float duration)
    {
        if (time < duration)
        {
            float rot = Mathf.Lerp(0, 360, time / duration);
            // rb.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed * Time.deltaTime));
            _rb.MoveRotation(Quaternion.Euler(-90, 0, rot));
            // transform.rotation = Quaternion.Euler(rot, 0, 0);

            // transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
        }
        else
        {
            transform.rotation = startValue;
            isSpinning = false;
            time = 0;
        }
    }

    void LerpSpin()
    {
        if (spinTimeElapsed < lerpDurationSpin)
        {
            transform.Rotate(0, -spinSpeed * Time.deltaTime, 0);
            currentSpeed = Mathf.Lerp(12f, speed, spinTimeElapsed / lerpDurationSpin);
            spinTimeElapsed += Time.deltaTime;
        }
        else
        {
            dodging = false;
            currentSpeed = speed;
            spinTimeElapsed = 0f;
        }
    }
    float lerpDurationSpin = 0f;
    float spinSpeed = 1f;
    float lastTimeSpin = 0f;
    float currentTimeSpin = 0f;
    float spinTimeElapsed = 0f;

    void SpinShipAround()
    {
        isSpinning = true;
    }
    // Update is called once per frame
    public Vector3 targetRotation;
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        gutlingLastFired += Time.deltaTime;
        currentSpecialCD += Time.deltaTime;

        Vector2 move = new Vector2(horizontal, vertical);

        if (Input.GetKey("space") && gutlingLastFired > gutlingInterval)
        {
            gutlingLastFired = 0f;
            ShootMissiles();
            PlaySound(missiles);
        }

        if (Input.GetKeyDown("j"))
        {
            UseHeal();
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Game over");
            Destroy(gameObject);
        }

        currentSphereTime += Time.deltaTime;
        if (Input.GetKeyDown("i") && currentSphereTime > sphereTimeDelay)
        {
            currentSphereTime = 0f;
            sphereScript.Attack();
        }
        if (Input.GetKeyDown("l"))
        {
            Debug.Log("Melee swing attack");
        }
        if (Input.GetKeyDown("o"))
        {
            ShootSpecial();
        }

        if (Input.GetKeyDown("k") && currentDodgeTime > dodgeCDTime)
        {
            currentDodgeTime = 0f;
            timeElapsed = 0f;
            dodging = true;
            SpinShipAround();
        }
        currentDodgeTime += Time.deltaTime;

        if (dodging)
        {
            Lerp();
        }

        if (isSpinning)
        {
            LerpFunction(Quaternion.Euler(targetRotation), lerpDuration);
        }

        /*        HandlePowerUps();*/

        for (int i = 0; i < powerUps.Count; i++)
        {

            if (powerUps[i].time > 0f)
            {
                UI.instance.PUp.SetActive(true);
                powerUps[i].time -= Time.deltaTime;
            }
            else
            {
                if (powerUps[i].name == "Attack PowerUp")
                {
                    if (powerUps.Count <= 1)
                    UI.instance.PUp.SetActive(false);
                    RemoveAttackPowerUp();
                    powerUps.RemoveAt(i);
                }
            }
        }
    }

    void HandlePowerUps()
    {

    }
    void RemoveAttackPowerUp()
    {
        gutlingDamage -= 1f;
        gutlingSize -= 0.6f;
    }
    public void AttackPowerup()
    {
        gutlingDamage += 1f;
        gutlingSize += 0.6f;
        powerUps.Add(new Powerup(30f, "Attack PowerUp"));
    }
    void FixedUpdate()
    {
        Vector2 position = _rb.position;
        
        position.x = Mathf.Clamp(position.x + currentSpeed * horizontal * Time.deltaTime, -9, 9);
        position.y = Mathf.Clamp(position.y + currentSpeed * vertical * verticalSpeedMult * Time.deltaTime, -3, 7);
        _rb.MovePosition(position);
    }

    public GameObject Potion;

    void UseHeal()
    {
        Potions potion = Potion.GetComponent<Potions>();
        if (potion.Remove() && currentHealth < maxhealth)
        {
            currentHealth += potion.healAmount;
            healthBar.SetHealth(currentHealth);
        }
    }

    public GameObject rocketPrefab;

    public float specialCD = 15f;
    public float currentSpecialCD = 16f;
    void ShootSpecial()
    {
        if (currentSpecialCD > specialCD)
        {
            currentSpecialCD = 0f;
            // int rand = Random.Range(8, 12);
            int rand = 15;
            for (int i = 0; i < rand; i++)
            {
                GameObject rocket = Instantiate(rocketPrefab, _rb.position, Quaternion.identity);
            }
        }
    }

    public GameObject projectilePrefab;

    void ShootMissiles()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, _rb.position, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        // animator.SetTrigger("Launch");
    }

    public void PlaySound(AudioClip clip)
    {
        // audioSource.PlayOneShot(clip);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
