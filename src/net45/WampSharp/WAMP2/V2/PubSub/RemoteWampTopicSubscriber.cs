﻿using System.Collections.Generic;
using WampSharp.V2.Core;
using WampSharp.V2.Core.Contracts;

namespace WampSharp.V2.PubSub
{
    internal class RemoteWampTopicSubscriber : IRemoteWampTopicSubscriber
    {
        private readonly IWampClientProxy mSubscriber;
        private readonly long mSubscriptionId;
        private readonly WampIdGenerator mIdGenerator = new WampIdGenerator();

        public RemoteWampTopicSubscriber(long subscriptionId, IWampSubscriber subscriber)
        {
            mSubscriber = subscriber as IWampClientProxy;
            mSubscriptionId = subscriptionId;
        }

        public long SessionId
        {
            get
            {
                return mSubscriber.Session;
            }
        }

        public long SubscriptionId
        {
            get
            {
                return mSubscriptionId;
            }
        }

        private long GeneratePublicationId()
        {
            return mIdGenerator.Generate();
        }

        public void Event(EventDetails details)
        {
            long publicationId = GeneratePublicationId();
            mSubscriber.Event(this.SubscriptionId, publicationId, details);
        }

        public void Event(EventDetails details, object[] arguments)
        {
            long publicationId = GeneratePublicationId();
            mSubscriber.Event(this.SubscriptionId, publicationId, details, arguments);
        }

        public void Event(EventDetails details, object[] arguments, IDictionary<string, object> argumentsKeywords)
        {
            long publicationId = GeneratePublicationId();
            mSubscriber.Event(this.SubscriptionId, publicationId, details, arguments, argumentsKeywords);
        }
    }
}