#!/usr/bin/env Fuxion

import { WebSocketServer } from './core/websocket';

const app = require('express')();
const http = require('http').Server(app);
const io = require('socket.io')(http);
const port = process.env.PORT || 3000;

export class FuxionServer {

    private websocket: WebSocketServer;
    constructor() {
        this.websocket = new WebSocketServer(
            port.toString()
        )
        this.websocket.startServer()
    }

    onConnect() {

    }

    onMessage() {

    }

    onDisconnect() {

    }
}
const fuxion = new FuxionServer();