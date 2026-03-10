using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public static class BackgroundParser
{
    [System.Serializable]
    public class BackgroundList
    {
        public List<BackgroundData> datas;
    }

    [System.Serializable]
    public class BackgroundData
    {
        public string title;
        public List<string> abilities;
        public List<string> skills;
        public List<string> feats;
        public string rarity;
        public string summary;
    }

    [MenuItem("Pre Production/Generate/Backgrounds")]
    public static void GenerateAll()
    {
        if (!AssetDatabase.IsValidFolder("Assets/AutoGeneration"))
            AssetDatabase.CreateFolder("Assets", "AutoGeneration");
        if (!AssetDatabase.IsValidFolder("Assets/AutoGeneration/Backgrounds"))
            AssetDatabase.CreateFolder("Assets/AutoGeneration", "Backgrounds");

        string filePath = "Assets/Docs/Backgrounds.json";
        TextAsset asset = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);
        var result = JsonUtility.FromJson<BackgroundList>(asset.text);
        foreach (var data in result.datas)
        {
            GenerateAsset(data);
        }
    }

    static void GenerateAsset(BackgroundData data)
    {
        var asset = new GameObject(data.title);
        AddBackground(asset, data);
        AddBoosts(asset, data);
        AddSkills(asset, data);
        CreatePrefab(asset, data);
        GameObject.DestroyImmediate(asset);
    }

    static void AddBackground(GameObject asset, BackgroundData data)
    {
        var bg = asset.AddComponent<Background>();
        bg.Title = data.title;
        bg.Rarity = (Rarity)Enum.Parse(typeof(Rarity), data.rarity);
        bg.Summary = data.summary;
    }

    static void AddBoosts(GameObject asset, BackgroundData data)
    {
        var boosts = new List<AbilityBoost>();
        foreach (var ability in data.abilities)
        {
            if (string.IsNullOrEmpty(ability))
                continue;

            AbilityBoost boost;
            if (Enum.TryParse<AbilityBoost>(ability, out boost))
            {
                boosts.Add(boost);
            }
            else
            {
                Debug.Log("Unhandled boost: " + boost);
            }
        }
        var boostProvider = asset.AddComponent<AbilityBoostProvider>();
        boostProvider.boosts = boosts;
    }

    static void AddSkills(GameObject asset, BackgroundData data)
    {
        var valuePairs = new List<SkillsProficiencyProvider.ValuePair>();
        foreach (var name in data.skills)
        {
            if (string.IsNullOrEmpty(name))
                continue;

            Skill skill;
            if (Enum.TryParse<Skill>(name, out skill))
            {
                var pair = new SkillsProficiencyProvider.ValuePair();
                pair.skill = skill;
                pair.proficiency = Proficiency.Trained;
                valuePairs.Add(pair);
            }
            else
            {
                Debug.LogWarning("Unhandled skill: " + name);
            }
        }
        var provider = asset.AddComponent<SkillsProficiencyProvider>();
        provider.valuePairs = valuePairs;
    }

    static void CreatePrefab(GameObject asset, BackgroundData data)
    {
        string path = string.Format("Assets/AutoGeneration/Backgrounds/{0}.prefab", data.title);
        PrefabUtility.SaveAsPrefabAsset(asset, path);
    }
}