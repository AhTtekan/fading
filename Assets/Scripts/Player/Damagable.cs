using UnityEngine;

public class Damagable : MonoBehaviour {

    public SoundPlayer sp;
    public float HP, Max_HP, recoverySpeed;

    public void TakeDamage()
    {
        sp.PlaySound(3);
        HP-=1;
        if(HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject.FindObjectOfType<GameManager>().GameOver();
        //Game Over
        //Pause all enemies and player movement
        //fade screen
        //game over text
        //Restart/Quit option
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (HP < Max_HP)
            HP += recoverySpeed * Time.deltaTime;
    }
}
