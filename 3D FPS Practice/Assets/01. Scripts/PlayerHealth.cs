using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �÷��̾� ĳ������ ����ü�μ��� ������ ���
public class PlayerHealth : LivingEntity
{
    public Slider healthSlider; // ü���� ǥ���� UI �����̴�

    public AudioClip deathClip; // ��� �Ҹ�
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
        // ���ŵ� ü������ ü�� �����̴� ����
        healthSlider.value = health;
    }

    // ����� ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(!dead)
        {
            // ������� ���� ��쿡�� ȿ���� ���
            playerAudioPlayer.PlayOneShot(hitClip);
        }

        // LivingEntity�� OnDamage() ����(����� ����)
        base.OnDamage(damage, hitPoint, hitNormal);
        // ���ŵ� ü���� ü�� �����̴��� �ݿ�
        healthSlider.value = health;
    }

    // ��� ó��
    public override void Die()
    {
        // LivingEntity�� Die() ����(��� ����)
        base.Die();

        // ü�� �����̴� ��Ȱ��ȭ
        healthSlider.gameObject.SetActive(false);

        // ����� ���
        playerAudioPlayer.PlayOneShot(deathClip);
        // �ִϸ������� DieƮ���Ÿ� �ߵ����� ��� �ִϸ��̼�
        playerAnimator.SetTrigger("Die");

        // �÷��̾� ������ �޴� ������Ʈ�� ��Ȱ��ȭ
        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �����۰� �浹�� ��� �ش� �������� ����ϴ� ó��
        // ������� ���� ��쿡�� ������ ��� ����
        if(!dead)
        {
            // �浹�� �������κ��� IItem ������Ʈ �������� �õ�
            IItem item = other.GetComponent<IItem>();

            // �浹�� �������κ��� IItem ������Ʈ�� �������µ� �����ߴٸ� 
            if(item != null)
            {
                // Use �޼��带 �����Ͽ� ������ ���
                item.Use(gameObject);
                // ������ ���� �Ҹ�
                playerAudioPlayer.PlayOneShot(itemPcikupClip);
            }
        }
    }
}
