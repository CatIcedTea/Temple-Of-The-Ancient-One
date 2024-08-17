using Godot;
using System;

public partial class Event2 : Event
{
	
	public override void _Ready()
    {
    	text = "The doorway ahead of you is blocked by a large pile of rubble. With no obvious way forward, you decided to...";

		box1 = "Start clearing the rubble in front of you";
		box1Stat = Stat.STR;
		box1Target = 9;
		box1FailDamageType = Stat.CURSE;
		box1FailDamage = 4;
		box1Success = "You were able to clear the rubble with ease, allowing you to proceed forward without much time lost.";
		box1Fail = "Clearing the rubble took a long time, allowing more of the curse on you to build up.";

		box2 = "Try to squeeze through the rubble";
		box2Stat = Stat.DEX;
		box2Target = 9;
		box2FailDamage = 4;
		box2FailDamageType = Stat.CURSE;
		box2Success = "With how nimble you are, you are able to successfully squeeze through the rubble, saving you time that would've otherwise been spent clearing the whole pile.";
		box2Fail = "You struggled to squeeze through any openings you created, wasting more time than necessary";

		box3 = "Retrace your steps to find an alternative path";
		box3Stat = Stat.INT;
		box3Target = 9;
		box3FailDamage = 4;
		box3FailDamageType = Stat.CURSE;
		box3Success = "You mapped out the area pretty well, allowing you to easily find a new path to move forward in your search.";
		box3Fail = "Wandering around, you lost track of where you are. It took you quite a long time to find another path forward";
    }
}
