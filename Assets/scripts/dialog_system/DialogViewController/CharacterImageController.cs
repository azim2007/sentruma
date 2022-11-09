using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// класс, контролирующий картинку какого либо персонажа в диалоге. dirPath - путь до папки, где хранятся все спрайты этого персонажа
/// </summary>
public class CharacterImageController : MonoBehaviour
{
    private Image characterImage;
    private string dirPath;
    private const int deltaOrderInLayer = 6; //Dialog View находится на 5 order in layer, поэтому картинки персонажей надо распологать хотя бы на 6 уровне
    private Animator animator;
    public string DirPath
    {
        set
        {
            if (dirPath == null || dirPath == "")
            {
                dirPath = value;
            }
            else
            {
                throw new InvalidOperationException("нельзя изменить dirPath у CharacterImageController");
            }
        }
    }

    /// <summary>
    /// отрисовывает нужный спрайт в нужном положении на экране
    /// </summary>
    /// <param name="emotion">название эмоции (имя файла)</param>
    /// <param name="atHorizontal">положение по горизонтали (см. api.xlsx)</param>
    /// <param name="atVertical">положение по вертикали (см. api.xlsx)</param>
    /// <param name="atDistance">положение по дальности от камеры (см. api.xlsx)</param>
    /// <param name="layer">default order in layer</param>
    public void Show(string emotion, string atHorizontal = "middle", string atVertical = "middle",
        string atDistance = "middle", int layer = 0, float animationDurationSeconds = 0, string animationType = "")
    {
        animator = GetComponent<Animator>();
        var animationName = "";
        if (!atHorizontalPositionNameImagePosition.ContainsKey(atHorizontal))
        {
            Debugger.LogError("нет горизонтального положения " + atHorizontal);
            return;
        }

        if (!atVerticalPositionNameImagePosition.ContainsKey(atVertical))
        {
            Debugger.LogError("нет вертикального положения " + atVertical);
            return;
        }

        if (!atDistancePositionNameImagePosition.ContainsKey(atDistance))
        {
            Debugger.LogError("нет дистанции " + atDistance);
            return;
        }

        if (animationDurationSeconds == 0 && animationType != "")
        {
            Debugger.LogError("параметр длительность анимации не может быть равен 0 при какой-либо анимации");
            return;
        }

        try
        {
            characterImage = GetComponent<Image>();
            characterImage.sprite = Resources.Load<Sprite>(CharacterHandler.GetFolderName(dirPath + "/" + emotion));
        }
        catch
        {
            Debugger.LogError("нет эмоции " + emotion + " путь до файла " +
                CharacterHandler.GetFolderName(dirPath + "/" + emotion));
            return;
        }

        try
        {
            animator.speed = 1 / animationDurationSeconds;
            animationName = GetAnimationName(animationType: animationType, isOnShow: true);
        }
        catch (Exception e)
        {
            Debugger.LogError(e.Message);
            return;
        }

        if(animationType == "")
        {
            animator.speed = 1;
            animator.Play("DefaultState");
        }   
        else if (animationType != "" && animationDurationSeconds > 0)
            animator.Play(animationName);


        var orderInLayerChanger = this.transform.GetComponent<Canvas>();
        orderInLayerChanger.overrideSorting = true;
        orderInLayerChanger.sortingOrder = deltaOrderInLayer + layer;

        var xPos = atHorizontalPositionNameImagePosition[atHorizontal];
        var yPos = atVerticalPositionNameImagePosition[atVertical];
        transform.position = new Vector3(xPos, yPos, 0f);

        var scale = atDistancePositionNameImagePosition[atDistance];
        transform.localScale = new Vector3(scale, scale, 1f);
    }

    public void Hide(float animationDurationSeconds = 0, string animationType = "")
    {
        animator = GetComponent<Animator>();
        var animationName = "";
        if (animationDurationSeconds == 0 && animationType != "")
        {
            Debugger.LogError("параметр длительность анимации не может быть равен 0 при какой-либо анимации");
            return;
        }

        try
        {
            animator.speed = 1 / animationDurationSeconds;
            animationName = GetAnimationName(animationType: animationType, isOnShow: false);
        }
        catch (Exception e)
        {
            Debugger.LogError(e.Message);
            return;
        }

        if (animationType != "" && animationDurationSeconds > 0)
            animator.Play(animationName);

        StartCoroutine(WaitingForAnimation());

        IEnumerator WaitingForAnimation()
        {
            Debugger.Log(animationName);
            Debugger.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            yield return null;
            yield return new WaitWhile(() => animator.GetCurrentAnimatorClipInfo(0)[0].clip.name
                .Equals(animationName));

            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// функция для получения имени нужной нам анимации
    /// </summary>
    /// <param name="animationType">параметр из animationType в функции Show()</param>
    /// <param name="isOnShow">применяется ли эта анимация на показе персонажа</param>
    /// <returns>имя анимации, которое можно пихать в Animator.Play()</returns>
    private string GetAnimationName(string animationType, bool isOnShow)
    {
        if (!animationTypes.ContainsKey(animationType))
            throw new InvalidOperationException("нет анимации " + animationType);
        return animationTypes[animationType] + (isOnShow ? "Up" : "Down");
    }

    private Dictionary<string, float> atHorizontalPositionNameImagePosition = new Dictionary<string, float>()
    {
        { "left", -6.6f },
        { "midleft", -3.3f },
        { "middle", 0 },
        { "midright", 3.3f },
        { "right", 6.6f },
    };

    private Dictionary<string, float> atVerticalPositionNameImagePosition = new Dictionary<string, float>()
    {
        { "bottom", -12 },
        { "midbottom", -7 },
        { "middle", -2 },
        { "midtop", 3 },
        { "top", 8 },
    };

    private Dictionary<string, float> atDistancePositionNameImagePosition = new Dictionary<string, float>()
    {
        { "back", 0.6f },
        { "midback", 0.9f },
        { "middle", 1.2f },
        { "midfront", 1.6f },
        { "front", 2 },
    };

    private Dictionary<string, string> animationTypes = new Dictionary<string, string>()
    {
        { "", "" },
        { "fade", "Fade" }
    };
}
