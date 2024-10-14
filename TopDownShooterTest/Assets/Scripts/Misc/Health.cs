public class Health
{
    public float maxHealth = 100.0f, currentHealth = 100.0f;

    public Health(float maxHealth, float currentHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
    }
    public void UpgradeMaxHealth(float upgrade)
    {
        maxHealth += upgrade;
    }
    public bool ReduceHealth(float damage)
    {
        currentHealth -= damage;
        if ( currentHealth <= 0 ) 
        { 
            currentHealth = 0;
            return false; 
        }
        return true;
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
