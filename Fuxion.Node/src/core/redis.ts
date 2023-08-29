import { Server } from 'socket.io'
import { createClient } from 'redis'
import { createAdapter } from '@socket.io/redis-adapter'
import { environment } from '../config'

const pubClient = createClient({url: `redis://`+environment.redis+`:6379/1`})
pubClient.on('error', err => console.log('Redis Main Client Error', err))
pubClient.connect()

const subClient = pubClient.duplicate()
subClient.on('error', err => console.log('Redis Sub Client Error', err))
subClient.connect()

export class RedisConnect 
{
    constructor(public server: Server) {
        server.adapter( createAdapter(pubClient, subClient) )
    }
}