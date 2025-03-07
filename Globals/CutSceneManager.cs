using System.Threading.Tasks;
using Godot;
using Godot.Collections;

public partial class CutSceneManager : Node
{
	[Export]
	public Texture cutScenePath;
	[Export]
	public string cutSceneText;
	CutSceneLoader cutSceneLoader;

	[Export] Array<Texture> CutSceneImage = new();
	[Export] Array<string> CutSceneText = new();

	public override void _Ready()
	{
		cutSceneLoader = (CutSceneLoader)ResourceLoader.Load<PackedScene>("res://Scenes/UI/CutSceneLoader/CutSceneLoader.tscn").Instantiate();
		AddChild(cutSceneLoader);

		cutSceneLoader.ChangeTexture(cutScenePath.ResourcePath);
		// cutSceneLoader.DisplayText(cutSceneText);
		_ = LoadText();

		// cutSceneLoader.Connect("FinishedDisplaying", Callable.From(() =>
		// {
		// 	cutSceneLoader.Hide();
		// }));

	}

	int index = 0;
	private async Task LoadText()
	{

		foreach (string dialougeText in CutSceneText)
		{


			cutSceneLoader.DisplayText(dialougeText);


			cutSceneLoader.ChangeTexture(CutSceneImage[index].ResourcePath);
			index++;

			await cutSceneLoader.ToSignal(cutSceneLoader, "FinishedDisplaying");

			if (index == CutSceneText.Count)
			{
				cutSceneLoader.Hide();
			}
		}
	}
}
