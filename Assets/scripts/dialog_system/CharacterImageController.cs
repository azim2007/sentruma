using System;
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
    public string DirPath
    {
        set
        {
            if(dirPath == null || dirPath == "")
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
    public void Show(string emotion, string atHorizontal = "middle", string atVertical = "middle", string atDistance = "middle", int layer = 0)
    {
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

        try
        {
            characterImage = GetComponent<Image>();
            characterImage.sprite = Resources.Load<Sprite>(CharacterHandler.GetFolderName(dirPath + "/" + emotion));
        }
        catch
        {
            Debugger.Log("нет эмоции " + emotion + " путь до файла " + CharacterHandler.GetFolderName(dirPath + "/" + emotion));
        }

        var orderInLayerChanger = this.transform.GetComponent<Canvas>();
        orderInLayerChanger.overrideSorting = true;
        orderInLayerChanger.sortingOrder = deltaOrderInLayer + layer;

        var xPos = atHorizontalPositionNameImagePosition[atHorizontal];
        var yPos = atVerticalPositionNameImagePosition[atVertical];
        transform.position = new Vector3(xPos, yPos, 0f);

        var scale = atDistancePositionNameImagePosition[atDistance];
        transform.localScale = new Vector3(scale, scale, 1f);
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
}
