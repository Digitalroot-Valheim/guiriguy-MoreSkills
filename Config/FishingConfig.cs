﻿using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using MoreSkills.UI;
using Pipakin.SkillInjectorMod;
using UnityEngine;

namespace MoreSkills.Config
{
    [BepInPlugin("MoreSkills.FishingConfig", "MoreSkills: Fishing", "0.0.1")]
    [BepInDependency("com.pipakin.SkillInjectorMod")]
    public class MoreSkills_FishingConfig: BaseUnityPlugin
    {
        public void Awake()
        {
            Debug.Log("Loading Fishing Skill...");
            //Enablers
            //Fishing Skill
            EnableFishingSkill = base.Config.Bind<bool>("1. Enablers", "Enable Fishing Skill", true, "Enables or disables the Fishing Skill.");
            //FishStack
            EnableFishStackMod = base.Config.Bind<bool>("1. Enablers", "Enable Fish Drop Amount Mod", true, "Enables or disables the Fish Drop Amount Modification.");
            //BaseHook
            EnableFishBaseHookMod = base.Config.Bind<bool>("1. Enablers", "Enable Fish Base Chance of Hook Mod", true, "Enables or disables the Fish Base Hook Chance Modification.");
            //Stamina
            EnableFishingStaminaMod = base.Config.Bind<bool>("1. Enablers", "Enable Stamina Drain Decrease Mod", true, "Enables or disables the Stamina Decrease Modification.");
            //AutoHook
            EnableFishingAutoHook = base.Config.Bind<bool>("1. Enablers", "Enable Fishing Auto Hook", true, "Enables or disables the Fishing Auto Hook.");
            //Variety
            EnableFishVariety = base.Config.Bind<bool>("1. Enablers", "Enable Fish Variety", true, "Enables or disables the Fish Variety.");
            //Multipliers
            //FishDrops
            FishDropMultiplier = base.Config.Bind<float>("2. Multipliers", "Fish Drop Multiplier", 4.0f, "Multiplies the amount of Raw Fish dropped from a Fish");
            //Skill
            FishingSkillIncreaseMultiplier = base.Config.Bind<float>("2. Multipliers", "Fishing Skill Increase Multiplier", 3.0f, "Multiplies the Amount of Skill Gain at Fishing");
            //Base Configs
            //Auto Hook Level
            AutoHookLevel = base.Config.Bind<int>("3. BaseConfigs", "Fishing Skill Auto Hook Level", 75, "The level at which you reach the max Percentage to Auto Hook.");
            //Auto Hook Level
            AutoHookPercentage = base.Config.Bind<int>("3. BaseConfigs", "Auto Hook Max Percentage", 50, "The Max Percentage once reached the Fishing Skill Auto Hook Level, this will be the Chance of Auto Hook.");

            //Inject.Strength
            if (EnableFishingSkill.Value)
                try
                {
                    SkillInjector.RegisterNewSkill(706, "Fishing", "Fishing Stamina Drain, Fishing Base Hook, Fish Drop", 1f, SkillIcons.Load_FishingIcon(), Skills.SkillType.Unarmed);
                }
                catch
                {
                }

            //--
            Debug.Log("Fishing Skill Patched!");
            harmonyFishing = new Harmony("MoreSkills.FishingConfig.GuiriGuyMods");

            //Logs
            if (!EnableFishingSkill.Value)
                Debug.LogWarning("[MoreSkills] Fishing Skill Disabled");
            else
            {
                Debug.Log("[MoreSkills] Fishing Skill Enabled");
                if (!EnableFishStackMod.Value)
                    Debug.LogWarning("[MoreSkills]Fishing/Fish Stack Amount Mod Disabled");
                else
                    Debug.Log("[MoreSkills] Fishing/Fish Stack Amount  Mod Enabled");
                if (!EnableFishBaseHookMod.Value)
                    Debug.LogWarning("[MoreSkills]Fishing/Fish Base Hook Chance Mod Disabled");
                else
                    Debug.Log("[MoreSkills]Fishing/Fish Base Hook Chance Mod Enabled");
                if (!EnableFishVariety.Value)
                    Debug.LogWarning("[MoreSkills]Fishing/Variety Mod Disabled");
                else
                    Debug.Log("[MoreSkills]Fishing/Variety Mod Enabled");
                if (!EnableFishingStaminaMod.Value)
                    Debug.LogWarning("[MoreSkills]Fishing/Stamina Drain Decrease Mod Disabled");
                else
                    Debug.Log("[MoreSkills]Fishing/Stamina Drain Decrease Mod Enabled");
            }


            Debug.Log("Fishing Skill Loaded!");
        }
        private void OnDestroy()
        {

            Debug.Log("Fishing Skill UnPatched!");
            harmonyFishing.UnpatchSelf();
        }

        // Stats Bases

        public static ConfigEntry<int> AutoHookLevel;

        public static ConfigEntry<int> AutoHookPercentage;

        private Harmony harmonyFishing;

        //Multipliers

        public static ConfigEntry<float> FishDropMultiplier;

        //Skill Increases Multpliers

        public static ConfigEntry<float> FishingSkillIncreaseMultiplier;

        //Enables

        public static ConfigEntry<bool> EnableFishingSkill;

        public static ConfigEntry<bool> EnableFishStackMod;

        public static ConfigEntry<bool> EnableFishBaseHookMod;

        public static ConfigEntry<bool> EnableFishingStaminaMod;

        public static ConfigEntry<bool> EnableFishingAutoHook;

        public static ConfigEntry<bool> EnableFishVariety;

        //Skills Types

        public const int FishingSkill_Type = 706;



    }
}
