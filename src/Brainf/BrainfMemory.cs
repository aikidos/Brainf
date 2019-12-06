using System;

namespace Brainf
{
    /// <summary>
    /// Implementation of the memory usage algorithm.  
    /// </summary>
    public sealed class BrainfMemory : IBrainfMemory
    {
        private const int DefaultCapacity = 4;

        private int[] _positive;
        private int[] _negative;
        private bool _isPositive;
        private int _pointer;
        private int _cellValue;

        /// <inheritdoc />
        public int Capacity => _positive.Length + _negative.Length;

        /// <inheritdoc />
        public int Pointer
        {
            get => _isPositive ? _pointer : -_pointer;
            set
            {
                int index = Math.Abs(value);

                if (_pointer == index)
                    return;

                _isPositive = value > 0;
                _pointer = index;

                if (_isPositive)
                {
                    int length = _positive.Length;

                    if (index > length - 1)
                    {
                        Array.Resize(ref _positive, length == 0 ? DefaultCapacity : length * 2);
                    }

                    _cellValue = _positive[index];
                }
                else
                {
                    int length = _negative.Length;

                    if (index > length - 1)
                    {
                        Array.Resize(ref _negative, length == 0 ? DefaultCapacity : length * 2);
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
    }
}
