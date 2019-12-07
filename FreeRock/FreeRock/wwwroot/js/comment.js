"use strict";

var type = document.getElementById("typeModel").value
var connection = new signalR.HubConnectionBuilder().withUrl("/CommentHub/" + type).build();

//Disable send button until connection is established
var b = document.getElementById("sendButton")
if (b != null) { b.disabled = true; }

connection.on("ReceiveMessage", function (user, message, date) {

    var msg = `<div class="card text"> <div class="card-header"> <b>` + user + `</b>` + " comemnted at " + date + `</div> <div class="card-body"> <p>` + message + `</p> </div> </div> <hr />`;

    var div = document.createElement("div");
    div.innerHTML = msg;
    document.getElementById("messagesList").prepend(div);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userName").value;
    var message = document.getElementById("NewCommentary").value;

    var id = document.getElementById("id").value;


    connection.invoke("SendMessage", user, message, id).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    document.getElementById("NewCommentary").value = "";
    document.getElementById("NewCommentary").focus();
});

document.getElementById("NewCommentary").value = "";
document.getElementById("NewCommentary").focus();

document.getElementById("NewCommentary").addEventListener("keyup", function (event) {
    event.preventDefault();
    if (event.keyCode === 13) {
        document.getElementById("sendButton").click();
    }
});