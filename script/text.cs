using Godot;
using System;

public partial class text : Label
{
	Random rand = new Random();
	AudioStreamPlayer beep;

	string [] flavorText = {
		"The air around you smells sickly. It's not enough to make you gag, but it never lets you relax and take a deep breath either.",
		"You hear distant shuffling of footsteps echoing and reverberating throughout the halls. Knowing that someone or something is present yet not knowing how far away it is makes feel uneasy.",
		"You swore you could've heard someone whispering behind you. As you turn back to check, you catch a glimpse at what seems to be a dark figure disappearing into the shadows as quickly as it had appeared.",
		"A low, rumbly growl can be heard just beyond the darkness in front of you. It is not a sound that a human can possibly make, but some kind of large otherworldly being. You decided to turn back where you came from.",
		"In the pitch black darkness, you spot two white dots that seemingly forms a pair of eyes. You stare back, unable to move. It felt like an eternity until the two white dots slink back into the darkness, allowing you to regain your movement.",
		"Your mind starts to wander, you can't help but imagine the worst.",
		"You heard multiple pairs of wet footsteps. Luckily, the sources of those sound seems to be running away from you.",
		"Passing by a large body of flowing water, you noticed an unfathomably giant slow-moving dark shadow just beneath the surface of the water. The thought of whatever it could be makes you shudder.",
		"You feel a cold gaze making the hair on the back of your neck stand.",
		"You stumble across a human skeleton and can't help but think what poor fate must've happened to this fool. And if that same fate will also be upon you.",
		"There are quiet, indistinct whispers inside your head. Though you try to ignore and deny it, you fear that you even aren't alone even in your own mind.",
		"You breathe a heavy sigh, thinking back on what made you even come here in the first place.",
		"Looking down on your hand, you see that some of your veins have black liquids flowing through as a result of your curse affecting your blood."
	};
	public override void _Ready()
	{
		VisibleCharacters = 0;
		beep = GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX").GetNode<AudioStreamPlayer>("dialogue_low");
	}

	public override void _Process(double delta)
	{
		if(VisibleCharacters < Text.Length){
			if(!beep.Playing){
				beep.Play();
			}
			if(VisibleCharacters == Text.Length - 1){
			beep.Stop();
			}
			VisibleCharacters++;
		}
	}

	public void displayFlavorText(){
		VisibleCharacters = 0;
		Text = flavorText[rand.Next(0, flavorText.Length)];
	}

	public void emptyText(){
		Text = "";
	}
}
