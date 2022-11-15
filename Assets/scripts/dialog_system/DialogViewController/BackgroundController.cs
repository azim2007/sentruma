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
        var currentColor = background.color;
        background.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0);
        background.sprite = sprite;
        StartCoroutine(ChangeBackgroundAlpha(bgDuration, true));
        StartCoroutine(ChangeBackgroundColor(bgColorDuration, bgColor));
    }

    public void HideBackground(float duration)
    {
        StartCoroutine(ChangeBackgroundAlpha(duration, false));
    }

    private IEnumerator ChangeBackgroundAlpha(float bgDuration, bool isShowing)
    {
        var waitingTime = 0.02f;
        var framesCount = (int)(bgDuration / waitingTime);
        if (framesCount == 0)
        {
            var currentColor = background.color;
            background.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
            yield break;
        }

        var toPlusInEveryFrame = 1f / framesCount * (isShowing ? 1 : -1);
        for(int i = 0; i < framesCount; i++)
        {
            yield return new WaitForSeconds(waitingTime);
            var currentColor = background.color;
            background.color = new Color(currentColor.r, currentColor.g, 
                currentColor.b, currentColor.a + toPlusInEveryFrame);
        }
    }

    private IEnumerator ChangeBackgroundColor(float bgColorDuration, Color color)
    {
        var waitingTime = 0.02f;
        var framesCount = (int)(bgColorDuration / waitingTime);
        var currentColor = background.color;
        if (framesCount == 0)
        {
            background.color = new Color(color.r, color.g, color.b, currentColor.a);
            yield break;
        }

        var toPlusInEveryFrame = new Color(
            (color.r - currentColor.r) / framesCount, 
            (color.g - currentColor.g) / framesCount, 
            (color.b - currentColor.b) / framesCount);

        for (int i = 0; i < framesCount; i++)
        {
            yield return new WaitForSeconds(waitingTime);
            currentColor = background.color;
            background.color = new Color(
                currentColor.r + toPlusInEveryFrame.r, 
                currentColor.g + toPlusInEveryFrame.g,
                currentColor.b + toPlusInEveryFrame.b, 
                currentColor.a);
        }
    }
}
