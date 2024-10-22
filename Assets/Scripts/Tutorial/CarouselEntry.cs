using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewCarouselEntry", menuName = "UI/Carousel Entry", order = 0)]
public class CarouselEntry : ScriptableObject
{
    [field:SerializeField] public Sprite EntryGraphic { get; private set; }
    
    [field:SerializeField, Multiline(10)] public string Description { get; private set; }
    
    [Header("Interaction")]
    [SerializeField] private string levelNameToLoad;

    public void Interact()
    {
        SceneManager.LoadScene(levelNameToLoad);
    }
}
