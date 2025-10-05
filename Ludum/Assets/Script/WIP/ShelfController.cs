using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class ShelfController : MonoBehaviour
{
    [Header("��������� �����")]
    public int shelfSize = 6;
    public string[] correctSequence; // ���������� ������������������ ���������

    [Header("�������")]
    public UnityEvent onCorrectSequence; // ������� ��� ���������� ������������������
    public UnityEvent onWrongSequence;   // ������� ��� ������������ ������������������

    private string[] currentSequence;    // ������� ������������������ �� �����
    private bool sequenceChecked = false; // ���� �������� ������������������

    void Start()
    {
        // �������������� ������ ��� ������� ������������������
        currentSequence = new string[shelfSize];
        for (int i = 0; i < shelfSize; i++)
        {
            currentSequence[i] = ""; // ������ ������ ��� ������ ������
        }

        // ���������, ��� ���������� ������������������ ����� ���������� ������
        if (correctSequence.Length != shelfSize)
        {
            Debug.LogError("���������� ������������������ ������ ����� ������ " + shelfSize);
        }
    }

    // ����� ��� ���������� �������� �� �����
    public bool PlaceItem(string itemName, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= shelfSize)
        {
            Debug.LogError("�������� ������ �����: " + slotIndex);
            return false;
        }

        // ���� ���� ��� �����, �� ��������� �������
        if (!string.IsNullOrEmpty(currentSequence[slotIndex]))
        {
            Debug.Log("���� ��� �����!");
            return false;
        }

        // ��������� �������
        currentSequence[slotIndex] = itemName;
        Debug.Log($"������� {itemName} �������� � ����� {slotIndex}");

        // ��������� ������������������
        CheckSequence();

        return true;
    }

    // ����� ��� ������ �������� � �����
    public bool TakeItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= shelfSize)
        {
            Debug.LogError("�������� ������ �����: " + slotIndex);
            return false;
        }

        // ���� ���� ������, �� ����� ������� �������
        if (string.IsNullOrEmpty(currentSequence[slotIndex]))
        {
            Debug.Log("���� ������!");
            return false;
        }

        string itemName = currentSequence[slotIndex];
        currentSequence[slotIndex] = "";
        Debug.Log($"������� {itemName} ������ �� ����� {slotIndex}");

        // ���������� ���� ��������, ���� ������������������ ���� ����������
        if (sequenceChecked)
        {
            sequenceChecked = false;
        }

        return true;
    }

    // ����� ��� �������� ������������������
    private void CheckSequence()
    {
        // ���������, ��� �� ����� ���������
        for (int i = 0; i < shelfSize; i++)
        {
            if (string.IsNullOrEmpty(currentSequence[i]))
            {
                return; // �� ��� ����� ��������� - �������
            }
        }

        // ��� ����� ��������� - ��������� ������������������
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
            Debug.Log("���������� ������������������! ������� ������!");
            onCorrectSequence?.Invoke();
            sequenceChecked = true;
        }
        else if (!isCorrect && !sequenceChecked)
        {
            Debug.Log("������������ ������������������! ���������� ��� ���.");
            onWrongSequence?.Invoke();
            sequenceChecked = true;
        }
    }

    // ����� ��� ��������� ������� ������������������ (��� �������)
    public string GetCurrentSequence()
    {
        return string.Join(", ", currentSequence);
    }

    // ����� ��� ������� �����
    public void ClearShelf()
    {
        for (int i = 0; i < shelfSize; i++)
        {
            currentSequence[i] = "";
        }
        sequenceChecked = false;
        Debug.Log("����� �������");
    }
}