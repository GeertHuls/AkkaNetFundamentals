namespace MovieStreamingConsole.Messages
{
    public class StopMovieMessage
    {
        private int userId;

        public StopMovieMessage(int userId)
        {
            this.userId = userId;
        }
    }
}