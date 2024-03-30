namespace Application.OtherUtils
{
    public static class ObjectSize
    {
        public static long Get(object obj)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(obj);
            return System.Text.Encoding.UTF8.GetByteCount(json);
        }
    }
}