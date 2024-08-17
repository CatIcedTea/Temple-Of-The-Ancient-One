using Godot;
using System;

public partial class Event8 : Event
{
	public override void _Ready()
    {
        text = "Suddenly, you hear a familiar, comforting voice calling from behind you. Though you can't quite tell whose it is, perhaps a friend, maybe your partner. Upon hearing this voice, you...";

		box1 = "Take a look back";
		box1Stat = Stat.CON;
		box1Target = 7;
		box1FailDamageType = Stat.HP;
		box1FailDamage = 3;
		box1Success = "The moment you turn to look back, you are suddenly pulled and dragged back with terrifying speed before you are slammed into the ground, luckily you weren't too hurt.";
		box1Fail = "All of a sudden, you are pulled and dragged back at an inhuman speed, then you are slammed into the ground, leaving you injured.";

		box2 = "Keep going forward and don't look back";
		box2Stat = Stat.MND;
		box2Target = 7;
		box2FailDamage = 2;
		box2FailDamageType = Stat.CURSE;
		box2Success = "You kept moving, using all your mental fortitude to not turn back, despite the cries of the voice behind you, until it finally stopped.";
		box2Fail = "You trudged forward, the familiar voice cries were horrific, calling for your help and to come back. It finally stopped but it left you in sheer terror.";
    }

}
