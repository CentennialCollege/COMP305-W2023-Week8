using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    [Header("Life Properties")] 
    public int value = 3;

    private Image lifeImage;

    // Start is called before the first frame update
    void Start()
    {
        lifeImage = GetComponent<Image>();
        ResetLives();
    }

    public void LoseLife()
    {
        value -= 1;
        if (value < 0)
        {
            value = 0;
        }

        lifeImage.sprite = Resources.Load<Sprite>($"Sprites/Life{value}");
    }

    public void GainLife()
    {
        value += 1;
        if (value > 3)
        {
            value = 3;
        }

        lifeImage.sprite = Resources.Load<Sprite>($"Sprites/Life{value}");
    }

    public void ResetLives()
    {
        value = 3;
        lifeImage.sprite = Resources.Load<Sprite>($"Sprites/Life3");
    }
}
