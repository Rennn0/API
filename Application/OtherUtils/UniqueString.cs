namespace Application.OtherUtils
{
    public static class UniqueString
    {
        private static readonly string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0zxcvbnmasdfghjklqwertyuiop0123456789!@#$%^&*()_+|}{:?></.,";

        /// <summary>
        ///     probability 96^length
        /// </summary>
        /// <param name="length">chars for string</param>
        /// <returns></returns>
        public static string Get(int length = 32)
        {
            Random random = new();
            string randomString = new(Enumerable.Repeat(_chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }
    }
}