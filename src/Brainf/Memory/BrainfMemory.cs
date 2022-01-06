using System;

namespace Brainf.Memory;

/// <summary>
/// Implementation of the memory usage algorithm.
/// </summary>
public sealed class BrainfMemory : IBrainfMemory
{
    private const int DefaultCapacity = 4;

    private int[] _positive;
    private int[] _negative;
    private BrainfMemoryPointer _pointer;
    private int _cellValue;
    private bool _needUpdateCell;

    /// <inheritdoc />
    public int Capacity => _positive.Length + _negative.Length;

    /// <inheritdoc />
    public int Pointer
    {
        get => _pointer.AbsoluteIndex;
        set
        {
            var index = Math.Abs(value);

            if (_pointer.RelativeIndex == index)
            {
                return;
            }

            if (_needUpdateCell)
            {
                SaveCellValue(_pointer, _cellValue);
                _needUpdateCell = false;
            }

            _pointer = new BrainfMemoryPointer(index, value < 0);

            _cellValue = GetSavedCellValue(_pointer);
        }
    }

    /// <inheritdoc />
    public int CellValue
    {
        get => _cellValue;
        set
        {
            if (_cellValue == value)
            {
                return;
            }

            _cellValue = value;

            _needUpdateCell = true;
        }
    }

    /// <summary>
    /// Initializes a new <see cref="BrainfMemory"/>.
    /// </summary>
    /// <param name="capacityPositive">The total number of positive memory cells.</param>
    /// <param name="capacityNegative">The total number of negative memory cells.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     The <paramref name="capacityPositive"/> parameter is less than zero.
    ///     The <paramref name="capacityNegative"/> parameter is less than zero.
    /// </exception>
    public BrainfMemory(int capacityPositive = 0, int capacityNegative = 0)
    {
        if (capacityPositive < 0)
            throw new ArgumentOutOfRangeException(nameof(capacityPositive));
        if (capacityNegative < 0)
            throw new ArgumentOutOfRangeException(nameof(capacityNegative));

        _positive = new int[capacityPositive];
        _negative = new int[capacityNegative];
    }

    private void SaveCellValue(BrainfMemoryPointer pointer, int value)
    {
        ref var array = ref pointer.IsNegative
            ? ref _negative
            : ref _positive;

        EnsureCapacity(ref array, pointer.RelativeIndex);

        array[pointer.RelativeIndex] = value;
    }

    private static void EnsureCapacity(ref int[] array, int index)
    {
        var length = array.Length;

        while (index >= length)
        {
            length = length == 0
                ? DefaultCapacity
                : length * 2;
        }

        Array.Resize(ref array, length);
    }

    private int GetSavedCellValue(BrainfMemoryPointer pointer)
    {
        var array = pointer.IsNegative
            ? _negative
            : _positive;

        return pointer.RelativeIndex < array.Length
            ? array[pointer.RelativeIndex]
            : 0;
    }
}