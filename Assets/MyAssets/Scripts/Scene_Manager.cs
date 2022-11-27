using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    private static Scene_Manager _instance { get; set; } //Singleton
    [SerializeField]
    private static string siguienteNivel;

    private void Awake()
    {
        siguienteNivel = "Level_0";
        if(_instance!=null && _instance!=this) //Si la instancia esta vacia y es distinta a la actual
        {
            Destroy(gameObject);
            return;
        } else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject); //No se destruye al cambiar escenas
        }
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(siguienteNivel);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadStage(string stageName)
    {
        SceneManager.LoadScene(stageName);
    }

    public void LoadNext()
    {
        SceneManager.LoadScene(siguienteNivel);
    }

    public void setSelectedLevel(string level)
    {
        siguienteNivel = level;
    }
    public string getSelectedLevel()
    {
        return siguienteNivel;
    }

    /*public void LoadControls()
    {

    }*/

    public void QuitGame()
    {
        Application.Quit();
    }
}
