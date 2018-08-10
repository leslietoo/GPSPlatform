namespace GPS.PubSub.Abstractions
{
    public static class ProducerFactoryExtensions
    {
        public static IProducer CreateProducer(this IProducerFactory factory,ushort categoryId)
        {
            return factory.ProducerDict[categoryId];
        }

        public static void Dispose(this IProducerFactory factory, ushort categoryId)
        {
            factory.ProducerDict[categoryId].Dispose();
        }

        public static void Dispose(this IProducerFactory factory)
        {
            foreach(var item in factory.ProducerDict)
            {
                try
                {
                    item.Value.Dispose();
                }
                catch (System.Exception ex)
                {

                }
            }
        }
    }
}
