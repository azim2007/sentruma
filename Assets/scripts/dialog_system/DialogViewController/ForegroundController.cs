using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForegroundController : MonoBehaviour
{
    private Image foreground;
    void Start()
    {
        foreground = GetComponent<Image>();
    }

    public void SetForegroundColor(Color fgColor, float fgColorDuration)
    {
        var currentColor = foreground.color;
        foreground.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a);
        StartCoroutine(ChangeForegroundColor(fgColorDuration, fgColor));
    }

    private IEnumerator ChangeForegroundColor(float fgColorDuration, Color color)
    {
        var waitingTime = 0.02f;
        var framesCount = (int)(fgColorDuration / waitingTime);
        var currentColor = foreground.color;
        if (framesCount == 0)
        {
            foreground.color = color;
            yield break;
        }

        var toPlusInEveryFrame = new Color(
            (color.r - currentColor.r) / framesCount,
            (color.g - currentColor.g) / framesCount,
            (color.b - currentColor.b) / framesCount,
            (color.a - currentColor.a) / framesCount);

        for (int i = 0; i < framesCount; i++)
        {
            yield return new WaitForSeconds(waitingTime);
            currentColor = foreground.color;
            foreground.color = new Color(
                currentColor.r + toPlusInEveryFrame.r,
                currentColor.g + toPlusInEveryFrame.g,
                currentColor.b + toPlusInEveryFrame.b,
                currentColor.a + toPlusInEveryFrame.a);
        }
    }
}
