using System.Collections.Generic;

namespace EntityContext
{
    public partial class User
    {
        public User()
        {
            Words = new HashSet<Word>();
        }

        public int Id { get; set; }
        public string Ip { get; set; }

        public virtual ICollection<Word> Words { get; set; }
    }
}
