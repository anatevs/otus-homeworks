namespace SampleGame
{
    public static class TriggersNames
    {
        private static readonly string[] _nameTemplate = 
            new string[2] { "[Trigger", "]" };

        public static bool IsTriggerName(string name)
        {
            return name.Contains(_nameTemplate[0]) &&
                name.Contains(_nameTemplate[1]);
        }

        public static string GetTriggerIndex(string triggerName)
        {
            return triggerName[(_nameTemplate[0].Length)..
                ^_nameTemplate[1].Length];
        }
    }
}