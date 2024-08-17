using Godot;
using System;

public partial class Event : Node
{
	//Stat to check for an event encounter
	public enum Stat{
		STR,
		CON,
		DEX,
		INT,
		MND,
		HP,
		SANITY,
		CURSE,
		NONE
	}

	//Text for event
	public string text = null;

	//Text for each box
	public string box1 = null;
	public string box2 = null;
	public string box3 = null;
	public string box4 = null;

	//Text for successful roll
	public string box1Success = null;
	public string box2Success = null;
	public string box3Success = null;
	public string box4Success = null;

	//Text for failed roll
	public string box1Fail = null;
	public string box2Fail = null;
	public string box3Fail = null;
	public string box4Fail = null;

	//Skillcheck stat type for each box
	public Stat box1Stat = Stat.NONE;
	public Stat box2Stat = Stat.NONE;
	public Stat box3Stat = Stat.NONE;
	public Stat box4Stat = Stat.NONE;

	//Skillcheck target number for each box
	public int box1Target;
	public int box2Target;
	public int box3Target;
	public int box4Target;

	//Fail damage type (HP, Sanity, or Curse)
	public Stat box1FailDamageType;
	public Stat box2FailDamageType;
	public Stat box3FailDamageType;
	public Stat box4FailDamageType;

	//Fail damage
	public int box1FailDamage;
	public int box2FailDamage;
	public int box3FailDamage;
	public int box4FailDamage;
}
