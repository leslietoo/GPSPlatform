# 参考资料

## 踩过的坑：

![图1-Orleans](https://github.com/SmallChi/GPSPlatform/blob/master/doc/img/Orleans1.png)

> 注意：一定要按照官方的demo来搞，该引用的引用。

> 要是没有引用Microsoft.Orleans.OrleansCodeGenerator.Build会报上面的错误。

In the grain interfaces project:
```
PM> Install-Package Microsoft.Orleans.Core.Abstractions
PM> Install-Package Microsoft.Orleans.OrleansCodeGenerator.Build
```
In the grain implementations project:
```
PM> Install-Package Microsoft.Orleans.Core.Abstractions
PM> Install-Package Microsoft.Orleans.OrleansCodeGenerator.Build
```
In the server (silo) project:
```
PM> Install-Package Microsoft.Orleans.Server
```
In the client project:
```
PM> Install-Package Microsoft.Orleans.Client
```