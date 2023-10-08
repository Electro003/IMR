using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class BearBehaivour : MonoBehaviour
{
    private GameObject[] gameObjects;
    private Animator animator;
    private Vector3 position;

    void Start()
    {
        gameObjects = RemoveCurrentObjectFromList(GameObject.FindGameObjectsWithTag("Bear"));
        animator = GetComponent<Animator>();
        position = GetComponent<Transform>().position;
        ChangeBearAnimation(gameObjects);
    }

    private void Update()
    {
        ChangeBearAnimation(gameObjects);
    }

    GameObject[] RemoveCurrentObjectFromList(GameObject[] objects)
    {
        GameObject[] filteredObjects = new GameObject[objects.Length - 1];
        int currentIndex = 0;

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != gameObject)
            {
                filteredObjects[currentIndex] = objects[i];
                currentIndex++;
            }
        }

        return filteredObjects;
    }

    void ChangeBearAnimation(GameObject[] gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
            //if (Vector3.Distance(gameObject.GetComponent<Transform>().position, position) < 100)
            //{
                animator.SetTrigger("Attack1");
                gameObject.GetComponent<Animator>().SetTrigger("Attack1");
            //}
        }
    }
}
