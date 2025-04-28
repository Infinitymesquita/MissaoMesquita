using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class CanvasGame : MonoBehaviour
{
    private GameManagerScript gameManagerScript;
    [SerializeField] private GameObject painel;
    [SerializeField] PlayerRunnerScript player;
    [SerializeField] TextMeshProUGUI textoTempo;
    [SerializeField] TextMeshProUGUI textoMoeda;
    [SerializeField] private float segundos = 0f;
    private int minutos = 0;


    [SerializeField] private GameObject painelGameOver;
    [SerializeField] TextMeshProUGUI textoGameOverMoeda;
    [SerializeField] TextMeshProUGUI textoGameOverTempo;
    [SerializeField] TextMeshProUGUI textoGameOverItens;
    [SerializeField] TextMeshProUGUI textoGameOverPowerUP;
    [SerializeField] TextMeshProUGUI textoGameOverDistancia;
    [SerializeField] private int metros = 0;
    [SerializeField] private float contadorMetros = 0f;
    [SerializeField] float tempoGameOver = 2.5f;
    private bool finish = false;
    void Awake()
    {
        gameManagerScript = FindObjectOfType<GameManagerScript>();
        Time.timeScale = 1;
        painel.SetActive(false);
        painelGameOver.SetActive(false);
    }
    void Update()
    {
        if (gameManagerScript.playerDie == false)
        {
            //contadorMetros += Time.deltaTime;
            segundos += Time.deltaTime;
            
        }
        if (segundos >= contadorMetros + 0.1)
        {
            contadorMetros = segundos ;
            // 1Segundos = 10 Metros
        }
        if (segundos >= 60)
        {
            minutos++;
            segundos = 0;
        }
        textoTempo.text = "Tempo: "+ minutos.ToString("00") + ":" + ((int)segundos).ToString("00");
        textoMoeda.text = "Livros: " + player.coin;
        if (gameManagerScript.playerDie && !finish)
        {
            finish = true;
            StartCoroutine(GameOverInterval());
        }
        metros = player.metros;

    }

    public void Menu()
    {
        if (painelGameOver.activeSelf == false)
        {
            painel.SetActive(true);
            Time.timeScale = 0;
        }
        

    }
    public void Retornar()
    {
        painel.SetActive(false);
        Time.timeScale = 1;
    }
    
    IEnumerator GameOverInterval()
    {

        yield return new WaitForSeconds(tempoGameOver);
        textoGameOverTempo.text = textoTempo.text;
        textoGameOverMoeda.text = "Moedas Coletadas: " + player.coin;
        textoGameOverDistancia.text = "Distancia Percorrida: " + metros + "M";

        //SaveGame save = GameObject.Find("SaveData").GetComponent<SaveGame>();
        //save.SaveData(0, player.coin);
        //save.SaveData(3, metros);

        painelGameOver.SetActive(true);
    }
}
