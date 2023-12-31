#!/usr/bin/env Fuxion

import { Server } from "socket.io"
import { RedisConnect } from './redis'
import { Handshake } from './handshake' 
import { environment } from '../config'
import path from "path"

const app = require('express')()
const http = require('http').Server(app)

export class WebSocketServer {

    public port: string = ""
    public server: Server
    public redis: RedisConnect

    constructor() {
        this.server = new Server(http, {
            pingInterval: 10000,
            pingTimeout: 5000
        })
        this.server.use(Handshake.verify);
        this.redis = new RedisConnect(this.server)
    }
    
    startServer(port: string) {
        this.port = port; //Set the port.

        this.server.on('connection', (socket: any) => {
            console.log("Connected: " + socket.id);

            //tempory
            socket.on('chat message', (msg: any) => {
                this.server.emit('chat message', msg)
            })

            socket.on('disconnect', (reason: any) => {
                console.log("Disconnected: " + socket.id);
            })

            socket.on('error', (error: any) => {
                console.log("Error: " + socket.id + " > " + error);
            });
        });
        
        if(environment.production == false) {
            app.get('/', (req: any, res: any) => {
                const index = path.join(__dirname, '/public/demo.html')
                res.sendFile( index );
            })
        }

        http.listen(this.port, () => {
            console.log(`Fuxion NET server running at http://localhost:${this.port}/`);
        })
    }
} 