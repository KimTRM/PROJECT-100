using Godot;
using GodotUtilities;
using System.Threading.Tasks;

[Scene]
public partial class ExpressionBlock : CodeBlock
{
	[Node ("MarginContainer/HBoxContainer/ValueContainer1/Value1")]
	private LineEdit value1;
	[Node ("MarginContainer/HBoxContainer/ValueContainer2/Value2")]
	private LineEdit value2;

	public string Value1;
	public string Value2;

	public override void _Notification(int what)
    {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }

    public override async Task Execute()
    {
		Value1 = value1.Text;
		Value2 = value2.Text;

		await Task.CompletedTask;
    }
}
