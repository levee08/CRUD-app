let teams = [];
let managers = [];
let players = [];
let connection = null;
let teamtoUpdateID = -1;
let managertoUpdateID = -1;
let playertoUpdateID = -1;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:29829/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("footballTeamCreated", (user, message) => {
                getdata();
    });
    connection.on("footballTeamDeleted", (user, message) => {
        getdata();
    });
    connection.on("footballTeamUpdated", (user, message) => {
        getdata();
    });
    connection.on("ManagerCreated", (user, message) => {
        getdata();
    });
    connection.on("ManagerDeleted", (user, message) => {
        getdata();
    });
    connection.on("ManagerUpdated", (user, message) => {
        getdata();
    });
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
    await fetch('http://localhost:29829/FootballTeam')
        .then(x => x.json())
        .then(y => {
            teams = y;
        });

    await fetch('http://localhost:29829/Manager')
        .then(x => x.json())
        .then(y => {
            managers = y;
            display();
        });

    await fetch('http://localhost:29829/Player')
        .then(x => x.json())
        .then(y => {
            players= y;
            display();
        });
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};


function display() {

    const resultArea = document.getElementById('resultarea');
    const Managerresultarea = document.getElementById('managerresultarea');
    const Playerresultarea = document.getElementById('playerresultarea');
    resultArea.innerHTML = '';
    Managerresultarea.innerHTML = '';
    Playerresultarea.innerHTML = '';

    teams.forEach(t => {
        resultArea.innerHTML +=
            "<tr><td>" + t.footballTeamId + "</td><td>"
            + t.footballTeamName + "</td><td>"
            + t.currentPlacement + "</td><td>"
        + t.trophiesWon + "</td><td>"
            + `<button type="button" onclick="remove(${t.footballTeamId})">Delete</button>`
        + `<button type="button" onclick="ShowUpdate(${t.footballTeamId})">Update</button>`
            "</tr > ";
    });

    managers.forEach(y => {
        Managerresultarea.innerHTML +=
            "<tr><td>" + y.managerId + "</td><td>"
            + y.managerName + "</td><td>"
            + y.managerAge + "</td><td>"
            + y.isBold + "</td><td>"
            + `<button type="button" onclick="managerremove(${y.managerId})">Delete</button>`
            + `<button type="button" onclick="managerShowUpdate(${y.managerId})">Update</button>`
        "</tr > ";

    });
    players.forEach(y => {
        Playerresultarea.innerHTML +=
            "<tr><td>" + y.playerId+ "</td><td>"
            + y.playerName + "</td><td>"
            + y.playerPosition + "</td><td>"
            + `<button type="button" onclick="playerremove(${y.playerId})">Delete</button>`
            + `<button type="button" onclick="playerShowUpdate(${y.playerId})">Update</button>`
        "</tr > ";

    });

}
function ShowUpdate(id) {
    document.getElementById('teamnametoupdate').value = teams.find(t => t['footballTeamId'] == id)['footballTeamName'];
    document.getElementById('teamplacementtoupdate').value = teams.find(t => t['footballTeamId'] == id)['currentPlacement'];
    document.getElementById('teamtrophiestoupdate').value = teams.find(t => t['footballTeamId'] == id)['trophiesWon'];
    document.getElementById('updateformdiv').style.display = 'flex';
    teamtoUpdateID = id;
}
function managerShowUpdate(id) {
    document.getElementById('managernametoupdate').value = managers.find(t => t['managerId'] == id)['managerName'];
    document.getElementById('manageragetoupdate').value = managers.find(t => t['managerId'] == id)['managerAge'];
    document.getElementById('isboldtoupdate').checked = managers.find(t => t['managerId'] == id)['isBold'];
    document.getElementById('managerupdateformdiv').style.display = 'flex';
    managertoUpdateID = id;
}
function playerShowUpdate(id) {
    document.getElementById('playernametoupdate').value = players.find(t => t['playerId'] == id)['playerName'];
    document.getElementById('playerpositiontoupdate').value = players.find(t => t['playerId'] == id)['playerPosition'];
    document.getElementById('playerupdateformdiv').style.display = 'flex';
    playertoUpdateID = id;
}

function remove(id) {
    fetch('http://localhost:29829/FootballTeam/' + id, {
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
function managerremove(id) {
    fetch('http://localhost:29829/Manager/' + id, {
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
    


function create() {
    let teamname = document.getElementById('teamname').value;
    let teamplacement = document.getElementById('teamplacement').value;
    let teamthrophies = document.getElementById('teamtrophies').value;
    fetch('http://localhost:29829/FootballTeam', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                footballTeamName: teamname, currentPlacement: teamplacement, trophiesWon: teamthrophies})
    }).then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error: ', error);
        });
        
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
function managercreate() {
    let managername = document.getElementById('managername').value;
    let managerage = document.getElementById('managerage').value;
    let isbold = document.getElementById('isbold').checked;
    fetch('http://localhost:29829/Manager', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                managerName: managername, managerAge: managerage, isBold: isbold
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
function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let teamname = document.getElementById('teamnametoupdate').value;
    let teamplacement = document.getElementById('teamplacementtoupdate').value;
    let teamthrophies = document.getElementById('teamtrophiestoupdate').value;
    fetch('http://localhost:29829/FootballTeam', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                footballTeamName: teamname, currentPlacement: teamplacement, trophiesWon: teamthrophies,footballTeamId:teamtoUpdateID
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
function managerupdate() {
    document.getElementById('managerupdateformdiv').style.display = 'none';
    let managername = document.getElementById('managernametoupdate').value;
    let managerage = document.getElementById('manageragetoupdate').value;
    let isbold = document.getElementById('isboldtoupdate').checked;

    fetch('http://localhost:29829/Manager', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                managerName: managername, managerAge: managerage, isBold: isbold,managerId:managertoUpdateID
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