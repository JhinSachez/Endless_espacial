using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public GameObject[] skins;  // Skins disponibles (pueden ser prefabs o materiales)

    private void Start()
    {
        // Recupera el índice de la skin seleccionada
        int savedSkinIndex = PlayerPrefs.GetInt("SelectedSkinIndex", 0);

        // Aplica la skin seleccionada al personaje
        SetCharacterSkin(savedSkinIndex);
    }

    // Establece la skin del personaje
    void SetCharacterSkin(int index)
    {
        // Desactiva todas las skins
        foreach (var skin in skins)
        {
            skin.SetActive(false);
        }

        // Activa la skin seleccionada
        if (index >= 0 && index < skins.Length)
        {
            skins[index].SetActive(true);
        }
    }
}
