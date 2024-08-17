using Godot;
using System;

public partial class Event3 : Event
{
	public override void _Ready()
    {
    	text = "Your search has ran into a dead end. However, there appears to be an underwater tunnel that looks like it goes underground that might lead to a new area. Only problem is, you don't know how long you will be submerged in water or where exactly it might lead. You take a moment to think of the risk and...";

		box1 = "Quickly dive into the water and start swimming";
		box1Stat = Stat.DEX;
		box1Target = 8;
		box1FailDamageType = Stat.HP;
		box1FailDamage = 2;
		box1Success = "With how quick you are, you safely emerged on the other side of the tunnel. You take a quick break to dry yourself off as you prepare yourself to continue.";
		box1Fail = "In your attempt to be quick, you expended more energy than needed, causing you to lose your breath. Luckily you were close to the end as you emerged gasping for air and violently coughing up the water in your lungs.";

		box2 = "Rely on your breath and slowly swim through the tunnel";
		box2Stat = Stat.CON;
		box2Target = 8;
		box2FailDamage = 2;
		box2FailDamageType = Stat.HP;
		box2Success = "As you are beginning to run out of air, the surface appears before you. Getting yourself up on ground again, you take a moment to catch your breath and dry off.";
		box2Fail = "Halfway through the tunnel, you began to lose air and consciousness. Just as you were about to drown, you somehow managed to emerge on the other side, taking the best breath of air you have ever took immediately followed by coughing out the water in your lungs.";
    }
}
