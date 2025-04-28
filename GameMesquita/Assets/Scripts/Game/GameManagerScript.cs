using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Rendering;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [Header("mapas")]
    [SerializeField] private GameObject[] centro;
    [SerializeField] private GameObject[] emil;
    [SerializeField] private GameObject[] mesquita;
    [SerializeField] private float timerMap = 0;
    [SerializeField] private Transform[] map;
    private int i;
    [Header("Objetos")]
    [SerializeField] private GameObject[] obistaculos;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject[] powerUps ;
    public BoxCollider nextSpaw;
    public BoxCollider colliderAntigo;
    [SerializeField] private float DistanceSpaw = -10;

    [Header("Parents")]
    [SerializeField] private Transform centroParent;
    [SerializeField] private Transform emilParent;
    [SerializeField] private Transform mesquitaParent;
    [SerializeField] private Transform ObistaculoParent;
    [SerializeField] private Transform coinParent;
    [SerializeField] private Transform PowerUPParent;

    [Header("Timers")]
    [SerializeField] private float timerCoin = 10;
    [SerializeField] private float frequenciaCoin = 15;

    [SerializeField] private float timerPowerUP = 60;
    [SerializeField] private float frequenciaPowerUP = 60;

    [SerializeField] private float timerObstaculo = 10;
    [SerializeField] private float frequenciaObstaculo = 5;

    [Header("Velocty")]
    [SerializeField] public float speed = 4;
    [SerializeField] private float CooldownTimerSpeedUP = 10;
    [SerializeField] private float timerSpeedUP = 10;
    [SerializeField] public bool playerDie = false;
                     public Material skybox;

    private List<GameObject> mapasInativos = new List<GameObject>();


    void Awake()
    {
        foreach (var map in centro)
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject newMapa = Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity);
                mapasInativos.Add(newMapa);
                newMapa.transform.SetParent(centroParent);
                newMapa.SetActive(false);
            }
            //Debug.Log("Mapa 0");

        }
        foreach (var map in emil)
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject newMapa = Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity);
                mapasInativos.Add(newMapa);
                newMapa.transform.SetParent(emilParent);
                newMapa.SetActive(false);
            }
            //Debug.Log("Mapa 1");

        }
        foreach (var map in mesquita)
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject newMapa = Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity);
                mapasInativos.Add(newMapa);
                newMapa.transform.SetParent(mesquitaParent);
                newMapa.SetActive(false);
            }
            //Debug.Log("Mapa 2");

        }
        foreach (var powerUp in powerUps)
        {
            for (int i = 0; i < 1; i++)
            {
                GameObject newPowerUp = Instantiate(powerUp, new Vector3(0, 0, 0), Quaternion.identity);
                newPowerUp.transform.SetParent(PowerUPParent);
                newPowerUp.SetActive(false);
            }
            //Debug.Log("PowerUP 1");

        }
        foreach (var obistaculo in obistaculos)
        {
            for (var i = 0; i <= 5; i++)
            {
                GameObject newObistaculo = Instantiate(obistaculo, new Vector3(0, 0, 0), Quaternion.identity);
                newObistaculo.transform.SetParent(ObistaculoParent);
                newObistaculo.SetActive(false);
                //Debug.Log("obstaculo" + i);
            }
        }
           
        for (var i = 0; i <= 30; i++)
        {
            GameObject newCoin = Instantiate(coin, new Vector3(0, 0, 0), Quaternion.identity);
            newCoin.transform.SetParent(coinParent);
            newCoin.SetActive(false);
            //Debug.Log("coin" + i);
        }

    }
    private void Start()
    {
        map[0] = centroParent;
        map[1] = emilParent;
        map[2] = mesquitaParent;

        for (int i = 0; i < 5; i++)
        {

            CreateMap();
            
        }
    }
    private void Update()
    {
        timerMap += Time.deltaTime;
        if (timerMap > 30)
        {
            //mudar centro,emil e outro
            i++;
            if (i >= map.Length)
                i = 0;
            timerMap = 0;
        }
    }
    private void FixedUpdate()
    {
        if (!playerDie)
        {
            timerCoin -= Time.deltaTime;
            if (timerCoin <= 0)
            {
                timerCoin = frequenciaCoin;
                CreateCoin();
            }

            timerPowerUP -= Time.deltaTime;
            if (timerPowerUP <= 0)
            {
                timerPowerUP = frequenciaPowerUP;
                CreatePowerUp();
            }

            timerObstaculo -= Time.deltaTime;
            if (timerObstaculo <= 0)
            {
                timerObstaculo = frequenciaObstaculo;
                CreateObstaculo();
            }


            timerSpeedUP -= Time.deltaTime;
            if (timerSpeedUP <= 0 && speed < 25)
            {
                speed++;
                timerSpeedUP = CooldownTimerSpeedUP;
            }
        }
        else
            speed = 0;

        if (skybox.GetFloat("_Rotation") == 360)
        {
            skybox.SetFloat("_Rotation", 0);
        }
        skybox.SetFloat("_Rotation", Mathf.Lerp(skybox.GetFloat("_Rotation"), 360, 0.01f * Time.deltaTime));

    }
    public void CreateMap()
    {
        
        //Debug.Log("Mapa criado foi da regiao:" + i); 
        Transform MapParent = map[i];
        
        int randomIndex = Random.Range(0, MapParent.childCount);
        
        Transform randomChild = mapasInativos[randomIndex].transform;
        mapasInativos.RemoveAt(randomIndex);

        randomChild.gameObject.SetActive(true);
        randomChild.GetChild(1).gameObject.SetActive(true);
        nextSpaw = randomChild.GetChild(1).GetComponent<BoxCollider>();
        Vector3 facePosition = GetFaceWorldPosition(colliderAntigo, nextSpaw);
        colliderAntigo = nextSpaw;
        randomChild.transform.position = new Vector3(randomChild.transform.position.x, randomChild.transform.position.y, facePosition.z);
    }

    Vector3 GetFaceWorldPosition(BoxCollider colliderAntigo, BoxCollider nextSpawn)
    {
        // Obter o centro do colisor no espaço mundial
        Vector3 centerWorld = colliderAntigo.transform.TransformPoint(colliderAntigo.center);
        Vector3 centerWorldSpawn = nextSpawn.transform.TransformPoint(nextSpawn.center);

        // Calcular o tamanho da metade do colisor em relação ao mundo
        Vector3 halfSize = Vector3.Scale(colliderAntigo.size / 2, colliderAntigo.transform.lossyScale);
        Vector3 halfSizeSpawn = Vector3.Scale(nextSpawn.size / 2, nextSpawn.transform.lossyScale);

        // Direção no espaço mundial (normalizada para evitar distorções)
        Vector3 faceOffset = Vector3.Scale(halfSize, Vector3.back.normalized);
        Vector3 faceOffsetSpawn = Vector3.Scale(halfSizeSpawn, Vector3.forward.normalized);

        // Retornar a posição da face no espaço mundial
        return centerWorld + faceOffset - faceOffsetSpawn;
    }

    public void DisableMap(GameObject gO)
    {
        CreateMap();
        mapasInativos.Add(gO);
        gO.SetActive(false);
    }
    public void CreateObstaculo()
    {
        float[] lanesNumbers = { 0f, 1.5f, -1.5f };
        int randomIndex = Random.Range(0, ObistaculoParent.childCount);

        Transform randomChild = ObistaculoParent.GetChild(randomIndex);
        if (randomChild.gameObject.activeSelf)
        {
            CreateObstaculo();
            return;
        }
        randomChild.gameObject.SetActive(true);
        int randomLane = Random.Range(0, lanesNumbers.Length);
        randomChild.transform.position = new Vector3(lanesNumbers[randomLane], 0.4f, DistanceSpaw);
    }
    public void CreateCoin()
    {
        float[] lanesNumbers = { 0f, 1.5f, -1.5f };
        int randomIndex = Random.Range(0, coinParent.childCount);

        Transform randomChild = coinParent.GetChild(randomIndex);
        if (randomChild.gameObject.activeSelf)
        {
            CreateCoin();
            return;
        }
        randomChild.gameObject.SetActive(true);
        int randomLane = Random.Range(0, lanesNumbers.Length);
        randomChild.transform.position = new Vector3(lanesNumbers[randomLane], 1.5f, DistanceSpaw);
    }
    public void CreatePowerUp()
    {
        float[] lanesNumbers = { 0f, 1.5f, -1.5f };

        int randomIndex = Random.Range(0, PowerUPParent.childCount);

        Transform randomChild = PowerUPParent.GetChild(randomIndex);

        if (randomChild.gameObject.activeSelf)
        {
            CreatePowerUp();
            return;
        }
        randomChild.gameObject.SetActive(true);
        int randomLane = Random.Range(0, lanesNumbers.Length);
        randomChild.transform.position = new Vector3(lanesNumbers[randomLane], 1.5f, DistanceSpaw);
    }

    //public void CreateMaps()
    //{
    //    float Pz = 19.5f;
    //    for (int i = 0; i < 10; i++)
    //    {
    //        int randomIndex ;
    //        Transform randomChild;
    //        do
    //        {
    //            randomIndex = Random.Range(0, MapParent.childCount);
    //            randomChild = MapParent.GetChild(randomIndex);
    //        } while (randomChild.gameObject.activeSelf);

    //        randomChild.gameObject.SetActive(true);
    //        randomChild.GetChild(0).gameObject.SetActive(true);
    //        randomChild.transform.position = new Vector3(0, 0, -Pz+ -66.5f);
    //        Pz = Pz + 19.5f;
    //    }
    //}
}
