using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �÷��̾� ĳ������ ����ü�μ��� ������ ���
public class PlayerHealth : LivingEntity
{
    public Slider healthSlider; // ü���� ǥ���� UI �����̴�

    public AudioClip audioClip; // ��� �Ҹ�
    public AudioClip hitClip; // �ǰ� �Ҹ�
    public AudioClip itemPcikupClip; //  ������ ���� �Ҹ�

    private AudioSource playerAudioPlayer; // �÷��̾� �Ҹ� �����
    private Animator playerAnimator; // �÷��̾��� �ִϸ�����

    private PlayerMovement playerMovement; // �÷��̾� ������ ������Ʈ
    private PlayerShooter playerShooter; // �÷��̾� ���� ������Ʈ

    private void Awake()
    {
        // ����� ������Ʈ ��������
        playerAnimator = GetComponent<Animator>();
        playerAudioPlayer = GetComponent<AudioSource>();

        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();

    }

    protected override void OnEnable()
    {
        // LivingEntity�� OnEnable() ����(���� �ʱ�ȭ)
        base.OnEnable();

        // ü�� �����̴� Ȱ��ȭ
        healthSlider.gameObject.SetActive(true);
        // ü�� �����̴��� �ִ��� �⺻ ü�°����� ����
        healthSlider.maxValue = startingHealth;
        // ü�� �����̴��� ���� ���� ü�°����� ����
        healthSlider.value = health;

        // �÷��̾� ������ �޴� ������Ʈ Ȱ��ȭ
        playerMovement.enabled = true;
        playerShooter.enabled = true;
    }

    // ü�� ȸ��
    public override void RestoreHealth(float newHealth)
    {
        // LivingEntity�� RestorHealth() ����(ü�� ����)
        base.RestoreHealth(newHealth);
    }

    // ����� ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        // LivingEntity�� OnDamage() ����(����� ����)
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    // ��� ó��
    public override void Die()
    {
        // LivingEntity�� Die() ����(��� ����)
        base.Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �����۰� �浹�� ��� �ش� �������� ����ϴ� ó��
    }
}
