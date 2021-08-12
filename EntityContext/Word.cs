namespace EntityContext
{
    public partial class Word
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Word1 { get; set; }
        public int Count { get; set; }

        public virtual User User { get; set; }
    }
}
