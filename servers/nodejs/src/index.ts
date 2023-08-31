#!/usr/bin/env Fuxion
import { WebSocketServer } from './core/websocket';

const app = require('express')();
const http = require('http').Server(app);
const io = require('socket.io')(http);
const port = process.env.PORT || 8000;

export class FuxionServer {

    private websocket: WebSocketServer;
    constructor() {
        this.websocket = new WebSocketServer()
        this.websocket.startServer(port.toString())
    }

    onConnect() {

    }

    onMessage() {

    }

    onDisconnect() {

    }
}
const fuxion = new FuxionServer();