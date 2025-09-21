using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class SpeechManager : MonoBehaviour
{
    public static SpeechManager Instance;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI speechText;
    [SerializeField] GameObject speechPanel;
    [SerializeField] Image image;

    private Queue<SpeechData> speechQueue = new Queue<SpeechData>();
    private string[] currentLines;
    private int currentLineIndex;
    private bool isActive = false;
    private bool cooldownInputs = false;

    public Queue<SpeechData> SpeechQueue => speechQueue;

    public bool IsActive => isActive;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        InputsManagers.Instance.OnActionKeyPressed += OnActionKeyPressed;
    }

    private void OnDestroy()
    {
        InputsManagers.Instance.OnActionKeyPressed -= OnActionKeyPressed;
    }

    private void OnActionKeyPressed()
    {
        StartCoroutine(HelperFunctionsUtility.IE_WaitForFrames(() => ClickedNewSpeech(), 1));
    }

    private void ClickedNewSpeech()
    {
        if (isActive && !cooldownInputs)
        {
            ShowNextLine();
        }
    }

    /// <summary>
    /// Adiciona um SpeechData à fila
    /// </summary>
    public void StartSpeech(SpeechData speechData)
    {
        if (speechData == null) return;

        speechQueue.Enqueue(speechData);

        // se não tem nenhum diálogo ativo, inicia imediatamente
        if (!isActive && cooldownInputs == false)
        {
            StartNextSpeech();
        }
    }

    /// <summary>
    /// Começa um SpeechData da fila
    /// </summary>
    private void StartNextSpeech()
    {
        if (speechQueue.Count == 0)
        {
            EndSpeech();
            return;
        }

        SpeechData nextSpeech = speechQueue.Dequeue();
        currentLines = nextSpeech.Lines;
        currentLineIndex = 0;
        image.sprite = nextSpeech.Icon;

        speechPanel.SetActive(true);
        isActive = true;

        ShowNextLine();
    }

    /// <summary>
    /// Mostra a próxima linha dentro do SpeechData atual
    /// </summary>
    private void ShowNextLine()
    {
        if (currentLines == null || currentLineIndex >= currentLines.Length)
        {
            // inicia cooldown
            StartCoroutine(InputCooldownCoroutine());
            // terminou este SpeechData, vai pro próximo
            StartNextSpeech();
            return;
        }

        speechText.text = currentLines[currentLineIndex];
        currentLineIndex++;
        // inicia cooldown
        StartCoroutine(InputCooldownCoroutine());
    }

    private void EndSpeech()
    {
        isActive = false;
        speechPanel.SetActive(false);
        currentLines = null;
        currentLineIndex = 0;
    }

    private IEnumerator InputCooldownCoroutine()
    {
        cooldownInputs = true;
        yield return new WaitForSeconds(0.5f);
        cooldownInputs = false;
    }
}
