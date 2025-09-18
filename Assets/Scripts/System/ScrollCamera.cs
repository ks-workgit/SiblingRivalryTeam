using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour
{
	[SerializeField] GameObject cameraGuide;

	private float cameraGuideX = 0.0f;    //����X���W�擾
	private float cameraY = 0.0f;  //�J������Y���W�擾
	private float cameraZ = 0.0f;  //�J������Z���W�擾
	private float offsetX = 0.0f;  //�L�����ƃJ������X���W�I�t�Z�b�g


	void Start()
	{
		//�J�����ƃ{�[����X�������I�t�Z�b�g�Ƃ��Ď�荞��
		offsetX = transform.position.x - cameraGuide.transform.position.x;

		//Y�EZ���͑��삵�Ȃ��̂ŏ����ʒu���擾���Ă���
		cameraY = transform.position.y;
		cameraZ = transform.position.z;

		//�����̃{�[��X�����擾����
		cameraGuideX = cameraGuide.transform.position.x;
	}

	void Update()
	{
		//�{�[�����E�ړ����Ă鎞�����{�[��X���W���擾����
		if (cameraGuideX < cameraGuide.transform.position.x)
			cameraGuideX = cameraGuide.transform.position.x;

		//�{�[���Ƃ̃I�t�Z�b�g���v�Z���č��W�ړ�������
		transform.position = new Vector3(cameraGuideX + offsetX, cameraY, cameraZ);
	}
}
