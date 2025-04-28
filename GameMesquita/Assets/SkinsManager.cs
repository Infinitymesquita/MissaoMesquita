using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsManager : MonoBehaviour
{
    string skinsData;
    [SerializeField] Transform[] skinsTransform;
    // Start is called before the first frame update
    void Start()
    {
        skinsData = SaveGame.Instance.GetSaveData("skins");
        string[] skinsArray = skinsData.Split(";");
        foreach (string skins in skinsArray)
        {
            if (int.Parse(skins) == 1)
            {
                skinsTransform[1].GetChild(0).gameObject.SetActive(false);
                skinsTransform[1].GetChild(1).gameObject.SetActive(true);

            }
            else if (int.Parse(skins) == 2)
            {
                skinsTransform[2].GetChild(0).gameObject.SetActive(false);
                skinsTransform[2].GetChild(1).gameObject.SetActive(true);
            }
            else if (int.Parse(skins) == 3)
            {
                skinsTransform[3].GetChild(0).gameObject.SetActive(false);
                skinsTransform[3].GetChild(1).gameObject.SetActive(true);
            }
        }

        int ultimaSkin = PlayerPrefs.GetInt("Skin");
        if (ultimaSkin != 0)
        {
            skinsTransform[0].GetChild(2).gameObject.SetActive(false);
            skinsTransform[1].GetChild(2).gameObject.SetActive(false);
            skinsTransform[2].GetChild(2).gameObject.SetActive(false);
            skinsTransform[3].GetChild(2).gameObject.SetActive(false);
            skinsTransform[ultimaSkin].GetChild(2).gameObject.SetActive(true);
        }
    }

    public void SelectSkin(int skin)
    {
        PlayerPrefs.SetInt("Skin", skin);
        skinsTransform[0].GetChild(2).gameObject.SetActive(false);
        skinsTransform[1].GetChild(2).gameObject.SetActive(false);
        skinsTransform[2].GetChild(2).gameObject.SetActive(false);
        skinsTransform[3].GetChild(2).gameObject.SetActive(false);
        skinsTransform[skin].GetChild(2).gameObject.SetActive(true);
    }
}
