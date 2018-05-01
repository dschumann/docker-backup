var storedRows = []
var dockerEndpoints = []
var latestRowNumber = 1
var latestRowNumberDocker = 1
var serverToRemove = ""

$(document).ready(function()
{
    const {ipcRenderer} = require('electron')

    if (localStorage.getItem("sshconnections") === null) {
        localStorage.setItem("sshconnections", "[]")
    }

    if (localStorage.getItem("dockerendpoints") === null) {
        localStorage.setItem("dockerendpoints", "[]")
    }

    if (JSON.parse(localStorage.getItem("dockerendpoints")).length != 0) {
        dockerEndpoints = JSON.parse(localStorage.getItem("dockerendpoints"))
        for (i=0;i<dockerEndpoints.length;i++) {
            ipcRenderer.send('isDockerEndpointAvailable', dockerEndpoints[i], false)
        }
    }

    ipcRenderer.on('adddockerhost', (event, arg, active, add) => {
        if (add) {
            dockerEndpoints = dockerEndpoints.concat(arg)
            localStorage.setItem("dockerendpoints", JSON.stringify(dockerEndpoints))
        }
        if (active) { 
            $("#dockerContent .table tbody").append("<tr><th scope=\"row\">"+latestRowNumberDocker.toString()+"</th><td>"+arg+"</td><td>Active</td></tr>")
        } else {
            $("#dockerContent .table tbody").append("<tr><th scope=\"row\">"+latestRowNumberDocker.toString()+"</th><td>"+arg+"</td><td>Inactive</td></tr>")
        }
        latestRowNumberDocker = latestRowNumberDocker += 1
    })

    ipcRenderer.on('connection-failed', (event, arg, addedBefore) => {
        $("#homeContent .table tbody").append("<tr><th scope=\"row\">"+latestRowNumber.toString()+"</th><td>"+arg.host+":"+arg.port+"</td><td>"+arg.localPort+"</td><td>"+arg.dstHost+":"+arg.dstPort+"</td><td>Inactive/failed</td></tr>")
        if (!addedBefore) {
            storedRows = storedRows.concat(arg)
            localStorage.setItem("sshconnections", JSON.stringify(storedRows))
        }
        latestRowNumber = latestRowNumber += 1
    })

    ipcRenderer.on('connection-success', (event, arg, addedBefore) => {
        $("#homeContent .table tbody").append("<tr><th scope=\"row\">"+latestRowNumber.toString()+"</th><td>"+arg.host+":"+arg.port+"</td><td>"+arg.localPort+"</td><td>"+arg.dstHost+":"+arg.dstPort+"</td><td>Active</td></tr>")
        if (!addedBefore) {
            storedRows = storedRows.concat(arg)
            localStorage.setItem("sshconnections", JSON.stringify(storedRows))
        }
        latestRowNumber = latestRowNumber += 1
    })

    ipcRenderer.on('returndockercontainers', (event, arg, body, isForDropdown) => {
        if (isForDropdown) {
            $("#containerListDropdown").empty()
            for (i=0;i<body.length;i++) {
                $("#containerListDropdown").append("<a class=\"dropdown-item\" href=\"#\">"+body[i].Names.join(', ')+"</a>")
            }
            $("#containerListDropdown a").on('click', function() {
                $("#containerListDropdownMenuButton").text(this.text)
            })
        } else {
            $("#backupContent .table tbody").empty()
            $("#containerEndpointDropdown").empty()
            for (i=0;i<dockerEndpoints.length;i++) {
                $("#containerEndpointDropdown").append("<a class=\"dropdown-item\" href=\"#\">"+arg+"</a>")
            }
            $("#containerEndpointDropdown a").on('click', function() {
                $("#containerEndpointDropdownMenuButton").text(this.text)
                ipcRenderer.send('getcontainers', this.text, body, true)
            })
            for (i=0;i<body.length;i++) {
                $("#backupContent .table tbody").append("<tr><td>"+arg+"</td><td>"+body[i].Names.join(', ')+"</td></tr>")
            }
        }
    })

    if (JSON.parse(localStorage.getItem("sshconnections")).length != 0) {
        storedRows = JSON.parse(localStorage.getItem("sshconnections"))
        for (i=0;i<storedRows.length;i++) {
            ipcRenderer.send('connectSSH', storedRows[i], true)
        }
    }

    $("#dockerEndpointAddButton").on('click', function() {
        ipcRenderer.send('isDockerEndpointAvailable', $("#endpointInput").val(), true)
    })

    $("#sshTunnelAddButton").on('click', function() {
        var config = {
            'username': $("#usernameInput").val(),
            'host': $("#addressInput").val(),
            'port': $("#portInput").val(),
            'dstHost': $("#dockerHostInput").val(),
            'dstPort': $("#dockerPortInput").val(),
            'localPort': $("#localPortInput").val()
        }

        if ($("#passwordInput").val() != "") {
            config.password = $("#passwordInput").val()
        }

        if ($("#keyInput").val() != "") {
            config.privateKey = $("#keyInput").val()
        }

        if ($("#keyPassInput").val() != "") {
            config.passphrase = $("#keyPassInput").val()
        }

        ipcRenderer.send('connectSSH', config, false)
    })

    $("#removeServerButton").on('click', function() {
        $("#serverListDropdown").empty()
        for (i=0;i<storedRows.length;i++) {
            $("#serverListDropdown").append("<a class=\"dropdown-item\" href=\"#\">"+storedRows[i].host+":"+storedRows[i].port+"</a>")
        }

        $("#serverListDropdown .dropdown-item").on('click', function() {
            $("#dropdownMenuButton").text(this.text)
        })
    })

    $("#removeDockerButton").on('click', function() {
        $("#dockerListDropdown").empty()
        for (i=0;i<dockerEndpoints.length;i++) {
            $("#dockerListDropdown").append("<a class=\"dropdown-item\" href=\"#\">"+dockerEndpoints[i]+"</a>")
        }

        $("#dockerListDropdown .dropdown-item").on('click', function() {
            $("#dockerDropdownMenuButton").text(this.text)
        })
    })

    $("#backupCreateButton").on('click', function() {
        //ipcRenderer.send('createBackup', $("#containerEndpointDropdownMenuButton").text(), $("#containerListDropdownMenuButton").text())
        window.location = $("#containerEndpointDropdownMenuButton").text()+"/containers"+$("#containerListDropdownMenuButton").text()+"/export"
    })

    $("#removeServerButtonConfirm").on('click', function() {
        ipcRenderer.send('removeSSH', $("#dropdownMenuButton").text())
        for (i=0;i<storedRows.length;i++) {
            if (storedRows[i].host+":"+storedRows[i].port == $("#dropdownMenuButton").text()) {
                storedRows.splice(i,1)
                localStorage.setItem("sshconnections", JSON.stringify(storedRows))
                break
            }
        }
        $("#homeContent table tbody tr").each(function(i) {
            if ($(this).find("td").first().text() == $("#dropdownMenuButton").text()) {
                $(this).remove()
            }
        })
        //$("#modalRemoveSSH").toggle('modal')
    })

    $("#removeDockerButtonConfirm").on('click', function() {
        for (i=0;i<dockerEndpoints.length;i++) {
            if (dockerEndpoints[i] == $("#dockerDropdownMenuButton").text()) {
                dockerEndpoints.splice(i,1)
                localStorage.setItem("dockerendpoints", JSON.stringify(dockerEndpoints))
                break
            }
        }
        $("#dockerContent table tbody tr").each(function(i) {
            if ($(this).find("td").first().text() == $("#dockerDropdownMenuButton").text()) {
                $(this).remove()
            }
        })
        //$("#modalRemoveDocker").toggle('modal')
    })

    $("#containerEndpointDropdown").on('click', function() {
        for (i=0;i<dockerEndpoints.length;i++) {
            ipcRenderer.send('getcontainers', dockerEndpoints[i], false)
        }
    })

    $("#homeNavLink").on('click', function() {
        $("#dockerContent").hide()
        $("#backupContent").hide()
        $("#restoreContent").hide()

        $("#dockerNavLink").removeClass("active")
        $("#backupNavLink").removeClass("active")
        $("#restoreNavLink").removeClass("active")

        $("#homeContent").show()
        $("#homeNavLink").addClass("active")
    })

    $("#dockerNavLink").on('click', function() {
        $("#homeContent").hide()
        $("#backupContent").hide()
        $("#restoreContent").hide()

        $("#homeNavLink").removeClass("active")
        $("#backupNavLink").removeClass("active")
        $("#restoreNavLink").removeClass("active")

        $("#dockerContent").show()
        $("#dockerNavLink").addClass("active")
    })

    $("#backupNavLink").on('click', function() {
        $("#homeContent").hide()
        $("#dockerContent").hide()
        $("#restoreContent").hide()

        $("#homeNavLink").removeClass("active")
        $("#dockerNavLink").removeClass("active")
        $("#restoreNavLink").removeClass("active")

        $("#backupContent").show()
        $("#backupNavLink").addClass("active")

        for (i=0;i<dockerEndpoints.length;i++) {
            ipcRenderer.send('getcontainers', dockerEndpoints[i], false)
        }
    })

    $("#restoreNavLink").on('click', function() {
        $("#homeContent").hide()
        $("#dockerContent").hide()
        $("#backupContent").hide()

        $("#homeNavLink").removeClass("active")
        $("#dockerNavLink").removeClass("active")
        $("#backupNavLink").removeClass("active")

        $("#restoreContent").show()
        $("#restoreNavLink").addClass("active")
    })
})