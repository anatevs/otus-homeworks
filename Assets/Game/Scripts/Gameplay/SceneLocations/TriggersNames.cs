namespace SampleGame
{
    public static class TriggersNames
    {
        private static readonly string[] _namePattern = 
            new string[2] { "[Trigger", "]" };

        public static bool IsTriggerName(string name)
        {
            return name.Contains(_namePattern[0]) &&
                name.Contains(_namePattern[1]);
        }

        public static string GetTriggerIndex(string triggerName)
        {
            return triggerName[(_namePattern[0].Length)..
                ^_namePattern[1].Length];
        }
    }
}