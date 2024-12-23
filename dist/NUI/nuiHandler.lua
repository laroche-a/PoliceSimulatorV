-- POLSIM V - FiveM Ressource
-- nuiHandler.lua
-- Handles some of the communication between C# and NUI JavaScript

print("[nuiHandler.lua] OK.")

-- Receives license data from the C# client script and sends it to .js for update
RegisterNetEvent('updateLicenseData')
AddEventHandler('updateLicenseData', function(firstName, lastName, gender, exp, dob, licenseNumber, issue, confirmation, signature)
    SendNUIMessage({
        type = 'updateLicenseData',
        firstName = firstName,
        lastName = lastName,
        gender = gender,
		exp = exp, 
		dob = dob,
		licenseNumber = licenseNumber,
		issue = issue,
		confirmation = confirmation,
		signature = signature
    })
end)

-- Receives dispatch data from the C# client script and sends it to a .js file for update
RegisterNetEvent('addDispatchEntry')
AddEventHandler('addDispatchEntry', function(currentTime, locationString, postalString, calloutTypeString, commentString, calloutPriority)
	print("[nuiHandler.lua] Add dispatch entry.")
    -- Send data to JavaScript file for update
    SendNUIMessage({
        type = 'addDispatchEntry',
        currentTime = currentTime,
        locationString = locationString,
        postalString = postalString,
        calloutTypeString = calloutTypeString,
        commentString = commentString,
        calloutPriority = calloutPriority
    })
end)


RegisterNetEvent('nuiHandler_updatePlayerProfile')
AddEventHandler('nuiHandler_updatePlayerProfile', function(playerJson)
    print("[nuiHandler.lua] Received player profile, passing to nui.js...")
    
    -- Send data to JavaScript file for update
    SendNUIMessage({
        type = 'updatePlayerProfile',
        data = playerJson
    })
end)


RegisterNetEvent('nuiHandler_departmentData')
AddEventHandler('nuiHandler_departmentData', function(departmentJson)
    print("[nuiHandler.lua] Received department data, passing to nui.js...")
    
    -- Send data to JavaScript file for update
    SendNUIMessage({
        type = 'myDepartmentUpdate',
        data = departmentJson
    })
end)

-- Receives dispatch data from the C# client script and sends it to a .js file for update
RegisterNetEvent('updateMyDepartment')
AddEventHandler('updateMyDepartment', function(pName, deptFull, deptShort, rank)
	print("[nuiHandler.lua] updateMyDepartment.")
    -- Send data to JavaScript file for update
    SendNUIMessage({
        type = 'updateMyDepartment',
        pName = pName,
        deptFull = deptFull,
        deptShort = deptShort,
        rank = rank
    })
end)

-- Tell the C# client the MDC was closed (X button pressed)
RegisterNUICallback('mdcHasBeenClosed', function(data, cb)
    print("[nuiHandler.lua] Closing MDC.")
	SetNuiFocus(false, false)
	SetNuiFocusKeepInput(true)
end)

-- Receives vehicle registration data from the C# client script and sends it to .js for update
AddEventHandler('updateRegistrationData', function(issue, exp, model, license, vehicleid, regowner)
    SendNUIMessage({
        type = 'updateRegistrationData',
		issue = issue,
		exp = exp, 
		model = model,
		license = license, 
		vehicleid = vehicleid,
		regowner = regowner
    })
end)

RegisterNetEvent("PedMugshotData")
AddEventHandler("PedMugshotData", function(imageData)
print("[nuiHandler.lua] Received ped mugshot.")
end)

-- Function to toggle the visibility of the MDC section
RegisterCommand('toggleMDC', function()
    TriggerEvent('ToggleMDCVisibility')
end)
RegisterNetEvent('ToggleMDCVisibility')
AddEventHandler('ToggleMDCVisibility', function()
    SendNUIMessage({
        type = 'toggleMDCVisibility'
    })
end)

-- Function to toggle the visibility of the license section
RegisterCommand('toggleLicense', function()
    TriggerEvent('ToggleLicenseVisibility')
end)
RegisterNetEvent('ToggleLicenseVisibility')
AddEventHandler('ToggleLicenseVisibility', function()
    SendNUIMessage({
        type = 'toggleLicenseVisibility'
    })
end)

-- Function to toggle the visibility of the vehicle registration section
RegisterCommand('togreg', function()
    TriggerEvent('ToggleVehicleRegistrationVisibility')
end)
RegisterNetEvent('ToggleVehicleRegistrationVisibility')
AddEventHandler('ToggleVehicleRegistrationVisibility', function()
    SendNUIMessage({
        type = 'toggleVehicleRegistrationVisibility'
    })
end)

RegisterNetEvent('fs_dependencies:ShowNotification')
AddEventHandler('fs_dependencies:ShowNotification', function(text, color, textcolor, time)
print("[nuiHandler.lua] fs_dependencies:ShowNotification")
    SendNUIMessage({
        shown = true,
        text = text,
        color = color,
        textcolor = textcolor, 
    })
    Citizen.Wait(time * 1000)
    HideNotify()
end)

function ShowNotify(text, color, textcolor, time)
print("[nuiHandler.lua] ShowNotify()")
    SendNUIMessage({
        shown = true,
        text = text,
        color = color,
        textcolor = textcolor, 
    })
    Citizen.Wait(time * 1000)
    HideNotify()
end

exports('ShowNotify', ShowNotify)

function HideNotify()
    SendNUIMessage({
        close_notify = true
    })
end
