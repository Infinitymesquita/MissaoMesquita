using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmazingAssets.CurvedWorld;

public class CuvedWorldMoviment : MonoBehaviour
{
    CurvedWorldController curvedWorldController;
    GameManagerScript gameManagerScript;
    float curveChance;
    float curveTimer;
    float curveValue;
    // Start is called before the first frame update
    void Start()
    {
        curvedWorldController = GetComponent<CurvedWorldController>();
        gameManagerScript = FindObjectOfType<GameManagerScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!gameManagerScript.playerDie)
        {
            curveTimer += Time.deltaTime;

            if(curveTimer > 4)
            {
                curveChance = Random.Range(0, 5);
                if (curveChance == 4)
                {
                    curveValue = Random.Range(-15, 15);
                }
                curveTimer = 0;
            }

            curvedWorldController.bendHorizontalSize = Mathf.Lerp(curvedWorldController.bendHorizontalSize, curveValue, Time.deltaTime);
        }
    }
}
