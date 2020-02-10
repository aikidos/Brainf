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
        private int _pointer;
        private int _cellValue;
        private bool _isPositive;
        private bool _needUpdateCell;

        /// <inheritdoc />
        public int Capacity => _positive.Length + _negative.Length;

        /// <inheritdoc />
        public int Pointer
        {
            get => _isPositive ? _pointer : -_pointer;
            set
            {
                // Note!!
                // Small duplication of the resizing arrays logic is used as optimization.
                // `MethodImplOptions.AggressiveInlining` also affects performance, albeit slightly.

                var index = Math.Abs(value);

                if (_pointer == index)
                {
                    return;
                }

                if (_needUpdateCell)
                {
                    if (_isPositive)
                    {
                        int length;

                        while (_pointer >= (length = _positive.Length))
                        {
                            Array.Resize(ref _positive, length == 0 ? DefaultCapacity : length * 2);
                        }

                        _positive[_pointer] = _cellValue;
                    }
                    else
                    {
                        int length;

                        while (_pointer >= (length = _negative.Length))
                        {
                            Array.Resize(ref _negative, length == 0 ? DefaultCapacity : length * 2);
                        }

                        _negative[_pointer] = _cellValue;
                    }

                    _needUpdateCell = false;
                }

                _isPositive = value > 0;
                _pointer = index;

                if (_isPositive)
                {
                    _cellValue = index < _positive.Length 
                        ? _positive[_pointer] 
                        : 0;
                }
                else
                {
                    _cellValue = index < _negative.Length 
                        ? _negative[_pointer] 
                        : 0;
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
    }
}
