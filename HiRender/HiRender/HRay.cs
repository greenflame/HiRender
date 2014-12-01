namespace HiRender
{
    internal class HRay
    {
        public HRay(HPoint source, HPoint direction)
        {
            Source = source;
            Direction = direction;
        }

        public HPoint Source { get; set; }
        public HPoint Direction { get; set; }
    }
}