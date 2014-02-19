$(function () {
    var hideDelay = 500;   
    var hideTimer = null;

    // One instance that's reused to show info for the current person
    var container = $('<div id="tooltipContainer">'
      + '<table width="" border="0" cellspacing="0" cellpadding="0" align="center" class="tooltipTable">'
      + '<tr>'
      + '   <td class="corner topLeft"></td>'
      + '   <td class="top"></td>'
      + '   <td class="corner topRight"></td>'
      + '</tr>'
      + '<tr>'
      + '   <td class="left">&nbsp;</td>'
      + '   <td><div id="popupContent" class="grid"></div></td>'
      + '   <td class="right">&nbsp;</td>'
      + '</tr>'
      + '<tr>'
      + '   <td class="corner bottomLeft">&nbsp;</td>'
      + '   <td class="bottom">&nbsp;</td>'
      + '   <td class="corner bottomRight"></td>'
      + '</tr>'
      + '</table>'
      + '</div>');

    $('body').append(container);

    $('.popupTrigger').live('click', function () {
        // format of 'rel' tag: ordno
       var OrdNo = $(this).text();
   

        // If no guid in url rel tag, don't popup blank
        //if (currentID == '')
        // return;
        
        if (hideTimer)
            clearTimeout(hideTimer);

        var docBounds = GetClientBounds();
        
        var x = docBounds.x + Math.round(docBounds.width / 2) - Math.round(525 / 2) + 75;
        var y = docBounds.y + Math.round(docBounds.height / 2) - Math.round(300 / 2);

        // Move the popup over to the left so it doesnt cover up the link
        var path = window.location.pathname;
        var startLoc = path.lastIndexOf("/");
        var page = path.substring(startLoc);
        if (page == "/ProductionOrders.aspx" || page == "/History.aspx" )
            x = x - 400

        container.css({
            //left: (pos.left + width) + 'px',
            //top: pos.top - 5 + 'px'
            left: x + 'px',
            top:  y + 'px'
        });

        $('#popupContent').html(' ');

        $.ajax({
            url: 'OrderLineInfoAjax.aspx',   
            cache: false,        
            data: 'OrdNo=' + OrdNo,
            success: function (data) {
                var text = $(data).find('#popupResult').html();   
                $('#popupContent').html(text);

            }
        });

        container.css('display', 'block');
    });

    $('.popupTrigger').live('mouseover', function () {
        // format of 'rel' tag: ordno
        var OrdNo = $(this).text();


        // If no guid in url rel tag, don't popup blank
        //if (currentID == '')
        // return;

        if (hideTimer)
            clearTimeout(hideTimer);

        var docBounds = GetClientBounds();

        var x = docBounds.x + Math.round(docBounds.width / 2) - Math.round(525 / 2) + 75;
        var y = docBounds.y + Math.round(docBounds.height / 2) - Math.round(300 / 2);

        // Move the popup over to the left so it doesnt cover up the link
        var path = window.location.pathname;
        var startLoc = path.lastIndexOf("/");
        var page = path.substring(startLoc);
        if (page == "/ProductionOrders.aspx" || page == "/History.aspx")
            x = x - 400

        container.css({
            //left: (pos.left + width) + 'px',
            //top: pos.top - 5 + 'px'
            left: x + 'px',
            top: y + 'px'
        });

        $('#popupContent').html(' ');

        $.ajax({
            url: 'OrderLineInfoAjax.aspx',
            cache: false,
            data: 'OrdNo=' + OrdNo,
            success: function (data) {
                var text = $(data).find('#popupResult').html();
                $('#popupContent').html(text);

            }
        });

        container.css('display', 'block');
    });

    $('.popupTrigger').live('mouseout', function () {
        if (hideTimer)
            clearTimeout(hideTimer);
        hideTimer = setTimeout(function () {
            container.css('display', 'none');
        }, hideDelay);

        
    });

    // Allow mouse over of details without hiding details
    $('#tooltipContainer').mouseover(function () {
        if (hideTimer)
            clearTimeout(hideTimer);
    });

    // Hide after mouseout
    $('#tooltipContainer').mouseout(function () {
        if (hideTimer)
            clearTimeout(hideTimer);
        hideTimer = setTimeout(function () {
            container.css('display', 'none');
        }, hideDelay);
    });
});


