internal class BiosTweakerBase
{
    protected int offset;
    protected byte[] buffer;

    public BiosTweakerBase(byte[] buffer, int startIndex)
    {
        this.offset = startIndex;
        this.buffer = buffer;
    }
}
