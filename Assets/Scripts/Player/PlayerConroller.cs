using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerConroller : MonoBehaviour
{
	[Tooltip("Начальное здоровье игрока")]
	[SerializeField] private float healt = 10f;
	[SerializeField] private Text healtText,scoreText;

	private void Start()
	{
		healtText.text = "Healt:" + healt.ToString();
	}
	public void FixedUpdate()
	{
		if (Input.GetMouseButton(0))//Отслеживание нажатия на экран 
		{
			Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Запись в переменную pos координат места, где произошло касание экрана.
			transform.position = pos;//присвоение позиции игровому объекту координат из переменной pos
			transform.position = new Vector2(transform.position.x, transform.position.y + 1f);//корректировка координат игрока(необязательно)
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		var obj = col.gameObject;
		if (EnemySpawner.Enemies.ContainsKey(obj))
		{
			
			healt -= EnemySpawner.Enemies[obj].Attack;
			healtText.text = "Healt:" + healt.ToString();
			if (healt < 0)
			{
				Destroy(gameObject);
				SceneManager.LoadScene(0);
			}
		}
	}
}
