using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    //Static
    public static ExperienceManager Instance;
    public static event Action UpdateExperienceUIEvent;

    //Delegates and Events
    public delegate void ExperienceChangeHandler(int amount);
    public event ExperienceChangeHandler OnExperienceChange;

    [Header("Experience")]
    [SerializeField] AnimationCurve experienceCurve;
    [SerializeField] int currentLevel = 1;
    [SerializeField] int maxLevel = 10;
    [SerializeField] int totalExperience;
    [SerializeField] int previousLevelExperience;
    [SerializeField] int nextLevelExperience;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
    }

    private void Start()
    {
        UpdateLevel();
    }

    public void AddExperience(int amount)
    {
        OnExperienceChange?.Invoke(amount);
    }

    public void UpdateExperienceUI()
    {
        UpdateExperienceUIEvent?.Invoke();
    }

    private void HandleExperienceChange(int newExperience)
    {
        totalExperience += newExperience;
        CheckForLevelUp();
        UpdateExperienceUI();
    }

    private void CheckForLevelUp()
    {
        if(currentLevel == maxLevel) return;
        
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
        UpdateExperienceUI();
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public int GetTotalExperience()
    {
        return totalExperience;
    }

    public int GetPreviousLevelExperience()
    {
        return previousLevelExperience;
    }

    public int GetNextLevelExperience()
    {
        return nextLevelExperience;
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
    }
}
