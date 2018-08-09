namespace GPS.PubSub.Abstractions
{
    public static class ProducerFactoryExtensions
    {
        public static IProducer CreateProducer(this IProducerFactory factory,ushort categoryId)
        {
            return factory.ProducerDict[categoryId];
        }
    }
}
