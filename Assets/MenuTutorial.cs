using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTutorial : MonoBehaviour
{
    public DistanceScore _distanceScore;
    [SerializeField] private GameObject menuTutorial; 
    bool isOnPlay;
    
    void OnGameStateChanged(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (_distanceScore.distance >= 5000)
        {
            menuTutorial.SetActive(true);
            GameManager.GetInstance().ChangeGameState(Game_State.Pause);

        }
    }
    
    public void Reiniciar()
    {
        GameManager.GetInstance().ChangeGameState(Game_State.Play);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Jugar()
    {
        GameManager.GetInstance().ChangeGameState(Game_State.Play);
        SceneManager.LoadScene("SampleScene");
    }

    public void Menu()
    {
        GameManager.GetInstance().ChangeGameState(Game_State.Play);
        SceneManager.LoadScene("MenuInicial");
    }
}
