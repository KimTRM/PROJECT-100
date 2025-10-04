using Godot;
using GodotUtilities;
using System.Threading.Tasks;

[Scene]
public partial class VariableBlock : CodeBlock
{
	[Export] public string varNameStr;

	[Node] private TemplateEditor templateEditor;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
		base._Ready();

		varNameStr = templateEditor.TemplateName;
	}

	public override async Task Execute()
	{
		var block = dropAreaComponent.DroppedBlock;
		if (dropAreaComponent.HasBlock())
		{
			await block.Execute();
			// SetBlockValue(block.Value2);
		}
		else
		{
			SetBlockValue(varNameStr);
		}

		await Task.CompletedTask;
	}
}
