using NUnit.Framework;
using System.Linq;
using UnityEngine;
using EntityBase;

public class AnimationsSwitcher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;
    private Entity entity;
    private AnimationClip nextStanding;
    [SerializeField]
    private float movingAnimationSpeed;
    [SerializeField]
    private float standingAnimationSpeed;
    [SerializeField]
    private AnimationClip[] AnimatedSpritesWalk;
    [SerializeField]
    private AnimationClip[] AnimatedSpritesStand;
    void Awake()
    {
        animator = GetComponent<Animator>();
        entity = GetComponent<Entity>();
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
        animator.speed = movingAnimationSpeed * entity.MoveSpeed;
        nextStanding = sprites.Stand;
    }

    public void SetSpriteStanding()
    {
        animator.Play(nextStanding.name);
        animator.speed = standingAnimationSpeed;
    }
}
