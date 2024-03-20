using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;
using UnityEngine.UI;

public class NormalProbabilityScript : MonoBehaviour
{
    static int _moduleIdCounter = 1;
    int _moduleID = 0;

    public KMBombModule Module;
    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMSelectable[] Buttons;
    public Image StageIndUnder, StageIndOver;
    public Text EventText, InputText;

    private static readonly List<List<int>> TableLeft = new List<List<int>>()
    {
        new List<int>() { 5000, 5040, 5080, 5120, 5160, 5199, 5239, 5279, 5319, 5359 },
        new List<int>() { 5398, 5438, 5478, 5517, 5557, 5596, 5636, 5675, 5714, 5753 },
        new List<int>() { 5793, 5832, 5871, 5910, 5948, 5987, 6026, 6064, 6103, 6141 },
        new List<int>() { 6179, 6217, 6255, 6293, 6331, 6368, 6406, 6443, 6480, 6517 },
        new List<int>() { 6554, 6591, 6628, 6664, 6700, 6736, 6772, 6808, 6844, 6879 },
        new List<int>() { 6915, 6950, 6985, 7019, 7054, 7088, 7123, 7157, 7190, 7224 },
        new List<int>() { 7257, 7291, 7324, 7357, 7389, 7422, 7454, 7486, 7517, 7549 },
        new List<int>() { 7580, 7611, 7642, 7673, 7704, 7734, 7764, 7794, 7823, 7852 },
        new List<int>() { 7881, 7910, 7939, 7967, 7995, 8023, 8051, 8078, 8106, 8133 },
        new List<int>() { 8159, 8186, 8212, 8238, 8264, 8289, 8315, 8340, 8365, 8389 },
        new List<int>() { 8413, 8438, 8461, 8485, 8508, 8531, 8554, 8577, 8599, 8621 },
        new List<int>() { 8643, 8665, 8686, 8708, 8729, 8749, 8770, 8790, 8810, 8830 },
        new List<int>() { 8849, 8869, 8888, 8907, 8925, 8944, 8962, 8980, 8997, 9015 },
        new List<int>() { 9032, 9049, 9066, 9082, 9099, 9115, 9131, 9147, 9162, 9177 },
        new List<int>() { 9192, 9207, 9222, 9236, 9251, 9265, 9279, 9292, 9306, 9319 },
        new List<int>() { 9332, 9345, 9357, 9370, 9382, 9394, 9406, 9418, 9429, 9441 },
        new List<int>() { 9452, 9463, 9474, 9484, 9495, 9505, 9515, 9525, 9535, 9545 },
        new List<int>() { 9554, 9564, 9573, 9582, 9591, 9599, 9608, 9616, 9625, 9633 },
        new List<int>() { 9641, 9649, 9656, 9664, 9671, 9678, 9686, 9693, 9699, 9706 },
        new List<int>() { 9713, 9719, 9726, 9732, 9738, 9744, 9750, 9756, 9761, 9767 },
        new List<int>() { 9772, 9778, 9783, 9788, 9793, 9798, 9803, 9808, 9812, 9817 },
        new List<int>() { 9821, 9826, 9830, 9834, 9838, 9842, 9846, 9850, 9854, 9857 },
        new List<int>() { 9861, 9864, 9868, 9871, 9875, 9878, 9881, 9884, 9887, 9890 },
        new List<int>() { 9893, 9896, 9898, 9901, 9904, 9906, 9909, 9911, 9913, 9916 },
        new List<int>() { 9918, 9920, 9922, 9925, 9927, 9929, 9931, 9932, 9934, 9936 },
        new List<int>() { 9938, 9940, 9941, 9943, 9945, 9946, 9948, 9949, 9951, 9952 },
        new List<int>() { 9953, 9955, 9956, 9957, 9959, 9960, 9961, 9962, 9963, 9964 },
        new List<int>() { 9965, 9966, 9967, 9968, 9969, 9970, 9971, 9972, 9973, 9974 },
        new List<int>() { 9974, 9975, 9976, 9977, 9977, 9978, 9979, 9979, 9980, 9981 },
        new List<int>() { 9981, 9982, 9982, 9983, 9984, 9984, 9985, 9985, 9986, 9986 },
        new List<int>() { 9987, 9987, 9987, 9988, 9988, 9989, 9989, 9989, 9990, 9990 }
    };

    private static readonly List<List<int>> TableRight = new List<List<int>>()
    {
        new List<int> { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36 },
        new List<int> { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36 },
        new List<int> { 0, 4, 8, 12, 15, 19, 23, 27, 31, 35 },
        new List<int> { 0, 4, 8, 11, 15, 19, 23, 26, 30, 34 },
        new List<int> { 0, 4, 7, 11, 14, 18, 22, 25, 29, 32 },
        new List<int> { 0, 3, 7, 10, 14, 17, 21, 24, 27, 31 },
        new List<int> { 0, 3, 6, 10, 13, 16, 19, 23, 26, 29 },
        new List<int> { 0, 3, 6, 9, 12, 15, 18, 21, 24, 27 },
        new List<int> { 0, 3, 6, 8, 11, 14, 17, 19, 22, 25 },
        new List<int> { 0, 3, 5, 8, 10, 13, 15, 18, 20, 23 },
        new List<int> { 0, 2, 5, 7, 9, 12, 14, 16, 18, 21 },
        new List<int> { 0, 2, 4, 6, 8, 10, 12, 14, 16, 19 },
        new List<int> { 0, 2, 4, 6, 7, 9, 11, 13, 15, 16 },
        new List<int> { 0, 2, 3, 5, 6, 8, 10, 11, 13, 14 },
        new List<int> { 0, 1, 3, 4, 6, 7, 8, 10, 11, 13 },
        new List<int> { 0, 1, 2, 4, 5, 6, 7, 8, 10, 11 },
        new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
        new List<int> { 0, 1, 2, 3, 3, 4, 5, 6, 7, 8 },
        new List<int> { 0, 1, 1, 2, 3, 4, 4, 5, 6, 6 },
        new List<int> { 0, 1, 1, 2, 2, 3, 4, 4, 5, 5 },
        new List<int> { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4 },
        new List<int> { 0, 0, 1, 1, 2, 2, 2, 3, 3, 4 },
        new List<int> { 0, 0, 1, 1, 1, 2, 2, 2, 3, 3 },
        new List<int> { 0, 0, 1, 1, 1, 1, 2, 2, 2, 2 },
        new List<int> { 0, 0, 0, 1, 1, 1, 1, 1, 2, 2 },
        new List<int> { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 },
        new List<int> { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 },
        new List<int> { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
        new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
        new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };

    private Coroutine[] ButtonAnimCoroutines;
    private Coroutine ProgressStageCoroutine;
    private string InputDigits = "";
    private const string RandomCharacters = "!\"£$%^&*()-_=+[{]};:'@#~,<.>/?`¬¦";
    private float StageIndUnderInit, StageIndOverInit;
    private List<int> QuestionTypes = new List<int>();
    private List<List<int>> AssociatedNumbers = new List<List<int>>();
    private List<string> Answers = new List<string>();
    private int CurrentStage;
    private bool Solved;

    private int FindProbability(int input)
    {
        if (input < 0)
            return 10000 - (TableLeft[-input / 100][(-input / 10) % 10] + TableRight[-input / 100][-input % 10]);
        else
            return TableLeft[input / 100][(input / 10) % 10] + TableRight[input / 100][input % 10];
    }

    void Awake()
    {
        _moduleID = _moduleIdCounter++;

        ButtonAnimCoroutines = new Coroutine[Buttons.Length];
        StageIndUnderInit = StageIndUnder.transform.localPosition.x;
        StageIndOverInit = StageIndOver.transform.localPosition.x;

        Calculate();
        FormatQuestion();
        FormatInput();

        for (int i = 0; i < Buttons.Length; i++)
        {
            int x = i;
            Buttons[x].OnInteract += delegate { ButtonPress(x); return false; };
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Calculate()
    {
        QuestionTypes = new List<int>() { 0, Rnd.Range(1, 4), 4 };
        /* 0: Z < +ve
         * 1: Z < -ve
         * 2: Z > +ve
         * 3: Z > -ve
         * 4: # < Z < #
         */
        foreach (var type in QuestionTypes)
        {
            switch (type)
            {
                case 0:
                    AssociatedNumbers.Add(new List<int>() { Rnd.Range(0, 301) * 10 });
                    Answers.Add(FindProbability(AssociatedNumbers.Last().First()).ToString("0000"));
                    Debug.LogFormat("[Normal Probability #{0}] Stage 1: Z < {1}. Using the table like normal, the answer is 0.{2}.", _moduleID, AssociatedNumbers.Last().First() / 1000f, Answers.Last());
                    break;

                case 1:
                    AssociatedNumbers.Add(new List<int>() { -Rnd.Range(0, 3001) });
                    Answers.Add(FindProbability(AssociatedNumbers.Last().First()).ToString("0000"));
                    Debug.LogFormat("[Normal Probability #{0}] Stage 2: Z < {1}. Using the table, and taking 1 minus the answer (since we're using a negative number), the answer is 0.{2}.", _moduleID, AssociatedNumbers.Last().First() / 1000f, Answers.Last());
                    break;

                case 2:
                    AssociatedNumbers.Add(new List<int>() { Rnd.Range(0, 3001) });
                    Answers.Add(FindProbability(-AssociatedNumbers.Last().First()).ToString("0000"));
                    Debug.LogFormat("[Normal Probability #{0}] Stage 2: Z > {1}. Using the table, and taking 1 minus the answer (since there's a greater than sign, and not a less than sign), the answer is 0.{2}.", _moduleID, AssociatedNumbers.Last().First() / 1000f, Answers.Last());
                    break;

                case 3:
                    AssociatedNumbers.Add(new List<int>() { -Rnd.Range(0, 3001) });
                    Answers.Add(FindProbability(-AssociatedNumbers.Last().First()).ToString("0000"));
                    Debug.LogFormat("[Normal Probability #{0}] Stage 2: Z > {1}. Using the table like normal (there is both a negative number and a greater than sign), the answer is 0.{2}.", _moduleID, AssociatedNumbers.Last().First() / 1000f, Answers.Last());
                    break;

                case 4:
                    retry:
                    var rand1 = Rnd.Range(-1000, 1000);
                    var rand2 = rand1;
                    while (Mathf.Abs(rand2 - rand1) < 50)
                        rand2 = Rnd.Range(-1000, 1000);
                    AssociatedNumbers.Add(new List<int>() { rand1, rand2 });
                    AssociatedNumbers.Last().Sort();
                    var answerTemp = FindProbability(AssociatedNumbers.Last().Last()) - FindProbability(AssociatedNumbers.Last().First());
                    if (answerTemp < 1 || answerTemp >= 10000)
                        goto retry;
                    Answers.Add(answerTemp.ToString("0000"));
                    Debug.LogFormat("[Normal Probability #{0}] Stage 3: {1} < Z < {2}. Finding the probabilities that Z is less than both numbers, separately, then subtracting these two answers, the answer is 0.{3}.", _moduleID, AssociatedNumbers.Last().First() / 1000f, AssociatedNumbers.Last().Last() / 1000f, Answers.Last());
                    break;
            }
        }
    }

    void ButtonPress(int pos)
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, Buttons[pos].transform);
        Buttons[pos].AddInteractionPunch();
        if (ButtonAnimCoroutines[pos] != null)
            StopCoroutine(ButtonAnimCoroutines[pos]);
        ButtonAnimCoroutines[pos] = StartCoroutine(ButtonAnim(pos));
        if (!Solved)
        {
            if (pos == 11)
            {
                var tempInput = InputDigits;
                if (tempInput.Length < 4)
                    tempInput += "0";
                if (tempInput == Answers[CurrentStage])
                {
                    CurrentStage++;
                    Audio.PlaySoundAtTransform("progress", transform);
                    if (CurrentStage == 3)
                    {
                        Debug.LogFormat("[Normal Probability #{0}] You submitted 0.{1} for stage {2}, which was correct. Module solved!", _moduleID, InputDigits, CurrentStage);
                        Solved = true;
                        InputDigits = "";
                        InputText.text = "";
                    }
                    else
                    {
                        Debug.LogFormat("[Normal Probability #{0}] You submitted 0.{1} for stage {2}, which was correct.", _moduleID, InputDigits, CurrentStage);
                        FormatQuestion();
                        InputDigits = "";
                        FormatInput();
                    }
                    if (ProgressStageCoroutine != null)
                        StopCoroutine(ProgressStageCoroutine);
                    ProgressStageCoroutine = StartCoroutine(ProgressStage());
                }
                else
                {
                    Module.HandleStrike();
                    Debug.LogFormat("[Normal Probability #{0}] You submitted 0.{1} for stage {2}, which was incorrect. Strike!", _moduleID, InputDigits, CurrentStage + 1);
                }
            }
            else if (pos == 10)
            {
                if (InputDigits.Length > 0)
                {
                    InputDigits = InputDigits.Substring(0, InputDigits.Length - 1);
                    Audio.PlaySoundAtTransform("delete", Buttons[pos].transform);
                    FormatInput();
                }
                else
                    Audio.PlaySoundAtTransform("buzzer", Buttons[pos].transform);
            }
            else
            {
                if (InputDigits.Length < 4)
                {
                    InputDigits += pos.ToString();
                    Audio.PlaySoundAtTransform("type", Buttons[pos].transform);
                    FormatInput();
                }
                else
                    Audio.PlaySoundAtTransform("buzzer", Buttons[pos].transform);
            }
        }
        else if (pos < 10)
        {
            InputDigits = InputDigits + pos;
            if (InputDigits.Length > 3)
                InputDigits = InputDigits.Substring(1, 3);
            if (InputDigits == "215")
                Audio.PlaySoundAtTransform("easter egg", transform);
        }
    }

    void FormatQuestion()
    {
        switch (QuestionTypes[CurrentStage])
        {
            case 0: case 1:
                EventText.text = "Z < " + (AssociatedNumbers[CurrentStage].First() / 1000f).ToString();
                break;
            case 2: case 3:
                EventText.text = "Z > " + (AssociatedNumbers[CurrentStage].First() / 1000f).ToString();
                break;
            case 4:
                EventText.text = (AssociatedNumbers[CurrentStage].First() / 1000f).ToString() + " < Z < " + (AssociatedNumbers[CurrentStage].Last() / 1000f).ToString();
                break;
        }
    }

    void FormatInput()
    {
        InputText.text = "0." + InputDigits + "<color=#444>" + "0000".Take(4 - InputDigits.Length).Join("") + "</color>";
    }

    private IEnumerator ButtonAnim(int pos, float duration = 0.075f, float depression = 0.003f)
    {
        float timer = 0;
        while (timer < duration)
        {
            yield return null;
            timer += Time.deltaTime;
            Buttons[pos].transform.localPosition = new Vector3(Buttons[pos].transform.localPosition.x, Mathf.Lerp(0, -depression, timer / duration), Buttons[pos].transform.localPosition.z);
        }
        Buttons[pos].transform.localPosition = new Vector3(Buttons[pos].transform.localPosition.x, -depression, Buttons[pos].transform.localPosition.z);
        timer = 0;
        while (timer < duration)
        {
            yield return null;
            timer += Time.deltaTime;
            Buttons[pos].transform.localPosition = new Vector3(Buttons[pos].transform.localPosition.x, Mathf.Lerp(-depression, 0, timer / duration), Buttons[pos].transform.localPosition.z);
        }
        Buttons[pos].transform.localPosition = new Vector3(Buttons[pos].transform.localPosition.x, 0, Buttons[pos].transform.localPosition.z);
    }

    private IEnumerator ProgressStage(float duration = 0.5f, float solveDuration = 1f, float duplicateDist = 20f, float moveDuration = 0.5f)
    {
        StageIndUnder.transform.localPosition = new Vector3(StageIndUnderInit * (1 - ((CurrentStage - 1) / 3f)), StageIndUnder.transform.localPosition.y, StageIndUnder.transform.localPosition.z);
        StageIndOver.transform.localPosition = new Vector3(StageIndOverInit * (1 - ((CurrentStage - 1) / 3f)), StageIndOver.transform.localPosition.y, StageIndOver.transform.localPosition.z);
        float timer = 0;
        while (timer < duration)
        {
            yield return null;
            timer += Time.deltaTime;
            StageIndUnder.transform.localPosition = new Vector3(Easing.OutExpo(timer, StageIndUnderInit * (1 - ((CurrentStage - 1) / 3f)), StageIndUnderInit * (1 - (CurrentStage / 3f)), duration), StageIndUnder.transform.localPosition.y, StageIndUnder.transform.localPosition.z);
            StageIndOver.transform.localPosition = new Vector3(Easing.OutExpo(timer, StageIndOverInit * (1 - ((CurrentStage - 1) / 3f)), StageIndOverInit * (1 - (CurrentStage / 3f)), duration), StageIndOver.transform.localPosition.y, StageIndOver.transform.localPosition.z);
        }
        StageIndUnder.transform.localPosition = new Vector3(StageIndUnderInit * (1 - (CurrentStage / 3f)), StageIndUnder.transform.localPosition.y, StageIndUnder.transform.localPosition.z);
        StageIndOver.transform.localPosition = new Vector3(StageIndOverInit * (1 - (CurrentStage / 3f)), StageIndOver.transform.localPosition.y, StageIndOver.transform.localPosition.z);
        if (CurrentStage == 3)
        {
            timer = 0;
            while (timer < 0.25f)
            {
                yield return null;
                timer += Time.deltaTime;
            }
            yield return "solve";
            Module.HandlePass();
            Audio.PlaySoundAtTransform("solve", transform);
            var initUnder = StageIndUnder.color;
            StageIndUnder.color = Color.white;
            var initOver = StageIndOver.color;
            StageIndOver.color = Color.white;
            timer = 0;
            while (timer < solveDuration)
            {
                yield return null;
                timer += Time.deltaTime;
                StageIndUnder.color = Color.Lerp(Color.white, initUnder, timer / solveDuration);
                StageIndOver.color = Color.Lerp(Color.white, initOver, timer / solveDuration);
            }
            StageIndUnder.color = initUnder;
            StageIndOver.color = initOver;
            timer = 0;
            while (timer < 0.25f)
            {
                yield return null;
                timer += Time.deltaTime;
            }
            var duplicateText = Instantiate(EventText, EventText.transform.parent);
            EventText.transform.localPosition = new Vector3(0, duplicateDist, 0);
            EventText.text = "Module solved!";
            duplicateText.transform.localPosition = Vector3.zero;
            timer = 0;
            while (timer < moveDuration)
            {
                yield return null;
                timer += Time.deltaTime;
                EventText.transform.localPosition = Vector3.up * Easing.InOutQuint(timer, duplicateDist, 0, moveDuration);
                duplicateText.transform.localPosition = Vector3.up * Easing.InOutQuint(timer, 0, -duplicateDist, moveDuration);
            }
            EventText.transform.localPosition = Vector3.zero;
            Destroy(duplicateText.gameObject);
        }
    }

#pragma warning disable 414
    private string TwitchHelpMessage = "Use '!{0} 1234-*' to press 1, 2, 3, 4, DEL and SUB.";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {
        command = command.ToLowerInvariant();
        var validCommands = "0123456789-*";

        foreach (var character in command)
            if (!validCommands.Contains(character))
            {
                yield return "sendtochaterror Invalid command.";
                yield break;
            }
        yield return null;
        foreach (var character in command)
        {
            Buttons[validCommands.IndexOf(character)].OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        while (!Solved)
        {
            while (InputDigits != Answers[CurrentStage].Substring(0, InputDigits.Length))
            {
                Buttons[10].OnInteract();
                yield return new WaitForSeconds(0.05f);
            }
            while (InputDigits.Length < 4)
            {
                Buttons[int.Parse(Answers[CurrentStage][InputDigits.Length].ToString())].OnInteract();
                yield return new WaitForSeconds(0.05f);
            }
            if (InputDigits == Answers[CurrentStage])
                Buttons[11].OnInteract();
        }
    }
}