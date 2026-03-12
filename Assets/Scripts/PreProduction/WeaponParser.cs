using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public static class WeaponParser
{
    [System.Serializable]
    public class WeaponList
    {
        public List<WeaponData> datas;
    }

    [System.Serializable]
    public class WeaponData
    {
        public string name;
        public string weaponType;
        public string weaponCategory;
        public string weaponGroup;
        public string weaponTraits;
        public string damage;
        public string hands;
        public string range;
        public string reload;
        public string bulk;
        public string price;
        public string level;
    }

    [MenuItem("Pre Production/Generate/Weapons")]
    public static void GenerateAll()
    {
        if (!AssetDatabase.IsValidFolder("Assets/AutoGeneration"))
            AssetDatabase.CreateFolder("Assets", "AutoGeneration");
        if (!AssetDatabase.IsValidFolder("Assets/AutoGeneration/Weapons"))
            AssetDatabase.CreateFolder("Assets/AutoGeneration", "Weapons");

        string filePath = "Assets/Docs/Weapons.json";
        TextAsset asset = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);
        var result = JsonUtility.FromJson<WeaponList>(asset.text);
        foreach (var data in result.datas)
        {
            GenerateAsset(data);
        }
    }

    static void GenerateAsset(WeaponData data)
    {
        var asset = new GameObject(data.name);
        AddType(asset, data);
        AddCategory(asset, data);
        AddGroup(asset, data);
        AddTraits(asset, data);
        AddDamage(asset, data);
        AddHands(asset, data);
        AddRange(asset, data);
        AddReload(asset, data);
        AddBulk(asset, data);
        AddPrice(asset, data);
        AddLevel(asset, data);
        CreatePrefab(asset, data);
        GameObject.DestroyImmediate(asset);
    }

    static void AddType(GameObject asset, WeaponData data)
    {
        if (string.IsNullOrEmpty(data.weaponType))
            return;
        WeaponType weaponType;
        if (Enum.TryParse<WeaponType>(data.weaponType, out weaponType))
        {
            var provider = asset.AddComponent<WeaponTypeProvider>();
            provider.value = weaponType;
        }
    }

    static void AddCategory(GameObject asset, WeaponData data)
    {
        if (string.IsNullOrEmpty(data.weaponCategory))
            return;
        WeaponCategory weaponCategory;
        if (Enum.TryParse<WeaponCategory>(data.weaponCategory, out weaponCategory))
        {
            var provider = asset.AddComponent<WeaponCategoryProvider>();
            provider.value = weaponCategory;
        }
    }

    static void AddGroup(GameObject asset, WeaponData data)
    {
        if (string.IsNullOrEmpty(data.weaponGroup))
            return;
        WeaponGroup weaponGroup;
        if (Enum.TryParse<WeaponGroup>(data.weaponGroup, out weaponGroup))
        {
            var provider = asset.AddComponent<WeaponGroupProvider>();
            provider.value = weaponGroup;
        }
    }

    static void AddTraits(GameObject asset, WeaponData data)
    {
        // TODO
    }

    static void AddDamage(GameObject asset, WeaponData data)
    {
        if (string.IsNullOrEmpty(data.damage))
            return;

        var components = data.damage.Split(" ");
        if (components.Length != 2)
            return;

        AddDamageDiceRoll(asset, components[0]);
        AddDamageType(asset, components[1]);
    }

    static void AddDamageDiceRoll(GameObject asset, string data)
    {
        var components = data.Split("d");
        if (components.Length != 2)
            return;

        int count, sides;
        if (int.TryParse(components[0], out count) && int.TryParse(components[1], out sides))
        {
            var provider = asset.AddComponent<DamageRollProvider>();
            provider.value = new DiceRoll(count, sides);
        }
    }

    static void AddDamageType(GameObject asset, string data)
    {
        // TODO
    }

    static void AddHands(GameObject asset, WeaponData data)
    {
        // TODO
    }

    static void AddRange(GameObject asset, WeaponData data)
    {
        // TODO
    }

    static void AddReload(GameObject asset, WeaponData data)
    {
        // TODO
    }

    static void AddBulk(GameObject asset, WeaponData data)
    {
        // TODO
    }

    static void AddPrice(GameObject asset, WeaponData data)
    {
        // TODO
    }

    static void AddLevel(GameObject asset, WeaponData data)
    {
        if (string.IsNullOrEmpty(data.level))
            return;

        int level;
        if (int.TryParse(data.level, out level))
        {
            var provider = asset.AddComponent<LevelProvider>();
            provider.value = level;
        }
    }

    static void CreatePrefab(GameObject asset, WeaponData data)
    {
        string path = string.Format("Assets/AutoGeneration/Weapons/{0}.prefab", data.name);
        PrefabUtility.SaveAsPrefabAsset(asset, path);
    }
}