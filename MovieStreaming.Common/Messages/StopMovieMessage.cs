namespace MovieStreaming.Common.Messages
{
    public class StopMovieMessage
    {
        public int UserId { get; }

        public StopMovieMessage(int userId)
        {
            UserId = userId;
        }
    }
}