using NUnit.Framework;
using System.Linq;
using UnityEngine;

public class AnimationsSwitcher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AnimationClip nextStanding;
    [SerializeField]
    private AnimationClip[] AnimatedSpritesWalk;
    [SerializeField]
    private AnimationClip[] AnimatedSpritesStand;
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSpriteWalkingByDirection(Vector2 direction)
    {
        var sprites = WorldManager.HalfCellMoveVectors.Zip(
            AnimatedSpritesWalk.Zip(AnimatedSpritesStand, (Walk, Stand) => (Walk, Stand)),
            (vector, sprites) => (vector, sprites))
            .OrderBy(x => (x.vector + direction).magnitude)
            .Last()
            .sprites;
        animator.Play(sprites.Walk.name);
        nextStanding = sprites.Stand;
    }

    public void SetSpriteStanding()
    {
        animator.Play(nextStanding.name);
    }
}
