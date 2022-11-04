using System.Collections;
using UnityEngine;
using UnityEngine.AI; // AI, ������̼� �ý��� ���� �ڵ� ��������

// ���� AI ����
public class Zombie : LivingEntity
{
    public LayerMask whatIsTarget; // ���� ��� ���̾�

    private LivingEntity targetEntity; // ���� ���
    private NavMeshAgent naveMeshAgent; // ��� ��� AI ������Ʈ

    public ParticleSystem hitEffect; // �ǰ� �� ����� ��ƼŬ ȿ��
    public AudioClip deathSound; // ��� �� ����� �Ҹ�
    public AudioClip hitSound; // �ǰ� �� ����� �Ҹ�

    private Animator zombieAnimator; // �ִϸ����� ������Ʈ
    private AudioSource zombieAudioPlayer; // ����� �ҽ� ������Ʈ
    private Renderer zombieRenderer; // ������ ������Ʈ

    public float damage = 20f; // ���ݷ�
    public float timeBetAttack = 0.5f; // ���� ����
    private float lastAttackTime; // ������ ���� ����

    // ������ ����� �����ϴ��� �˷��ִ� ������Ƽ
    private bool hasTarget
    {
        get
        {
            // ������ ����� �����ϰ�, ����� ������� �ʾҴٸ� true
            if(targetEntity != null && !targetEntity.dead)
            {
                return true;
            }

            // �׷��� �ʴٸ� false
            return false;
        }
    }

    private void Awake()
    {
        // �ʱ�ȭ
    }

    // ���� AI�� �ʱ� ������ �����ϴ� �¾� �޼���
    public void Setup(ZombieData zombieData)
    {
    
    }

    private void Start()
    {
        // ���� ������Ʈ Ȱ��ȭ�� ���ÿ� AI�� ���� ��ƾ ����
        StartCoroutine(UpdatePath());

    }
    // Start is called before the first frame update
    void Update()
    {
        // ���� ����� ���� ���ο� ���� �ٸ� �ִϸ��̼� ���
        zombieAnimator.SetBool("HasTarget", hasTarget);
    }


    //�ֱ������� ������ ����� ��ġ�� ã�� ��� ����
    private IEnumerator UpdatePath()
    {
        // ��� �ִ� ���� ���� ����
        while(!dead)
        {
            // 0.25�� �ֱ�� ó�� �ݺ�
            yield return new WaitForSeconds(0.25f);
        }
    }

    // ������� �Ծ��� �� ������ ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        // LivingEntity�� OnDamage()�� �����Ͽ� ����� ����
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    // ���ó��
    public override void Die()
    {
        // LivingEntity�� Die()�� �����Ͽ� �⺻ ��� ó�� ����
        base.Die();
    }

    private void OnTriggerStay(Collider other)
    {
        // Ʈ���� �浹�� ���� ���� ������Ʈ�� ���� ����̶�� ���� ���� 
    }
}
