function JumpToManager() {

    window.location.href = "ManagerPage.html";
}
function JumpToPlayer() {

    window.location.href = "PlayerPage.html";
}

let teams = [];
let connection = null;
let teamtoUpdateID = -1;
let BoldTeams = [];
let PlayersCount = [];
let OldManagers = [];
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
            display();
        });

}

async function getBoldManagersTeamNameData() {
    await fetch('http://localhost:29829/noncrud/BoldManagersTeamName')
        .then(x => x.json())
        .then(y => {
            BoldTeams = y;
            Bolddisplay();
        });
}
async function getPlayersCount() {
    await fetch('http://localhost:29829/noncrud/TeamPlayersCount')
        .then(x => x.json())
        .then(y => {
            PlayersCount = y;
            Countdisplay();
        });
}
async function getOldManagers() {
    await fetch('http://localhost:29829/noncrud/OldManagersTeamName')
        .then(x => x.json())
        .then(y => {
            OldManagers = y;
            OldManagersdisplay();
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
function Bolddisplay() {
    const boldresultarea = document.getElementById('boldresultarea');
    boldresultarea.innerHTML = "";
    BoldTeams.forEach(t => {
        boldresultarea.innerHTML +=
            "<tr><td>" + t.footballTeamId + "</td><td>"
        + t.footballTeamName + "</td></tr>"
    })

}
function Countdisplay() {
    const countresultarea = document.getElementById('countresultarea');
    countresultarea.innerHTML = "";
    PlayersCount.forEach(t => {
        countresultarea.innerHTML +=
            "<tr><td>" + t.key + "</td><td>"
            + t.value + "</td></tr>"
    })

}
function OldManagersdisplay() {
    const oldresultarea = document.getElementById('oldresultarea');
    oldresultarea.innerHTML = "";
    OldManagers.forEach(t => {
        oldresultarea.innerHTML +=
            "<tr><td>" + t.key + "</td><td>"
            + t.value + "</td></tr>"
    })

}

function display() {

    const resultArea = document.getElementById('resultarea');
  
    resultArea.innerHTML = '';
   
    teams.forEach(t => {
        resultArea.innerHTML +=
            "<tr><td>" + t.footballTeamId + "</td><td>"
            + t.footballTeamName + "</td><td>"
            + t.currentPlacement + "</td><td>"
        + t.trophiesWon + "</td><td>"
            + `<button type="button" onclick="remove(${t.footballTeamId})">Delete</button>`
            + `<button type="button" onclick="ShowUpdate(${t.footballTeamId})">Update</button>`
            + "</tr > ";
           
    });

}

function ShowUpdate(id) {
    document.getElementById('teamnametoupdate').value = teams.find(t => t['footballTeamId'] == id)['footballTeamName'];
    document.getElementById('teamplacementtoupdate').value = teams.find(t => t['footballTeamId'] == id)['currentPlacement'];
    document.getElementById('teamtrophiestoupdate').value = teams.find(t => t['footballTeamId'] == id)['trophiesWon'];
    document.getElementById('updateformdiv').style.display = 'flex';
    teamtoUpdateID = id;
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

