using System;

namespace Brainf
{
    /// <summary>
    /// Implementation of the memory usage algorithm.  
    /// </summary>
    public sealed class BrainfMemory : IBrainfMemory
    {
        private int[] _positive = new int[1];
        private int[] _negative = new int[1];
        private bool _isPositive;
        private int _pointer;
        private int _cellValue;

        /// <inheritdoc />
        public int Pointer
        {
            get => _isPositive ? _pointer : -_pointer;
            set
            {
                if (_pointer == value)
                    return;

                int index = Math.Abs(value);

                if (_pointer == index)
                    return;

                _isPositive = value > 0;
                _pointer = index;

                if (_isPositive)
                {
                    if (index >= _positive.Length)
                    {
                        while (index >= _positive.Length)
                            Array.Resize(ref _positive, _positive.Length * 2);
                    }

                    _cellValue = _positive[index];
                }
                else
                {
                    if (index >= _negative.Length)
                    {
                        while (index >= _negative.Length)
                            Array.Resize(ref _negative, _negative.Length * 2);
                    }

                    _cellValue = _negative[index];
                }
            }
        }

        /// <inheritdoc />
        public int CellValue
        {
            get => _cellValue;
            set
            {
                if (_cellValue == value)
                    return;

                _cellValue = value;
                
                if (_isPositive)
                    _positive[_pointer] = value;
                else
                    _negative[_pointer] = value;
            }
        }
    }
}
