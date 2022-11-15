using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogController : MonoBehaviour
{
    private static float textSpeed = 1.1f - 0.7f;

    private CommandHandler commandHandler;
    private Image background;
    private Image foreground;
    private GameObject replicView;
    private GameObject selectingView;
    private PreviousReplicManager previousReplicsView;
    private Waiter waiter;

    private Dialog thisDialog;
    public Dialog ThisDialog
    {
        get { return thisDialog; }

        set
        {
            if (thisDialog == null)
            {
                thisDialog = value;
            }

            else
            {
                throw new System.InvalidOperationException
                    ("thisDialog isnt empty. You cannot change its value");
            }
        }
    }

    private void ServiceHandler(string command)
    {
        commandHandler.HandleCommand(command);
    }

    private void GameObjectSetting()
    {
        background = this.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        background.sprite = ThisDialog.BackgroundName != null ?
            Resources.Load<Sprite>("backgrounds/" + ThisDialog.BackgroundName) :
            background.sprite;

        foreground = this.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        replicView = this.transform.GetChild(0).GetChild(2).gameObject;
        selectingView = this.transform.GetChild(0).GetChild(3).gameObject;
        previousReplicsView = this.transform.GetChild(0).GetChild(4).GetComponent<PreviousReplicManager>();
        selectingView.SetActive(false);
        replicView.SetActive(true);
    }

    private void Start()
    {
        this.GetComponent<Canvas>().worldCamera =
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        GameObjectSetting();

        waiter = new Waiter();

        commandHandler = new CommandHandler(
            new List<ICommandHandler>()
            {
                new BackgroundHandler(background.GetComponent<BackgroundController>(), 
                    foreground.GetComponent<ForegroundController>()),
                new CharacterHandler(background),
                new WaitHandler(waiter),
            }
        );

        StartCoroutine(ShowDialog());
    }

    private IEnumerator ShowDialog()
    {
        if (ThisDialog == null)
        {
            thisDialog = new Dialog(
                new List<Replic>(),
                null,
                () => true,
                () => { },
                "bg"
            );
        }

        var nextReplicButton = replicView.transform.GetChild(2).GetComponent<Button>();
        var previousReplicsButton = replicView.transform.GetChild(3).GetComponent<Button>();

        var sender = replicView.transform.GetChild(0).GetComponent<Text>();
        var message = replicView.transform.GetChild(1).GetComponent<Text>();

        bool canChange = true;

        nextReplicButton.onClick.AddListener(() => canChange = true);
        previousReplicsButton.onClick.AddListener(() => previousReplicsView.Show());

        foreach (var replic in ThisDialog.GetReplic())
        {
            yield return waiter.Call();
            if (replic.IsService)
            {
                string command = "";
                foreach (var _operator in replic.GetText())
                {
                    command += _operator + " ";
                }

                ServiceHandler(command.Trim(new[] { ' ' }));
                continue;
            }

            sender.text = replic.Sender;
            message.text = "";
            foreach (var sentence in replic.GetText())
            {
                canChange = false;
                bool skip = false;
                foreach (var e in sentence)
                {
                    message.text += e.ToString();
                    if (!skip)
                    {
                        yield return new WaitForSeconds(textSpeed * 0.04f);
                        skip = canChange || Input.GetMouseButton(0);
                    }                    
                }

                yield return new WaitForSeconds(0.2f);
                canChange = false;
                while (!CanChange())
                {
                    yield return null;
                }
            }

            previousReplicsView.AddReplicToList(replic);
        }

        Debugger.Log("dialog has answers " + ThisDialog.HasAnswers);
        this.gameObject.SetActive(ThisDialog.HasAnswers);

        if (ThisDialog.HasAnswers)
        {
            replicView.SetActive(false);
            var VM = new VariantManager(ThisDialog.AnswersAndNextDialogs, selectingView);

            while (VM.SelectedDialog == null)
            {
                yield return null;
            }

            thisDialog = VM.SelectedDialog;
            GameObjectSetting();
            StartCoroutine(ShowDialog());
        }

        bool CanChange() => (canChange || Input.GetMouseButtonUp(0)) && (!previousReplicsView.IsActive);
    }
}

public class VariantManager
{
    private List<Tuple<string, Dialog>> Variants { get; set; }
    private GameObject SelectingView { get; set; }
    public Dialog SelectedDialog { get; private set; }

    public VariantManager(List<Tuple<string, Dialog>> variants, GameObject selectingView)
    {
        Variants = variants;
        SelectingView = selectingView;

        SelectingView.SetActive(true);
        var dialogFactory = new DialogFactory();

        foreach (var e in Variants)
        {
            var answer = new Tuple<string, Dialog>(e.Item1, e.Item2);
            var variant = dialogFactory.Instantiate("vrntVw");

            variant.transform.SetParent(SelectingView.transform);
            variant.GetComponent<Text>().text = answer.Item1;
            variant.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

            variant.GetComponent<Button>().onClick.AddListener(() => { SelectedDialog = answer.Item2; });
        }
    }
}

/// <summary>
/// класс, осуществляющий ожидание во время диалога в DialogController
/// </summary>
public class Waiter
{
    private YieldInstruction timeMeasure;

    public Waiter()
    {
        timeMeasure = null;
    }

    /// <summary>
    /// надо вызывать каждый раз в начале foreach в ShowDialog() корутине
    /// </summary>
    /// <returns>либо ожидание, либо null</returns>
    public YieldInstruction Call()
    {
        YieldInstruction result = timeMeasure;
        timeMeasure = null;
        return result;
    }

    /// <summary>
    /// задает время ожидания при следующем вызове Call
    /// </summary>
    /// <param name="measure">WaitForSeconds or smth idk</param>
    public void SetTimeMeasure(YieldInstruction measure)
    {
        timeMeasure = measure;
    }
}