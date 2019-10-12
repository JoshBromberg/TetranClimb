using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Public Variables
    public AudioClip startMusic;
    public AudioClip[] levelMusic = new AudioClip [2], bossMusic = new AudioClip[2];
    public AudioSource backgroundMusicS;
    public int levelTimer, levelTimerCount, bossTimerCount;
    public Transform fanSpawnPoint1, fanSpawnPoint2;
    public int fanSpawnTimerReset;
    public AudioClip playerDeathSound, capsuleCollect, upgradeActivate, playerFire, playerLaser, gameOver, victory;
    public int[] respawnPoints = {0, 16, 34, 52, 68};
    public int playerDeathTimer = 0;
    public int playerDeathTimerReset;
    public int lives;
    public int powerCapsules;
    public Vector3 playerRespawnPoint;
    public int destroyPoint = -5;
    public float destroyPointRocks = -6.3f;
    public float fanTurnPoint = -1.5f;
    public Transform[] garrunSpawnPoints = new Transform[3];
    public Sprite[] garrunSprites = new Sprite[3];
    public Transform[] rockSpawnPoints = new Transform[2];
    public bool testingCheat;
    public int score = 0;
    public Text livesText, scoreText, highscoreText;
    public bool pause = false;

    //Public Game Objects
    public GameObject fan, garrun, rugurr, redRugurr, jumper, redJumper, bigCore, playerDeath, black, playerPrefab, capsule, SFXPlayer, panel, panel2, startRock, rock, mountain, volcano, DUCKER, rasheSpawn, dai, explosion, explosionFan, explosionBig, explosionBigCore, grass, bonusMountain;
    public GameObject[] panelText = new GameObject[6];

    //Private Variables
    private int levelTimerBase;
    private bool createFanWave = false, spawnedFanWave=false;
    private int fanSpawnPoint = 0, fanGenerator = 0, fanSpawnTimer = 0, spawnedFanWaveTimer = 0, spawnedFanWaveReset=50;
    private int[] fanSpawns = {1, 3, 5, 7, 9, 15, 17, 19};
    private int[] garrunSpawns = { 11, 12, 13, 14, 20, 21, 22, 27, 28, 29, 34, 35, 55, 56, 57, 63, 64, 65, 70, 75, 81, 82};
    private int[] rugurrSpawns = { 23, 24, 25, 37, 38, 42, 46, 47, 48, 49, 50, 58, 59, 66, 67, 71, 72, 79, 80 };
    private bool[] rugurrRed = { false, true, false, false, true, false, false, false, false, false, false, false, false, false, false, false, true, false, true };
    private int[] jumperSpawns = { 36, 37, 43, 44, 54, 58, 69, 89, 92, 93, 94 };
    private bool[] jumperRed = { true, false, false, true, true, true, false, false, false, true, false };
    private int[] mountainSpawns = { 31, 65, 87 };
    private int[] DUCKERSpawns = { 35, 45, 52, 77, 81, 85, 88, 90 }; //88+ only top one spawns
    private bool[] DUCKERRed = { false, false, false, true, false, false, true, false };
    private int[] rasheSpawns = { 41, 62, 78, 85 }; //78 is top, each one spawns 8 rashe and takes 5 hits to kill. Goes red at 1 hp
    private int[] daiSpawns = { 30, 40, 61, 77, 83 };
    private int[] daiNum = { 3, 2, 2, 2, 2 };
    private bool[] daiRed = { false, false, false, false, true };
    private bool[] daiTop = { true, false, false, true, false };
    private int[] grassSpawn = { 30, 32, 33, 34, 35, 36, 37, 38, 39, 47, 48, 50, 51, 63, 64, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 95, 96, 97, 98, 99, 100, 101, 102};
    private int volcanoSpawn;
    private int bonusMountainSpawn = 49;
    private GameObject[,] fans = new GameObject[2, 4];
    private bool[,] fanShot = new bool[2, 4];
    private int tempTime = 0;
    private float[] playerAxis = new float[2];
    private float playerDeathSpotY;
    private bool useDeathSpot = false;
    private GameObject[] panels = new GameObject[6];
    private GameObject[] panelDisplayText = new GameObject[6];
    private Vector2[] panelSpawn = new Vector2[6];
    private Vector2 bossSpawnPoint = new Vector2(6, 0);
    private int rocksStart = 26;
    private bool drawRocks = false, stopRocks = false;
    private int spawnEnemiesTimer = 0, spawnEnemiesTimerReset = 75;
    private int[] musicLength = { 1041, 66 };
    private int[] musicTimer = { 0, 0 };
    private bool stopGrass = false;
    private int pauseTimer = 0, pauseTimerReset = 500;

    // Start is called before the first frame update
    void Start()
    {
        volcanoSpawn = bossTimerCount + 200;

        //Set FanShot
        for (int z=0; z<2; ++z)
        {
            for (int y=0; y<4; ++y)
            {
                fanShot[z, y] = false;
            }
        }

        //Create Control Panel
        for (int z = -3; z < 3; ++z) {
            panelSpawn[z + 3] = new Vector2(0.525f + (z * 1.05f), -4.5f);
            panels[z + 3] = Instantiate(panel, panelSpawn[z + 3], transform.rotation);
            panelDisplayText[z + 3] = Instantiate(panelText[z+3], panelSpawn[z + 3], transform.rotation);

        }

        //Set Music
        backgroundMusicS.clip = startMusic;
        backgroundMusicS.Play();

        //Set Level Timer
        levelTimerBase = levelTimer;

        //Create Player
        Instantiate(playerPrefab, playerRespawnPoint, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().attackSpeedSprites[0].transform.position = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().attackSpeedSpawn1.position;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().attackSpeedSprites[1].transform.position = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().attackSpeedSpawn2.position;
        } catch (System.NullReferenceException) { }
        catch (UnassignedReferenceException) { }
        catch (MissingReferenceException) { }
    }

    private void FixedUpdate()
    {
        if (pause == false)
        {
            int time = (levelTimerBase - levelTimer) / 50;
            int timeModulator = levelTimerBase - levelTimer;

            //Find all game objects
            GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
            GameObject[] capsules = GameObject.FindGameObjectsWithTag("Capsule");
            GameObject[] rocks = GameObject.FindGameObjectsWithTag("Rock");
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            //Move Rocks
            if (stopRocks == false)
            {
                foreach (var i in rocks)
                {
                    Rigidbody2D rBody = i.GetComponent<Rigidbody2D>();
                    MoveLeft move = i.GetComponent<MoveLeft>();
                    rBody.position += move.speed;
                    if (rBody.position.x <= destroyPointRocks)
                    {
                        Destroy(i);
                    }
                }
            }

            //Move Capsules
            for (int z = 0; z < capsules.Length; ++z)
            {
                Rigidbody2D rBody = capsules[z].GetComponent<Rigidbody2D>();
                MoveLeft move = capsules[z].GetComponent<MoveLeft>();
                rBody.position += move.speed;
                if (rBody.position.x <= destroyPoint)
                {
                    Destroy(capsules[z]);
                }
                if (playerDeathTimer == 0)
                {
                    //Power Capsule Collection
                    if (player.GetComponent<Collider2D>().IsTouching(capsules[z].GetComponent<Collider2D>()))
                    {
                        SFXPlay(capsuleCollect);
                        if (powerCapsules > 0)
                        {
                            Destroy(panels[powerCapsules - 1]);
                            panels[powerCapsules - 1] = Instantiate(panel, panelSpawn[powerCapsules - 1], transform.rotation);
                        }
                        powerCapsules = powerCapsules == 6 ? 1 : ++powerCapsules;
                        Destroy(panels[powerCapsules - 1]);
                        panels[powerCapsules - 1] = Instantiate(panel2, panelSpawn[powerCapsules - 1], transform.rotation);
                        UpdateScore(500);
                        Destroy(capsules[z]);
                    }
                }
            }
            //Enemy Commands
            for (int y = 0; y < enemies.Length; ++y)
            {
                Rigidbody2D rBody = enemies[y].GetComponent<Rigidbody2D>();
                MoveLeft move = enemies[y].GetComponent<MoveLeft>();

                //Garrun Sprites
                if (enemies[y].name.Contains("Garrun"))
                {
                    float target = 0;
                    if (useDeathSpot)
                    {
                        target = playerDeathSpotY;
                    }
                    else
                    {
                        target = player.GetComponent<Transform>().position.y;
                    }
                    if (rBody.position.y >= target - 0.01f && rBody.position.y <= target + 0.01f)
                    {
                        enemies[y].GetComponent<SpriteRenderer>().sprite = garrunSprites[1];
                        if (rBody.position.y != target)
                        {
                            rBody.position = new Vector2(rBody.position.x, target);
                        }
                        move.speed = new Vector2(move.speedFactor * 2f, 0);
                    }
                    else if (rBody.position.y > target)
                    {
                        enemies[y].GetComponent<SpriteRenderer>().sprite = garrunSprites[0];
                        move.speed = new Vector2(move.speedFactor, -Mathf.Abs(move.speedFactor));
                    }
                    else if (rBody.position.y < target)
                    {
                        enemies[y].GetComponent<SpriteRenderer>().sprite = garrunSprites[2];
                        move.speed = new Vector2(move.speedFactor, Mathf.Abs(move.speedFactor));
                    }
                }

                //Rugurr Movement
                else if (enemies[y].name.Contains("Rugurr"))
                {
                    int m = move.rugurrTimer * 3; //the multiplication number slows the y-movement. bigger number = more sine waves
                    if (m % 180 == 0)
                    {
                        move.rugurrTimer = 0;
                    }
                    else if (m % 90 == 0)
                    {
                        move.rugurrTimer = -move.rugurrTimer;
                    }
                    float z = 0.017607f * 6; //0.19016f; //this is the magic number to make the player move a certain amount on the y-axis. Equation: z = maxY/56.74433
                    float tempSpeed = -Mathf.Sin(ToDeg(m));
                    float speed = tempSpeed == -1 ? 0 : tempSpeed * z;
                    move.speed = new Vector2(move.speedFactor, speed);
                    ++move.rugurrTimer;
                    //Debug.Log(tempSpeed);
                }

                //Jumper Movement
                else if (enemies[y].name.Contains("Jumper"))
                {
                    int m = move.rugurrTimer * 2; //the multiplication number slows the y-movement. bigger number = more sine waves
                    if (m % 180 == 0)
                    {
                        move.rugurrTimer = 0;
                        ++move.jump;
                        ++move.totalJumps;
                        if (move.jump == 2 && move.totalJumps < 4)
                        {
                            move.jump = -move.jump;
                        }
                    }
                    else if (m % 90 == 0)
                    {
                        move.rugurrTimer = -move.rugurrTimer;
                    }
                    float z = 0.017607f * 6; //0.19016f; //this is the magic number to make the player move a certain amount on the y-axis. Equation: z = maxY/56.74433
                    float tempSpeed = Mathf.Sin(ToDeg(m));
                    float speed = /*tempSpeed == -1 ? 0 :*/ tempSpeed * z;
                    move.speed = move.jump < 0 ? new Vector2(-move.speedFactor / 2, speed) : new Vector2(move.speedFactor, speed);
                    ++move.rugurrTimer;
                    //Debug.Log(tempSpeed);
                }
                //DUCKER Commands
                if (enemies[y].name.Contains("DUCKER"))
                {
                    DuckerController d = enemies[y].GetComponent<DuckerController>();
                    if (enemies[y].GetComponent<Fire>().fired == false) //Walk commands
                    {
                        rBody.position += move.speed;
                        if (timeModulator % 10 == 0)
                        {
                            enemies[y].GetComponent<SpriteRenderer>().sprite = enemies[y].GetComponent<SpriteRenderer>().sprite == d.DUCKERSprites[d.red, 1] ? d.DUCKERSprites[d.red, 0] : d.DUCKERSprites[d.red, 1];
                        }
                    }
                    else if (enemies[y].GetComponent<Fire>().fired && enemies[y].GetComponent<SpriteRenderer>().sprite != d.DUCKERSprites[d.red, 2]) //Stop Walking
                    {
                        ++d.fired;
                        d.fireCooldown = d.fireCooldownReset;
                        enemies[y].GetComponent<SpriteRenderer>().sprite = d.DUCKERSprites[d.red, 2];
                        move.speed.x = move.speedFactor = -0.05f;
                        if (d.rotateOnFire)
                        {
                            enemies[y].transform.Rotate(0, 180, 0);
                        }
                    }
                    else if (enemies[y].GetComponent<Fire>().fired && d.fired < d.firedMaximum && d.fireCooldown == 0) //Start Walking
                    {
                        enemies[y].GetComponent<SpriteRenderer>().sprite = d.DUCKERSprites[d.red, 0];
                        enemies[y].GetComponent<Fire>().fired = false;
                        try
                        {
                            if (GameObject.FindGameObjectWithTag("Player").transform.position.x > enemies[y].transform.position.x)
                            {
                                d.rotateOnFire = true;
                                enemies[y].transform.Rotate(0, -180, 0);
                            }
                            else
                            {
                                d.rotateOnFire = false;
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            d.rotateOnFire = false;
                        }
                        move.speedFactor = d.rotateOnFire ? d.speed : -d.speed;
                        move.speed.x = move.speedFactor;
                    }
                    else //Move Stationary
                    {
                        move.speed.x = -0.05f;
                        rBody.position += move.speed;
                    }
                }
                //else if (enemies[y].name.Contains("Bottom")) { }
                else
                {
                    //Move Enemies
                    rBody.position += move.speed;
                }

                //Destroy off-screen
                destroyPoint = enemies[y].name.Contains("Mountain") || enemies[y].name.Contains("Spawn") ? -7 : -5;
                try
                {
                    if (rBody.position.x <= destroyPoint || (enemies[y].GetComponent<MoveLeft>().fanTurned && rBody.position.x >= -destroyPoint) || rBody.position.y >= -destroyPoint || rBody.position.y <= destroyPoint)
                    {
                        Destroy(enemies[y]);
                    }

                    //Fan Commands
                    else if (enemies[y].name.Contains("Fan") && rBody.position.x <= fanTurnPoint && move.fanTurned == false)
                    {
                        try
                        {
                            if (fans[move.line, 0] == enemies[y])
                            {
                                playerAxis[move.line] = player.transform.position.y;
                            }
                        }
                        catch (System.NullReferenceException) { };
                        float up = playerAxis[move.line] >= rBody.position.y ? -move.speedFactor : move.speedFactor;
                        move.fanTurnedUp = playerAxis[move.line] >= rBody.position.y ? false : true;
                        move.speed = new Vector2(-move.speedFactor, up);
                        move.fanTurned = true;
                    }
                    else if (move.fanTurned && ((move.fanTurnedUp && rBody.position.y <= playerAxis[move.line]) || (move.fanTurnedUp == false && rBody.position.y >= playerAxis[move.line])))
                    {
                        move.speed = new Vector2(-move.speedFactor, 0f);
                        if (rBody.position.y != playerAxis[move.line])
                            transform.position = new Vector2(transform.position.x, playerAxis[move.line]);
                    }
                }
                catch (System.NullReferenceException) { }
                //End of Fan Commands

                //Player Death Commands
                if (playerDeathTimer > 0 || testingCheat) { }
                else if (player.GetComponent<Collider2D>().IsTouching(enemies[y].GetComponent<Collider2D>()) && playerDeathTimer == 0)
                {
                    //Player Death Commands
                    playerDeathTimer = playerDeathTimerReset;
                    playerDeathSpotY = player.GetComponent<Transform>().position.y;
                    useDeathSpot = true;

                    //Return Background Mover
                    try
                    {
                        if (GameObject.FindGameObjectWithTag("Background").GetComponent<MoveBackground>().reset != 0) { }
                    }
                    catch (System.NullReferenceException)
                    {
                        foreach (var i in GameObject.FindGameObjectsWithTag("Background"))
                        {
                            i.AddComponent<MoveBackground>();
                        }
                    }

                    //Play death animation sounds
                    backgroundMusicS.mute = true;
                    GameObject[] sfxPlayers = GameObject.FindGameObjectsWithTag("SFXPlayer");
                    for (int z = 0; z < sfxPlayers.Length; ++z)
                    {
                        sfxPlayers[z].GetComponent<AudioSource>().mute = true;
                    }
                    SFXPlay(playerDeathSound);

                    //Switch player to death animation
                    Instantiate(playerDeath, player.transform.position, player.transform.rotation);
                    playerAxis[1] = playerAxis[0] = player.transform.position.y;
                    PlayerController p = player.GetComponent<PlayerController>();
                    for (int a = 0; a < p.optionsActive; ++a)
                    {
                        Destroy(p.options[a]);
                    }
                    Destroy(player);
                    --lives;
                    tempTime = time;
                    stopRocks = false;
                }
            }

            //Move Bosses
            foreach (var i in bosses)
            {
                try
                {
                    i.GetComponent<Rigidbody2D>().position += i.GetComponent<MoveLeft>().speed;
                    if (i.transform.position.y <= -7)
                    {
                        Destroy(i);
                    }
                }
                catch (System.NullReferenceException) { }
            }

            //Move Grass
            if (stopGrass == false)
            {
                foreach (var i in GameObject.FindGameObjectsWithTag("Grass"))
                {
                    i.GetComponent<Rigidbody2D>().position += i.GetComponent<MoveLeft>().speed;
                    if (i.transform.position.x < -5.6)
                    {
                        Destroy(i);
                    }
                }
            }

            //Spawn Grass
            for (int z = 0; z < grassSpawn.Length; ++z)
            {
                if (time == grassSpawn[z] && timeModulator % 25 == 0 && stopRocks == false)
                {
                    GameObject temp = Instantiate(grass, new Vector2(5.6f, -3.7f), transform.rotation);
                    foreach (var i in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        if (temp.GetComponent<Collider2D>().IsTouching(i.GetComponent<Collider2D>()) && i.name.Contains("Mountain"))
                        {
                            Destroy(temp);
                        }
                    }
                }
            }


            if (spawnEnemiesTimer == 0)
            {
                for (int z = 0; z < fanSpawns.Length; ++z)
                {
                    if (time == fanSpawns[z] && spawnedFanWave == false)
                    {
                        createFanWave = true;
                    }
                }
                if (createFanWave && fanSpawnTimer == 0)
                {
                    Transform fanSpawn = fanSpawnPoint == 0 ? fanSpawnPoint1 : fanSpawnPoint2;
                    fans[fanSpawnPoint, fanGenerator] = Instantiate(fan, fanSpawn.position, fanSpawn.rotation); //Create instance of 'weapon' [Instantiate means "create an object"... or... "create an instance"]
                    fans[fanSpawnPoint, fanGenerator].GetComponent<MoveLeft>().line = fanSpawnPoint;
                    fanShot[fanSpawnPoint, fanGenerator] = false;
                    //Debug.Log($"Enemy spawned! {fans[fanSpawnPoint, fanGenerator]}");
                    fanSpawnTimer += fanSpawnTimerReset;
                    ++fanGenerator;
                    if (fanGenerator == 4)
                    {
                        fanSpawnTimer = 0;
                        fanGenerator = 0;
                        fanSpawnPoint = 1 - fanSpawnPoint;
                        createFanWave = false;
                        spawnedFanWave = true;
                        spawnedFanWaveTimer = spawnedFanWaveReset;
                    }
                }
                if (timeModulator % 50 == 0)
                {
                    //Spawn Garrun
                    for (int z = 0; z < garrunSpawns.Length; ++z)
                    {
                        if (time == garrunSpawns[z])
                        {
                            for (int y = 0; y < 3; ++y)
                            {
                                Instantiate(garrun, garrunSpawnPoints[y].position, garrunSpawnPoints[y].rotation);
                            }
                        }
                    }

                    //Spawn Rugurr
                    for (int z = 0; z < rugurrSpawns.Length; ++z)
                    {
                        if (time == rugurrSpawns[z])
                        {
                            for (int y = 1; y < garrunSpawnPoints.Length; ++y)
                            {
                                float a = y == 1 ? 1.5f : 1;
                                if (rugurrRed[z] == true)
                                {
                                    Instantiate(redRugurr, garrunSpawnPoints[y].position + new Vector3(0, a, 0), garrunSpawnPoints[y].rotation);
                                }
                                else
                                {
                                    Instantiate(rugurr, garrunSpawnPoints[y].position + new Vector3(0, a, 0), garrunSpawnPoints[y].rotation);
                                }
                            }
                        }
                    }

                    //Spawn Jumper
                    for (int z = 0; z < jumperSpawns.Length; ++z)
                    {
                        if (time == jumperSpawns[z])
                        {
                            if (jumperRed[z] == true)
                            {
                                Instantiate(redJumper, new Vector3(5.3f, -4, 0), transform.rotation);
                            }
                            else
                            {
                                Instantiate(jumper, new Vector3(5.3f, -4, 0), transform.rotation);
                            }
                        }
                    }

                    //Spawn DUCKER
                    for (int z = 0; z < DUCKERSpawns.Length; ++z)
                    {
                        if (time == DUCKERSpawns[z])
                        {
                            GameObject temp1 = Instantiate(DUCKER, new Vector2(-4.9f, 4.3f), transform.rotation);
                            temp1.GetComponent<DuckerController>().red = DUCKERRed[z] ? 1 : 0;
                            temp1.transform.Rotate(180, 0, 0);
                            temp1.GetComponent<DuckerController>().reverseMovement = true;
                            if (time < 88)
                            {
                                GameObject temp2 = Instantiate(DUCKER, new Vector2(-4.9f, -3.7f), transform.rotation);
                                temp2.GetComponent<DuckerController>().red = DUCKERRed[z] ? 1 : 0;
                            }
                            //Debug.Break();
                        }
                    }

                    //Spawn Dai
                    for (int z = 0; z < daiSpawns.Length; ++z)
                    {
                        if (time == daiSpawns[z])
                        {
                            for (int y = 0; y < daiNum[z]; ++y)
                            {
                                GameObject temp = daiTop[z] ? Instantiate(dai, new Vector2(5.3f + (y * 0.6f), 4.3f), transform.rotation) : Instantiate(dai, new Vector2(5.3f + (y * 0.6f), -3.7f), transform.rotation);
                                temp.GetComponent<DaiController>().red = daiRed[z] && y + 1 == daiNum[z] ? 1 : 0;
                                if (daiTop[z])
                                {
                                    temp.transform.Rotate(180, 0, 0);
                                }
                            }
                        }
                    }

                    //Spawn Rashe Spawner
                    for (int z = 0; z < rasheSpawns.Length; ++z)
                    {
                        if (time == rasheSpawns[z])
                        {
                            GameObject temp = z == 2 ? Instantiate(rasheSpawn, new Vector2(5.5f, 4.3f), transform.rotation) : Instantiate(rasheSpawn, new Vector2(5.5f, -3.7f), transform.rotation);
                            if (z == 2)
                            {
                                temp.transform.Rotate(180, 0, 0);
                            }
                        }
                    }

                    //Spawn Mountain
                    for (int z = 0; z < mountainSpawns.Length; ++z)
                    {
                        if (time == mountainSpawns[z])
                        {
                            if (z == 2)
                            {
                                GameObject temp1 = Instantiate(mountain, new Vector2(7f, 0.9f), transform.rotation);
                                temp1.GetComponent<SpriteRenderer>().sprite = volcano.GetComponent<SpriteRenderer>().sprite;
                                temp1.GetComponent<PolygonCollider2D>().points = volcano.GetComponent<PolygonCollider2D>().points;
                                GameObject temp2 = Instantiate(mountain, new Vector2(7f, -0.9f), transform.rotation);
                                temp2.GetComponent<SpriteRenderer>().sprite = volcano.GetComponent<SpriteRenderer>().sprite;
                                temp2.GetComponent<PolygonCollider2D>().points = volcano.GetComponent<PolygonCollider2D>().points;
                                temp2.transform.Rotate(180, 0, 0);
                            }
                            else
                            {
                                Instantiate(mountain, new Vector2(7f, -2.9f), transform.rotation);
                            }
                        }
                    }

                    //Spawn Bonus Mountain
                    if (time == bonusMountainSpawn)
                    {
                        Instantiate(bonusMountain, new Vector2(7f, -1.85f), transform.rotation);
                    }

                    //Spawn Volcano
                    if (levelTimer == volcanoSpawn)
                    {
                        Instantiate(volcano, new Vector2(7f, -3.4f), transform.rotation);
                        Instantiate(volcano, new Vector2(13f, -3.4f), transform.rotation);
                    }
                }
            }

            //Draw rocks
            if (time == rocksStart && timeModulator % 50 == 0)
            {
                drawRocks = true;
                foreach (var i in rockSpawnPoints)
                {
                    Instantiate(startRock, i.position, i.rotation);
                }
            }
            else if (drawRocks && timeModulator % 30 == 0)
            {
                foreach (var i in rockSpawnPoints)
                {
                    Instantiate(rock, i.position, i.rotation);
                }
            }

            if (levelTimer > -100)
            {
                --levelTimer;
                //Start Challenger
                if (levelTimer == levelTimerCount)
                {
                    backgroundMusicS.Stop();
                    backgroundMusicS.clip = levelMusic[0];
                    backgroundMusicS.Play();
                    musicTimer[0] = musicLength[0];
                }
                //Start Volcano
                else if (levelTimer == bossTimerCount)
                {
                    backgroundMusicS.Stop();
                    backgroundMusicS.clip = bossMusic[0];
                    backgroundMusicS.Play();
                    musicTimer[0] = 0;
                    musicTimer[1] = musicLength[1];
                    GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");
                    for (int z = 0; z < backgrounds.Length; ++z)
                    {
                        Destroy(backgrounds[z].GetComponent<MoveBackground>());
                    }
                    drawRocks = false;
                    stopRocks = true;
                    stopGrass = true;
                }
                //End Volcano
                else if (levelTimer == 0)
                {
                    GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");
                    for (int z = 0; z < backgrounds.Length; ++z)
                    {
                        backgrounds[z].AddComponent<MoveBackground>();
                    }
                    stopRocks = false;
                    stopGrass = false;
                }
                //Summon Big Core
                else if (levelTimer == -100)
                {
                    GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");
                    for (int z = 0; z < backgrounds.Length; ++z)
                    {
                        Destroy(backgrounds[z].GetComponent<MoveBackground>());
                    }
                    Instantiate(bigCore, bossSpawnPoint, transform.rotation);
                }
            }
            if (spawnedFanWaveTimer > 0)
            {
                --spawnedFanWaveTimer;
                if (spawnedFanWaveTimer == 0)
                    spawnedFanWave = false;
            }
            if (fanSpawnTimer > 0)
                --fanSpawnTimer;
            if (spawnEnemiesTimer > 0)
            {
                --spawnEnemiesTimer;
            }
            for (int z = 0; z < musicTimer.Length; ++z)
            {
                if (musicTimer[z] > 0)
                {
                    --musicTimer[z];
                    if (musicTimer[z] == 0)
                    {
                        backgroundMusicS.Stop();
                        backgroundMusicS.clip = z == 0 ? levelMusic[1] : bossMusic[1];
                        backgroundMusicS.Play();
                        musicTimer[0] = 0;
                        musicTimer[1] = 0;
                    }
                }
            }
            if (playerDeathTimer > 0)
            {
                --playerDeathTimer;
                if (playerDeathTimer == 75)
                {
                    Destroy(GameObject.FindGameObjectWithTag("PlayerGhost")); //clear player ghost
                }
                else if (playerDeathTimer == 25 && lives >= 0)
                {
                    Instantiate(black, transform.position, transform.rotation); //black the screen
                                                                                //clear all enemies
                    for (int z = 0; z < enemies.Length; ++z)
                    {
                        Destroy(enemies[z]);
                    }
                    spawnEnemiesTimer = spawnEnemiesTimerReset;
                    //clear all bosses
                    foreach (var boss in bosses)
                    {
                        Destroy(boss);
                    }
                    //clear all power capsules
                    for (int z = 0; z < capsules.Length; ++z)
                    {
                        Destroy(capsules[z]);
                    }
                    //clear Control Panel
                    for (int z = 0; z < panels.Length; ++z)
                    {
                        Destroy(panels[z]);
                        Destroy(panelDisplayText[z]);
                    }
                }
                else if (playerDeathTimer == 0 && lives >= 0)
                {
                    //reset timer
                    for (int z = 0, end = 0; z < respawnPoints.Length && end == 0; ++z)
                    {
                        if (tempTime <= respawnPoints[z])
                        {
                            levelTimer = respawnPoints[z - 1] * -50 + levelTimerBase;
                            ++end;
                        }
                        else if (z == respawnPoints.Length - 1)
                        {
                            levelTimer = respawnPoints[z] * -50 + levelTimerBase;
                            ++end;
                        }
                        if (end > 0)
                        {
                            powerCapsules = z > 1 ? 1 : 0; //set power capsules
                            backgroundMusicS.clip = z > 1 ? levelMusic[0] : startMusic; //set music
                            if (backgroundMusicS.clip == levelMusic[0])
                            {
                                musicTimer[0] = musicLength[0];
                                musicTimer[1] = 0;
                            }
                            //Add Mountain
                            if (z == 2)
                            {
                                Instantiate(mountain, new Vector2(-0.5f, -2.9f), transform.rotation);
                            }

                            //Draw or don't draw Rocks
                            if (z <= 2)
                            {
                                foreach (var rock in rocks)
                                {
                                    Destroy(rock);
                                }
                                drawRocks = false;
                            }
                            else
                            {
                                drawRocks = true;
                            }
                            fanSpawnPoint = z == 1 ? 0 : 1;
                        }
                    }
                    //re-create Control Panel
                    for (int z = 0; z < 6; ++z)
                    {

                        panels[z] = z == powerCapsules - 1 ? Instantiate(panel2, panelSpawn[z], transform.rotation) : Instantiate(panel, panelSpawn[z], transform.rotation);
                        panelDisplayText[z] = Instantiate(panelText[z], panelSpawn[z], transform.rotation);
                    }

                    //play music
                    backgroundMusicS.mute = false; //unmute volume
                    backgroundMusicS.Play();

                    //Respawn Player
                    Instantiate(playerPrefab, playerRespawnPoint, transform.rotation); //instantiate player at respawn
                    useDeathSpot = false;

                    //Move Rocks
                    stopRocks = false;

                    //Update Lives Text
                    livesText.text = "" + lives;

                    //Destroy Black
                    Destroy(GameObject.FindGameObjectWithTag("Black"));
                }
                else if (playerDeathTimer == 0)
                {
                    SFXPlay(gameOver);
                    pause = true;
                    pauseTimer = pauseTimerReset;
                }
            }
        }
        else
        {
            --pauseTimer;
            if (pauseTimer == 0)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }
        }
    }
    public void runDestructionCommands(GameObject weapon, GameObject enemy)
    {
        if (enemy.name.Contains("Fan")) //If the hit enemy is a fan
        {
            for (int x = 0; x < 2; ++x)
            {
                for (int w = 0; w < 4; ++w)
                {
                    if (fans[x, w] == enemy)
                    {
                        fanShot[x, w] = true;
                        Transform enemyLocation = enemy.transform;
                        //check if that was the last fan
                        for (int b = 0; b < 4; ++b)
                        {
                            if (fanShot[x, b] == false)
                            {
                                break;
                            }
                            else if (b == 3 && (fanSpawnPoint != x || createFanWave == false)) //if all fans are shot and fans aren't spawning out of that spawn point or fans aren't spawning at all
                            {
                            createPowerCapsule(enemyLocation);
                            }
                        }
                    }
                }
            }
        }
        else if (enemy.name.Contains("Red"))
        {
            createPowerCapsule(enemy.transform);
        }
        if (enemy.name.Contains("Spawn"))
        {
            --enemy.GetComponent<RasheSpawnController>().health;
            if (enemy.GetComponent<RasheSpawnController>().health == 0)
            {
                GameObject temp = Instantiate(explosionBig, enemy.transform.position, enemy.transform.rotation);
                temp.GetComponent<ExplosionController>().sound = "big";
                UpdateScore(1000);
                Destroy(enemy);
            }
            Destroy(weapon);
        }
        else
        {
            if (enemy.name.Contains("Fan"))
            {
                Instantiate(explosionFan, enemy.transform.position, enemy.transform.rotation);
            }
            else
            {
                GameObject temp = Instantiate(explosion, enemy.transform.position, enemy.transform.rotation);
                if (enemy.name.Contains("DUCKER") || enemy.name.Contains("Dai"))
                {
                    temp.GetComponent<ExplosionController>().sound = "mechanical";
                }
                else if (enemy.name.Contains("Volcano"))
                {
                    temp.GetComponent<ExplosionController>().sound = "volcano";
                }
            }
            UpdateScore(100);
            Destroy(enemy);
        }
    }
    private void createPowerCapsule(Transform t) {
        Instantiate(capsule, t.position, t.rotation);
    }
    public void SFXPlay(AudioClip clip)
    {
        GameObject SFX = Instantiate(SFXPlayer, transform.position, transform.rotation);
        SFX.GetComponent<AudioSource>().clip = clip;
        SFX.GetComponent<AudioSource>().Play();
    }
    public void resetPanel(int wipedPanel, bool wipeText)
    {
        Destroy(panels[wipedPanel]);
        Destroy(panelDisplayText[wipedPanel]);
        panels[wipedPanel] = Instantiate(panel, panelSpawn[wipedPanel], transform.rotation);
        if (wipeText == false)
        {
            panelDisplayText[wipedPanel] = Instantiate(panelText[wipedPanel], panelSpawn[wipedPanel], transform.rotation);
        }
    }
    private float ToDeg (int i)
    {
        return i * Mathf.PI / 180;
    }
    public void UpdateScore(int i)
    {
        score += i;
        string a="";
        for (int z= score.ToString().Length; z<8; ++z)
        {
            a += "0";
        }
        scoreText.text = a + score;
        if (score > 50000)
        {
            highscoreText.text = "HI  " + a + score;
        }
    }
}
/*
 //run a seperate function from rest of code in its own thread
 StartCoroutine(SpawnWaves());

    //must return IEnumerator for variable type
    IEnumerator SpawnWaves(){
    yield return new WaitForSeconds(X); //pauses the thread for X seconds
    //will run the following line of code after waiting... i think
    Instantiate (BigCore, BigCoreSpawn.Position, BigCoreSpawn.Rotation);
    }
     */