// POLSIM nui.js
// Handles MDC & Vehicle Registration documents UI


// =====================================================================
// sendDataToClient()
// Function to send data from nui.js to client script
// =====================================================================
function sendDataToClient(data) {
    fetch(`https://${GetParentResourceName()}/sendToClient`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json; charset=UTF-8',
        },
        body: JSON.stringify(data)
    }).then(resp => {
        if (!resp.ok) {
            throw new Error('Network response was not ok ' + resp.statusText);
        }
        return resp.json();
    }).then(resp => {
        console.log(resp);
    }).catch(error => {
        console.log("Error sending data to client: ", error);
    });
}


// =====================================================================
// Message event listener
// This section handles receiving data from the client script and updating the UI accordingly
// =====================================================================
window.addEventListener('message', function(event) {
    const data = event.data;

    // Receiving name check response from client C#
    if (data.type === 'nameCheckResponse') {
        console.log('Received name check response: ', data.data);

        // Handle the response data (e.g., display it in the UI)
        const response = JSON.parse(data.data);
        if (response.identityNotFound) {
            console.log('Identity not found');
            document.getElementById('errorMessage').style.display = 'block';
        } else {
            console.log('Identity found: ', response);
            document.getElementById('errorMessage').style.display = 'none';

            // Show the content area
            document.querySelector('.content').style.display = 'block';

            // Populate the content area with received data
            document.getElementById('fntest').innerText = response.firstName;
            document.getElementById('lntest').innerText = response.lastName;
            document.getElementById('dobtest').innerText = response.dob;
            document.getElementById('age').innerText = response.age;
            document.getElementById('sextest').innerText = response.sex;
            document.getElementById('race').innerText = response.race;
            document.getElementById('height').innerText = response.height;
            document.getElementById('address').innerText = response.address;
            document.getElementById('aptNumber').innerText = response.aptNumber;
            document.getElementById('phoneNumber').innerText = response.phoneNumber;
            document.getElementById('driversLicenseNumber').innerText = response.driversLicenseNumber;
            document.getElementById('driversLicenseConfirmationNumber').innerText = response.driversLicenseConfirmationNumber;
            document.getElementById('isDriversLicenseValid').innerText = response.isDriversLicenseValid;
            document.getElementById('driversLicenseReason').innerText = response.driversLicenseReason;
            document.getElementById('driversLicenseIssue').innerText = response.driversLicenseIssue;
            document.getElementById('driversLicenseExp').innerText = response.driversLicenseExp;
            document.getElementById('driversLicenseClass').innerText = response.driversLicenseClass;
            document.getElementById('isDriversLicenseCommercial').innerText = response.isDriversLicenseCommercial;
            document.getElementById('isDonor').innerText = response.isDonor;
            document.getElementById('registeredVehicle').innerText = response.registeredVehicle;
            document.getElementById('hasFirearmLicense').innerText = response.hasFirearmLicense;
            document.getElementById('isFirearmLicenseValid').innerText = response.isFirearmLicenseValid;
            document.getElementById('firearmLicenseReason').innerText = response.firearmLicenseReason;
            document.getElementById('firearmLicenseType').innerText = response.firearmLicenseType;

            // Access and populate events data
            if (response.events && response.events.length > 0) {
                const eventsContainer = document.getElementById('eventsContainer');
                eventsContainer.innerHTML = ''; // Clear previous content

                response.events.forEach((event) => {
                    const eventElement = document.createElement('div');
                    eventElement.classList.add('event');

                    eventElement.innerHTML = `
                        <p><strong>Event Name:</strong> ${event.eventName}</p>
                        <p><strong>Event Description:</strong> ${event.eventDescription}</p>
                        <p><strong>Event ID:</strong> ${event.eventID}</p>
                        <hr>
                    `;
                    eventsContainer.appendChild(eventElement);
                });
            }

            if (response.warrants && response.warrants.length > 0) {
                const warrantsContainer = document.getElementById('warrantsContainer');
                warrantsContainer.innerHTML = ''; // Clear previous content

                response.warrants.forEach((warrant) => {
                    const warrantElement = document.createElement('div');
                    warrantElement.classList.add('warrant'); // Add appropriate classes

                    warrantElement.innerHTML = `
                        <p><strong>TYPE:</strong> ${warrant.warrantType}</p>
                        <p><strong>WARRANT:</strong> ${warrant.warrantTitle}</p>
                        <p><strong>DETAILS:</strong> ${warrant.warrantDetails}</p>
                        <hr>
                    `;
                    warrantsContainer.appendChild(warrantElement);
                });
            }
        }
    }

    // Receiving arrests list from client C#
    if (data.type === 'arrestListUpdate') {
        console.log('Received arrests list update from server');

        let arrests;
        try {
            arrests = JSON.parse(data.data);
        } catch (error) {
            console.error('Failed to parse arrests data', error);
            return;
        }

        if (!Array.isArray(arrests)) {
            console.error('Arrests data is not an array');
            return;
        }

        const arrestsContainer = document.getElementById('arrestsContainer');
        arrestsContainer.innerHTML = ''; // Clear previous content

        arrests.forEach((arrest) => {
            const arrestElement = document.createElement('div');
            arrestElement.classList.add('arrest');

            arrestElement.innerHTML = `
                <p><strong>Arrest ID:</strong> ${arrest.arrestID}</p>
                <p><strong>Date:</strong> ${arrest.date}</p>
                <p><strong>Time:</strong> ${arrest.time}</p>
                <p><strong>Officer Name:</strong> ${arrest.name}</p>
                <p><strong>Badge Number:</strong> ${arrest.badgeNumber}</p>
                <p><strong>Unit Number:</strong> ${arrest.unitNumber}</p>
                <p><strong>Suspect Name:</strong> ${arrest.suspectName}</p>
                <p><strong>ID Type:</strong> ${arrest.idType}</p>
                <p><strong>ID Number:</strong> ${arrest.idNumber}</p>
                <p><strong>Narrative:</strong> ${arrest.narrativeText}</p>
                <hr>
            `;
            arrestsContainer.appendChild(arrestElement);
        });

        // Example of simple pagination (adjust based on your requirements)
        const paginationContainer = document.getElementById('pagination');
        paginationContainer.innerHTML = ''; // Clear previous content

        const totalPages = Math.ceil(arrests.length / 10); // Assuming 10 arrests per page
        for (let i = 1; i <= totalPages; i++) {
            const pageButton = document.createElement('button');
            pageButton.innerText = i;
            pageButton.addEventListener('click', () => {
                // Add logic to handle pagination click event
                console.log(`Page ${i} clicked`);
            });
            paginationContainer.appendChild(pageButton);
        }
    }

	if (data.type === 'myDepartmentUpdate') {
		const departmentData = JSON.parse(data.data); // Parse JSON string to JavaScript object

		console.log('Received department update:', JSON.stringify(departmentData)); // Convert object to string for logging

		// Extract data
		const dName = departmentData.DeptNameFull;
		const dLogo = departmentData.DeptLogo;
		const deptDivisions = departmentData.Divisions;
		const deptRanks = departmentData.Ranks;

		// Invert the order of ranks (highest rank first)
		deptRanks.reverse();

		// Update the department logo in the identity-card
		document.getElementById('department-logo').src = dLogo;

		// Populate HTML fields
		document.getElementById('department-content').innerHTML = `
			<div class="department-info">
				<img src="${dLogo}" alt="Department Logo" class="department-logo"> <!-- This will display another logo if needed -->
				<div class="department-name"><h4>${dName}</h4></div>
			</div>
			<div class="divisions-container">
				<div><h4>Divisions</h4></div>
				<ul id="divisions-list">
					${deptDivisions.map(division => `
						<li class="division-item">
							<span>${division.Name}</span>
							<img src="${division.DivisionIcon}" alt="Division Icon">
						</li>
					`).join('')}
				</ul>
			</div>
			<div class="ranks-container">
				<div><h4>Rank Structure</h4></div>
				${deptRanks.map(rank => `
					<div class="rank">
						<span>${rank.Name}</span>
						<img src="${rank.Icon}" alt="Rank Icon">
					</div>
				`).join('')}
			</div>
		`;
	}



    if (data.type === 'updatePlayerProfile') {
        const playerProfile = JSON.parse(data.data); // Parse JSON string to JavaScript object
        console.log('Received player profile:', JSON.stringify(playerProfile)); // Convert object to string for logging
		
		document.getElementById('department').innerText = playerProfile.Dept || 'N/A';
		document.getElementById('rank').innerText = playerProfile.Rank || 'N/A';
		document.getElementById('name').innerText = playerProfile.Name || 'N/A';

        // Update the HTML fields with the received data
        document.getElementById('player-agency').innerText = playerProfile.Dept || 'N/A';
        document.getElementById('player-name').innerText = playerProfile.Name || 'N/A';
        document.getElementById('player-rank').innerText = playerProfile.Rank || 'N/A';
        document.getElementById('player-badge').innerText = playerProfile.Badge || 'N/A';
        document.getElementById('player-division').innerText = playerProfile.Division || 'N/A';
	
		document.getElementById('player-experience').innerText = playerProfile.Xp !== undefined ? playerProfile.Xp.toString() : 'N/A';
		document.getElementById('player-arrests').innerText = playerProfile.Arrests !== undefined ? playerProfile.Arrests.toString() : 'N/A';
		document.getElementById('player-citations').innerText = playerProfile.Citations !== undefined ? playerProfile.Citations.toString() : 'N/A';
		
    }


// <p>You're logged in as <b id="department"></b> <b>/</b> <b id="rank"></b> <b id="name"></b></p>


});


// =====================================================================
// Notifications & Interaction Menu
// =====================================================================

document.querySelector('.content').style.display = 'none';

$(document).ready(function() {
    window.addEventListener('message', function(event) {
        var item = event.data;
        if (item.shown == true) {
            document.getElementById("text").innerHTML = event.data.text;
            let root = document.documentElement;
            var color = event.data.color;
            var textcolor = event.data.textcolor;
            root.style.setProperty('--color', color);
            root.style.setProperty('color', textcolor);
            $('body').css('display', 'block');
            $('#notification').css('display', 'block');
        } else if (item.close_notify == true) {
            $('#notification').css('display', 'none'); // Only hide the notification element
        }
    });
});

$(document).ready(function() {
	window.addEventListener('message', function(event) {
		var item = event.data;
		if (item.showi == true) {
            document.getElementById("interaction-text").innerHTML = event.data.text;
            let root = document.documentElement;
            var color = event.data.color
            var textcolor = event.data.textcolor
            root.style.setProperty('--color', color);
            root.style.setProperty('color', textcolor);
            $('body').css('display', 'block');
            $('#interaction-menu').css('display', 'block');
        } else if (item.close_interaction == true) {
            $('body').css('display', 'none');
            $('#interaction-menu').css('display', 'none');
        }
	});
});


// =====================================================================
// Minimize & Exit Button
// =====================================================================
document.addEventListener('DOMContentLoaded', function () {
    var minimizeButton = document.querySelector('.minimize-button');
    var contentContainer = document.querySelector('.pages');
    var toolbar = document.querySelector('.toolbar');
    var content = document.querySelector('.content');

    var exitButton = document.querySelector('.exit-button');
    var fullMDC = document.querySelector('.windowsui');

	exitButton.addEventListener('click', function () {
		if (fullMDC.style.display === 'none') {
			fullMDC.style.display = 'block';
		} else {
			fullMDC.style.display = 'none';

			// browser-side JS
			fetch(`https://${GetParentResourceName()}/mdcHasBeenClosed`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json; charset=UTF-8',
				},
				body: JSON.stringify({
					itemId: 'mdcHasBeenClosed'
				})
			}).then(resp => resp.json()).then(resp => console.log(resp));
		}
	});

    minimizeButton.addEventListener('click', function () {
        if (contentContainer.style.display === 'none') {
            contentContainer.style.display = 'block';
            toolbar.style.display = 'block';
            content.style.display = 'block';
        } else {
            contentContainer.style.display = 'none';
            toolbar.style.display = 'none';
            content.style.display = 'none';
        }
    });
});



let currentPage = 1;
const reportsPerPage = 5;

function populateArrestsUI(data) {
    const arrestsContainer = document.getElementById('arrestsContainer');
    const pagination = document.getElementById('pagination');
    // Clear previous data
    arrestsContainer.innerHTML = '';
    pagination.innerHTML = '';
    
    // Check if data is empty
    if (data && data.length > 0) {
        // Calculate pagination variables
        const startIndex = (currentPage - 1) * reportsPerPage;
        const endIndex = startIndex + reportsPerPage;
        const displayedData = data.slice(startIndex, endIndex);

        // Loop through the data and create HTML elements for each arrest report
        displayedData.forEach(arrest => {
            const arrestElement = document.createElement('div');
            arrestElement.classList.add('arrest');

            // Create a table element
            const table = document.createElement('table');
            table.classList.add('arrest-table');

            // Create table rows and cells for each property of the arrest report
            const row1 = table.insertRow();
            row1.innerHTML = `<td><strong>ARR-</strong>${arrest.arrestID}</td><td>${arrest.date}</td><td>${arrest.time}</td>`;
            const row2 = table.insertRow();
            row2.innerHTML = `<td>SUSPECT: ${arrest.suspectName}</td><td>IDENTIFIED WITH: ${arrest.idType}</td><td>ID #: ${arrest.idNumber}</td>`;
            const row3 = table.insertRow();
            row3.innerHTML = `<td colspan="3">REPORT WRITTEN BY: ${arrest.name}</td>`;
            const row4 = table.insertRow();
            row4.innerHTML = `<td colspan="3">REPORT: ${arrest.narrativeText}</td>`;

            // Append the table to the arrest element
            arrestElement.appendChild(table);

            // Append the arrest element to the container
            arrestsContainer.appendChild(arrestElement);
        });

        // Implement pagination
        const totalPages = Math.ceil(data.length / reportsPerPage);
        for (let i = 1; i <= totalPages; i++) {
            const pageButton = document.createElement('button');
            pageButton.textContent = i;
            pageButton.addEventListener('click', () => {
                currentPage = i;
                populateArrestsUI(data);
            });
            pagination.appendChild(pageButton);
        }

    } else {
        // Display a message if no arrest data is available
        arrestsContainer.innerHTML = '<p>No arrest data available.</p>';
    }
}

// Add event listener to the refresh button
document.getElementById('refreshButton').addEventListener('click', handleRefreshButtonClick);

// Function to handle refresh button click event
function handleRefreshButtonClick() {
			// Example data to send
		const exampleData = {
			type: 'arrestListUpdate'
		};
		// Sending example data
		sendDataToClient(exampleData);
}

    document.getElementById('submitNameSearchButton').addEventListener('click', function () {
        const firstName = document.getElementById('firstNameInput').value.trim();
        const lastName = document.getElementById('lastNameInput').value.trim();

        // Log values before sending to server
        console.log('First Name:', firstName);
        console.log('Last Name:', lastName);


		// Example data to send
		const exampleData = {
			type: 'nameCheck',
			firstName: firstName,
			lastName: lastName
		};

		// Sending example data
		sendDataToClient(exampleData);
    });

    // Function to send ID to the WebSocket server
    document.getElementById('submitIDSearchButton').addEventListener('click', function () {
        const idNumber = document.getElementById('idNumberInput').value;

        // Sending a message type ID 2 for ID search
        sendSearchData(2, { idNumber: idNumber });
        console.log('Search with ID:', idNumber);
    });

    // Search with Firstname Lastname button
    document.getElementById('searchByNameButton').addEventListener('click', function () {
        document.getElementById('searchByNameForm').style.display = 'block'; // searchByNameForm searchByPlateForm searchByRegLicForm
        document.getElementById('searchByPlateForm').style.display = 'none';
		document.getElementById('searchByRegLicForm').style.display = 'none';
    });

	// Search with ID button
    document.getElementById('submitIDSearchButton').addEventListener('click', function () {
        const idNumber = document.getElementById('idNumberInput').value;

        // Perform search with ID number (Add your logic here)
        console.log('Search with ID:', idNumber);
    });

    // Menu Navigation Buttons (i.e MASTER CHECK, DISPATCH, etc)
    document.addEventListener('DOMContentLoaded', function () {
		
		const homeButton = document.getElementById('homeButton');
        const masterCheckButton = document.getElementById('masterCheckButton');
		const dispatchButton = document.getElementById('dispatchButton'); //servicesButton arrestsButton
		
		const servicesButton = document.getElementById('servicesButton');
		//const arrestsButton = document.getElementById('arrestsButton');
		//const arrestReportsButton = document.getElementById('arrestReportsButton');
		
		const myDeptButton = document.getElementById('myDeptButton');
		
		const myProfileButton = document.getElementById('myProfileButton');
		
        const page1 = document.getElementById('page1');
        const page2 = document.getElementById('page2');
		const page3 = document.getElementById('page3');
		const page4 = document.getElementById('page4');
		const page5 = document.getElementById('page5');
		const page6 = document.getElementById('page6');
		const page7 = document.getElementById('page7');
		const page8 = document.getElementById('page8');

        // Show the initial welcome page
        page1.style.display = 'block';
		
        // HOME BUTTON
        homeButton.addEventListener('click', function () {
            // Hide all pages
            page1.style.display = 'block';
            page2.style.display = 'none'; // only show master check
			page3.style.display = 'none';
			page4.style.display = 'none';
			page5.style.display = 'none';
			page6.style.display = 'none';
			page7.style.display = 'none';
			page8.style.display = 'none';
        });

        // MASTER CHECK BUTTON
        masterCheckButton.addEventListener('click', function () {
            // Hide all pages
            page1.style.display = 'none';
            page2.style.display = 'block'; // only show master check
			page3.style.display = 'none';
			page4.style.display = 'none';
			page5.style.display = 'none';
			page6.style.display = 'none';
			page7.style.display = 'none';
			page8.style.display = 'none';
        });
		
        // DISPATCH BUTTON
        dispatchButton.addEventListener('click', function () {
            // Hide all pages
            page1.style.display = 'none';
			page2.style.display = 'none';
            page3.style.display = 'block'; // only show dispatch
			page4.style.display = 'none';
			page5.style.display = 'none';
			page6.style.display = 'none';
			page7.style.display = 'none';
			page8.style.display = 'none';
        });
		
        // SERVICES BUTTON
        servicesButton.addEventListener('click', function () {
            // Hide all pages
            page1.style.display = 'none';
			page2.style.display = 'none';
            page3.style.display = 'none'; // only show dispatch
			page4.style.display = 'block';
			page5.style.display = 'none';
			page6.style.display = 'none';
			page7.style.display = 'none';
			page8.style.display = 'none';
        });
		
        // CREATE ARREST REPORT BUTTON
        /*arrestsButton.addEventListener('click', function () {
            // Hide all pages
            page1.style.display = 'none';
			page2.style.display = 'none';
            page3.style.display = 'none'; // only show dispatch
			page4.style.display = 'none';
			page5.style.display = 'block';
			page6.style.display = 'none';
			page7.style.display = 'none';
			page8.style.display = 'none';
        });
		
        // ARREST REPORTS
        arrestReportsButton.addEventListener('click', function () {
            // Hide all pages
            page1.style.display = 'none';
			page2.style.display = 'none';
            page3.style.display = 'none'; // only show dispatch
			page4.style.display = 'none';
			page5.style.display = 'none';
			page6.style.display = 'block';
			page7.style.display = 'none';
			page8.style.display = 'none';
        });*/
		
        // ARREST REPORTS
        myDeptButton.addEventListener('click', function () {
            // Hide all pages
            page1.style.display = 'none';
			page2.style.display = 'none';
            page3.style.display = 'none'; // only show dispatch
			page4.style.display = 'none';
			page5.style.display = 'none';
			page6.style.display = 'none';
			page7.style.display = 'block';
			page8.style.display = 'none';
        });
		
        // ARREST REPORTS
        myProfileButton.addEventListener('click', function () {
            // Hide all pages
            page1.style.display = 'none';
			page2.style.display = 'none';
            page3.style.display = 'none'; // only show dispatch
			page4.style.display = 'none';
			page5.style.display = 'none';
			page6.style.display = 'none';
			page7.style.display = 'none';
			page8.style.display = 'block';
        });

        // Add event listeners for other buttons if present
    });


	// Receives data from the C# script and updates the MDC html
    window.addEventListener('message', function (event) {
        switch (event.data.type) {
            case 'toggleLicenseVisibility':
                toggleLicenseVisibility();
                break;
            case 'toggleVehicleRegistrationVisibility':
                toggleVehicleRegistrationVisibility();
                break;
            case 'toggleMDCVisibility':
                toggleMDCVisibility();
                console.log('toggleMDCVisibility requested.');
                break;
            case 'toggleArrestReportVisibility':
                toggleMDCVisibility();
                console.log('toggleArrestReportVisibility requested.');
                break;
            case 'toggleOtherFunction':
                // Handle the toggle for the other function
                break;
            case 'updateLicenseData':
                document.getElementById('firstName').textContent = event.data.firstName;
                document.getElementById('lastName').textContent = event.data.lastName;
                document.getElementById('sex').textContent = event.data.gender;
                document.getElementById('expDate').textContent = event.data.exp;
                document.getElementById('dob').textContent = event.data.dob;
                document.getElementById('licenseNumber').textContent = event.data.licenseNumber;
                document.getElementById('issue').textContent = event.data.issue;

                document.getElementById('confNumb1').textContent = event.data.confirmation;
                document.getElementById('confNumb2').textContent = event.data.confirmation;

                document.getElementById('signature').textContent = event.data.signature;
                break;
            case 'updateRegistrationData':
                document.getElementById('startDate').textContent = event.data.issue;
                document.getElementById('endDate').textContent = event.data.exp;
                document.getElementById('vehModel').textContent = event.data.model;
                document.getElementById('licenseNumberVehicle').textContent = event.data.license;
                document.getElementById('vehicleIDNumber').textContent = event.data.vehicleid;
                document.getElementById('registeredOwner').textContent = event.data.regowner;
                break;
        case 'addDispatchEntry':
            updateDispatchData(event.data);
            break;
			
            default:
                // Handle unknown message types
                break;
        }
		
		
		
    if (event.data.type === 'headshot') {
        // Get the Base64-encoded image data
        var base64Image = event.data.base64;
        // Display the image using the unique ID
        var imageId = event.data.id;
        // Add your code to display the image using the ID and Base64 data
        // For example:
        document.getElementById('driverPhoto').innerHTML += '<img id="' + imageId + '" src="data:image/png;base64,' + base64Image + '">';
    }
		
    });
	
// Define an array to store the latest calls
var latestCalls = [];

// Function to update UI with dispatch data
function updateDispatchData(data) {
	console.log('Adding new entry to dispatch page...');

    // Add the current call data to the array
    latestCalls.push(data);

    // Remove the oldest call if the array length exceeds 30
    if (latestCalls.length > 10) {
        latestCalls.shift(); // Remove the oldest call
    }

    // Update UI to display the latest calls
    updateLatestCallsUI();
}

function updateProfileUI(pName, deptFull, deptShort, pRank, pBadge, pDivision, deptLogo) {
    // Select the elements where you want to display the values
    var nameElement = document.getElementById("name");
    var rankElement = document.getElementById("rank");
    var departmentElement = document.getElementById("department");

    // My Profile page
    var profileName = document.getElementById("player-name");
    var profileRank = document.getElementById("player-rank");
    var profileDivision = document.getElementById("player-division");
    var profileBadge = document.getElementById("player-badge");
	
    // Set the department logo
    var departmentLogo = document.getElementById("department-logo");
    departmentLogo.src = deptLogo;
	
	var profileAgency = document.getElementById("player-agency");

    // Populate the elements with the retrieved values
    // Welcome page
    nameElement.textContent = pName;
    rankElement.textContent = pRank;
    departmentElement.textContent = deptShort;

    // My Profile page
    profileName.textContent = pName;
    profileRank.textContent = pRank;
    profileDivision.textContent = pDivision;
    profileBadge.textContent = pBadge;
	profileAgency.textContent = deptFull;
}


// Function to update UI with the latest calls
function updateLatestCallsUI() {
    // Clear previous latest calls UI
    var calloutTableBody = document.getElementById('calloutTableBody');
    calloutTableBody.innerHTML = '';

    // Iterate over the latest calls array and update UI
    latestCalls.forEach(function(call, index) {
        var row = document.createElement('tr');
        
        // Create table data cells for each call property
        var cellNumber = document.createElement('td');
        cellNumber.textContent = index + 1;
        row.appendChild(cellNumber);

        var cellTime = document.createElement('td');
        // Retrieve current time
        var currentTime = new Date();
        var currentTimeString = currentTime.toLocaleTimeString();
        cellTime.textContent = currentTimeString;
        row.appendChild(cellTime);

        var cellLocation = document.createElement('td');
        cellLocation.textContent = call.locationString;
        row.appendChild(cellLocation);

        var cellPostal = document.createElement('td');
        cellPostal.textContent = call.postalString;
        row.appendChild(cellPostal);

        var cellCalloutType = document.createElement('td');
        cellCalloutType.textContent = call.calloutTypeString;
        row.appendChild(cellCalloutType);

        var cellComment = document.createElement('td');
        cellComment.textContent = call.commentString;
        row.appendChild(cellComment);

        var cellPriority = document.createElement('td');
        cellPriority.textContent = call.calloutPriority;
        row.appendChild(cellPriority);

        // Append the row to the table body
        calloutTableBody.appendChild(row);
    });
}

function toggleMDCVisibility() {
	const windowsUi = document.getElementById('uiwin95id');
	if (windowsUi.style.display === 'none') {
		windowsUi.style.display = 'block'; // Show the windows95-ui section
		
	} else {
		windowsUi.style.display = 'none'; // Hide the windows95-ui section
	}
}
	
function toggleArrestReportVisibility() {
    const windowsUi = document.getElementById('uiwin95id');
    
    if (windowsUi.style.display === 'none') {
        windowsUi.style.display = 'block'; // Show the windows95-ui section
    } else {
        windowsUi.style.display = 'none'; // Hide the windows95-ui section
    }
}

function toggleLicenseVisibility() {
	const license = document.getElementById('license');
	if (license.style.display === 'none') {
		license.style.display = 'block'; // Show the license section
	} else {
		license.style.display = 'none'; // Hide the license section
	}
}

function toggleVehicleRegistrationVisibility() {
	const vehicleRegistration = document.getElementById('vehicleRegistration');
	if (vehicleRegistration.style.display === 'none') {
		vehicleRegistration.style.display = 'block'; // Show the vehicle registration section
	} else {
		vehicleRegistration.style.display = 'none'; // Hide the vehicle registration section
	}
}