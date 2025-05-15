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
    private AnimationClip nextStanding;
    [SerializeField]
    private float movingAnimationSpeed;
    [SerializeField]
    private float standingAnimationSpeed;
    [SerializeField]
    private AnimationClip[] AnimatedSpritesWalk;
    [SerializeField]
    private AnimationClip[] AnimatedSpritesStand;
    private Vector2[] eightDirections = new Vector2[]
    {
        new (0.71f, 0.71f),
        new (0.71f, -0.71f),
        new (-0.71f, -0.71f),
        new (-0.71f, 0.71f),
        new (1, 0),
        new (0, -1),
        new (-1, 0),
        new (0, 1)
    };

    void Awake()
    {
        animator = GetComponent<Animator>();
        entity = GetComponent<Entity>();
    }

    public void SetSpriteWalkingByEightDirections(Vector2 moveDirection) =>
        SetSpriteWalkingByDirections(eightDirections, moveDirection);

    public void SetSpriteWalkingByCellDirections(Vector2 moveDirection) =>
        SetSpriteWalkingByDirections(WorldManager.HalfCellMoveVectors, moveDirection);

    private void SetSpriteWalkingByDirections(IEnumerable<Vector2> directions, Vector2 moveDirection)
    {
        var sprites = directions.Zip(
            AnimatedSpritesWalk.Zip(AnimatedSpritesStand, (Walk, Stand) => (Walk, Stand)),
            (vector, sprites) => (vector, sprites))
            .OrderBy(x => (x.vector + moveDirection).magnitude)
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
