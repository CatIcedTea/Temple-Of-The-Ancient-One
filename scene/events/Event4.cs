using Godot;
using System;

public partial class Event4 : Event
{
	public override void _Ready()
    {
    	text = "You passed by a strange looking mural. It's content draws you in and you can't help but keep staring at it. Entranced and unable to take your eyes off, you...";

		box1 = "Keep looking at the mural";
		box1Stat = Stat.STR;
		box1Target = 10;
		box1FailDamageType = Stat.CURSE;
		box1FailDamage = 4;
		box1Success = "None of it made sense to you. In a fit of rage you destroy the mural";
		box1Fail = "You kept on staring at the mural, eventually losing track of time.";

		box2 = "Try to make sense of it all";
		box2Stat = Stat.INT;
		box2Target = 7;
		box2FailDamage = 2;
		box2FailDamageType = Stat.SANITY;
		box2Success = "The mural seems to depict a prophecy regarding The Ancient One and how it will one day bring about the end of the world and also the eventual rebirth of the world. Knowing this, you continued on, satisfied with your curiosity and knowledge of what you're currently facing.";
		box2Fail = "In an attempt to make sense of the mural, you have gone mad knowing the world has been doomed since its birth.";

		box3 = "Use all your will to try to pry yourself away";
		box3Stat = Stat.MND;
		box3Target = 5;
		box3FailDamage = 2;
		box3FailDamageType = Stat.SANITY;
		box3Success = "You quickly turn around and kept moving on your way, trying not to think of what was back there.";
		box3Fail = "You jerk your body in an attempt to get yourself away from the mural, collapsing on the floor. Although you've gotten away, whatever was on the the mural continues to haunt you.";

		box4 = "Attempt to use pain to wake yourself";
		box4Stat = Stat.CON;
		box4Target = 7;
		box4FailDamage = 2;
		box4FailDamageType = Stat.HP;
		box4Success = "Blood starts dripping as you dig your nails into your skin. You managed to wake yourself up from the trance, although bleeding but you've been through worse.";
		box4Fail = "You start digging your nails into your skin, the pain is immense but you at least you were able to regain your consciousness.";
    }
}
