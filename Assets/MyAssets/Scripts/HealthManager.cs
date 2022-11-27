using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public PlayerController thePlayer;

    public float invincibilityLength;
    private float invincibilityCounter;

    public Renderer PlayerRenderer;
    private float flashCounter;
    public float flashLength= 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;

    public float respawnLength;

    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        //thePlayer = FindObjectOfType<PlayerController>();
        respawnPoint = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = currentHealth;
        //Validación para hacer que el jugador desaparezca al recibir daño
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                PlayerRenderer.enabled = !PlayerRenderer.enabled;
                flashCounter = flashLength;
            }
            if(invincibilityCounter <= 0)
            {
                PlayerRenderer.enabled = true;
            }
        }
    }

    //Función cuando el personaje obtiene un daño
    //Se disminuye su salud
    //Rebota el personaje conforme a la direccion en que llegó
    //Se muestra un efecto cuando recibe un daño (una leve desaparicion)
    //Si la salud llega a 0 se reinicia en la posicion original del personaje
    public void hurtPlayer(int damage, Vector3 direction)
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            if (currentHealth <= 0)
            {
                Respawn();
                FindObjectOfType<GameManager>().LoseLife();
            }
            else
            {
                thePlayer.KnockBack(direction);

                invincibilityCounter = invincibilityLength;

                PlayerRenderer.enabled = false;
                flashCounter = flashLength;
            }
        }
    }

    //Función para reaparecer en el punto inicial del nivel, se utiliza una corutina
    public void Respawn()
    {
        if (!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnLength);

        isRespawning = false;

        thePlayer.gameObject.SetActive(true);
        thePlayer.transform.position = respawnPoint;
        currentHealth = maxHealth;

        invincibilityCounter = invincibilityLength;
        PlayerRenderer.enabled = false;
        flashCounter = flashLength;
    }

    //Funcion para aumentar la salud del personaje
    public void healPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
