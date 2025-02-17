using Godot;
using Godot.Collections;
using System;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; }

	[Export] public Vector2[] currentTilemapBounds;
	[Signal] public delegate void TileMapBoundsChangedEventHandler(Vector2[] bounds);

	public override void _Ready()
	{
		Instance = this;
	}
	
	public void ChangeTilemapBounds(Vector2[] bounds)
	{
		currentTilemapBounds = bounds;
		EmitSignal("TileMapBoundsChanged", bounds);
	}

	public void SaveScore(string scoreID, string playerID, string chapterID, string score)
	{
		var data = new Dictionary
		{
			{ "ScoreID", scoreID },
			{ "PlayerID", playerID },
			{ "ChapterID", chapterID },
			{ "Score", score },
		};
		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["ADD_SCORE"], data);
	}

	public void SavePlayerData(string playerID, Vector2 playerSavePoint, string chapterNumber, string lessonNumber)
	{
		var data = new Dictionary
		{
			{ "PlayerID", playerID },
			{ "PlayerSavePoint", playerSavePoint },
			{ "ChapterNumber", chapterNumber },
			{ "LessonNumber", lessonNumber },
		};
		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["ADD_PLAYER_DATA"], data);
	}
}
