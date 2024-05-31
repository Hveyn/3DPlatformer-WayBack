using UnityEngine;

/// <summary>
/// PhysicsWorldSingleton
/// </summary>
public class PhysicsWorldSingleton : MonoBehaviour
{
    public static PhysicsWorldSingleton instance = null;
    
    void Start () {
        // Теперь, проверяем существование экземпляра
        if (instance == null) { // Экземпляр менеджера был найден
            instance = this; // Задаем ссылку на экземпляр объекта
        } else if(instance == this){ // Экземпляр объекта уже существует на сцене
            Destroy(gameObject); // Удаляем объект
        }

        // Теперь нам нужно указать, чтобы объект не уничтожался
        // при переходе на другую сцену игры
        DontDestroyOnLoad(gameObject);
        
    }

    public static  int SphereCastNonAlloc(Transform origin, float radius, RaycastHit[] hits, float distance, LayerMask layers)
    {
        int hitCount = Physics.SphereCastNonAlloc(
            origin.position,
            radius,
            -origin.up,
            hits,
            distance,
            layers);
        
        Debug.Log("Cast");
        return hitCount;
    }

    
}
