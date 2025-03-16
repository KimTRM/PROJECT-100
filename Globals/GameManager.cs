using System.Threading.Tasks;
using Godot;
using Godot.Collections;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; }

	[Export] public Vector2[] currentTilemapBounds;
	[Signal] public delegate void TileMapBoundsChangedEventHandler(Vector2[] bounds);

	[Signal] public delegate void GamePauseToggleEventHandler(bool isPaused);
	public bool isPaused = false;

	[Export] public string PlayerUsername;
	[Export] public string PlayerID;

	public override void _Ready()
	{
		Instance = this;
		ProcessMode = ProcessModeEnum.Always;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed)
		{
			if (keyEvent.Keycode != Key.Escape) return;

			isPaused = !isPaused;
			EmitSignal(SignalName.GamePauseToggle, isPaused);

			GetTree().Paused = isPaused;
		}
	}

	public void ChangeTilemapBounds(Vector2[] bounds)
	{
		currentTilemapBounds = bounds;
		EmitSignal("TileMapBoundsChanged", bounds);
	}

	public async void LoadScene(string scenePath)
	{
		SceneTransition sceneTransition = GetNode<SceneTransition>("/root/SceneTransition");

		sceneTransition.Show();
		GetTree().Paused = true;

		await sceneTransition.FadeOut();

		GetTree().ChangeSceneToFile(scenePath);

		await sceneTransition.FadeIn();
		GetTree().Paused = false;

		sceneTransition.Hide();
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
