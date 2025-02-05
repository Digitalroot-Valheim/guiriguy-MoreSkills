﻿using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using MoreSkills.UI;
using Pipakin.SkillInjectorMod;
using System.Collections.Generic;
using UnityEngine;

namespace MoreSkills.Config
{
    [BepInPlugin("MoreSkills.StrengthConfig.GuiriGuyMods", "MoreSkills: Strength", "0.0.3")]
    [BepInDependency("com.pipakin.SkillInjectorMod")]
    public class MoreSkills_StrengthConfig : BaseUnityPlugin
    {
        public void Awake()
        {            
            Debug.Log("Loading Strength Skill...");
            //Enablers
            //Strength
            EnableStrengthMod = base.Config.Bind<bool>("Enablers", "Enable Strength Mod", true, "Enables or disables the Strength Modification.");
            //Weight
            EnableWeightMod = base.Config.Bind<bool>("Enablers", "Enable Weight Mod", true, "Enables or disables the Weight Modification.");
            //Stamina
            EnableStrengthStaminaMod = base.Config.Bind<bool>("Enablers", "Enable Weight Mod", true, "Enables or disables the Weight Modification.");
            //Multipliers
            //Strength
            //Skill
            StrengthSkillIncreaseMultiplier = base.Config.Bind<float>("Multipliers", "Strength Skill Increase Multiplier", 1.0f, "Change the skill increase Multiplier, based on the Weight you are carrying 1/400000 when Encumbred, 1/800000 when HalfWeight and Running and 1/1200000 when Halfweight and Moving.");
            //Stamina 
            StrengthStaminaMultiplier = base.Config.Bind<float>("Multipliers", "Strength Stamina Multiplier", 1.0f, "Change the stamina increase Multiplier, that you recieve when not moving Encumbred");
            //Base Configs
            //Strength
            BaseWeight = base.Config.Bind<float>("BaseConfigs", "Base Weight", 300f, "Change the base Weight. (Valheim Default is 300)");
            BaseMaxWeight = base.Config.Bind<float>("BaseConfigs", "Base Max Weight", 600f, "Change the total Weight when Strength is at level 100. (Valheim Default is 300)");

            //Inject.Strength
            if (EnableStrengthMod.Value)
                try
                {
                    SkillInjector.RegisterNewSkill(700, "Strength", "Able to carry more and with higher numbers", 1f, SkillIcons.Load_StrengthIcon(), Skills.SkillType.Unarmed);
                }
                catch
                {
                }

            //--
            Debug.Log("Strength Skill Patched!");
            harmonyStrength = new Harmony("MoreSkills.StrengthConfig.GuiriGuyMods");

            //Logs
            if (!EnableStrengthMod.Value)
                Debug.LogWarning("[MoreSkills] Strength Skill Disabled");
            else
            {
                Debug.Log("[MoreSkills] Strength Skill Enabled");
                if (!EnableWeightMod.Value)
                    Debug.LogWarning("[MoreSkills]Strength/Weight Mod Disabled");
                else
                    Debug.Log("[MoreSkills]Strength/Weight Mod Enabled");
                if (!EnableStrengthStaminaMod.Value)
                    Debug.LogWarning("[MoreSkills]Strength/Stamina Mod Disabled");
                else
                    Debug.Log("[MoreSkills]Strength/Stamina Mod Enabled");
            }


            Debug.Log("Strength Skill Loaded!");
        }
        private void OnDestroy()
        {

            Debug.Log("Strength Skill UnPatched!");
            harmonyStrength.UnpatchSelf();
        }

        // Stats Bases

        public static ConfigEntry<float> BaseWeight;

        public static ConfigEntry<float> BaseMaxWeight;

        private Harmony harmonyStrength;
       
        //Multipliers

        public static ConfigEntry<float> StrengthStaminaMultiplier;

        //Skill Increases Multpliers

        public static ConfigEntry<float> StrengthSkillIncreaseMultiplier;

        //Enables

        public static ConfigEntry<bool> EnableWeightMod;

        public static ConfigEntry<bool> EnableStrengthMod;

        public static ConfigEntry<bool> EnableStrengthStaminaMod;

        //Skills Types

        public const int StrengthSkill_Type = 700;

        

    }
}