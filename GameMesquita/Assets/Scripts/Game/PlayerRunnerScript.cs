using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerRunnerScript : MonoBehaviour
{
    [Header("Lane")]
    [SerializeField] private GameManagerScript gameManager;
    [SerializeField] private float laneSpeed = 10;
    [SerializeField] private float lanePosition = 0;
    private Vector3 verticalTargetLane;
    [SerializeField] private Animator anim; 

    [Header("Jump")]
    [SerializeField] private float jumpForce = 6;
    private Rigidbody rb;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isJump;
    private bool isGrounded;
    private float height;

    [Header("Slide")]
    [SerializeField] bool isSlide = false;
    public float timerSlide = 1.17f;

    [Header("Moedas e Power UP")]
    [SerializeField] public int coin;
    [SerializeField] private bool shield;
    [SerializeField] private bool Imortal;

    [SerializeField] private float timerShield = 20;
    private float timerS;

    [Header("Audio e Efeitos")]
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource, audioSource2, audioSorce3;
    [SerializeField] private GameObject shieldEffect;

    [SerializeField] private GameObject jumpEffect;
    [SerializeField] private GameObject damageEffect;
    [SerializeField] private GameObject coinEffect;
    [SerializeField] private GameObject slideEffect;
    GameObject efeitoslide;
    
    [SerializeField] private GameObject posPe;
    [SerializeField] private Vector3 posCorpo;

    [SerializeField] Transform[] skins;
    private float timerRecord;
    private float segundos;
    private double contadorMetros;
    public int metros;
    [SerializeField] SortStringsFeedbackUI sortStringsFeedbackUI;

    void Start()
    {
        // seta Rigibody e animator e deixa rotaçao do jogador parada
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        timerS = timerShield;

        try
        {
            int skin = PlayerPrefs.GetInt("Skin");
            if(skin != 0)
            {
                skins[skin].gameObject.SetActive(true);
                skins[0].gameObject.SetActive(false);
                anim = skins[skin].GetComponent<Animator>();
            }
            else
            {
                skins[0].gameObject.SetActive(true);
                anim = skins[0].GetComponent<Animator>();
            }
        }
        catch 
        {
            skins[0].gameObject.SetActive(true);
            anim = skins[0].GetComponent<Animator>();
        }
    }


    void FixedUpdate()
    {
        if (!gameManager.playerDie)
        {
                //animators 

                anim.SetBool("InAir", isJump);
            if (isSlide)
            {
                timerSlide -= Time.deltaTime;
                if (timerSlide <= 0)
                {
                    FinishSlide();
                }
            }

            //Timer do escudo
            if (shield)
            {
                timerS -= Time.deltaTime;
                if (timerS <= 0)
                {
                    timerS = timerShield;
                    shieldEffect.SetActive(false);
                    shield = false;
                }
            }

            //Verificar se jogador estar no chao ou pulando.
            height = GetComponent<Collider>().bounds.size.y;
            isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

            if (isGrounded)
            {
                isJump = false;
            }
            else
            {
                isJump = true;
            }

            if(gameManager.playerDie == false)
            {
                segundos += Time.deltaTime;
                if (segundos >= contadorMetros + 0.1)
                {
                    metros += 1;
                    contadorMetros = segundos;
                }
            }

            //Faz amudanças de movimento Y e X
            Vector3 targetPosition = new Vector3(verticalTargetLane.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);
        }
    }
    public void ChangeLane(float direction)
    {
        if (!gameManager.playerDie)
        {
            // Codigo  para Mudar Caminho que jogador estar Andando
            if (lanePosition + direction >= 2 || lanePosition + direction <= -2)
                return;
            lanePosition += direction;
            if (direction < 0 && !isSlide) // Direita<0<Esquerda
            {
                anim.CrossFade("ChangeLeft", 0.25f);
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
            else if (!isSlide)
            {
                anim.CrossFade("ChangeRight", 0.25f);
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
            verticalTargetLane = new Vector3(lanePosition, 0, 0);
            //Debug.Log("Moveu Para lane: " + lanePosition);
        }

    }
    public void InputJump(InputAction.CallbackContext context)
    {
        //Para Inputs no PC
        if (context.performed)
        {
            Jump();
        }
    }
    public void TurnLeft(InputAction.CallbackContext context)
    {
        //Para Inputs no PC
        if (context.performed)
            ChangeLane(1.5f);
    }
    public void TurnRight(InputAction.CallbackContext context)
    {
        //Para Inputs no PC
        if (context.performed)
            ChangeLane(-1.5f);
    }

    public void InputSlide(InputAction.CallbackContext context)
    {
        //Para Inputs no PC
        if (context.performed)
        {
            Slide();
        }
    }
    public void Jump()
    {
        //faz o pulo do jogador e confirma que nao estar no slide
        if (isGrounded && !gameManager.playerDie)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
            anim.CrossFade("Jump Begin", 0.25f);
            rb.AddForce(Vector3.up * jumpForce);
            //Debug.Log("Pulou");
            FinishSlide();
            GameObject efeito = Instantiate(jumpEffect, posPe.transform.position, Quaternion.identity);
            Destroy(efeito, 1f);
        }      
    }
    
    public void Slide()
    {
        // Ativa animaçao slide do jogador
        if(isGrounded && !isJump && !isSlide && !gameManager.playerDie)
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
            isSlide = true;
            anim.CrossFade("Slide", 0.25f);
            efeitoslide = Instantiate(slideEffect, posPe.transform.position, Quaternion.identity);
        }
    }
    public void FinishSlide()
    {
        //termina animaçao slide do jogador na animacao
        timerSlide = 1.17f;
        isSlide = false;
        Destroy(efeitoslide);
    }

    public void GameOver()
    {
        int somaLivros = coin + int.Parse(SaveGame.Instance.GetSaveData("livros"));
        SaveGame.Instance.UpdateSaveData("livros", somaLivros.ToString());
        if (metros > int.Parse(SaveGame.Instance.GetSaveData("recordes")))
        {
            SaveGame.Instance.UpdateSaveData("recordes", metros.ToString());
        }
        GameObject efeito = Instantiate(damageEffect, this.transform.position, Quaternion.identity);
        Destroy(efeito, 1f);
        audioSorce3.Stop();
        audioSource.clip = audioClips[2];
        audioSource.Play();
        isJump = false;
        anim.CrossFade("Death", 0.25f);
        Debug.Log("Acabou o jogo");
        gameManager.playerDie = true;
        sortStringsFeedbackUI.SortLines();
        //Time.timeScale = 0;

    }
    private void OnTriggerStay(Collider other)
    {
        ////Cria outro terreno quando passar pela colisao
        //if (other.gameObject.CompareTag("collisionTerrain"))
        //{
        //    gameManager.CreateMap();
        //    other.gameObject.SetActive(false);
        //}

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            audioSource2.clip = audioClips[3];
            audioSource2.Play();
            coin++;
            other.gameObject.SetActive(false);
            GameObject efeito = Instantiate(coinEffect, this.transform.position, Quaternion.identity);
            Destroy(efeito,1f);
        }
        if (other.gameObject.CompareTag("Shield"))
        {
            audioSource2.clip = audioClips[4];
            audioSource2.Play();
            shieldEffect.SetActive(true);
            shield = true;
            timerS = timerShield;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Obstaculo"))
        {
            audioSource2.clip = audioClips[5];
            audioSource2.Play();
            if (shield || Imortal)
            {
                other.gameObject.SetActive(false);
                shieldEffect.SetActive(false);
                shield = false;
                timerS = 0;
                
            }
            else
            GameOver();
        }
        if (other.gameObject.CompareTag("ObstaculoSlide"))
        { 
            if (isSlide || Imortal)
            {
                
                return;
            }
            else
            {
                if (shield)
                {
                    other.gameObject.SetActive(false);
                    shieldEffect.SetActive(false);
                    shield = false;
                    timerS = 0;
                    return;

                }
                else
                GameOver();
            }
            
        }
    }
}
