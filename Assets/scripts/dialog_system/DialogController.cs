using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class DialogController : MonoBehaviour
{
    private static float textSpeed = 1.1f - 0.7f;

    private CommandHandler CommandHandler { get; set; }
    private Image Background { get; set; }
    private Image Foreground { get; set; }
    private GameObject ReplicView { get; set; }
    private GameObject SelectingView { get; set; }
    private PreviousReplicManager PreviousReplicsView { get; set; }

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
        CommandHandler.HandleCommand(command);
    }

    private void GameObjectSetting()
    {
        Background = this.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        Background.sprite = Resources.Load<Sprite>("backgrounds/" + ThisDialog.BackgroundName);
        Background.color = Color.white;

        Foreground = this.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        ReplicView = this.transform.GetChild(0).GetChild(2).gameObject;
        SelectingView = this.transform.GetChild(0).GetChild(3).gameObject;
        PreviousReplicsView = this.transform.GetChild(0).GetChild(4).GetComponent<PreviousReplicManager>();
        SelectingView.SetActive(false);
        ReplicView.SetActive(true);
    }

    private void Start()
    {
        this.GetComponent<Canvas>().worldCamera =
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        GameObjectSetting();

        CommandHandler = new CommandHandler(
            new List<ICommandHandler>()
            {
                new BackgroundHandler(Background, Foreground),
                new CharacterHandler(Background)
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

        var nextReplicButton = ReplicView.transform.GetChild(2).GetComponent<Button>();
        var previousReplicsButton = ReplicView.transform.GetChild(3).GetComponent<Button>();

        var sender = ReplicView.transform.GetChild(0).GetComponent<Text>();
        var message = ReplicView.transform.GetChild(1).GetComponent<Text>();

        bool canChange = true;

        nextReplicButton.onClick.AddListener(() => canChange = true);
        previousReplicsButton.onClick.AddListener(() => PreviousReplicsView.Show());

        foreach (var replic in ThisDialog.GetReplic())
        {
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

            PreviousReplicsView.AddReplicToList(replic);
        }

        Debugger.Log("dialog has answers " + ThisDialog.HasAnswers);
        this.gameObject.SetActive(ThisDialog.HasAnswers);

        if (ThisDialog.HasAnswers)
        {
            ReplicView.SetActive(false);
            var VM = new VariantManager(ThisDialog.AnswersAndNextDialogs, SelectingView);

            while (VM.SelectedDialog == null)
            {
                yield return null;
            }

            thisDialog = VM.SelectedDialog;
            GameObjectSetting();
            StartCoroutine(ShowDialog());
        }

        bool CanChange() => (canChange || Input.GetMouseButtonUp(0)) && (!PreviousReplicsView.IsActive);
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
