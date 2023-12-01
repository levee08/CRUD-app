let players = [];
let connection = null;
let playertoUpdateID = -1;
let trophiesbyposition = [];
let playerstat = [];
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:29829/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("PlayerCreated", (user, message) => {
        getdata();
    });
    connection.on("PlayerDeleted", (user, message) => {
        getdata();
    });
    connection.on("PlayerUpdated", (user, message) => {
        getdata();
    });


    connection.onclose
        (async () => {
            await start();
        });
    start();

}

async function getdata() {
    await fetch('http://localhost:29829/Player')
        .then(x => x.json())
        .then(y => {
            players = y;
            display();
        });
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
        getdata();
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};
async function PlayerStat(name) {
    let playername = name;

    await fetch('http://localhost:29829/PlayerNonCrud/PlayerTrophiesAndPosition/'+playername)
        .then(x => x.json())
        .then(y => {
            playerstat = y;
            playerstatdisplay();
        });
}
async function getTrophiesByPosition() {
    await fetch('http://localhost:29829/PlayerNonCrud/ThrophiesByPosition')
        .then(x => x.json())
        .then(y => {
            trophiesbyposition = y;
            trophiesdisplay();
        });
}
function playerstatdisplay() {
    const statresultarea = document.getElementById('statresultarea');
    statresultarea.innerHTML = "";
    playerstat.forEach(t => {
        statresultarea.innerHTML +=
            "<tr><td>" + t.key + "</td><td>"
            + t.value + "</td></tr>"
    })

}
function trophiesdisplay() {
    const trophiesresultarea = document.getElementById('trophiesresultarea');
    trophiesresultarea.innerHTML = "";
    trophiesbyposition.forEach(t => {
        trophiesresultarea.innerHTML +=
            "<tr><td>" + t.key + "</td><td>"
            + t.value + "</td></tr>"
    })

}

function display() {
    const Playerresultarea = document.getElementById('playerresultarea');
   
    Playerresultarea.innerHTML = '';

    players.forEach(y => {
        Playerresultarea.innerHTML +=
            "<tr><td>" + y.playerId + "</td><td>"
            + y.playerName + "</td><td>"
            + y.playerPosition + "</td><td>"
            + `<button type="button" onclick="playerremove(${y.playerId})">Delete</button>`
            + `<button type="button" onclick="playerShowUpdate(${y.playerId})">Update</button>`
        + `<button type="button" onclick="PlayerStat('${y.playerName}')">Stat</button>`
        "</tr > ";

    });

}

function playerShowUpdate(id) {
    document.getElementById('playernametoupdate').value = players.find(t => t['playerId'] == id)['playerName'];
    document.getElementById('playerpositiontoupdate').value = players.find(t => t['playerId'] == id)['playerPosition'];
    document.getElementById('playerupdateformdiv').style.display = 'flex';
    playertoUpdateID = id;
}

function playerremove(id) {
    fetch('http://localhost:29829/Player/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); })

}

function playercreate() {
    let playername = document.getElementById('playername').value;
    let playerposition = document.getElementById('playerposition').value;
    fetch('http://localhost:29829/Player', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                playerName: playername, playerPosition: playerposition
            })
    }).then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error: ', error);
        });

}
function playerupdate() {
    document.getElementById('playerupdateformdiv').style.display = 'none';
    let playername = document.getElementById('playernametoupdate').value;
    let playerposition = document.getElementById('playerpositiontoupdate').value;

    fetch('http://localhost:29829/Player', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                playerName: playername, playerPosition: playerposition, playerId: playertoUpdateID
            })
    }).then(response => response.json())
        .then(data => {
            console.log('Success: ', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error: ', error);
        });


}
