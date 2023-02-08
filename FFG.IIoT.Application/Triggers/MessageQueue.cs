namespace IIoT.Application.Triggers;
public sealed class MessageQueue : IEntranceTrigger
{
    public ValueTask BuildAsync()
    {
        Basic.Transport.InterceptingPublishAsync += @event => Task.Run(() =>
        {
            try
            {
                var paths = @event.ApplicationMessage.Topic.Split('/');
                var text = Encoding.UTF8.GetString(@event.ApplicationMessage.Payload);
                switch (paths.Length)
                {
                    case 1:
                        switch (paths[0])
                        {
                            case var item when item.Equals(IInteriorQueue.Model.VTM415, StringComparison.OrdinalIgnoreCase):
                                Queue.Interior.Push(IInteriorQueue.Model.VTM415, text);
                                break;

                            case var item when item.Equals(IInteriorQueue.Model.VUX400, StringComparison.OrdinalIgnoreCase):
                                Queue.Interior.Push(IInteriorQueue.Model.VUX400, text);
                                break;

                            case var item when item.Equals(IInteriorQueue.Model.UCT600, StringComparison.OrdinalIgnoreCase):
                                Queue.Interior.Push(IInteriorQueue.Model.UCT600, text);
                                break;
                        }
                        break;

                    case 4:
                        switch (paths[0])
                        {
                            case "":
                                switch (paths[1])
                                {
                                    case "v1":
                                        switch (paths[2])
                                        {
                                            case "device":
                                                switch (paths[3])
                                                {
                                                    case var item when item.Equals(ITangramQueue.Label.Status, StringComparison.OrdinalIgnoreCase):
                                                        {
                                                            var meta = text.ToObject<ITangramConnection.Entity.Meta>();
                                                        }
                                                        break;

                                                    case var item when item.Equals(ITangramQueue.Label.Connection, StringComparison.OrdinalIgnoreCase):
                                                        {
                                                            var meta = text.ToObject<ITangramConnection.Entity.Meta>();
                                                            if (!string.IsNullOrEmpty(meta.TangramId)) Queue.Tangram.Push(meta);
                                                        }
                                                        break;

                                                    case var item when item.Equals(ITangramQueue.Label.SensorData, StringComparison.OrdinalIgnoreCase):
                                                        {
                                                            var meta = text.ToObject<ITangramConnection.Entity.Meta>();
                                                        }
                                                        break;
                                                }
                                                break;
                                        }
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Log.Error(Menu.Title, nameof(Basic.Transport.InterceptingPublishAsync), new { e.Message });
            }
        });
        return ValueTask.CompletedTask;
    }
    public required IBasicExpert Basic { get; init; }
    public required IQueueWrapper Queue { get; init; }
}