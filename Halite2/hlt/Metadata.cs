namespace Halite2.hlt
{
    public class Metadata
    {
        private string[] metadata;
        private int index = 0;

        public Metadata(string[] metadata)
        {
            this.metadata = metadata;
        }

        public string pop()
        {
            return metadata[index++];
        }

        public bool isEmpty()
        {
            return index == metadata.Length;
        }
    }
}
