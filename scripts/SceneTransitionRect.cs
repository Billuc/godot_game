using Godot;
using System;
using System.Threading.Tasks;

public partial class SceneTransitionRect : ColorRect
{
	// Reference to the AnimationPlayer node
	private AnimationPlayer _animPlayer;

	public override void _Ready()
	{
		_animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animPlayer.PlayBackwards("fade");
	}

	public async Task TransitionTo(string nextScene)
	{
		string sceneToLoad = nextScene;
		_animPlayer.Play("fade");
		await ToSignal(_animPlayer, "animation_finished");
		GetTree().ChangeSceneToFile(sceneToLoad);
	}
}
