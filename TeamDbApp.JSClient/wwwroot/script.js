let teams = [];
getdata();
async function getdata() {
    await fetch('http://localhost:29829/FootballTeam')
        .then(x => x.json())
        .then(y => {
            teams = y;
            console.log(teams);
            display();
        });
}


function display() {

    const resultArea = document.getElementById('resultarea');

    // Töröld a meglévő tartalmat
    resultArea.innerHTML = '';

    teams.forEach(t => {
        resultArea.innerHTML +=
            "<tr><td>" + t.footballTeamId + "</td><td>"
            + t.footballTeamName + "</td><td>"
            + t.currentPlacement + "</td><td>"
        + t.trophiesWon + "</td><td>"
        +`<button type="button" onclick="remove(${t.footballTeamId})">Delete</button>`
            "</tr > ";
    });

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