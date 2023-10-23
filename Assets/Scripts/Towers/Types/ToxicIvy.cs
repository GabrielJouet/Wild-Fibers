using System.Collections;
using System.Collections.Generic;
using Enemies.Enemy_Types;
using UnityEngine;

/// <summary>
/// Class used by toxic-like towers for behavior modification purposes.
/// </summary>
public class ToxicIvy : Tower
{
    /// <summary>
    /// All leaf projectiles available.
    /// </summary>
    protected Stack<ToxicLeaf> _availableLeaves = new Stack<ToxicLeaf>();


    /// <summary>
    /// Number of shots before a bigger projectile is sent.
    /// </summary>
    private int _shotsBeforeBiggerProjectile = 8;



    /// <summary>
    /// Level two augmentation special behavior.
    /// </summary>
    protected override void LevelTwoAugmentation()
    {
        DotDuration += 0.5f;
    }


    /// <summary>
    /// Level three augmentation special behavior.
    /// </summary>
    protected override void LevelThreeAugmentation()
    {
        Dot++;
    }


    /// <summary>
    /// Level four augmentation special behavior.
    /// </summary>
    protected override void LevelFourAugmentation()
    {
        ArmorThroughMalus *= 1.15f;
    }



    /// <summary>
    /// Method called before an attack occurs to apply modifications on the current attack.
    /// </summary>
    /// <param name="enemy">The enemy targeted</param>
    /// <returns>The new attack with new parameters</returns>
    /// <remarks>This method is only valable when the augmentation level is higher than 0</remarks>
    protected override Attack ChangeNextAttack(Enemy enemy)
    {
        Attack newAttack = new Attack(_attack);

        if (AugmentationLevel > 0)
        {
            if (_shotsBeforeBiggerProjectile > 0)
                _shotsBeforeBiggerProjectile--;
            else
            {
                newAttack.DotDamage = Mathf.FloorToInt(newAttack.DotDamage * 1.5f);
                newAttack.ArmorThroughMalus *= 1.75f;

                _shotsBeforeBiggerProjectile = 8;
            }
        }

        return newAttack;
    }


    /// <summary>
    /// Special behavior method used to improve tower method Initialize.
    /// </summary>
    protected override void SpecialBehavior()
    {
        for (int i = 0; i < Shots; i++)
            StartCoroutine(SummonLeaf(0.25f));
    }


    /// <summary>
    /// FixedUpdate, called 50 times a second.
    /// </summary>
    protected override void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && _availableLeaves.Count > 0)
        {
            List<Enemy> enemies = RecoverAvailableEnemies(_availableLeaves.Count);

            for (int i = 0; i < enemies.Count; i++)
            {
                StartCoroutine(SummonLeaf(1));

                _availableLeaves.Pop().StartFollowing(enemies[i], _nextAttack.Dequeue());
            }
        }
    }


    /// <summary>
    /// Upgrade special behavior method used to improve tower method Upgrade.
    /// </summary>
    protected override void UpgradeSpecialBehavior()
    {
        StopAllCoroutines();

        foreach (ToxicLeaf current in _availableLeaves)
            current.StopProjectile();
    }


    /// <summary>
    /// Method called when the tower is resell.
    /// </summary>
    protected override void ResellSpecialBehavior()
    {
        foreach (ToxicLeaf current in _availableLeaves)
            current.StopProjectile();
    }


    /// <summary>
    /// Method used to summon projectile.
    /// </summary>
    /// <param name="multiplier">Coroutine execution time multiplier</param>
    protected virtual IEnumerator SummonLeaf(float multiplier)
    {
        yield return new WaitForSeconds(TimeShots * multiplier);

        ToxicLeaf buffer = Instantiate(Projectile).GetComponent<ToxicLeaf>();
        buffer.Initialize(transform.localPosition, AugmentationLevel > 3);

        _availableLeaves.Push(buffer);
    }
}