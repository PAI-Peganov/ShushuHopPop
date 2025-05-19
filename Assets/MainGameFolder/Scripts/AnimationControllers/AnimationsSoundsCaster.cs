using NUnit.Framework;
using System.Linq;
using UnityEngine;
using EntityBase;
using System.Collections.Generic;

public class AnimationsSwitcher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;
    private Entity entity;
    private AnimationClip currentClip;
    private AnimationClip nextStanding;
    [SerializeField]
    private float movingAnimationSpeed;
    [SerializeField]
    private float standingAnimationSpeed;
    [SerializeField]
    private AnimationClip[] AnimatedSpritesWalk;
    [SerializeField]
    private AnimationClip[] AnimatedSpritesStand;
    private readonly Vector2[] eightDirections = new Vector2[]
    {
        new Vector2(0.71f, 0.71f).normalized,
        new Vector2(0.71f, -0.71f).normalized,
        new Vector2(-0.71f, -0.71f).normalized,
        new Vector2(-0.71f, 0.71f).normalized,
        new (1, 0),
        new (0, -1),
        new (-1, 0),
        new (0, 1)
    };

    void Awake()
    {
        animator = GetComponent<Animator>();
        entity = GetComponent<Entity>();
        SetSpriteWalkingByEightDirections(new Vector2(0, 1), true);
    }

    public void SetSpriteWalkingByEightDirections(Vector2 moveDirection, bool hard=false) =>
        SetSpriteWalkingByDirections(eightDirections, moveDirection, hard);

    public void SetSpriteWalkingByCellDirections(Vector2 moveDirection, bool hard=false) =>
        SetSpriteWalkingByDirections(WorldManager.HalfCellMoveVectors, moveDirection, hard);

    private void SetSpriteWalkingByDirections(IEnumerable<Vector2> directions, Vector2 moveDirection, bool hard)
    {
        var sprites = directions.Zip(
            AnimatedSpritesWalk.Zip(AnimatedSpritesStand, (Walk, Stand) => (Walk, Stand)),
            (vector, sprites) => (vector, sprites))
            .OrderBy(x => (x.vector + moveDirection).magnitude)
            .Last()
            .sprites;
        if (hard || currentClip.name != sprites.Walk.name)
        {
            currentClip = sprites.Walk;
            animator.Play(sprites.Walk.name);
            animator.speed = movingAnimationSpeed * entity.MoveSpeed;
            nextStanding = sprites.Stand;
        }
    }

    public void SetSpriteStanding()
    {
        currentClip = nextStanding;
        animator.Play(nextStanding.name);
        animator.speed = standingAnimationSpeed;
    }
}
