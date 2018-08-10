namespace GPS.PubSub.Abstractions
{
    public static class ConsumerFactoryExtensions
    {
        public static IConsumer CreateConsumer(this IConsumerFactory factory,ushort categoryId)
        {
            return factory.ConsumerDict[categoryId];
        }

        public static IConsumer Subscribe(this IConsumerFactory factory, ushort categoryId)
        {
            var consumer = factory.ConsumerDict[categoryId];
            consumer?.Subscribe();
            return consumer;
        }

        public static IConsumerFactory Unsubscribe(this IConsumerFactory factory, ushort categoryId)
        {
            factory.ConsumerDict[categoryId].Unsubscribe();
            return factory;
        }

        public static void Subscribe(this IConsumerFactory factory)
        {
            foreach(var item in factory.ConsumerDict)
            {
                try
                {
                    item.Value.Subscribe();
                }
                catch
                {

                }
            }
        }

        public static void Unsubscribe(this IConsumerFactory factory)
        {
            foreach (var item in factory.ConsumerDict)
            {
                try
                {
                    item.Value.Unsubscribe();
                }
                catch
                {

                }
            }
        }
    }
}
