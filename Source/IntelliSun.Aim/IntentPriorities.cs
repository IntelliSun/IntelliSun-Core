using System;

namespace IntelliSun.Aim
{
    public static class IntentPriorities
    {
        public static readonly IntentPriority Zero;
        public static readonly IntentPriority Integral;

        public static readonly IntentPriority Default;
        public static readonly IntentPriority Convention;
        public static readonly IntentPriority UserConvention;
        public static readonly IntentPriority Preferred;

        static IntentPriorities()
        {
            IntentPriorities.Zero = new IntentPriority(UInt32.MinValue);
            IntentPriorities.Integral = new IntentPriority(UInt32.MaxValue);

            IntentPriorities.Default = new IntentPriority(0x01);
            IntentPriorities.Convention = new IntentPriority(0xAA);
            IntentPriorities.UserConvention = new IntentPriority(0xBB);
            IntentPriorities.Preferred = new IntentPriority(0xCC);
        }

        public static IntentPriority Higher(IntentPriority priority)
        {
            if (priority.Value == UInt32.MaxValue)
                throw new ArgumentException("${Resources.CannotSetPriorityHigherIntegral}", "priority");

            return new IntentPriority(priority.Value + 1);
        }

        public static IntentPriority Lower(IntentPriority priority)
        {
            if (priority.Value == UInt32.MaxValue)
                throw new ArgumentException("${Resources.CannotSetPriorityLowerZero}", "priority");

            return new IntentPriority(priority.Value + 1);
        }
    }
}