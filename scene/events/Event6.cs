using Godot;
using System;

public partial class Event6 : Event
{
	public override void _Ready()
    {
        text = "A heavy mist suddenly sets in and forms, enveloping all around you. The thickness of the mist obscures your vision, not allowing you to see even an arms length in front of you. Not quite sure what to do, you decide to...";

		box1 = "Carefully navigate through the mist";
		box1Stat = Stat.MND;
		box1Target = 7;
		box1FailDamageType = Stat.CURSE;
		box1FailDamage = 2;
		box1Success = "Using your vision to the best of your abilities, you manage to make your way out of the damp mist. The cause of the mist inside here perplexes you.";
		box1Fail = "In an attempt to move through the mist, you've gotten lost.";

		box2 = "Wait out the mist";
		box2Stat = Stat.INT;
		box2Target = 8;
		box2FailDamage = 2;
		box2FailDamageType = Stat.SANITY;
		box2Success = "Waiting in the mist, you start hearing strange whispers coming from all around you. You rationalize that it is just a trick.";
		box2Fail = "As you are waiting for the mist to clear, you start hearing strange voices coming from all directions. You're not sure what the voices are saying, but it feelss that they're compelling you to do something unspeakable.";
    }
}
