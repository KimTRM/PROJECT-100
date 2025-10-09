using Godot;
using Godot.Collections;

public static class GameConstants
{
	public static Dictionary<BlockType, string> BlockScenePath = new()
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
