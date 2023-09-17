using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Experience")]
    [SerializeField] AnimationCurve experienceCurve;
    [SerializeField] int currentLevel;
    [SerializeField] int totalExperience;
    [SerializeField] int previousLevelExperience;
    [SerializeField] int nextLevelExperience;

    [Header("Interface")]
    [SerializeField] TextMeshProUGUI levlText;
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] Image experienceFill;

    private void OnEnable()
    {
        ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
    }

    private void Start()
    {
        UpdateLevel();
    }

     private void HandleExperienceChange(int newExperience)
    {
        totalExperience += newExperience;
        CheckForLevelUp();
        UpdateInterface();
    }

    private void CheckForLevelUp()
    {
        if(totalExperience >= nextLevelExperience)
        {
            currentLevel++;
            UpdateLevel();
            //Insert leveling sequence here
        }
    }

    private void UpdateLevel()
    {
        previousLevelExperience = (int)experienceCurve.Evaluate(currentLevel);
        nextLevelExperience = (int)experienceCurve.Evaluate(currentLevel + 1);
        UpdateInterface();
    }

    private void UpdateInterface()
    {
        int start = totalExperience - previousLevelExperience;
        int end = nextLevelExperience - previousLevelExperience;

        levlText.text = currentLevel.ToString();
        experienceText.text = start + " exp / " + end + " exp";
        experienceFill.fillAmount = (float)start / (float)end;
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
    }
}