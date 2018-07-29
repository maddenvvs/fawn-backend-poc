namespace Fawn.DAL.Models
{
    public enum OrderStatus : byte
    {
        New = 0,

        InProgress = 1,

        Done = 2,

        Cancelled = 3
    }
}