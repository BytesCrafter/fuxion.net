#!/usr/bin/env Fuxion

import { Server } from "socket.io"
import { environment } from '../config.js';
import path from "path"

const app = require('express')()
const http = require('http').Server(app)

export class WebSocketServer {

    public server: Server

    constructor(
        public port: string
    ) {
        this.server = new Server(http, {
            pingInterval: 10000,
            pingTimeout: 5000
        })
    }
    
    startServer() {
        this.server.on('connection', (socket: any) => {
            socket.on('chat message', (msg: any) => {
                this.server.emit('chat message', msg)
            });
        });

        if(environment.production == false) {
            app.get('/', (req: any, res: any) => {
                const index = path.join(__dirname, '..', '/public/demo.html')
                res.sendFile( index );
            });
        }

        http.listen(this.port, () => {
            console.log(`Fuxion NET server running at http://localhost:${this.port}/`);
        });
    }
} 