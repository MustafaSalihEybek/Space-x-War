using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float bulletSpeed;
    [HideInInspector] public bool isBulletPlayer;
    [HideInInspector] public int bulletAttack;

    private Rigidbody2D bulletRigid;
    private AudioSource bulletSoundEffect;
    private string soundEffectIsOn;

    private void Start()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
        bulletSoundEffect = GetComponent<AudioSource>();
        soundEffectIsOn = PlayerPrefs.GetString("SoundEffectIsOn", "true");
        if (soundEffectIsOn == "false")
        {
            bulletSoundEffect.playOnAwake = false;
            bulletSoundEffect.Stop();
        }
        if (isBulletPlayer) bulletRigid.AddForce(Vector2.right * bulletSpeed * 10000);
        else bulletRigid.AddForce(Vector2.left * bulletSpeed * 10000);
    }
}
