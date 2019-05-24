namespace EventSourcedVouchers.Api.EventSourcing
{
    using System.Collections.Generic;
    using EventSourcedVouchers.Api.Exceptions;
    using EventSourcedVouchers.Api.Models;

    public class VoucherEventStore
    {
        private readonly Dictionary<string, List<IVoucherEvent>> eventStreams;

        public VoucherEventStore()
        {
            this.eventStreams = new Dictionary<string, List<IVoucherEvent>>();
        }

        public void AppendEvent(string streamId, IVoucherEvent voucherEvent)
        {
            if (!this.eventStreams.ContainsKey(streamId))
            {
                this.eventStreams.Add(streamId, new List<IVoucherEvent> {voucherEvent});
            }
            else
            {
                this.eventStreams[streamId].Add(voucherEvent);
            }
        }

        public Voucher ProjectReadModel(string streamId)
        {
            if (!this.eventStreams.ContainsKey(streamId))
            {
                throw new VoucherNotFoundException(streamId);
            }
            
            var voucherBuilder = new VoucherBuilder(streamId);

            var eventStream = this.eventStreams[streamId];
            foreach (var evt in eventStream)
            {
                evt.Apply(voucherBuilder);
            }

            return voucherBuilder.Build();
        }
    }
}