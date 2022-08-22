namespace PracticeNotice.Models
{
    public class Customer
    {
        public List<Notice> Notices { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
