using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    private Image background;
    void Start()
    {
        background = GetComponent<Image>();
    }

    public void SetBackground(Sprite sprite, Color bgColor, 
        float bgDuration, float bgColorDuration)
    {
        background.color = new Color(1, 1, 1, 0);
        background.sprite = sprite;
        StartCoroutine(ChangeBackground(bgDuration, bgColorDuration, bgColor));
    }

    private IEnumerator ChangeBackground(float bgDuration, float bgColorDuration, Color color)
    {
        yield return null;
        //вот тута продолжить
    }
}
