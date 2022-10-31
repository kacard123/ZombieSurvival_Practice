using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �־��� Gun ������Ʈ�� ��ų� ������
// �˸��� �ִϸ��̼��� ����ϰ� IK�� ����� ĳ���� ����� �ѿ� ��ġ�ϵ��� ����

public class PlayerShooter : MonoBehaviour
{
    public Gun gun; // ����� ��
    public Transform gunPivot; // �� ��ġ�� ������
    public Transform leftHandMount; // ���� ���� ������, �޼��� ��ġ�� ����
    public Transform rightHandMount; //  ���� ������ ������, �������� ��ġ�� ����

    private PlayerInput playerInput; // �÷��̾��� �Է�
    private Animator playerAnimator; // �ִϸ����� ������Ʈ
    // Start is called before the first frame update
    void Start()
    {
        // ����� ������Ʈ ��������
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // ���Ͱ� Ȱ��ȭ�� �� �ѵ� �Բ� Ȱ��ȭ
        gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        // ���Ͱ� ��Ȱ��ȭ�� �� �ѵ� �Բ� ��Ȱ��ȭ
        gun.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �Է��� �����ϰ� ���� �M���ϰų� ������
        if(playerInput.fire) // �޼����̱� ������ ������Ƽ 
        {
            // �߻� �Է� ���� �� �� �߻�
            gun.Fire();
        }
        else if(playerInput.reload)
        {
            // ������ �Է� ���� �� ������
            if(gun.Reload())
            {
                // ������ ���� �ÿ��� ������ �ִϸ��̼� ���
                playerAnimator.SetTrigger("Reload");
            }
        }
        // ���� ź�� UI ����
        UpdateUI();
    }

    // ź�� UI ����
    private void UpdateUI()
    {
        if(gun != null && UIManager.instance != null)
        {
            // UI �Ŵ����� ź�� �ؽ�Ʈ�� źâ�� ź�˰� ���� ��ü ź�� ǥ��
            UIManager.instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain); // ��ȣ ���� �Ű��������� '�μ�'
        }
    }
    // �ִϸ������� IK ����
    private void OnAnimatorIK(int layerIndex)
    {
        // ���� ������ gunPivot�� 3D ���� ������ �Ȳ�ġ ��ġ�� �̵�
        gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        // IK�� ����Ͽ� �޼��� ��ġ�� ȸ���� ���� ���� �����̿� ����
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        // IK�� ����Ͽ� �������� ��ġ�� ȸ���� ���� ������ �����̿� ����
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    }
}
