using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class ShelfController : MonoBehaviour
{
    [Header("Настройки полки")]
    public int shelfSize = 6;
    public string[] correctSequence; // Правильная последовательность предметов

    [Header("События")]
    public UnityEvent onCorrectSequence; // Событие при правильной последовательности
    public UnityEvent onWrongSequence;   // Событие при неправильной последовательности

    private string[] currentSequence;    // Текущая последовательность на полке
    private bool sequenceChecked = false; // Флаг проверки последовательности

    void Start()
    {
        // Инициализируем массив для текущей последовательности
        currentSequence = new string[shelfSize];
        for (int i = 0; i < shelfSize; i++)
        {
            currentSequence[i] = ""; // Пустые строки для пустых слотов
        }

        // Проверяем, что правильная последовательность имеет правильный размер
        if (correctSequence.Length != shelfSize)
        {
            Debug.LogError("Правильная последовательность должна иметь размер " + shelfSize);
        }
    }

    // Метод для размещения предмета на полке
    public bool PlaceItem(string itemName, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= shelfSize)
        {
            Debug.LogError("Неверный индекс слота: " + slotIndex);
            return false;
        }

        // Если слот уже занят, не размещаем предмет
        if (!string.IsNullOrEmpty(currentSequence[slotIndex]))
        {
            Debug.Log("Слот уже занят!");
            return false;
        }

        // Размещаем предмет
        currentSequence[slotIndex] = itemName;
        Debug.Log($"Предмет {itemName} размещен в слоте {slotIndex}");

        // Проверяем последовательность
        CheckSequence();

        return true;
    }

    // Метод для забора предмета с полки
    public bool TakeItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= shelfSize)
        {
            Debug.LogError("Неверный индекс слота: " + slotIndex);
            return false;
        }

        // Если слот пустой, не можем забрать предмет
        if (string.IsNullOrEmpty(currentSequence[slotIndex]))
        {
            Debug.Log("Слот пустой!");
            return false;
        }

        string itemName = currentSequence[slotIndex];
        currentSequence[slotIndex] = "";
        Debug.Log($"Предмет {itemName} забран из слота {slotIndex}");

        // Сбрасываем флаг проверки, если последовательность была правильной
        if (sequenceChecked)
        {
            sequenceChecked = false;
        }

        return true;
    }

    // Метод для проверки последовательности
    private void CheckSequence()
    {
        // Проверяем, все ли слоты заполнены
        for (int i = 0; i < shelfSize; i++)
        {
            if (string.IsNullOrEmpty(currentSequence[i]))
            {
                return; // Не все слоты заполнены - выходим
            }
        }

        // Все слоты заполнены - проверяем последовательность
        bool isCorrect = true;
        for (int i = 0; i < shelfSize; i++)
        {
            if (currentSequence[i] != correctSequence[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect && !sequenceChecked)
        {
            Debug.Log("Правильная последовательность! Загадка решена!");
            onCorrectSequence?.Invoke();
            sequenceChecked = true;
        }
        else if (!isCorrect && !sequenceChecked)
        {
            Debug.Log("Неправильная последовательность! Попробуйте еще раз.");
            onWrongSequence?.Invoke();
            sequenceChecked = true;
        }
    }

    // Метод для получения текущей последовательности (для отладки)
    public string GetCurrentSequence()
    {
        return string.Join(", ", currentSequence);
    }

    // Метод для очистки полки
    public void ClearShelf()
    {
        for (int i = 0; i < shelfSize; i++)
        {
            currentSequence[i] = "";
        }
        sequenceChecked = false;
        Debug.Log("Полка очищена");
    }
}