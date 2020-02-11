using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{

    public float speed;
	public int damage; // 총알 데미지
	private float MoveTime = 0.2f;

	void Start () 
	{
		Destroy (this.gameObject, 0.2f); // 0.2초 뒤 자기 오브젝트 삭제
	}

	void Update ()
	{
		if (Input.GetMouseButton (0)) // 마우스 클릭 시
		{ 
			Move (Camera.main.ScreenToWorldPoint (Input.mousePosition)); // 총알 이동
		}

	}
			
	void Move (Vector2 target)
	{
		//target.z = -0.75f; // 2D 평면 이동이니 z는 0으로
		transform.position = Vector2.MoveTowards(transform.position, target, speed * MoveTime);//Time.deltaTime);
		//이 함수를 한번 호출할때마다 target과 아주 조금 가까워진다. Update에서 주기적으로 일어나서 이동이 가능한것
	}

  
		void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "OuterWall")
		{
			Destroy (this.gameObject);
		}
	}

}
