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
    BoardData BoardData { get; }
    BoardSkin BoardSkin { get; }
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

    public BoardData BoardData { get { return boardData; } }
    [SerializeField] BoardData boardData;

    public BoardSkin BoardSkin { get { return boardSkin; } }
    [SerializeField] BoardSkin boardSkin;
}