using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public struct MonsterSpawn
{
    public string assetName;
    public Point position;
}

public interface IEncounter
{
    string VictoryEntry { get; }
    string DefeatEntry { get; }
    List<MonsterSpawn> MonsterSpawns { get; }
    List<Point> HeroPositions { get; }
}

public class Encounter : MonoBehaviour, IEncounter
{
    public string VictoryEntry { get { return victoryEntry; } }
    [SerializeField] string victoryEntry;

    public string DefeatEntry { get { return defeatEntry; } }
    [SerializeField] string defeatEntry;

    public List<MonsterSpawn> MonsterSpawns { get { return monsterSpawns; } }
    [SerializeField] List<MonsterSpawn> monsterSpawns;

    public List<Point> HeroPositions { get { return heroPositions; } }
    [SerializeField] List<Point> heroPositions;
}