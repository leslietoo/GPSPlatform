# DotNetty参考资料

## ChannelOption
1. TcpNodelay
> 解释：是否启用Nagle算法，改算法将小的碎片数据连接成更大的报文来最小化所发送的报文的数量。

> 使用建议：如果需要发送一些较小的报文，则需要禁用该算法，从而最小化报文传输延时。只有在网络通信非常大时（通常指已经到100k+/秒了），设置为false会有些许优势，因此建议大部分情况下均应设置为true。
2. SoLinger
> 解释： Socket参数，关闭Socket的延迟时间，默认值为-1，表示禁用该功能。-1表示socket.close() 方法立即返回，但OS底层会将发送缓冲区全部发送到对端。0表示socket.close() 方法立即返回，OS放弃发送缓冲区的数据直接向对端发送RST包，对端收到复位错误。非0整数值表示调用socket.close() 方法的线程被阻塞直到延迟时间到或发送缓冲区中的数据发送完毕，若超时，则对端会收到复位错误。

> 使用建议： 使用默认值，不做设置。
3. SoSndbuf
> 解释： Socket参数，TCP数据发送缓冲区大小，即TCP发送滑动窗口，linux操作系统可使用命令：cat /proc/sys/net/ipv4/tcp_smem查询其大小。缓冲区的大小决定了网络通信的吞吐量（网络吞吐量=缓冲区大小/网络时延）。

> 使用建议： 缓冲区大小设为网络吞吐量达到带宽上限时的值，即缓冲区大小=网络带宽* 网络时延。以千兆网卡为例进行计算，假设网络时延为1ms，缓冲区大小=1000Mb/s* 1ms = 128KB。
4. SoRcvbuf与SoSndbuf理。
5. SoReuseaddr
> 解释：是否复用处于TIME_WAIT状态连接的端口，适用于有大量处于TIME_WAIT状态连接的场景，如高并发量的Http短连接场景等。
6. SoBacklog
> 解释： Backlog主要是指当ServerSocket还没执行accept时，这个时候的请求会放在os层面的一个队列里，这个队列的大小即为backlog值，这个参数对于大量连接涌入的场景非常重要，例如服务端重启，所有客户端自动重连，瞬间就会涌入很多连接，如backlog不够大的话，可能会造成客户端接到连接失败的状况，再次重连，结果就会导致服务端一直处理不过来（当然，客户端重连最好是采用类似tcp的自动退让策略）

> 使用建议： backlog的默认值为os对应的net.core.somaxconn，调整backlog队列的大小一定要确认ulimit -n中允许打开的文件数是够的。

7. SoKeepalive
> 解释：是否使用TCP的心跳机制,在双方TCP套接字建立连接后（即都进入ESTABLISHED状态）并且在两个小时左右上层没有任何数据传输的情况下，这套机制才会被激活。

> 使用建议： 心跳机制由应用层自己实现；

8. SoTimeout
> 解释：此选项表示等待客户连接的超时时间。

## 超时机制及心跳

#####Netty的超时类型IdleState主要分为：
ALL_IDLE： 一段时间内没有数据接收或者发送
READER_IDLE： 一段时间内没有数据接收
WRITER_IDLE： 一段时间内没有数据发送

#####在Netty的timeout包下，主要类有：
IdleStateEvent：超时的事件
IdleStateHandler：超时状态处理
ReadTimeoutHandler：读超时状态处理
WriteTimeoutHandler：写超时状态处理

