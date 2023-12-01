let managers = [];
let managertoUpdateID = -1;
let connection = null;
getdata();
setupSignalR();
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:29829/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ManagerCreated", (user, message) => {
        getdata();
    });
    connection.on("ManagerDeleted", (user, message) => {
        getdata();
    });
    connection.on("ManagerUpdated", (user, message) => {
        getdata();
    });

    connection.onclose
        (async () => {
            await start();
        });
    start();

}

async function getdata() {

    await fetch('http://localhost:29829/Manager')
        .then(x => x.json())
        .then(y => {
            managers = y;
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


function display() {
    const Managerresultarea = document.getElementById('managerresultarea');
    Managerresultarea.innerHTML = '';
   
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
   
}
function managerShowUpdate(id) {
    document.getElementById('managernametoupdate').value = managers.find(t => t['managerId'] == id)['managerName'];
    document.getElementById('manageragetoupdate').value = managers.find(t => t['managerId'] == id)['managerAge'];
    document.getElementById('isboldtoupdate').checked = managers.find(t => t['managerId'] == id)['isBold'];
    document.getElementById('managerupdateformdiv').style.display = 'flex';
    managertoUpdateID = id;
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
                managerName: managername, managerAge: managerage, isBold: isbold, managerId: managertoUpdateID
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