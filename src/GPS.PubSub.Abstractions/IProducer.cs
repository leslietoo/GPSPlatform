namespace GPS.PubSub.Abstractions
{
    public interface IProducer:IPubSub
    {
        void ProduceAsync(byte[] data);
    }
}
