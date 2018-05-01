"use strict"

const request = require('request')

const electron      = require('electron')
const app           = electron.app
const BrowserWindow = electron.BrowserWindow

require('electron-dl')()

var SSH = require('ssh2').Client;
var net = require('net')

const ipcMain = electron.ipcMain

let connections = []
let mainWindow

app.on('ready', function(){
    mainWindow = new BrowserWindow({width: 1400, height: 600})
    mainWindow.setMenu(null)

    mainWindow.webContents.openDevTools()

    mainWindow.loadURL(`file://${__dirname}/ui/home.html`)
})

function createTunnel(config, event, addedBefore) {
    var conn = new SSH()
    conn.on('ready', function() {
        connections = connections.concat({"config": config, "connection": conn})
        event.sender.send('connection-success', config, addedBefore)
        net.createServer(function(sock) {
            conn.forwardOut(sock.remoteAddress, sock.remotePort, config.dstHost, config.dstPort, function(err, stream) {
                if (err) throw err

                sock.pipe(stream)
                stream.pipe(sock)
            })
        }).listen(config.localPort)
    }).connect(config);
    conn.on('error', function() {
        event.sender.send('connection-failed', config, addedBefore)
    })
}

ipcMain.on('removeSSH', (event, arg) => {
    for (var j=0;j<connections.length;j++) {
        if (connections[j].config.host+":"+connections[j].config.port == arg) {
            connections[j].connection.end()
            connections.splice(j,1)
            break
        }
    }
})

ipcMain.on('getcontainers', (event, arg, isForDropdown) => {
    request(arg+"/containers/json?all=true", {json: true}, (err, res, body) => {
        if (!err) { event.sender.send('returndockercontainers', arg, body, isForDropdown) }
    })
})

ipcMain.on('connectSSH', (event, arg, addedBefore) => {
    createTunnel(arg, event, addedBefore)
})

ipcMain.on('isDockerEndpointAvailable', (event, arg, add) => {
    request(arg, {json: true, timeout: 8000}, (err, res, body) => {
        if (err) { event.sender.send('adddockerhost', arg, false, add) } else { event.sender.send('adddockerhost', arg, true, add) }
    })
})