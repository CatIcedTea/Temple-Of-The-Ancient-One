using Godot;
using System;

public partial class Event9 : Event
{
	public override void _Ready()
    {
        text = "The area you've arrived at contains a vast body of water that seems to connect out to the ocean. Then, in the corner of your eyes, you noticed someone. A large man, he seems to be chugging the water through some straw-shaped device. Seeing this, you...";

		box1 = "Try to converse with the man";
		box1Stat = Stat.CON;
		box1Target = 3;
		box1FailDamageType = Stat.HP;
		box1FailDamage = 1;
		box1Success = "You hailed the man, he turned to look back and gives you a friendly smile. He tells you he's from the BadLands, wherever that is, and that he is trying to chug the whole ocean. He insists that you join him. You decided that you would and before the both of you start, the man said, \"This is what you want, so let's get it. Enough talk.\"";
		box1Fail = "You yelled out to the man, and gives you a friendly smile. You learned that he's from a place called the BadLands. He invites you to join him in chugging the ocean, hesistant, you decided to do it since he insists. Before you began, the man said, \"This is what you want, so let's get it. Enough talk.\" You couldn't handle it and start coughing up the water. The man pats you on the back and tells you that you tried your best and that's what matters.";

		box2 = "Leave the man to his chugging";
		box2Stat = Stat.MND;
		box2Target = 10;
		box2FailDamage = 1;
		box2FailDamageType = Stat.SANITY;
		box2Success = "You can't shake the thought of what the man was doing but you try to ignore it for now.";
		box2Fail = "You leave, but you now live in fear that one day the entire ocean will be gone...";
    }
}
