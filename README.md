# GPS车辆监控平台

### 瞎逼逼：
&emsp;&emsp;搬砖搬了快四年呐，果然很快啊~~前几年都在与Web服务打交道，突然间公司要搞软硬结合，需要有个平台，这下好吧。。。公司原先有个老平台，窃喜之下打开老平台一看，好复杂，一脸懵逼的说。。。但是慢慢的研究，原来这一坨屎的代码是写的那么优美，那么生动。但是，老平台存在的问题有耦合度高、扩展性差、维护难度大，这才让我有业余时间重新自己瞎整的念头。

### 新平台的优势：
1. 跨平台
2. 借助.NET Core模块化的思想
3. 分久必合，合久必分的服务化思想
4. 站在巨人的肩膀上搬砖

### 发布/订阅(GPS.PubSub.Abstractions):
| 功能 | 说明 | 完成情况 |
|:-------:|:-------:|:-------:|
| GPS.JT808PubSubToKafka | 实现kafka发布/订阅 | 小部分完成 |
| GPS.JT808PubSubToRabbitMQ | 实现RabbitMQ发布/订阅 | 小部分完成 |

> 注意：MQ的选择适平台设备数量而定，选择kafka的原因是平台需要承载10万台设备。

##### GPS.JT808PubSubToKafka已实现的功能：
| 功能 | 说明 | 完成情况 |
|:-------:|:-------:|:-------:|
| JT808_UnificationSend | 统一下发设备消息 | √ |
| JT808_0x0200 | 位置信息汇报 | √ |

##### GPS.JT808PubSubToRabbitMQ已实现的功能：
| 功能 | 说明 | 完成情况 |
|:-------:|:-------:|:-------:|
| JT808_UnificationSend | 统一下发设备消息 | √ |
| JT808_0x0200 | 位置信息汇报 | √ |

### 分发器(GPS.Dispatcher.Abstractions)：
| 功能 | 说明 | 使用场景 |
|:-------:|:-------:|:-------:|
| IDeviceMonitoringDispatcher | 设备监控(点名）分发器 | 后台需要监控某一辆车的所有消息 |
| ISourcePackageDispatcher | 原包分发器 | 需要将源数据转给其他平台 |

##### 分发器(GPS.Dispatcher.Abstractions)功能实现：
| 功能 | 实现方式 | 备注 |
|:-------:|:-------:|:-------:|
| GPS.JT808DeviceMonitoringDispatcher | 通过kafka实现 | 可选择合适的方式实现 |
| GPS.JT808SourcePackageDispatcher | 通过kafka实现 | 可选择合适的方式实现 |

### 基于SignalR的JT808.WebSocketServer服务：
> 统一用户鉴权（待实现）
> 推送平台：Web平台、App、及小程序
> 使用场景：推送设备报警状态、胎压预警等





