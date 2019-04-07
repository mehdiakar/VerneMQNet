# VerneMQNet
VerneMQNet is a ASP.NET Core implementation of VerneMQ abilities on HTTP. this library helps to write VerneMQ web hooks server, run all vmq-admin commands using HTTP and monitor all VerneMQ server metrics.

[![NuGet Badge](https://buildstats.info/nuget/VerneMQnet.ASPNetCore?includePreReleases=true)](https://www.nuget.org/packages/VerneMQnet.ASPNetCore)
## Features

### Web hooks
Base implementation of all VerneMQ [Webhooks](https://docs.vernemq.com/plugin-development/webhookplugins) plugin events include
- All session life cycle events
- All subscribe flow events
- All publish flow events

in both MQTT verions 3.x and 5 for using in ASP.NET Core API controllers.

### Live Administration
A wrapper for vmq-admin commands and sub-commands.
- Config all MQTT standard and non-standard options
- Config VerneMQ Advances Options.
- Enable, disable VerneMQ plugins. Also show information about available plugings.
- Show and filter information about MQTT sessions, disconnect or re-authorize particular clients.
- Starts and stops the server application within node 
- Show cluster information and join/leave nodes to/from cluster.
- Show Webhooks registered information and register/deregister them.

### Monitoring
- Server health monitoring.
- Monitor nodes status including incoming and outgoing messages count, queue statistics and listeners informations.

## NuGet

his library is available as a nuget package: https://www.nuget.org/packages/VerneMQnet.ASPNetCore/

## MIT License
Copyright (c) 2019 Mehdi Akar

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.