using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerscript : MonoBehaviour
{
    float maxhealth = 3f;
    float currentHealth = 3f;
    float speed = 5f;
    float horizontal;
    float vertical;
    float gutlingLastFired = 0f;
    float gutlingInterval = 0.2f;
    Rigidbody _rb;
    Vector3 lookDirection = new Vector3(0, 1, 0);

    public GameObject attackSphere;
    SphereAttack sphereScript;
    float sphereTimeDelay = 2.2f;
    float currentSphereTime = 3f;

    public Healthbar healthBar;

    AudioSource audioSource;

    public AudioClip missiles;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        healthBar.SetMaxHealth(maxhealth);
        sphereScript = attackSphere.GetComponent<SphereAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        gutlingLastFired += Time.deltaTime;

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
            Debug.Log("Special attack");
        }
    }
    void FixedUpdate()
    {
        Vector2 position = _rb.position;
        
        position.x = Mathf.Clamp(position.x + speed * horizontal * Time.deltaTime, -9, 9);
        position.y = Mathf.Clamp(position.y + speed * vertical * Time.deltaTime, -3, 7);
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

    public GameObject projectilePrefab;

    void ShootMissiles()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, _rb.position + Vector3.up * 0.5f, Quaternion.identity);
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
