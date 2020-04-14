# api.gateway.with.service.discovery
api.gateway.with.service.discovery


# Consul configuration
```
{
  "datacenter": "east-aws",
  "data_dir": "d:\\workspace\\consul",
  "log_level": "INFO",
  "node_name": "foobar",
  "server": true,
  "bind_addr": "127.0.0.1:2180",
  "bootstrap": true,
  "ui": true,
  "log_file" : "d:\\workspace\\consul.log" 
}
```

# Install consul in dev mode

```consul agent -dev```

# Install consul in server mode

```consul agent -server -bootstrap -config-file="d:\\workspace\\consul\\config.json"```

or 

```consul agent -server -bootstrap-expect=1 -data-dir=d:\\workspace\\consul -ui -bind=127.0.0.1:2180```

# Install consul as windows services

```sc.exe create "Consul" binPath= "d:\\workspace\\consul\\Consul.exe agent -config-dir=d:\\workspace\\consul" start= auto```

```sc.exe start "Consul"```
