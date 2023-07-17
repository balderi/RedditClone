using HashidsNet;

namespace RedditClone.Shared
{
    public static class Hashing
    {
        public static string Encode(int number)
        {
            Guid guid = Guid.NewGuid();
            Hashids hashids = new Hashids(guid.ToString(), 7, "abcdefghijklmnopqrstuvwxyz0123456789", "cfhistu");
            return hashids.Encode(number);
        }
    }
}
