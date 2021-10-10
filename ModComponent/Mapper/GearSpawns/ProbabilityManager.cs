using System;
using UnityEngine;

namespace ModComponentMapper
{

	internal static class ProbabilityManager
	{
		internal static float GetAdjustedProbability(GearSpawnInfo gearSpawnInfo)
		{
			if (ModComponent.Main.Settings.instance.alwaysSpawnItems) return 100f; //overrides everything else

			DifficultyLevel difficultyLevel = GetDifficultyLevel();
			FirearmAvailability firearmAvailability = GetFirearmAvailability();

			if (SpawnTagManager.ContainsTag(gearSpawnInfo.tag))
			{
				return SpawnTagManager.GetTaggedFunction(gearSpawnInfo.tag).Invoke(difficultyLevel, firearmAvailability, gearSpawnInfo);
			}
			else return GetAdjustedProbability(difficultyLevel, gearSpawnInfo.SpawnChance);
		}

		private static float GetAdjustedProbability(DifficultyLevel difficultyLevel, float baseProbability)
		{
			float multiplier = 1f;
			switch (difficultyLevel)
			{
				case DifficultyLevel.Pilgram:
					multiplier = Math.Max(0f, ModComponent.Main.Settings.instance.pilgramSpawnProbabilityMultiplier);
					break;
				case DifficultyLevel.Voyager:
					multiplier = Math.Max(0f, ModComponent.Main.Settings.instance.voyagerSpawnProbabilityMultiplier);
					break;
				case DifficultyLevel.Stalker:
					multiplier = Math.Max(0f, ModComponent.Main.Settings.instance.stalkerSpawnProbabilityMultiplier);
					break;
				case DifficultyLevel.Interloper:
					multiplier = Math.Max(0f, ModComponent.Main.Settings.instance.interloperSpawnProbabilityMultiplier);
					break;
				case DifficultyLevel.Storymode:
					multiplier = Math.Max(0f, ModComponent.Main.Settings.instance.storySpawnProbabilityMultiplier);
					break;
				case DifficultyLevel.Challenge:
					multiplier = Math.Max(0f, ModComponent.Main.Settings.instance.challengeSpawnProbabilityMultiplier);
					break;
			}
			if (multiplier == 0f) return 0f; //can disable spawns for a game mode

			float clampedProbability = Mathf.Clamp(baseProbability, 0f, 100f);//just to be safe

			if (clampedProbability == 100f) return 100f; //for guaranteed spawns
			else return Mathf.Clamp(multiplier * clampedProbability, 0f, 100f); //for normal spawns
		}

		public static DifficultyLevel GetDifficultyLevel()
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

		public static FirearmAvailability GetFirearmAvailability()
		{
			if (GameManager.IsStoryMode())
			{
				if (SaveGameSystem.m_CurrentEpisode == Episode.One || SaveGameSystem.m_CurrentEpisode == Episode.Two) return FirearmAvailability.Rifle;
				else return FirearmAvailability.All;
			}
			ExperienceModeType experienceModeType = ExperienceModeManager.GetCurrentExperienceModeType();
			switch (experienceModeType)
			{
				case ExperienceModeType.Interloper:
					return FirearmAvailability.None;
				case ExperienceModeType.Custom:
					return GetCustomFirearmAvailability();
				default:
					return FirearmAvailability.All;
			}
		}

		private static FirearmAvailability GetCustomFirearmAvailability()
		{
			bool revolvers = GameManager.GetCustomMode().m_RevolversInWorld;
			bool rifles = GameManager.GetCustomMode().m_RiflesInWorld;
			if (revolvers && rifles) return FirearmAvailability.All;
			else if (revolvers) return FirearmAvailability.Revolver;
			else if (rifles) return FirearmAvailability.Rifle;
			else return FirearmAvailability.None;
		}
	}
}
