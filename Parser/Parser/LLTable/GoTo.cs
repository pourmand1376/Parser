namespace Parser.State
{
    /// <summary>
    /// Go to Record
    /// </summary>
    public class GoTo
    {
        public int StateId { get; set; }

        public GoTo(int stateId)
        {
            StateId = stateId;
        }
    }
}