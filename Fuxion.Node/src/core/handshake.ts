import { environment } from "../config";

export class Handshake {
    static verify(packet: any, next: any) {
        console.log(packet.handshake.query);

        //If you want to close or not allowing connecting, use this.
        //Disconnect socket connection and return error message.
        //return next( new Error("Sample Error Text") )

        if(environment.auth_uri !== "") {
            //Rest Api then check if authenticated.
        }

        //else just allow connection.
        return next();
    }
}