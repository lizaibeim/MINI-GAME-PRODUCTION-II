using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    MagnetGun gun;
    Player player;
    PlayerStatus playerStatus;

    //Player
    public AK.Wwise.Event Player_Footstep;
    public AK.Wwise.Event Player_Jump;
    public AK.Wwise.Event Player_Land;

    //Hit FX
    public AK.Wwise.Event Player_Hit_Sound;
    public AK.Wwise.Event Player_Death_Sound;

    //GravityGun
    public AK.Wwise.Event GravityGun_Shot;
    public AK.Wwise.Event GravityGun_Pickup;
    public AK.Wwise.Event GravityGun_NoShot;

    //MagnetGun
    public AK.Wwise.Event MagnetGun_Shot_Blue;
    public AK.Wwise.Event MagnetGun_Shot_Red;
    public AK.Wwise.Event MagnetGun_Shot_OutofRange;
    public AK.Wwise.Event MagnetGun_Shot_Reset;



    //UI
    public AK.Wwise.Event Level_Reset;

    //Switches
    //public AK.Wwise.Switch Switch_Surface_Type;


    public SurfaceMaterial currentMaterial;

    //public AK.Wwise.Event Player_LandHard;
    //public AK.Wwise.Event Player_Falling;


    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<MagnetGun>();
        player = GetComponent<Player>();
        playerStatus = GetComponent<PlayerStatus>();


        gun.shotLEvent += GunShotL;
        gun.shotREvent += GunShotR;
        gun.failedShootEvent += GravityFail;
        gun.gravityShootEvent += GravityShoot;
        gun.gravityPickupEvent += GravityPickup;
        gun.magnetFailEvent += MagnetFailSound;

        playerStatus.takeDamageEvent += HitSound;
        playerStatus.deathEvent += DeathSound;

        player.jumpEvent += JumpSound;
        player.landEvent += LandSound;
        player.hardLandEvent += HardLandSound;


        LevelManager.Instance.restartEvent += RestartSound;
        MagnetismManager.Instance.resetMagnetism += ResetMagnetism;
    }

    private void Update()
    {
        if (currentMaterial != player.currentSurfaceMaterial)
        {
            ChangeSurfaceMaterial(player.currentSurfaceMaterial);
        }
    }

    void ChangeSurfaceMaterial(SurfaceMaterial newMaterial)
    {
        currentMaterial = newMaterial;
        //Do your Wwise switch thing
        switch (currentMaterial)
        {
            case SurfaceMaterial.Metal:
                AkSoundEngine.SetSwitch("Surface_Type", "Metal", gameObject);
                break;
            default:
                AkSoundEngine.SetSwitch("Surface_Type", "¨Plastic", gameObject);
                break;
        }


    }

    private void OnDisable()
    {
        gun.shotLEvent -= GunShotL;
        gun.shotREvent -= GunShotR;
        gun.failedShootEvent -= GravityFail;
        gun.gravityShootEvent -= GravityShoot;
        gun.gravityPickupEvent -= GravityPickup;
        gun.magnetFailEvent -= MagnetFailSound;


        playerStatus.takeDamageEvent -= HitSound;
        playerStatus.deathEvent -= DeathSound;

        player.jumpEvent -= JumpSound;
        player.landEvent -= LandSound;
        player.hardLandEvent -= HardLandSound;

        LevelManager.Instance.restartEvent -= RestartSound;
        MagnetismManager.Instance.resetMagnetism -= ResetMagnetism;
    }

    public void GunShotL()
    {
        MagnetGun_Shot_Red.Post(gameObject);
    }
    public void GunShotR()
    {
        MagnetGun_Shot_Blue.Post(gameObject);
    }

    public void GravityPickup()
    {
        GravityGun_Pickup.Post(gameObject);
    }

    public void GravityFail()
    {
        GravityGun_NoShot.Post(gameObject);
    }

    public void GravityShoot()
    {
        GravityGun_Shot.Post(gameObject);
    }


    public void JumpSound()
    {
        Player_Jump.Post(gameObject);
    }

    public void LandSound()
    {
        Player_Land.Post(gameObject);
    }

    public void HardLandSound()
    {
        //MagnetGun_Shot_Reset.Post(gameObject);
    }

    void DeathSound()
    {
        Player_Death_Sound.Post(gameObject);
    }

    void HitSound()
    {
        Player_Hit_Sound.Post(gameObject);

    }

    public void MagnetFailSound()
    {
        MagnetGun_Shot_OutofRange.Post(gameObject);
    }

    public void ResetMagnetism()
    {
        MagnetGun_Shot_Reset.Post(gameObject);
    }


    public void FootL()
    {
        Player_Footstep.Post(gameObject);
    }
    public void FootR()
    {
        Player_Footstep.Post(gameObject);
    }

    public void RestartSound()
    {
        Level_Reset.Post(gameObject);
    }
}
