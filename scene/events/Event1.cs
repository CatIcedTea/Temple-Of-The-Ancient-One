using Godot;
using System;

public partial class Event1 : Event
{
    public override void _Ready()
    {
    	text = "As you step into a small room, you noticed that in the center of the room there is a large basin holding crystal clear water. Suddenly, you are overcome with an extreme sense of thirst, the likes you've never felt before. Feeling this, you...";

		box1 = "Start drinking the water in the basin";
		box1Stat = Stat.CON;
		box1Target = 8;
		box1FailDamageType = Stat.HP;
		box1FailDamage = 2;
		box1Success = "The water tastes rancid, however you are able to stomach it and the feeling of thirst fades away.";
		box1Fail = "The water tastes most foul. You recoil in disgust as you vomit out what seems to be water as black as tar into the basin, turning the rest of the water just like it.";

		box2 = "Resist the urge to drink and leave";
		box2Stat = Stat.MND;
		box2Target = 8;
		box2FailDamage = 2;
		box2FailDamageType = Stat.SANITY;
		box2Success = "You are able to take your mind off of the feeling of overwhelming thirst that enveloped you as it slowly fades away";
		box2Fail = "You decided to leave. Though the feeling of that accursed thirst has started to fade, it never fully left and it slowly starts eating away at your mind.";
    }
}
