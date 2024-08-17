using Godot;
using System;

public partial class Event5 : Event
{
    public override void _Ready()
    {
        text = "As you continued on your path, you spot a robed cultist in front of you. Noticing you, the cultist immediately starts sprinting away and you...";

		box1 = "Quickly Chase after them";
		box1Stat = Stat.DEX;
		box1Target = 9;
		box1FailDamageType = Stat.HP;
		box1FailDamage = 2;
		box1Success = "You manage to catch up right behind the cultist and just as you reach out and pulled on their robe, they instantly disintegrated into a black puddle of liquid as if they were never there.";
		box1Fail = "You used all your stamina but couldn't catch up with the cultist and lost sight of them, leaving you completely exhausted.";

		box2 = "Use your environment to outmanuever them";
		box2Stat = Stat.MND;
		box2Target = 7;
		box2FailDamage = 2;
		box2FailDamageType = Stat.CURSE;
		box2Success = "Utilizing all the different winding halls and paths ahead, you manage to get in front of the cultist and tackle them onto the ground. However, as you got up, nothing remains of the cultist aside from the robe and a puddle of black liquid.";
		box2Fail = "In your attempt to use all the different paths, you lost the cultist and got yourself lost.";

		box3 = "Let the cultist get away";
		box3Stat = Stat.INT;
		box3Target = 7;
		box3FailDamage = 2;
		box3FailDamageType = Stat.SANITY;
		box3Success = "Looking at the cultist, you recognized that they were just of a low rank. One that is not worth the bother to chase.";
		box3Fail = "Letting the cultist get away, you can't help but feel uneasy. Who exactly were they? What will they do now that they know where you are?";
    }

}
