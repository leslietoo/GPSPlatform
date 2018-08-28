namespace GPS.PubSub.Abstractions
{
    public static class ConsumerFactoryExtensions
    {
        public static IConsumer CreateConsumer(this IConsumerFactory factory, string topicName)
        {
            return factory.ConsumerDict[topicName];
        }

        public static IConsumer Subscribe(this IConsumerFactory factory, string topicName)
        {
            var consumer = factory.ConsumerDict[topicName];
            consumer?.Subscribe();
            return consumer;
        }

        public static IConsumerFactory Unsubscribe(this IConsumerFactory factory, string topicName)
        {
            factory.ConsumerDict[topicName].Unsubscribe();
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
