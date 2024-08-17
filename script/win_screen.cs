using Godot;
using System;

public partial class win_screen : Node2D
{
	string [] text = {"Walking up to the pedestal, you spot the relic, a golden chalice filled with a deep red liquid.",
	"Though apprehensive, you muster up the will to down all of its content.",
	"Suddenly the world around you seems to spin as you collapse to the floor, blacking out.",
	"...",
	"When you opened your eyes, you are blinded by intense light.",
	"As you eyes readjust, you realize that you are now back outside, just where the entrance to the temple was.",
	"However, the entrance to the temple is completely gone.",
	"Not a single rubble or debris, like it never existed in the first place.",
	"You're not sure what to make of this but you decided it's best not to question it any further.",
	"You look down on your arms, it seems that the curse has been lifted and you breathe a sigh of relief.",
	"With this, you moved on and try to get your life back to normal for once.",
	"However, in the back of your mind, you feel that this won't be the last time the world will see of The Ancient One..."};
	int index = -1;

	Label label;

    public override void _Ready()
    {
        label = GetNode<Label>("Label");
    }

    public override void _Process(double delta)
	{
		if(Visible){
			if(Input.IsActionJustPressed("Click")){
				if(index < text.Length - 1){
					index++;
					if(index == 0){
						GetTree().CurrentScene.GetNode<Panel>("TextBox").GetNode<Label>("Text").Text = "";
						GetNode<Sprite2D>("End1").Visible = true;
					}
					if(index == 1){
						GetNode<Sprite2D>("End1").Visible = false;
						GetNode<Sprite2D>("End2").Visible = true;
					}
					if(index == 3){
						GetNode<Sprite2D>("End2").Visible = false;
					}
					label.VisibleCharacters = 0;
					label.Text = text[index];
				}
				else{
					GetTree().ChangeSceneToFile("res://scene/main_menu.tscn");
				}
			}
		}
	}
}
