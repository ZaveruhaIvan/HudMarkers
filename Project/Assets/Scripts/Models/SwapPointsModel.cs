using UnityEngine;

namespace Models
{
    public class SwapPointsModel
    {
        private const int MinSwapPoint = 0;

        private readonly int _maxSwapPoint;

        public SwapPointsModel(int swapPointAmount, int current)
        {
            SwapPointAmount = swapPointAmount;
            _maxSwapPoint = SwapPointAmount - 1;
            Current = Mathf.Clamp(current, MinSwapPoint, SwapPointAmount);
        }
    
        public void SetPoint(int index)
        {
            index = Mathf.Clamp(index, MinSwapPoint, _maxSwapPoint);
            Current = index;
        }
    
        public int Current { get; private set; }
        private int SwapPointAmount { get; }
    }
}