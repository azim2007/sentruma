using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// класс для управления полем предыдущих реплик, вешается на менеджера
/// </summary>
public class PreviousReplicManager : MonoBehaviour
{
    private GameObject PreviousReplicsView { get; set; } // само поле с текстом
    private Text PreviousReplicsText { get; set; }
    private StringBuilder previousReplics;
    public bool IsActive { get => PreviousReplicsView.activeSelf; }

    private void Start()
    {
        previousReplics = new StringBuilder(100);
        PreviousReplicsView = transform.GetChild(0).gameObject;
        PreviousReplicsText = PreviousReplicsView.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        PreviousReplicsView.SetActive(false); // изначально при запуске диалога его не видно
    }

    /// <summary>
    /// показать поле с предыдущими репликами
    /// </summary>
    public void Show()
    {
        PreviousReplicsView.SetActive(true);
        PreviousReplicsText.text = previousReplics.ToString();
        StartCoroutine(CheckMousePressed());
    }

    /// <summary>
    /// добавить реплику к листу
    /// </summary>
    /// <param name="replic">реплика, которую надо добавить</param>
    public void AddReplicToList(Replic replic)
    {
        var replicText = string.Join("", replic.GetText());
        if (replic.IsService)
        {
            Debugger.LogError("в previous replics вы попытались добавить сервисную реплику: "
                + replicText);
            return;
        }

        if(previousReplics == null) 
            previousReplics = new StringBuilder(100); 
        previousReplics.Append(replic.Sender + " " + replicText + "\n\n");
    }

    /// <summary>
    /// при нажатии на лкм скрывает поле предыдущих реплик
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckMousePressed()
    {
        while (!Input.GetMouseButtonUp(1))
        {
            yield return null;
        }

        PreviousReplicsView.SetActive(false);
    }
}
