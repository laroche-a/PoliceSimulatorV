fx_version 'cerulean'
game 'gta5'

author 'Alex'
description 'POLSIM V'
version '0.1.3'

lua54 "yes"

ui_page {
    'NUI/nui.html'
}

files {
    'Client/*.dll',
    'Client/Newtonsoft.Json.dll',
    'Client/LemonUI.FiveM.dll',
	'outfits.json',
	'departments.json',
	'postals.json',
	'coordinates.json',
	'jails.json',
	'items.json',
	'server-settings.json',
	'NUI/**'
}

client_scripts {
    'Client/*.net.dll',
    'NUI/nuiHandler.lua'
}

server_scripts {
    'Server/*.net.dll'
}