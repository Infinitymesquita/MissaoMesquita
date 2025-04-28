using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveGame : MonoBehaviour
{
    // Singleton
    public static SaveGame Instance { get; private set; }
    private string saveFilePath;
    private SaveData saveData;

    private void Awake()
    {
        // Configuração do Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        saveFilePath = Application.persistentDataPath + "/savegame.json";
        saveData = LoadGame();
    }
  
    // Salvar informações no JSON
    private void SaveToFile()
    {
        string json = JsonUtility.ToJson(saveData, true);
        string encryptedJson = EncryptionUtility.Encrypt(json);
        File.WriteAllText(saveFilePath, encryptedJson);
        Debug.Log("Jogo salvo com sucesso:\n" + json);
    }

    // Carregar informações do JSON
    private SaveData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                string encryptedJson = File.ReadAllText(saveFilePath);
                string json = EncryptionUtility.Decrypt(encryptedJson);
                Debug.Log("Jogo carregado com sucesso:\n" + json);
                return JsonUtility.FromJson<SaveData>(json);
            }
            catch (Exception ex)
            {
                Debug.LogError("Erro ao carregar o jogo: " + ex.Message);
                return new SaveData();
            }
        }
        else
        {
            Debug.LogWarning("Arquivo de save não encontrado. Criando novo save.");
            return new SaveData();
        }
    }

    // Métodos Públicos para outros scripts
    public void UpdateSaveData(string key, string value)
    {
        switch (key)
        {
            case "livros":
                saveData.livros = value;
                break;
            case "cartas":
                saveData.cartas = value;
                break;
            case "locais":
                saveData.locais = value;
                break;
            case "recordes":
                saveData.recordes = value;
                break;
            case "skins":
                saveData.skins = value;
                break;
            default:
                Debug.LogWarning($"Chave {key} não reconhecida.");
                return;
        }

        SaveToFile();
    }
    public void AddToSaveData(string key, string value)
    {
        switch (key)
        {
            case "livros":
                saveData.livros = AddUniqueValue(saveData.livros, value);
                break;
            case "cartas":
                saveData.cartas = AddUniqueValue(saveData.cartas, value);
                break;
            case "locais":
                saveData.locais = AddUniqueValue(saveData.locais, value);
                break;
            case "recordes":
                // Recordes podem não precisar de lógica de adição, mas sim de substituição.
                saveData.recordes = value;
                break;
            case "skins":
                saveData.skins = AddUniqueValue(saveData.skins, value);
                break;
            default:
                Debug.LogWarning($"Chave {key} não reconhecida.");
                return;
        }

        SaveToFile();
    }
    public string GetSaveData(string key)
    {
        return key switch
        {
            "livros" => saveData.livros,
            "cartas" => saveData.cartas,
            "locais" => saveData.locais,
            "recordes" => saveData.recordes,
            "skins" => saveData.skins,
            _ => null
        };
    }
    private string AddUniqueValue(string existingData, string newValue)
    {
        HashSet<string> values = new HashSet<string>(existingData.Split(';', StringSplitOptions.RemoveEmptyEntries));
        values.Add(newValue);
        return string.Join(";", values);
    }
    public SaveData GetAllData()
    {
        return saveData;
    }
}

[Serializable]
public class SaveData
{
    public string livros = "0";   // Valor
    public string cartas = "";   // Formato de mensagem das cartas: 12;1;2;3;4;5
    public string locais = "";   // Formato de mensagem dos locais: 12;1;2;3;4;5
    public string recordes = "0"; // Metragem
    public string skins = "0";
}