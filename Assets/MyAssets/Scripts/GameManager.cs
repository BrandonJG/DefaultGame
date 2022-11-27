using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public int currentGold;
    [SerializeField]
    public int lives;
    public Text goldText;
    public Text livesText;
    public GameObject beacon;
    public GameObject message;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Función para agregar el valor del oro para mostrar cuantos se han recogido
    public void AddGold(int goldToAdd)
    {
        currentGold += goldToAdd;
        goldText.text = "Gold: " + currentGold;
        if(currentGold == 20)
        {
            beacon.SetActive(true);
            message.SetActive(true);
        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        Destroy(message);
    }

    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            FindObjectOfType<Scene_Manager>().LoadMainMenu();
        }
        livesText.text = "Vidas restantes: " + lives;
    }
}
