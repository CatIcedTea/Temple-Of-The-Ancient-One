using Godot;
using System;

public partial class Event7 : Event
{
	public override void _Ready()
    {
        text = "Looking down, you see a mindless wretched abomination in front of you, once a person but has completely overtaken by the same curse you now hold. It's lower half practically gone, crushed under a pile of debris. Seeing this, you decided to...";

		box1 = "Bash its head in, putting it out of its misery";
		box1Stat = Stat.STR;
		box1Target = 7;
		box1FailDamageType = Stat.SANITY;
		box1FailDamage = 1;
		box1Success = "In one swift, powerful blow, the abomination is no more.";
		box1Fail = "It took you quite a lot of swings to finally put down the abomination. You're horrified by what you've done and the mess you've created.";

		box2 = "Use a precise incision to quickly end it";
		box2Stat = Stat.DEX;
		box2Target = 7;
		box2FailDamage = 1;
		box2FailDamageType = Stat.SANITY;
		box2Success = "You skillfully make the incision, the abomination then ceases to be.";
		box2Fail = "You missed your incision, causing a massive amount of black liquid to spill out until the abomination stopped moving.";

		box3 = "Study the abomination";
		box3Stat = Stat.INT;
		box3Target = 7;
		box3FailDamage = 2;
		box3FailDamageType = Stat.SANITY;
		box3Success = "Studying the abomination, you learn of many things that would help you in your time here, including what will happen to you should you fail.";
		box3Fail = "You failed to learn anything from examining the abomination, wasting what precious time you have left.";

		box4 = "Leave it to its suffering";
		box4Stat = Stat.MND;
		box4Target = 6;
		box4FailDamage = 2;
		box4FailDamageType = Stat.SANITY;
		box4Success = "You decided to leave the abomination, knowing that it wouldn't even know or feel anything.";
		box4Fail = "Deciding to leave it, you hear its suffering voice echo behind you burning into your head. The thought of you becoming one of those fills you with dread.";
    }
}
