// http://www.websocket.org/echo.html

$(document).ready(function () {
    let websocket = null;
    const wsUri = "ws://localhost:4649/Chat/?name=";

    function debugMessage(message) {
        console.log(message);
    }

    let cur_id = "";
    let users = [];
    let messages = [];

    function getUserById(id) {
        let target = null;
        for (var i = 0; i < users.length; i++) {
            if (users[i].id == id) {
                target = users[i];
            }
        }
        return target;
    }

    function addNewUser(username, userid)
    {
        users.push({
            id: userid,
            name: username
        });
        var date = new Date();

        var chatItem = `
            <div id="user-`+ userid +`" class="chat_people">
                <div class="chat_img"> <img src='https://system.bytescrafter.net/assets/images/avatar.jpg' alt="fuxion"> </div>
                <div class="chat_ib">
                    <h5>`+ username + ` <span class="chat_date text-success">Online</span></h5>
                    <p>
                        ` + date.toLocaleDateString() + " " + date.toLocaleTimeString() + `
                    </p>
                </div>
            </div>`;

        $(".chat_list").append(chatItem);
    }

    function removeUser(userid) {
        //Clear all itme.
        if (userid == 0) {
            users = [];
            $(".chat_list").empty();
            return;
        }

        const index = users.indexOf(5);
        if (index > -1) {
            users.splice(index, 1);
        }

        $("#user-" + userid).remove();
    }

    function getIncomingMessage(data) {
        var date = new Date();
        const mesg = `<div class="incoming_msg">
                        <div class="incoming_msg_img"> <img src="https://system.bytescrafter.net/assets/images/avatar.jpg" alt="fuxion"> </div>
                        <div class="received_msg">
                            <div class="received_withd_msg">
                                <p>
                                    `+ data.sender + `: ` + data.data +`
                                </p>
                                <span class="time_date"> ` + date.toLocaleDateString() + " | " + date.toLocaleTimeString() + ` </span>
                            </div>
                        </div>
                    </div>`;   

        return mesg;
    }

    function getOutgoingMessage(data) {
        var date = new Date();
        const messg = `<div class="outgoing_msg"">
                        <div class="sent_ms" style="float: right; width: 46%;">
                                <p style="background: #8f8f8f none repeat scroll 0 0;
                                        border-radius: 3px;
                                        font-size: 14px;
                                        margin: 0;
                                        color: #fff;
                                        padding: 5px 10px 5px 12px;
                                        width: 100%; ">
                                    `+ data.sender + `: ` + data.data +`
                                </p>
                                <span class="time_date"> ` + date.toLocaleDateString() + " | " + date.toLocaleTimeString() + ` </span>
                            </div >
                        </div > `;
        return messg;
    }

    function addMessageToList(data) {
        messages.push(data);

        data.sender = (typeof getUserById(data.id).name !== 'undefined') ? getUserById(data.id).name : "";

        let chatItem = "";
        if (cur_id == data.id) { 
            chatItem = getOutgoingMessage(data);
            $("#send-input").val("");
        } else { //incoming
            chatItem = getIncomingMessage(data);
        }

        $(".msg_history").append(chatItem);
        $("#msg_history").animate({ scrollTop: $('#msg_history').prop("scrollHeight") }, 1000);
    }

    $('#send-input').keypress(function (e) {
        if (e.which == 13) {
            sendMessage();
        }
    });
    $("#send-button").on("click", sendMessage);

    function sendMessage() {
        const message = {
            data: $("#send-input").val(),
            type: "message-all"
        };

        websocket.send(JSON.stringify(message));
    }

    $('#connect-input').keypress(function (e) {
        if (e.which == 13) {
            connectToServer();
        }
    });
    $("#connect-button").on("click", connectToServer);

    function connectToServer() {
        if ($("#connect-input").val() == "") {
            return;
        }

        var username = $("#connect-input").val();
        websocket = new WebSocket(wsUri + username);

        websocket.onopen = (e) => {
            $("#login-container").addClass("hide");
            $("#chat-container").removeClass("hide");
            $("#disconnect").removeClass("hide");

            websocket.send("users-list");

            $("#username").val("");
            debugMessage("Event: Connected to WebSocket Server...");
        };

        websocket.onclose = (e) => {
            $("#chat-container").addClass("hide");
            $("#login-container").removeClass("hide");
            $("#disconnect").addClass("hide");

            removeUser(0);
            debugMessage("Event: Disconnected from WebSocket Server...");
        };

        websocket.onmessage = (e) => {
            //TODO: Add the message here.
            const data = JSON.parse(e.data);

            if (typeof data.type !== 'undefined') {
                if (data.type == "open") {
                    addNewUser(data.name, data.id);
                }
                if (data.type == "open-self") {
                    cur_id = data.id; //get self id.
                    $("#user-" + cur_id).addClass("self");
                }
                if (data.type == "close") {
                    removeUser(data.id);
                }
                if (data.type == "users") {
                    for (var i = 0; i < data.users.length; i++) {
                        addNewUser(data.users[i].name, data.users[i].id);
                    }
                }
                if (data.type == "message-all") {
                    //Add Message
                    addMessageToList(data);
                }
            }
        };

        websocket.onerror = (e) => {
            debugMessage(`Error: ${e.data}`);
        };

        $("#disconnect").on("click", function () {
            websocket.close();
        });
    }

});
