// http://www.websocket.org/echo.html

$(document).ready(function () {
    const wsUri = "ws://localhost:4649/Chat/?name=";

    function doSend(message) {
        //debugMessage(`SENT: ${message}`);
        websocket.send(message);
    }

    function debugMessage(message) {
        console.log(message);
    }

    let users = [];

    function addNewUser(username, userid)
    {
        users.push({
            id: userid,
            name: username
        });
        var date = new Date();

        var chatItem = `
            <div id="`+ userid +`" class="chat_people">
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

        $("#" + userid).remove();
    }

    function onClickButton() {
        const text = textarea.value;

        text && doSend(text);
        textarea.value = "";
        textarea.focus();
    }

    $('#connect-input').keypress(function (e) {
        if (e.which == 13) {
            connectToServer();
        }
    });
    $("#connect-button").on("click", connectToServer);

    function connectToServer() {
        var username = $("#connect-input").val();
        const websocket = new WebSocket(wsUri + username);

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
                if (data.type == "close") {
                    removeUser(data.id);
                }
                if (data.type == "users") {
                    for (var i = 0; i < data.users.length; i++) {
                        addNewUser(data.users[i].name, data.users[i].id);
                    }
                }
            }
            console.log(data);
        };

        websocket.onerror = (e) => {
            debugMessage(`Error: ${e.data}`);
        };

        $("#disconnect").on("click", function () {
            websocket.close();
        });
    }

});
