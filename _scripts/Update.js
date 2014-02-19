// get the id of the control which caused the postback
var postbackElement;
var postBackElementID = '';

Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(InitializeRequestHandler);
window.onload = function () { Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler); };

function InitializeRequestHandler(sender, args) {
    postbackElement = args.get_postBackElement();
    postBackElementID = args.get_postBackElement().id;
}

function EndRequestHandler() {

    if (typeof (postbackElement) === "undefined") {
        return;
    }
    if (postbackElement.className == "overlayList") {
        //$.scrollTo(0, { duration: 300 });
    }
}


function myUpdating() {
    var stringToFind = /Print/;
    if (!stringToFind.test(postBackElementID)) {
        
        // get the update progress div
        var pnlPopup = $get('pnlPopup');

        // make it visible
        pnlPopup.style.display = '';

        // Put it in the middle of the page
        var docBounds = GetClientBounds();
        var pnlPopupBounds = Sys.UI.DomElement.getBounds(pnlPopup);
        var x = docBounds.x + Math.round(docBounds.width / 2) - Math.round(pnlPopupBounds.width / 2);
        var y = docBounds.y + Math.round(docBounds.height / 2) - Math.round(pnlPopupBounds.height / 2);
        
        Sys.UI.DomElement.setLocation(pnlPopup, x, y);
    } 

}

function myUpdated() {
    // get the update progress div
    var pnlPopup = $get('pnlPopup');
    // make it invisible
    pnlPopup.style.display = 'none';
   
}

function GetClientBounds() {
    /// <summary>
    /// Gets the width and height of the browser client window (excluding scrollbars)
    /// </summary>
    /// <returns type="Sys.UI.Bounds">
    /// Browser's client width and height
    /// </returns>
    var clientWidth;
    var clientHeight;
    switch (Sys.Browser.agent) {
        case Sys.Browser.InternetExplorer:
            clientWidth = document.documentElement.clientWidth;
            clientHeight = document.documentElement.clientHeight;
            break;
        case Sys.Browser.Safari:
            clientWidth = window.innerWidth;
            clientHeight = window.innerHeight;
            break;      
        default:  // Sys.Browser.Firefox, etc.
            clientWidth = Math.min(window.innerWidth,
                document.documentElement.clientWidth);
            clientHeight = Math.min(window.innerHeight,
                document.documentElement.clientHeight);
            break;
    }
    var scrollLeft = (document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft);
    var scrollTop = (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop);

    return new Sys.UI.Bounds(scrollLeft, scrollTop, clientWidth, clientHeight);
};