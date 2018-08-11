# GPS车辆监控平台



### 发布/订阅(GPS.PubSub.Abstractions):
| 功能 | 说明 | 完成情况 |
|:-------:|:-------:|:-------:|
| GPS.JT808PubSubToKafka | 实现kafka发布/订阅 | 小部分完成 |
| GPS.JT808PubSubToRabbitMQ | 实现RabbitMQ发布/订阅 | 未实现 |
| GPS.JT808PubSubToActiveMQ | 实现ActiveMQ发布/订阅 | 未实现 |

> 注意：MQ的选择适平台设备数量而定，选择kafka的原因是平台需要承载10万台设备。

#### GPS.JT808PubSubToKafka已实现的功能：
| 功能 | 说明 | 完成情况 |
|:-------:|:-------:|:-------:|
| JT808_UnificationSend | 统一下发设备消息 | √ |
| JT808_0x0200 | 位置信息汇报 | √ |

### 分发器(GPS.Dispatcher.Abstractions):
| 功能 | 说明 | 使用场景 |
|:-------:|:-------:|:-------:|
| IDeviceMonitoringDispatcher | 设备监控(点名）分发器 | 后台需要监控某一辆车的所有消息 |
| ISourcePackageDispatcher | 源包分发器 | 需要将源数据转给其他平台 |

### 基于SignalR的JT808.WebSocketServer服务
> 统一用户鉴权（待实现）
推送平台：Web平台、App、及小程序
使用场景：推送设备报警状态、胎压预警等


