# 参考资料

## 安装（zookeeper-3.4.10）

1. [官网下载最新版本](https://www.apache.org/dyn/closer.cgi/zookeeper/)

> 指定到目录解压缩：
   
```
   wget https://mirrors.tuna.tsinghua.edu.cn/apache/zookeeper/zookeeper-3.4.10/zookeeper-3.4.10.tar.gz
   tar zxvf zookeeper-3.4.10.tar.gz -C /data
   cd /data/zookeeper-3.4.10/conf
   mv zoo_sample.cfg  zoo.cfg
   主要修改目录配置：
   dataDir=/data/zookeeper_data
   dataLogDir=/data/zookeeper_data_logs
```
> 注意：zk 指认这个配置zoo.cfg

2. 配置环境变量
```
export ZOOKEEPER_INSTALL=/data/zookeeper-3.4.10/
```
3. Zookeeper命令
```
Zookeeper启动
/data/zookeeper-3.4.10/bin/zkServer.sh start
Zookeeper停止
/data/zookeeper-3.4.10/bin/zkServer.sh stop
查看Zookeeper状态
/data/zookeeper-3.4.10/bin/zkServer.sh status
```
4. 如果不成功，请查看报错日志
```
   cat /data/zookeeper-3.4.10/bin/zookeeper.out
```

## 集群安装（zookeeper-3.4.10）

1. 准备三台机器分别为：192.168.2.27、192.168.2.28、192.168.2.29

> 注意：zookeeper的集群必须是2n+1必须为奇数

2. 分别在三台机器的zoo.cfg中添加如下：
```
# The number of snapshots to retain in dataDir
#autopurge.snapRetainCount=3
# Purge task interval in hours
# Set to "0" to disable auto purge feature
#autopurge.purgeInterval=1
# 
server.1=192.168.2.27:2888:3888
server.2=192.168.2.28:2888:3888
server.3=192.168.2.29:2888:3888
#
```
> 配置说明：
> server.1=192.168.2.27:2888:3888
> server.myid=主机名:心跳端口:数据端口

3. 分别在zoo.cfg配置的【dataDir】目录下新建一个名为myid的文件

> 注意：myid的内容为对应该机器的server.1编号，比如：

| 主机 | 对应主机的myid文件内容 | 针对该主机的zoo.cfg |
|:-------:|:-------:|:-------:|
| 192.168.2.27 | 1 | server.1=192.168.2.27:2888:3888 |
| 192.168.2.28 | 2 | server.2=192.168.2.28:2888:3888 |
| 192.168.2.29 | 3 | server.3=192.168.2.29:2888:3888 |

4. 分别启动zookeeper查看（一定要启动三台后在查看各自的状态）
   
| 主机 | mode | 
|:-------:|:-------:|
| 192.168.2.27 | leader |
| 192.168.2.28 | follower |
| 192.168.2.29 | follower |


