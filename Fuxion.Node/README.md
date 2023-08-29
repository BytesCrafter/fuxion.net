# Fuxion NET - Cross-Platform Real-time WebSocket Server and Tools

Fuxion NET is an innovative project that simplifies the deployment of WebSocket or real-time servers on both Windows and Linux environments. This versatile platform is designed to be compatible with various applications, including chat apps, real-time games, IoT devices, client computer monitoring, and network monitoring. With its multi-platform support, the project offers a seamless and efficient solution for developers looking to implement real-time communication, monitoring capabilities, and cross-language compatibility in their projects.

## Overview

Fuxion NET utilizes JavaScript and Node.js along with the powerful Socket.IO library to provide a comprehensive cross-platform solution for real-time communication. This project empowers developers to easily create and deploy WebSocket servers on Windows and Linux environments, catering to various application domains.

## Features

- **Cross-Platform Support:** Fuxion NET works effortlessly on both Windows and Linux environments, providing flexibility and compatibility for your projects.

- **Real-Time Communication:** Utilizing the Socket.IO library, Fuxion NET enables real-time bidirectional communication between clients and servers, making it perfect for chat apps, online games, IoT integrations, and more.

- **Easy Deployment:** With a user-friendly setup and deployment process, Fuxion NET streamlines the process of launching WebSocket servers, making it accessible to developers of all skill levels.

- **Scalable Architecture:** Fuxion NET's architecture is designed for scalability, allowing you to handle a growing number of clients and connections efficiently.

## Getting Started

To get started with Fuxion NET and create your real-time WebSocket server:

1. Clone or download this repository to your development environment.

2. Install Node.js and npm if you haven't already.

3. Navigate to the project directory and install the required dependencies:
   ```
   npm install
   ```
4. Configure the server settings and options to match your project requirements.
   ```
      master.port = 8000
      chat.port = 8001
      voice.port = 8002
      world.port = 8003
      game.port = 8004
      auto.port = 8005
      redis.ip = 0.0.0.0
      redis.port = 6379
   ```
5 The server is now up and running! You can begin building your real-time applications by integrating the provided WebSocket APIs.

## Development

### Command line stardards

-- If you want to run only the master server, you can declare all the possible modules and have a value which should be target port. 
```
node server.js --master=19090
```

-- If you want to run two or more modules at process, you can declare more than one arguments with their corresponding ports.
```
node server.js --master=19090 --game=9090
```

-- If you now want just to run all the module at one process, you can just execute this command.
```
node server.js --modules=all
```

Here are the arguments that you can pass for you to be able to have your development flexible.
```
--production=true //default false
--logging=false //default true
--redis-password=juandelacruz //default ""
--allowed-origins=0.0.0.0,example.com //default 0.0.0.0
```

Note: Please make sure that the port is is available or no service is listening. For linux, you can use:
```
netstat -tulpn | grep LISTEN
```

## Examples

Explore the examples/ directory for sample applications that showcase Fuxion NET's capabilities in various scenarios, such as chat applications, real-time games, and IoT device communication.

## Contributing

Contributions to Fuxion NET are welcome! Whether you want to improve existing features, add new functionality, or fix issues, your contributions will be appreciated. Please review the guidelines in the CONTRIBUTING.md file.

## License

This project is licensed under the [MIT License](LICENSE).