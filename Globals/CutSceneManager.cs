using System.Threading.Tasks;
using Godot;
using Godot.Collections;

public partial class CutSceneManager : Node
{
	public static CutSceneManager Instance { get; private set; }

	CutSceneLoader cutSceneLoader;

	public override void _Ready()
	{
		Instance = this;
	}

	public void ShowCutScene(Array<Texture> CutSceneImage = null, Array<string> CutSceneText = null)
	{
		cutSceneLoader = UiManager.Instance.uiElements["CutSceneLoader"] as CutSceneLoader;
		UiManager.Instance.ChangeCurrentUI(cutSceneLoader);

		_ = LoadText(CutSceneImage, CutSceneText);
	}

	int index = 0;
	private async Task LoadText(Array<Texture> CutSceneImage = null, Array<string> CutSceneText = null)
	{
		// GetTree().Paused = true;

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

		// GetTree().Paused = false;
	}
}
