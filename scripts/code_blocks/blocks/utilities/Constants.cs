using Godot;
using Godot.Collections;

public partial class Constants : Node
{
	public static Dictionary<BlockType, string> Scene = new()
	{
		{ BlockType.NONE, "" },
		{ BlockType.ENTRY, "" },
		{ BlockType.STATEMENT, "uid://dbkjr5boxysai" },
		{ BlockType.VARIABLE, "uid://drcorwafubvtf" },
		{ BlockType.CONTROL, "uid://bm8qnm1lt3fgb" },
	};
}

public enum BlockType
{
	NONE,
	ENTRY,
	STATEMENT,
	VARIABLE,
	CONTROL,
}
