using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] Image experienceFill;

    private void OnEnable()
    {
        ExperienceManager.UpdateExperienceUIEvent += UpdateDisplay;
    }

    private void UpdateDisplay()
    {
        int currentLevel = ExperienceManager.Instance.GetCurrentLevel();
        int start = ExperienceManager.Instance.GetTotalExperience() - ExperienceManager.Instance.GetPreviousLevelExperience();
        int end = ExperienceManager.Instance.GetNextLevelExperience() - ExperienceManager.Instance.GetPreviousLevelExperience();

        levelText.text = currentLevel.ToString();
        experienceText.text = start + " exp / " + end + " exp";
        experienceFill.fillAmount = (float)start / (float)end;
    }

    private void OnDisable()
    {
        ExperienceManager.UpdateExperienceUIEvent -= UpdateDisplay;
    }
}
