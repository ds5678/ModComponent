using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModComponentMapper
{
    internal class ProbabilityManager
    {
        private enum DifficultyLevel
        {
            Pilgram,
            Voyager,
            Stalker,
            Interloper,
            Challenge,
            Storymode,
            Other
        }

        internal static float GetAdjustedProbabilty(float baseProbality)
        {
            float clampedProbability = Mathf.Clamp(baseProbality, 0f, 100f);
            //Logger.Log("Initial Probability: {0}", clampedProbability);
            if(clampedProbability == 100f)
            {
                return 100f;
            }

            DifficultyLevel difficultyLevel = GetDifficultyLevel();
            float result;
            switch (difficultyLevel)
            {
                case DifficultyLevel.Pilgram:
                    result = clampedProbability * Math.Max(0f, ConfigurationManager.configurations.pilgramSpawnProbabilityMultiplier);
                    break;
                case DifficultyLevel.Voyager:
                    result = clampedProbability * Math.Max(0f, ConfigurationManager.configurations.voyagerSpawnProbabilityMultiplier);
                    break;
                case DifficultyLevel.Stalker:
                    result = clampedProbability * Math.Max(0f, ConfigurationManager.configurations.stalkerSpawnProbabilityMultiplier);
                    break;
                case DifficultyLevel.Interloper:
                    result = clampedProbability * Math.Max(0f, ConfigurationManager.configurations.interloperSpawnProbabilityMultiplier);
                    break;
                case DifficultyLevel.Storymode:
                    result = clampedProbability * Math.Max(0f, ConfigurationManager.configurations.storySpawnProbabilityMultiplier);
                    break;
                case DifficultyLevel.Challenge:
                    result = clampedProbability * Math.Max(0f, ConfigurationManager.configurations.challengeSpawnProbabilityMultiplier);
                    break;
                default:
                    result = clampedProbability;
                    break;
            }
            //Logger.Log("Adjusted Probability: {0}", result);
            return Mathf.Clamp(result, 0f, 100f);
        }

        private static DifficultyLevel GetDifficultyLevel()
        {
            if (GameManager.IsStoryMode())
            {
                return DifficultyLevel.Storymode;
            }
            ExperienceModeType experienceModeType = ExperienceModeManager.GetCurrentExperienceModeType();
            switch (experienceModeType)
            {
                case ExperienceModeType.Pilgrim:
                    return DifficultyLevel.Pilgram;
                case ExperienceModeType.Voyageur:
                    return DifficultyLevel.Voyager;
                case ExperienceModeType.Stalker:
                    return DifficultyLevel.Stalker;
                case ExperienceModeType.Interloper:
                    return DifficultyLevel.Interloper;
                case ExperienceModeType.Custom:
                    return GetCustomDifficultyLevel();

                case ExperienceModeType.ChallengeArchivist:
                    return DifficultyLevel.Challenge;
                case ExperienceModeType.ChallengeDeadManWalking:
                    return DifficultyLevel.Challenge;
                case ExperienceModeType.ChallengeHunted:
                    return DifficultyLevel.Challenge;
                case ExperienceModeType.ChallengeHuntedPart2:
                    return DifficultyLevel.Challenge;
                case ExperienceModeType.ChallengeNomad:
                    return DifficultyLevel.Challenge;
                case ExperienceModeType.ChallengeNowhereToHide:
                    return DifficultyLevel.Challenge;
                case ExperienceModeType.ChallengeRescue:
                    return DifficultyLevel.Challenge;
                case ExperienceModeType.ChallengeWhiteout:
                    return DifficultyLevel.Challenge;
                default:
                    return DifficultyLevel.Other;
            }
        }

        private static DifficultyLevel GetCustomDifficultyLevel()
        {
            switch (GameManager.GetCustomMode().m_BaseWorldDifficulty)
            {
                case CustomExperienceModeManager.CustomTunableLMHV.VeryHigh:
                    return DifficultyLevel.Pilgram;
                case CustomExperienceModeManager.CustomTunableLMHV.High:
                    return DifficultyLevel.Voyager;
                case CustomExperienceModeManager.CustomTunableLMHV.Medium:
                    return DifficultyLevel.Stalker;
                case CustomExperienceModeManager.CustomTunableLMHV.Low:
                    return DifficultyLevel.Interloper;
                default:
                    return DifficultyLevel.Other;
            }
        }
    }
}
