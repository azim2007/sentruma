using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterImageController : MonoBehaviour
{
    private Image characterImage;
    private string dirPath;
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

    void Start()
    {
        characterImage = GetComponent<Image>();
    }

    void Show(string emotion, string atHorizontal = "middle", string atVertical = "middle", string atDistance = "middle", int layer = 0)
    {
        try
        {
            characterImage.sprite = Resources.Load<Sprite>(CharacterHandler.GetFolderName(dirPath + emotion));
        }
        catch
        {
            Debugger.Log("в папке " + dirPath +" нет эмоции " + emotion);
            return;
        }
    }
}
