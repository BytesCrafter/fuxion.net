#!/usr/bin/env Fuxion

const app = require('express')();
const http = require('http').Server(app);
const io = require('socket.io')(http);
const port = process.env.PORT || 3000;

app.get('/', (req: any, res: any) => {
    res.sendFile(__dirname + '/public/demo.html');
});

io.on('connection', (socket: any) => {
    socket.on('chat message', (msg: any) => {
        io.emit('chat message', msg);
    });
});

http.listen(port, () => {
    console.log(`Fuxion NET server running at http://localhost:${port}/`);
});
