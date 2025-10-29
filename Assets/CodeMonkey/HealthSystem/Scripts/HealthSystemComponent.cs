using UnityEngine;

namespace CodeMonkey.HealthSystemCM {
    public class HealthSystemComponent : MonoBehaviour, IGetHealthSystem {

        [SerializeField] private float healthAmountMax = 100f;
        [SerializeField] private float startingHealthAmount;
        private HealthSystem healthSystem;
        private void Awake() {
            healthSystem = new HealthSystem(healthAmountMax);
            if (startingHealthAmount != 0) {
                healthSystem.SetHealth(startingHealthAmount);
            }
        }
        public HealthSystem GetHealthSystem() {
            return healthSystem;
        }
    }
}