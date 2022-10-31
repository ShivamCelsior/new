

function AddHelpdeskLink() {
    var IJPPCoreUserId = localStorage.getItem('IJPPCoreUserId');
    if (IJPPCoreUserId > 0) {
        $('app-root').append("<div id='mydiv'><div id='mydivheader'><button type='button' class='open-button' onclick='openForm()' ></button><div class='form-popup' id='hlpdskForm' style='background-color: #d5e7fd;'><div class='xxouter'><div class='buttoncl'><a href='javascript:void(0)' onclick='closeForm()'>X</a></div></div><iframe id='hlpIframe' src='' height='550' width='500' title='Iframe Example' frameBorder='0'></iframe></div></div></div>");

        $('#hlpIframe').attr('src', 'http://10.4.32.51:8079/Main/Ticket.aspx?q=' + IJPPCoreUserId);
    }
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function openForm() {
    document.getElementById("hlpdskForm").style.display = "block";
}

function closeForm() {
    document.getElementById("hlpdskForm").style.display = "none";
}