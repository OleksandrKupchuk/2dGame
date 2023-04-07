using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicEnemy : BaseCharacteristics {

    protected float _xScale;

    public bool IsLookRight { get => transform.localScale.x > 0; }
    public bool IsLookLeft { get => transform.localScale.x < 0; }
    public bool IsTargetBehindYouWhenLookRight { get => (transform.position.x - FieldOfView.Target.transform.position.x) >= 0 && IsLookRight; }
    public bool IsTargetBehindYouWhenLookLeft { get => (transform.position.x - FieldOfView.Target.transform.position.x) < 0 && IsLookLeft; }
    public float GetDirectionX { get => transform.localScale.x; }
    public FieldOfView FieldOfView { get; protected set; }
    public LogicEnemy LogicEnemy { get; private set; } = new LogicEnemy();
    public bool HasTarget { get => FieldOfView.Target != null; }

    public EnemyConfig Config { get => (EnemyConfig)_config; }
    public StateMachine<BasicEnemy> StateMachine { get; protected set; }
    public virtual EnemyIdleState IdleState { get; protected set; } = new EnemyIdleState();
    public virtual EnemyRunState RunState { get; protected set; } = new EnemyRunState();
    public virtual EnemyDetectTargetState DetectTarget { get; protected set; } = new EnemyDetectTargetState();
    public virtual EnemyAttackState AttackState { get; protected set; } = new EnemyAttackState();
    public virtual EnemyHitState HiState { get; protected set; } = new EnemyHitState();
    public virtual EnemyDeadState DeadState { get; protected set; } = new EnemyDeadState();

    [HideInInspector]
    public float delayAttack;
    [HideInInspector]
    public float delayStrikeAttack;
    [HideInInspector]
    public float distanceToTarget;

    protected new void Awake() {
        base.Awake();
        _xScale = transform.localScale.x;
        GameObject _fieldOfViewPrefab = Resources.Load(ResourcesPath.FieldOfViewPrefab) as GameObject;
        FieldOfView = Instantiate(_fieldOfViewPrefab.GetComponent<FieldOfView>());
        StateMachine = new StateMachine<BasicEnemy>(this);
    }

    public void Flip() {
        _xScale *= -1;
        gameObject.transform.localScale = new Vector3(_xScale, transform.localScale.y, transform.localScale.z);
    }

    public bool IsNeedLookOnPlayer() {
        if(FieldOfView.Target == null) {
            //print("Target is NULL");
            return false;
        }
        if (IsTargetBehindYouWhenLookRight) {
            return true;
        }
        else if (IsTargetBehindYouWhenLookLeft) {
            return true;
        }
        else {
            return false;
        }
    }

    public bool IsThereTargetInRangeOfDistance(float distancce) {
        if(distanceToTarget <= distancce) {
            return true;
        }

        return false;
    }
}