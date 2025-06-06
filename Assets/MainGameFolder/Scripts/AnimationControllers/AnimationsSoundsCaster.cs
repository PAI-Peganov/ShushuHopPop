using NUnit.Framework;
using System.Linq;
using UnityEngine;
using EntityBase;
using System.Collections.Generic;

public class AnimationsSoundsCaster : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;
    private Entity entity;
    private string currentClipName;
    private AnimationClip nextStanding;
    private AnimationClip nextAttack;
    [SerializeField] private float movingAnimationSpeed;
    [SerializeField] private float standingAnimationSpeed;
    [SerializeField] private float attackingAnimationSpeed;
    [SerializeField] private AnimationClip[] AnimatedSpritesWalk;
    [SerializeField] private AnimationClip[] AnimatedSpritesStand;
    [SerializeField] private AnimationClip[] AnimatedSpritesAttack;
    [SerializeField] private AnimationClip[] namedAnimations;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;
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
        audioSource = GetComponent<AudioSource>();
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
            AnimatedSpritesWalk.Zip(AnimatedSpritesStand, (Walk, Stand) => (Walk, Stand))
                .Zip(AnimatedSpritesAttack, (sprites, Attack) => (sprites.Walk, sprites.Stand, Attack)),
            (vector, sprites) => (vector, sprites))
            .OrderBy(x => (x.vector + moveDirection).magnitude)
            .Last()
            .sprites;
        if (hard || currentClipName != sprites.Walk.name)
        {
            currentClipName = sprites.Walk.name;
            animator.Play(sprites.Walk.name);
            nextStanding = sprites.Stand;
            nextAttack = sprites.Attack;
        }
        animator.speed = movingAnimationSpeed * entity.MoveSpeed;
    }

    public void SetAnimationSpeed(float speed)
    {
        animator.speed = speed;
    }

    public void SetSpriteStanding()
    {
        currentClipName = nextStanding.name;
        animator.Play(nextStanding.name);
        animator.speed = standingAnimationSpeed;
    }

    public void SetSpriteAttack()
    {
        currentClipName = nextAttack.name;
        animator.Play(nextAttack.name);
        animator.speed = attackingAnimationSpeed;
    }

    public void OpenEyes()
    {

    }

    public void CloseEyes()
    {

    }

    public void FlashEyesDuration(float duration)
    {

    }

    public void PlayAnimationByName(string name, float speed=1f)
    {
        currentClipName = name;
        animator.Play(name);
        animator.speed = speed;
    }

    public void PlaySoundByName(string audioName)
    {
        var audioClip = clips.FirstOrDefault(x => x.name == audioName);
        if (audioClip != null)
            audioSource.PlayOneShot(audioClip);
    }
}
