namespace RSI_REST_SERVER.Models
{
    public class Link
    {
        public string linkValue { get; set; }
        public string rel { get; set; }
        public Link() { }
        public Link(string LinkValue, string Rel)
        {
            this.linkValue = LinkValue;
            this.rel = Rel;
        }
    }
}
