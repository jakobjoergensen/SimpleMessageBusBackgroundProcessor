using System.Threading.Channels;

namespace JNJ.MessageBus;

internal sealed class InMemoryMessageQueue
{
    private readonly Channel<Event> _channel = Channel.CreateUnbounded<Event>();
    public ChannelReader<Event> Reader => _channel.Reader;
    public ChannelWriter<Event> Writer => _channel.Writer;
}
