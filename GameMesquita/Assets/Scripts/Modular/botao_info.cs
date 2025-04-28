using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botao_info : MonoBehaviour
{
    public GameObject[] itens; 
    public GameObject[] exibidos; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Ligarfuncao(){
        for(int i = 0; i < itens.Length; i++){
            itens[i].SetActive(false);
        }
         for(int i = 0; i < exibidos.Length; i++){
            exibidos[i].SetActive(true);
        }
    }
}
