namespace MathGame
{
    public static class RandomExtensions
    {
        public static Operator NextOperation(this Random rnd)
        {
            int index = rnd.Next(0, 4);
            if (index == 0)
                return Operator.Add;
            else if (index == 1)
                return Operator.Subtract;
            else if (index == 2)
                return Operator.Multiply;
            else
                return Operator.Divide;

        }
    }
}
