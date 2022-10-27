using System.Collections;
using UnityEngine;


// ���� ����
public class Gun : MonoBehaviour
{
    // ���� ���¸� ǥ���ϴ� �� ����� Ÿ���� ����
    
    public enum State
    {
        Ready, // �߻� �غ��
        Empty, // źâ�� ��
        Reloading, // ������ ��
    }

    public State state { get; private set; } // ���� ���� ����

    public Transform fireTransform; // ź���� �߻�� ��ġ

    public ParticleSystem muzzleFalshEffect; // �ѱ� ȭ�� ȿ��
    public ParticleSystem shellEjectEffect; // ź�� ���� ȿ��

    // public ParticleSystem[] particlesystem;

    private LineRenderer bulletLineRenderer; // ź�� ������ �׸��� ���� ������


    private AudioSource gundAudioPlayer; // �� �Ҹ� �����

    public GunData gunData; // ���� ���� ������

    private float fireDistance = 50f; // �����Ÿ�

    public int ammoRemain = 100; // ���� ��ü ź��
    public int magAmmo; // ���� źâ�� ���� �ִ� ź��

    private float lastFireTime; // ���� ���������� �߻��� ����

    private void Awake()
    {
        // ����� ������Ʈ�� ���� ��������
        gundAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        // ����� ���� �� ���� ����
        bulletLineRenderer.positionCount = 2;
        // ���� �������� ��Ȱ��ȭ
        bulletLineRenderer.enabled = false;

        // particlesystem = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        // �� ���� �ʱ�ȭ
        // ��ü ���� ź�� ���� �ʱ�ȭ
        ammoRemain = gunData.startAmmoRemain;
        // ���� źâ�� ���� ä���
        magAmmo = gunData.magCapacity;

        // ���� ���� ���¸� ���� �� �غ� �� ���·� ����
        state = State.Ready;
        // ���������� ���� �� ������ �ʱ�ȭ
        lastFireTime = 0;
    }

    // �߻� �õ�
    public void Fire()
    {
        // ���� ���°� �߻� ������ ����
        // && ������ �� �߻� �������� gunData.timeBetFire ���� �ð��� ����
        if(state == State.Ready && Time.time >= lastFireTime + gunData.timeBetFire)
        {
            // ������ �� �߻� ���� ����
            lastFireTime = Time.time;
            // ���� �߻� ó�� ����
            Shot();
        }
    }

    // ���� �߻� ó��
    private void Shot()
    {
        // ����ĳ��Ʈ�� ���� �浹 ������ �����ϴ� �����̳�
        RaycastHit hit;
        // ź���� ���� ���� ������ ����
        Vector3 hitPosition = Vector3.zero;

        // ����ĳ��Ʈ(���� ����, ����, �浹 ���� �����̳�, �����Ÿ�)
        if(Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            // ���̰� � ��ü�� �浹�� ���

            // �浹�� �������κ��� IDamageable ������Ʈ �������� �õ�
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            // �������κ��� IDamageable ������Ʈ�� �������� �� �����ߴٸ�
            if(target != null)
            {
                // �������κ��� OnDamage �Լ��� ������� ���濡 ����� �ֱ�
                target.OnDamage(gunData.damage, hit.point, hit.normal);
            }
            //���̰� �浹�� ��ġ ����
            hitPosition = hit.point;
        }
        else
        {
            // ���̰� �ٸ� ��ü�� �浹���� �ʾҴٸ�
            // ź���� �ִ� �����Ÿ����� ���ư��� ���� ��ġ�� �浹 ��ġ�� ���
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        // �߻� ����Ʈ ��� ����
        StartCoroutine(ShotEffect(hitPosition));

        // ���� ź�� ���� -1
        magAmmo--;
        if(magAmmo <= 0)
        {
            // źâ�� ���� ź���� ���ٸ� ���� ���� ���¸� Empty�� ����
            state = State.Empty;
        }
    }

    // �߻� ����Ʈ�� �Ҹ��� ����ϰ� ź�� ������ �׸�
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        // �ѱ� ȭ�� ȿ�� ���
        muzzleFalshEffect.Play();
        // ź�� ���� ȿ�� ���
        shellEjectEffect.Play();

        // �Ѱ� �Ҹ� ���
        gundAudioPlayer.PlayOneShot(gunData.shotClip);
        // ���� �������� �ѱ��� ��ġ
        bulletLineRenderer.SetPosition(0, fireTransform.position);
        // ���� ������ �Է����� ���� �浹 ��ġ
        bulletLineRenderer.SetPosition(1, hitPosition);
        // ���� �������� Ȱ��ȭ�Ͽ� ź�� ������ �׸�
        bulletLineRenderer.enabled = true;

        // 0.03�� ���� ��� ó���� ���
        yield return new WaitForSeconds(0.03f);

        // ���� �������� ��Ȱ��ȭ�Ͽ� ź�� ������ ����
        bulletLineRenderer.enabled = false;
    }

    // ������ �õ�
    public bool Reload()
    {
        return false;
    }

    // ���� ������ ó���� ����
    private IEnumerator ReloadRoutine()
    {
        // ���� ���¸� ������ �� ���·� ��ȣ��
        state = State.Reloading;

        // ������ �ҿ� �ð���ŭ ó�� ����
        yield return new WaitForSeconds(gunData.reloadTime);

        // ���� ���� ���¸� �߻� �غ�� ���·� ����
        state = State.Ready;
    }
}
