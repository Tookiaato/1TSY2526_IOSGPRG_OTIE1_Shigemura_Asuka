using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class Unit : MonoBehaviour
{
    public Gun _currentGun;
    public List<Unit> targetList = new List<Unit>();

    [SerializeField] protected Health _health;

    [SerializeField] protected string _name;
    [SerializeField] protected float _speed;

    [SerializeField] protected List<GameObject> _guns;
    [SerializeField] private GameObject _enemyHealthBar;
    [SerializeField] private Slider _enemyHpSlider;

    [SerializeField] protected float patrolRadius;

    private GameObject _enemyHealthBarHolder;

    protected Vector2 targetDestination;
    protected float _patrolWaitTime;
    protected float _switchDirectionTime;
    protected Quaternion targetRotation;

    protected Unit unit;
    protected bool isWithinRange;

    private void Update()
    {
        if (_enemyHealthBarHolder != null)
        {
            UpdateEnemyHealthBarPosition();
        }
    }

    public void Initialize(string name, int maxHealth, float speed)
    {
        _name = name;
        gameObject.name = _name;

        if (_health == null)
            _health = GetComponent<Health>();

        _health.Initialize(maxHealth);

        _speed = speed;

        Debug.Log($"{name} has been initialized");
    }

    public virtual void Shoot()
    {
        if (_currentGun == null)
            return;

        Debug.Log($"{_name} is shooting");
        _currentGun.Shoot();
    }

    public virtual void Reload()
    {
        _currentGun?.Reload();
    }

    public void ManageEnemyHealth()
    {
        if (_enemyHealthBar == null)
            return;

        _enemyHpSlider.enabled = false;
        _enemyHealthBarHolder.SetActive(true);

        _enemyHpSlider.value = (float)_health.CurrentHealth / _health.MaxHealth;

        if (_health.CurrentHealth <= 0)
        {
            GameUI.Instance.spawner._enemies.Remove(this);
            GameUI.Instance.UpdateEnemyCount();
        }
    }

    private void UpdateEnemyHealthBarPosition()
    {
        _enemyHealthBarHolder.transform.position = transform.position + Vector3.up;
        _enemyHealthBarHolder.transform.rotation = Quaternion.identity;
    }

    protected void InsantiateEnemyHealthBar()
    {
        _enemyHealthBarHolder = Instantiate(
            _enemyHealthBar, 
            transform.position + Vector3.up, 
            Quaternion.identity, 
            transform
        );

        _enemyHealthBarHolder.SetActive(false);

        _enemyHpSlider = _enemyHealthBarHolder
            .transform.GetChild(0)
            .GetComponentInChildren<Slider>();
    }

    // Movement and AI
    protected void Patrol()
    {
        HandleSwitchDirectionTimer();
        HandlePatrolWaitTimer();

        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            targetRotation, 
            Time.deltaTime * 5
        );

        transform.position = Vector2.MoveTowards(
            transform.position, 
            targetDestination, 
            _speed * Time.deltaTime
        );
    }

    private void HandleSwitchDirectionTimer()
    {
        if (_switchDirectionTime > 0)
        {
            _switchDirectionTime -= Time.deltaTime;
            return;
        }

        SetNewRandomDestination();
        _switchDirectionTime = 10f;
    }

    private void HandlePatrolWaitTimer()
    {
        if (_patrolWaitTime > 0)
        {
            _patrolWaitTime -= Time.deltaTime;
            return;
        }

        if (Vector2.Distance(transform.position, targetDestination) >= 0.1f)
            return;

        SetNewRandomDestination();

        _patrolWaitTime = 3f;
        _switchDirectionTime = 10f;

        Vector2 direction = ((Vector2)transform.position - targetDestination).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        targetRotation = Quaternion.Euler(0f, 0f, angle - 180f);
    }

    private void SetNewRandomDestination()
    {
        Vector2 randomPoint = Random.insideUnitCircle * patrolRadius;
        targetDestination = (Vector2)transform.position + randomPoint;
    }

    protected void MoveTowardsTarget(float attackRange)
    {
        if (!isWithinRange)
        {
            Patrol();
            return;
        }

        if (targetList == null || targetList.Count == 0)
        {
            isWithinRange = false;
            return;
        }

        Unit target = targetList[0];

        float distance = Vector2.Distance(target.transform.position, transform.position);

        if (distance >= attackRange)
        {
            transform.position = Vector2.MoveTowards(
                transform.position, 
                target.transform.position, 
                _speed * Time.deltaTime
            );
        }
        else
        {
            Shoot();
        }

        transform.right = target.transform.position - transform.position;
    }
}
