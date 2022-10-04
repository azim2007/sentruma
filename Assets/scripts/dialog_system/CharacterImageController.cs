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

    public void Show(string emotion, string atHorizontal = "middle", string atVertical = "middle", string atDistance = "middle", int layer = 0)
    {
        try
        {
            characterImage = GetComponent<Image>();
            characterImage.sprite = Resources.Load<Sprite>(CharacterHandler.GetFolderName(dirPath + "/" + emotion));
        }
        catch
        {
            Debugger.Log("нет эмоции " + emotion + " путь до файла " + CharacterHandler.GetFolderName(dirPath + "/" + emotion));
        }
    }
}
